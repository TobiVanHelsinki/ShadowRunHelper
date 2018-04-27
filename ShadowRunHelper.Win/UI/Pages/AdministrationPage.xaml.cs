using Microsoft.AppCenter.Analytics;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TAMARIN.IO;
using TAPPLICATION.IO;
using TLIB;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper.UI
{
    public sealed partial class AdministrationPage : Page
    {
        readonly AppModel Model = AppModel.Instance;
        readonly ObservableCollection<FileInfoClass> Summorys = new ObservableCollection<FileInfoClass>();
        ResourceLoader res;

        public AdministrationPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            CheckIAP();
            res = ResourceLoader.GetForCurrentView();
            Model.TutorialStateChanged += TutorialStateChanged;
#if DEBUG
            Btn_Exception.Visibility = Visibility.Visible;
#else
            Btn_Exception.Visibility = Visibility.Collapsed;
#endif
        }
        void CheckIAP()
        {
            if (Constants.IAP_HIDEADS)
            {
                Ad_MainPageRight.Visibility = Constants.IAP_HIDEADS ? Visibility.Collapsed : Visibility.Visible;
                Ad_MainPageBottom.Visibility = Constants.IAP_HIDEADS ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ChangeCurrentCharUI(Model.MainObject == null ? false : true);
            Model.PropertyChanged += (x, y) =>
            {
                if (e.Parameter.ToString() == "MainObject")
                {
                    ChangeCurrentCharUI(Model.MainObject == null ? false : true);
                }
            };

            if (SettingsModel.I.StartCount <= 1)
            {
                await CharHolderIO.CopyPreSavedCharToCurrentLocation(CharHolderIO.PreSavedChar.ExampleChar);
            }
            await Summorys_Aktualisieren();
            if (!SettingsModel.I.TutorialMainShown)
            {
                try
                {
#pragma warning disable CS4014
                    new Tutorial(0, 5).ShowAsync();
#pragma warning restore CS4014
                }
                catch (Exception)
                {

                    throw;
                }
                SettingsModel.I.TutorialMainShown = true;
            }
        }


        #region GUi Stuff

        void TutorialStateChanged(int StateNumber, bool Highlight)
        {
            Style StyleToBeApplied = Highlight ? Tutorial.HighlightBorderStyle_XAML : Tutorial.UnhighlightBorderStyle_XAML;
            switch (StateNumber)
            {
                case 2:
                    MainBarBorder.Style = StyleToBeApplied;
                    break;
                case 3:
                    ListViewBorder.Style = StyleToBeApplied;
                    break;
                case 4:
                    //CurrentCharBarBorder.Style = StyleToBeApplied;
                    break;
                default:
                    //MainBarBorder.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    //CurrentCharBarBorder.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    //ListViewBorder.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    break;
            }
        }

        bool IsOperationInProgres = false;
        void ChangeCurrentCharUI(bool bHow)
        {
            CurrentCharBtn_Save.IsEnabled = bHow;
            CurrentCharBtn_FolderOpen.IsEnabled = bHow;
            CurrentCharBtn_Rename.IsEnabled = bHow;
            CurrentCharBtn_Save_Intern.IsEnabled = bHow;
            CurrentCharBtn_Del.IsEnabled = bHow;
            CurrentCharBtn_FileExp.IsEnabled = bHow;
            CurrentCharBtn_CSVExp.IsEnabled = bHow;
            CurrentCharBtn_Repair.IsEnabled = bHow;

            CurrentCharPath.Visibility = bHow ? Visibility.Visible : Visibility.Collapsed;
            CurrentCharName.Visibility = bHow ? Visibility.Visible : Visibility.Collapsed;
        }
        void ChangeProgress(bool bHow)
        {
            IsOperationInProgres = bHow;
            ProgressBar_Char.IsIndeterminate = bHow;
            ProgressBar_Char.Visibility = bHow? Visibility.Visible : Visibility.Collapsed;
        }

        void Char_Sum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.RemovedItems)
            {
                ((ListViewItem)(sender as ListView).ContainerFromItem(item)).ContentTemplate = Normal;
            }
            foreach (var item in e.AddedItems)
            {
                ((ListViewItem)(sender as ListView).ContainerFromItem(item)).ContentTemplate = Active;
                if (!SettingsModel.I.TutorialCharListShown)
                {
#pragma warning disable CS4014
                    new Tutorial(10, 10).ShowAsync();
#pragma warning restore CS4014
                    SettingsModel.I.TutorialCharListShown = true;
                }
            }
        }

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
                Model.NewNotification(res.GetString("Notification_Error_SummorysREfresh"), ex);
            }
        }
        #endregion

        #region CharOperations
        async void Create_ExampleChar(object sender, RoutedEventArgs e)
        {
            try
            {
                await CharHolderIO.CopyPreSavedCharToCurrentLocation(CharHolderIO.PreSavedChar.ExampleChar);
            }
            catch (Exception)
            {

            }
            await Summorys_Aktualisieren();
        }
        void Click_OpenFolder(object sender, RoutedEventArgs e)
        {
            SharedIO.CurrentIO.OpenFolder(Model.MainObject.FileInfo);
        }
        void Click_OpenSTDFolder(object sender, RoutedEventArgs e)
        {
            SharedIO.CurrentIO.OpenFolder(new FileInfoClass(SharedIO.GetCurrentSavePlace(), "", SharedIO.GetCurrentSavePath()));
        }

        void Click_Erstellen(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            Model.MainObject = CharHolder.CreateCharWithStandardContent();
            Model.MainObject.AfterLoad();
            SettingsModel.I.CountCreations++;
            AppModel.Instance.RequestNavigation(ProjectPages.Char, ProjectPagesOptions.CharNewChar);
        }

        async void Click_Speichern(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                Model.NewNotification(res.GetString("Notification_Error_SaveFail_OPInProgress"), new System.Exception());
                return;
            }

            ChangeProgress(true);
            try
            {
                Model.MainObject.SetSaveTimerTo(0, true);
            }
            catch (Exception ex)
            {
                Model.NewNotification(res.GetString("Notification_Error_SaveFail"), ex);
            }
            ChangeProgress(false);
            await Summorys_Aktualisieren();
        }

        async void Click_Speichern_Intern(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }

            ChangeProgress(true);
            try
            {
                await CharHolderIO.SaveAtCurrentPlace(Model.MainObject);
                Model.MainObject.HasChanges = false;
            }
            catch (Exception ex)
            {
                Model.NewNotification(res.GetString("Notification_Error_SaveFail"), ex);
            }
            ChangeProgress(false);
            await Summorys_Aktualisieren();
        }

        async void Click_Laden(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            await LoadChar(() => CharHolderIO.Load(((sender as Button).DataContext as FileInfoClass), null, UserDecision.ThrowError));
        }

        async Task LoadChar(Func<Task<CharHolder>> LoadFunc)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            ChangeProgress(true);
            try
            {
                Model.MainObject = await LoadFunc();
            }
            catch (Exception ex)
            {
                Model.NewNotification(res.GetString("Notification_Error_LoadFail"), ex);
            }
            ChangeProgress(false);
            if (Model.MainObject != null)
            {
                AppModel.Instance.RequestNavigation(ProjectPages.Char, ProjectPagesOptions.Char_Action);
            }
            SettingsModel.I.CountLoadings++;
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
                    Model.NewNotification(res.GetString("Notification_Error_DelFail"), ex);
                }
            }
            await ShowMessageDialog(StringHelper.GetString("Request_Delete/Title")
    , StringHelper.GetString("Request_Delete/Text")
    , StringHelper.GetString("Request_Delete/Yes")
    , StringHelper.GetString("Request_Delete/No")
    , Delete);
            await Summorys_Aktualisieren();
            SettingsModel.I.CountDeletions++;
        }

        public static async Task ShowMessageDialog(string Title, string Message, string strOK, string strCancel, Action OK, Action Cancel = null)
        {
            var messageDialog = new MessageDialog(Message, Title);
            messageDialog.Commands.Add(new UICommand(
                strOK,
                new UICommandInvokedHandler((x) => OK())));
            messageDialog.Commands.Add(new UICommand(
                strCancel,
                new UICommandInvokedHandler((x) => Cancel())));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }

        void Click_Loeschen_CurrentChar(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.MainObject = null;
            }
            catch (Exception ex)
            {
                if (Model.MainObject != null)
                {
                    Model.NewNotification(res.GetString("Notification_Error_DelCurrentFail"), ex);
                }
            }
            finally
            {
                ChangeCurrentCharUI(Model.MainObject == null ? false : true);
            }
        }

        void Click_Datei_Export_CurrentChar(object sender, RoutedEventArgs e)
        {
            Click_Datei_Export(Model.MainObject);
        }

        async void Click_Datei_Export_OtherChar(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            var x = await CharHolderIO.Load(((FileInfoClass)((Button)sender).DataContext));
            Click_Datei_Export(x);
        }

        async void Rename_Click(object sender, RoutedEventArgs e)
        {
            Input dialog = new Input
            {
                InputValue = AppModel.Instance.MainObject.FileInfo.Filename
            };
            await dialog.ShowAsync();
            AppModel.Instance.MainObject.FileInfo.Filename = dialog.InputValue;
        }
        async void Click_Datei_Export(CharHolder CharToSave)
        {
            try
            {
                await CharHolderIO.Save(CharToSave, Info: new FileInfoClass() { Fileplace = Place.Extern, FolderToken = "Export" });
            }
            catch (Exception ex)
            {
                Model.NewNotification(res.GetString("Notification_Error_FileExportFail"), ex);
            }
        }
        // CSV #######

        void Click_CSV_Export_CurrentChar(object sender, RoutedEventArgs e)
        {
            if (!IsOperationInProgres)
            {
                CSV_Export(Model.MainObject);
            }
        }
        async void Click_CSV_Export_OtherChar(object sender, RoutedEventArgs e)
        {
            if (!IsOperationInProgres)
            {
                CSV_Export(await CharHolderIO.Load(((sender as Button).DataContext as FileInfoClass), null, UserDecision.ThrowError));
            }
        }

        void CSV_Export(CharHolder CharToSave)
        {
            try
            {
                string ret = "";
                foreach (var item in CharToSave.lstCTRL)
                {
                    ret += item.Data2CSV(';', '\n');
                }
                var ContentList = CharToSave.lstCTRL.Select(c => (TypeHelper.ThingDefToString(c.eDataTyp, true) + Constants.DATEIENDUNG_CSV, c.Data2CSV(';', '\n')));
                SharedIO.SaveTextesToFiles(ContentList, new FileInfoClass() { Fileplace = Place.Extern, FolderToken = "CSV_TEMP" });
            }
            catch (Exception ex)
            {
                Model.NewNotification(res.GetString("Notification_Error_CSVExportFail") + "2", ex);
            }
            Analytics.TrackEvent("Admin_CSV_EX");
        }

        void Click_Repair_CurrentChar(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.MainObject?.Repair();
            }
            catch (Exception ex)
            {
                Model.NewNotification(res.GetString("Notification_Error_RepairFail"), ex);
            }
        }

        #endregion

        #region Debug and Experimental
        void Exception(object sender, RoutedEventArgs e)
        {
            throw new Exception(Constants.TESTEXCEPTIONTEXT);
        }
        #endregion


    }
}