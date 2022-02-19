//using Microsoft.Advertising.WinRT.UI;
using Microsoft.Toolkit.Uwp.UI.Animations;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TAPPLICATION;
using TAPPLICATION.IO;
using TLIB;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper.UI
{
    public sealed partial class AdministrationPage : Page, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PlatformHelper.CallPropertyChanged(PropertyChanged, this, propertyName);
        }

        #endregion
        readonly AppModel Model = AppModel.Instance;
        readonly ObservableCollection<ExtendetFileInfo> Summorys = new ObservableCollection<ExtendetFileInfo>();

        public AdministrationPage()
        {
            InitializeComponent();
            Model.PropertyChanged += Model_PropertyChanged;
            NavigationCacheMode = NavigationCacheMode.Required;
            Model.TutorialStateChanged += TutorialStateChanged;
            SizeChanged += AdministrationPage_SizeChanged;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Model.IsCharInProgress):
                    ChangeProgress(Model.IsCharInProgress);
                    this.Fade(easingType: EasingType.Sine, value: 0f, duration: 0).StartAsync();
                    break;
                default:
                    break;
            }
        }
        void AdministrationPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth > ActualHeight)
            {
                Ad_MainPageBottomBox.Visibility = Visibility.Collapsed;
                Ad_MainPageRightBox.Visibility = Visibility.Visible;
            }
            else
            {
                Ad_MainPageBottomBox.Visibility = Visibility.Visible;
                Ad_MainPageRightBox.Visibility = Visibility.Collapsed;
            }
        }

        void CheckIAPStatus()
        {
            if (!Constants.IAP_HIDEADS)
            {
                //Ad_MainPageBottomBox.Child = new AdControl()
                //{
                //    Width = 640,
                //    Height = 100,
                //    Name = "Ad_MainPageBottom",
                //    HorizontalAlignment = HorizontalAlignment.Stretch,
                //    VerticalAlignment = VerticalAlignment.Stretch,
                //    ApplicationId = Constants.APP_STORE_ID_SRE,
                //    AdUnitId = Constants.AD_ADID_MainPageBottom
                //};
                //Ad_MainPageRightBox.Child = new AdControl()
                //{
                //    Width = 160,
                //    Height = 600,
                //    Name = "Ad_MainPageRight",
                //    HorizontalAlignment = HorizontalAlignment.Center,
                //    VerticalAlignment = VerticalAlignment.Center,
                //    ApplicationId = Constants.APP_STORE_ID_SRE,
                //    AdUnitId = Constants.AD_ADID_MainPageRight
                //};
                //(Ad_MainPageRightBox.Child as AdControl).AdRefreshed += AdministrationPage_AdRefreshed;
                //(Ad_MainPageRightBox.Child as AdControl).ErrorOccurred += AdministrationPage_ErrorOccurred;
            }
        }

        //private void AdministrationPage_ErrorOccurred(object sender, AdErrorEventArgs e)
        //{
        //    if (SettingsModel.I.DEBUG_FEATURES)
        //    {
        //        Model.lstNotifications.Add(new TAPPLICATION.Model.Notification("AdControlRigth_AdministrationPage_ErrorOccurred " + e.ErrorMessage));
        //    }
        //}

        //private void AdministrationPage_AdRefreshed(object sender, RoutedEventArgs e)
        //{
        //    if (SettingsModel.I.DEBUG_FEATURES)
        //    {
        //        Model.lstNotifications.Add(new TAPPLICATION.Model.Notification("AdControlRigth_AdministrationPage_AdRefreshed"));
        //    }
        //}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Visibility = Visibility.Visible;
            if (Model.IsCharInProgress)
            {
                this.Fade(easingType: EasingType.Sine, value: 0f, duration: 0).StartAsync();
            }
            else
            {
                this.Fade(value: 1f, duration: 0).Start();
            }

            CheckIAPStatus();

            if (SettingsModel.I.FIRST_START)
            {
                CopyExampleChar();
                SettingsModel.I.FIRST_START = false;
            }
            else
            {
                Summorys_Aktualisieren();
            }
            if (!SettingsModel.I.TUT_SHOWN_1)
            {
                try
                {

                    new Tutorial(1, 4).ShowAsync();
                }
                catch (Exception ex)
 { 
                }
                SettingsModel.I.TUT_SHOWN_1 = true;
            }
            //Debug_TimeAnalyser.Stop("PAdmin.OnNavigatedTo");
        }


        #region GUi Stuff

        void TutorialStateChanged(int StateNumber, bool Highlight)
        {
            Style StyleToBeApplied = Highlight ? Tutorial.HighlightBorderStyle_XAML : Tutorial.UnhighlightBorderStyle_XAML;
            switch (StateNumber)
            {
                case 3:
                    ListViewBorder.Style = StyleToBeApplied;
                    break;
                default:
                    break;
            }
        }

        public bool IsOperationInProgres;

        void ChangeProgress(bool bHow)
        {
            IsOperationInProgres = bHow;
            Char_Sum.IsEnabled = !bHow;
            Commandbar.IsEnabled = !bHow;
        }

        void Char_Sum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.RemovedItems)
            {
                ((ListViewItem)(sender as ListView).ContainerFromItem(item)).ContentTemplate = Normal;
            }
            foreach (var item in e.AddedItems)
            {
                CurrentListViewItem = ((ListViewItem)(sender as ListView).ContainerFromItem(item));
                ((ListViewItem)(sender as ListView).ContainerFromItem(item)).ContentTemplate = Active;
            }
        }
        ListViewItem CurrentListViewItem;

        bool Summorys_AktualisierenInProgress = false;
        async Task Summorys_Aktualisieren()
        {
            if (IsOperationInProgres || Summorys_AktualisierenInProgress)
            {
                return;
            }
            Summorys_AktualisierenInProgress = true;
            Summorys.Clear();
            try
            {
                Summorys.AddRange(await SharedIO.CurrentIO.GetFiles(new DirectoryInfo(SharedIO.CurrentSavePath), Constants.LST_FILETYPES_CHAR));
            }
            catch (Exception ex)
            {
                Log.Write(CustomManager.GetString("Error_LoadCharFolder"), ex);
            }
            Summorys_AktualisierenInProgress = false;
        }
        #endregion

        #region CharOperations
        void Create_ExampleChar(object sender, RoutedEventArgs e) => CopyExampleChar();
        async Task CopyExampleChar()
        {
            ChangeProgress(true);
            try
            {
                await CharHolderIO.CopyPreSavedCharToCurrentLocation(CharHolderIO.PreSavedChar.ExampleChar);
            }
            catch (Exception ex)
            {
                
            }
            ChangeProgress(false);
            await Summorys_Aktualisieren();
        }
        void Click_OpenSTDFolder(object sender, RoutedEventArgs e)
        {
            ChangeProgress(true);
            SharedIO.CurrentIO.OpenFolder(new DirectoryInfo(SharedIO.CurrentSavePath));
            ChangeProgress(false);
        }

        async void Click_Erstellen(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            if (!await AskForSaveCurrentChanges())
            {
                return;
            }
            ChangeProgress(true);
            Model.MainObject = CharHolderGenerator.CreateCharWithStandardContent();
            SettingsModel.I.COUNT_CREATIONS++;
            AppModel.Instance?.RequestNavigation(ProjectPages.Char, ProjectPagesOptions.CharNewChar);
            ChangeProgress(false);
            Model.CharInProgress = null;
        }

        async Task<bool> AskForSaveCurrentChanges()
        {
            if (Model?.MainObject?.HasChanges == true)
            {
                try
                {
                    bool Success = false;
                    var messageDialog = new MultiButtonMessageDialog(
                        CustomManager.GetString("Request_NotSave/Title"),
                        CustomManager.GetString("Request_NotSave/Text"),
                        (CustomManager.GetString("Request_NotSave/Yes"), () => { Success = true; Model.MainObject.SetSaveTimerTo(0, true); }),
                        (CustomManager.GetString("Request_NotSave/No"), () => { Success = true; }),
                        (CustomManager.GetString("Request_NotSave/Break"), () => { Success = false; })
                    );
                    await messageDialog.ShowAsync();
                    return Success;
                }
                catch (Exception ex)
 { 
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        async void Click_Laden(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            if (!await AskForSaveCurrentChanges())
            {
                return;
            }

            this.Fade(easingType: EasingType.Sine).StartAsync();

            var file = (FileInfo)((sender as FrameworkElement).DataContext as ExtendetFileInfo);
            Model.CharInProgress = file;
            ChangeProgress(true);
            try
            {
                Model.MainObject = await CharHolderIO.Load(file);
                SettingsModel.I.COUNT_LOADINGS++;
            }
            catch (Exception ex)
            {
                Log.Write(CustomManager.GetString("Notification_Error_LoadFail"), ex);
            }
            if (Model.MainObject != null)
            {
                Visibility = Visibility.Collapsed;
                AppModel.Instance?.RequestNavigation(ProjectPages.Char);
            }
            Model.CharInProgress = null;
            ChangeProgress(false);
        }

        async void Click_Loeschen_OtherChar(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            async void Delete()
            {
                try
                {
                    await SharedIO.CurrentIO.RemoveFile((sender as FrameworkElement).DataContext as ExtendetFileInfo);
                }
                catch (Exception ex)
                {
                    Log.Write(CustomManager.GetString("Notification_Error_DelFail"), ex);
                }
            }
            await new MultiButtonMessageDialog(CustomManager.GetString("Request_Delete/Title")
                , CustomManager.GetString("Request_Delete/Text")
                , (CustomManager.GetString("Request_Delete/Yes"), Delete)
                , (CustomManager.GetString("Request_Delete/No"),null)
                ).ShowAsync();
            await Summorys_Aktualisieren();
            SettingsModel.I.COUNT_DELETIONS++;
        }

        async void Click_Datei_Export_OtherChar(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            ChangeProgress(true);
            Click_Datei_Export(await CharHolderIO.Load((sender as FrameworkElement).DataContext as ExtendetFileInfo));
            ChangeProgress(false);
        }

        async void Rename_Click(object sender, RoutedEventArgs e)
        {
            var OldFile = (sender as FrameworkElement).DataContext as ExtendetFileInfo;

            Input dialog = new Input
            {
                InputValue = OldFile.Name.Remove(OldFile.Name.Length - Constants.DATEIENDUNG_CHAR.Length, Constants.DATEIENDUNG_CHAR.Length)
            };
            await dialog.ShowAsync();
            if (dialog.InputValue != null)
            {
                ChangeProgress(true);
                try
                {
                    dialog.InputValue = SharedIO.CorrectFilenameExtension(dialog.InputValue, Constants.DATEIENDUNG_CHAR);
                    await SharedIO.CurrentIO.Rename(OldFile, dialog.InputValue);
                }
                catch (Exception ex)
                {
                    Log.Write("Error", ex);
                }
                ChangeProgress(false);
                await Summorys_Aktualisieren();
            }
        }
        async void Click_Datei_Export(CharHolder CharToSave)
        {
            ChangeProgress(true);
            try
            {
                var ExportFolder = await SharedIO.CurrentIO.PickFolder(Constants.ACCESSTOKEN_EXPORT);
                await CharHolderIO.Save(CharToSave, Info: new FileInfo(ExportFolder.Path() + CharToSave.FileInfo.Name));
            }
            catch (Exception ex)
            {
                Log.Write(CustomManager.GetString("Notification_Error_FileExportFail"), ex);
            }
            ChangeProgress(false);
        }

        async void Click_UI_TxT_CSV_Cat_Exportport_OtherChar(object sender, RoutedEventArgs e)
        {
            if (!IsOperationInProgres)
            {
                ChangeProgress(true);
                SharedUIActions.UI_TxT_CSV_Cat_Exportport(await CharHolderIO.Load((sender as FrameworkElement).DataContext as ExtendetFileInfo));
                ChangeProgress(false);
            }
        }

        async void Click_UI_TxT_ExportAll(object sender, RoutedEventArgs e)
        {
            if (!IsOperationInProgres)
            {
                ChangeProgress(true);
                try
                {
                    var TargetInfo = await SharedIO.CurrentIO.PickFolder(Constants.ACCESSTOKEN_EXPORT);
                    await SharedIO.CurrentIO.CopyAllFiles(SharedIO.CurrentSaveDir, TargetInfo, Constants.LST_FILETYPES_CHAR);
                }
                catch (Exception ex)
                {
                    Log.Write("Error", ex);
                }
                ChangeProgress(false);
            }

        }
        #endregion
    }
}