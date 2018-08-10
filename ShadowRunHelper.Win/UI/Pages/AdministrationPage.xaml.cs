using Microsoft.Advertising.WinRT.UI;
using Microsoft.AppCenter.Analytics;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.Toolkit.Uwp.UI.Animations.Behaviors;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TAMARIN.IO;
using TAPPLICATION.IO;
using TLIB;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper.UI
{
    public sealed partial class AdministrationPage : Page, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ModelHelper.CallPropertyChanged(PropertyChanged, this, propertyName);
        }

        #endregion
        readonly AppModel Model = AppModel.Instance;
        readonly ObservableCollection<FileInfoClass> Summorys = new ObservableCollection<FileInfoClass>();

        public AdministrationPage()
        {
            //Debug_TimeAnalyser.Start("AdministrationPage()");
            InitializeComponent();
            ChangeProgress(false);
            NavigationCacheMode = NavigationCacheMode.Required;
            Model.TutorialStateChanged += TutorialStateChanged;
            //Debug_TimeAnalyser.Stop("AdministrationPage()");
            SizeChanged += AdministrationPage_SizeChanged;
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
                Ad_MainPageBottomBox.Child = new AdControl()
                {
                    Width = 640,
                    Height = 100,
                    Name = "Ad_MainPageBottom",
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    ApplicationId = Constants.APP_STORE_ID_SRE,
                    AdUnitId = Constants.AD_ADID_MainPageBottom
                };
                Ad_MainPageRightBox.Child = new AdControl()
                {
                    Width = 160,
                    Height = 600,
                    Name = "Ad_MainPageRight",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    ApplicationId = Constants.APP_STORE_ID_SRE,
                    AdUnitId = Constants.AD_ADID_MainPageRight
                };
                (Ad_MainPageRightBox.Child as AdControl).AdRefreshed += AdministrationPage_AdRefreshed;
                (Ad_MainPageRightBox.Child as AdControl).ErrorOccurred += AdministrationPage_ErrorOccurred;
            }
        }

        private void AdministrationPage_ErrorOccurred(object sender, AdErrorEventArgs e)
        {
            if (SettingsModel.I.DEBUG_FEATURES)
            {
                Model.lstNotifications.Add(new TAPPLICATION.Model.Notification("AdControlRigth_AdministrationPage_ErrorOccurred " + e.ErrorMessage));
            }
        }

        private void AdministrationPage_AdRefreshed(object sender, RoutedEventArgs e)
        {
            if (SettingsModel.I.DEBUG_FEATURES)
            {
                Model.lstNotifications.Add(new TAPPLICATION.Model.Notification("AdControlRigth_AdministrationPage_AdRefreshed"));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Visibility = Visibility.Visible;
            this.Fade(value: 1f, duration: 0).Start();


            //Debug_TimeAnalyser.Start("PAdmin.OnNavigatedTo");
            CheckIAPStatus();
            if (AppDataPorter.InProgress)
            {
                //Ask User
                //If YES, Import all
                ShowMessageDialog(StringHelper.GetString("Request_AppImport/Title")
, StringHelper.GetString("Request_AppImport/Text")
, StringHelper.GetString("Request_AppImport/Yes")
, StringHelper.GetString("Request_AppImport/No")
, ()=> AppDataPorter.ImportAppPacket());

            }

            if (SettingsModel.I.START_COUNT <= 1)
            {
                CopyExampleChar();
            }
            else
            {
                Summorys_Aktualisieren();
            }
            if (!SettingsModel.I.TUT_SHOWN_1)
            {
                try
                {
#pragma warning disable CS4014
                    new Tutorial(1, 4).ShowAsync();
#pragma warning restore CS4014
                }
                catch (Exception)
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
            Model.ChangeProgress(bHow, true);
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

        async Task Summorys_Aktualisieren()
        {
            if (IsOperationInProgres)
            {
                return;
            }
            Summorys.Clear();
            try
            {
                var List = await CharHolderIO.CurrentIO.GetListofFiles(new FileInfoClass() { Fileplace = CharHolderIO.GetCurrentSavePlace(), Filepath = CharHolderIO.GetCurrentSavePath(), FolderToken = Constants.ACCESSTOKEN_FOLDERMODE }, UserDecision.ThrowError, Constants.LST_FILETYPES_CHAR);
                foreach (var item in List.OrderByDescending((x) => x.DateModified))
                {
                    Summorys.Add(new FileInfoClass() { Filename = item.Filename, DateModified = item.DateModified, Filepath = item.Filepath, Fileplace = item.Fileplace, Size = item.Size });
                }
            }
            catch (Exception ex)
            {
                Model.NewNotification(StringHelper.GetString("Error_LoadCharFolder"), ex);
            }
        }
        #endregion

        #region CharOperations
#pragma warning disable CS4014
        void Create_ExampleChar(object sender, RoutedEventArgs e) => CopyExampleChar();
#pragma warning restore CS4014
        async Task CopyExampleChar()
        {
            ChangeProgress(true);
            try
            {
                await CharHolderIO.CopyPreSavedCharToCurrentLocation(CharHolderIO.PreSavedChar.ExampleChar);
            }
            catch (Exception)
            {

            }
            ChangeProgress(false);
            await Summorys_Aktualisieren();
        }
        void Click_OpenSTDFolder(object sender, RoutedEventArgs e)
        {
            ChangeProgress(true);
            SharedIO.CurrentIO.OpenFolder(new FileInfoClass(SharedIO.GetCurrentSavePlace(), "", SharedIO.GetCurrentSavePath()));
            ChangeProgress(false);
        }

        async void Click_Erstellen(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            await AskForSaveCurrentChanges();
            ChangeProgress(true);
            Model.MainObject = CharHolder.CreateCharWithStandardContent();
            Model.MainObject.AfterLoad();
            SettingsModel.I.COUNT_CREATIONS++;
            AppModel.Instance.RequestNavigation(ProjectPages.Char, ProjectPagesOptions.CharNewChar);
            ChangeProgress(false);
        }

        async Task AskForSaveCurrentChanges()
        {
            if (Model?.MainObject?.HasChanges == true)
            {
                await ShowMessageDialog(StringHelper.GetString("Request_NotSave/Title")
                    , StringHelper.GetString("Request_NotSave/Text")
                    , StringHelper.GetString("Request_NotSave/Yes")
                    , StringHelper.GetString("Request_NotSave/No")
                    , ()=> { Model.MainObject.SetSaveTimerTo(0, true);});
            }
        }

        async void Click_Laden(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            await AskForSaveCurrentChanges();

            this.Fade(easingType: EasingType.Sine).StartAsync();

            ChangeProgress(true);
            try
            {
                Model.MainObject = await CharHolderIO.Load(((sender as Button).DataContext as FileInfoClass), null, UserDecision.ThrowError);
                SettingsModel.I.COUNT_LOADINGS++;
            }
            catch (Exception ex)
            {
                Model.NewNotification(StringHelper.GetString("Notification_Error_LoadFail"), ex);
            }
            if (Model.MainObject != null)
            {
                Visibility = Visibility.Collapsed;
                AppModel.Instance.RequestNavigation(ProjectPages.Char);
            }
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
                    await CharHolderIO.CurrentIO.RemoveFile(((FileInfoClass)((Button)sender).DataContext));
                }
                catch (Exception ex)
                {
                    Model.NewNotification(StringHelper.GetString("Notification_Error_DelFail"), ex);
                }
            }
            await ShowMessageDialog(StringHelper.GetString("Request_Delete/Title")
    , StringHelper.GetString("Request_Delete/Text")
    , StringHelper.GetString("Request_Delete/Yes")
    , StringHelper.GetString("Request_Delete/No")
    , Delete);
            await Summorys_Aktualisieren();
            SettingsModel.I.COUNT_DELETIONS++;
        }

        public static async Task ShowMessageDialog(string Title, string Message, string strOK, string strCancel, Action OK, Action Cancel = null)
        {
            var messageDialog = new MessageDialog(Message, Title);
            messageDialog.Commands.Add(new UICommand(
                strOK,
                new UICommandInvokedHandler((x) => OK?.Invoke())));
            messageDialog.Commands.Add(new UICommand(
                strCancel,
                new UICommandInvokedHandler((x) => Cancel?.Invoke())));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }


        async void Click_Datei_Export_OtherChar(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            ChangeProgress(true);
            var x = await CharHolderIO.Load(((FileInfoClass)((Button)sender).DataContext));
            Click_Datei_Export(x);
            ChangeProgress(false);
        }

        async void Rename_Click(object sender, RoutedEventArgs e)
        {
            FileInfoClass OldFile = (((FileInfoClass)((Button)sender).DataContext));

            Input dialog = new Input
            {
                InputValue = OldFile.Filename.Remove(OldFile.Filename.Length - Constants.DATEIENDUNG_CHAR.Length, Constants.DATEIENDUNG_CHAR.Length)
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
                    Model.NewNotification("Error", ex);
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
                await CharHolderIO.Save(CharToSave, Info: new FileInfoClass() { Fileplace = Place.Extern, FolderToken = "Export" });
            }
            catch (Exception ex)
            {
                Model.NewNotification(StringHelper.GetString("Notification_Error_FileExportFail"), ex);
            }
            ChangeProgress(false);
        }

        async void Click_UI_TxT_CSV_Cat_Exportport_OtherChar(object sender, RoutedEventArgs e)
        {
            if (!IsOperationInProgres)
            {
            ChangeProgress(true);
                SharedUIActions.UI_TxT_CSV_Cat_Exportport(await CharHolderIO.Load(((sender as Button).DataContext as FileInfoClass), null, UserDecision.ThrowError));
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
                    var TargetInfo = await SharedIO.CurrentIO.GetFolderInfo(new FileInfoClass(Place.Extern) { FolderToken = "EXPORT"});
                    var SourceInfo = new FileInfoClass(SharedIO.GetCurrentSavePlace(), "", SharedIO.GetCurrentSavePath());
                    await SharedIO.CurrentIO.CopyAllFiles(TargetInfo, SourceInfo);
                }
                catch (Exception ex)
                {
                    Model.NewNotification("Error", ex);
                }
                ChangeProgress(false);
            }

        }
        #endregion
    }
}