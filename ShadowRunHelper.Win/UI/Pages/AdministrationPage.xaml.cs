﻿using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using ShadowRunHelper.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TLIB;
using TLIB.IO;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
namespace ShadowRunHelper
{
    public sealed partial class AdministrationPage : Page
    {
        readonly AppModel ViewModel = AppModel.Instance;
        readonly ObservableCollection<FileInfoClass> Summorys = new ObservableCollection<FileInfoClass>();
        event PropertyChangedEventHandler PropertyChanged;
        ResourceLoader res;

        void NotifySummoryChanged([CallerMemberName] String summoryName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(summoryName));
        }

        // Start Stuff ########################################################
        public AdministrationPage()
        {
            InitializeComponent();
            res = ResourceLoader.GetForCurrentView();
            ViewModel.TutorialStateChanged += TutorialStateChanged;
#if DEBUG
            CurrentCharBtn_CSVExp.Visibility = Visibility.Visible;
            Btn_CSV_Import.Visibility = Visibility.Visible;
            Btn_SecondView.Visibility = Visibility.Visible;
            Btn_Exception.Visibility = Visibility.Visible;
#else
            CurrentCharBtn_CSVExp.Visibility = Visibility.Collapsed;
            Btn_CSV_Import.Visibility = Visibility.Collapsed;
            Btn_SecondView.Visibility = Visibility.Collapsed;
            Btn_Exception.Visibility = Visibility.Collapsed;
#endif
        }

        Action<ProjectPages> NavigationMethod;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationMethod = (Action < ProjectPages > )e.Parameter;
            ChangeCurrentCharUI(ViewModel.MainObject == null ? false : true);
            ViewModel.PropertyChanged += (x, y) => { ChangeCurrentCharUI(ViewModel.MainObject == null ? false : true); };
            if (SettingsModel.I.StartCount <= 1)
            {
                await CharHolderIO.CopyPreSavedCharToCurrentLocation(CharHolderIO.PreSavedChar.ExampleChar);
            }
            await Summorys_Aktualisieren();
            if (true || SettingsModel.I.StartCount <= 1 || SettingsModel.I.TutorialMainShown)
            {
                new Tutorial(0, 5).ShowAsync();
            }
        }

        void CommandBar_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsPropertyPresent("Windows.UI.Xaml.Controls.CommandBar", "DefaultLabelPosition"))
            {
                (sender as CommandBar).DefaultLabelPosition = CommandBarDefaultLabelPosition.Right;
            }
        }

        // GUI Stuff ##########################################################
        private void TutorialStateChanged(int StateNumber, bool Highlight)
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
                    CurrentCharBarBorder.Style = StyleToBeApplied;
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
            CurrentCharBtn_Del.IsEnabled = bHow;
            CurrentCharBtn_FileExp.IsEnabled = bHow;
            CurrentCharBtn_CSVExp.IsEnabled = bHow;
            CurrentCharBtn_Repair.IsEnabled = bHow;
            EditBtn.IsEnabled = bHow;

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
                if (true || SettingsModel.I.TutorialCharListShown)
                {
                    new Tutorial(10, 10).ShowAsync();
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
                var List = await CharHolderIO.GetListOfFiles(new FileInfoClass() { Fileplace = CharHolderIO.GetCurrentSavePlace(), Filepath = CharHolderIO.GetCurrentSavePath(), FolderToken = Constants.ACCESSTOKEN_FOLDERMODE }, UserDecision.ThrowError, Constants.LST_FILETYPES_CHAR);
                foreach (var item in List.OrderByDescending((x) => x.DateModified))
                {
                    Summorys.Add(new FileInfoClass() { Filename = item.Filename, DateModified = item.DateModified, Filepath = item.Filepath, Fileplace = item.Fileplace, Size = item.Size });
                }
            }
            catch (Exception ex)
            {
                ViewModel.NewNotification(res.GetString("Notification_Error_SummorysREfresh"), ex);
            }
        }

        async void Create_ExampleChar(object sender, RoutedEventArgs e)
        {
            await CharHolderIO.CopyPreSavedCharToCurrentLocation(CharHolderIO.PreSavedChar.ExampleChar);
            await Summorys_Aktualisieren();
        }

        void Click_Erstellen(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            ViewModel.MainObject = new CharHolder();
            SettingsModel.I.CountCreations++;
            NavigationMethod(ProjectPages.Char);
        }

        async void Click_Speichern(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }

            ChangeProgress(true);
            try
            {
                await CharHolderIO.SaveAtOriginPlace(ViewModel.MainObject);
            }
            catch (Exception ex)
            {
                ViewModel.NewNotification(res.GetString("Notification_Error_SaveFail"), ex);
            }
            ChangeProgress(false);
            await Summorys_Aktualisieren();
            SettingsModel.I.CountSavings++;
        }

        async void Click_Laden(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            await Laden(()=>CharHolderIO.Load(((sender as Button).DataContext as FileInfoClass), null, UserDecision.ThrowError));
        }

        async void Click_Datei_Import(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            await Laden(() => CharHolderIO.Load(new FileInfoClass() { Fileplace = Place.Extern, FolderToken = "Import" }, Constants.LST_FILETYPES_CHAR, UserDecision.AskUser));
            ViewModel.CurrentChar.FileInfo.Filepath = CharHolderIO.GetCurrentSavePath();
            ViewModel.CurrentChar.FileInfo.Fileplace = CharHolderIO.GetCurrentSavePlace();
        }

        async Task Laden(Func<Task<CharHolder>> LoadFunc)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            ChangeProgress(true);
            try
            {
                ViewModel.MainObject = await LoadFunc();
            }
            catch (Exception ex)
            {
                ViewModel.NewNotification(res.GetString("Notification_Error_LoadFail"), ex);
            }
            ChangeProgress(false);
            if (ViewModel.MainObject != null)
            {
                NavigationMethod(ProjectPages.Char);
            }
            SettingsModel.I.CountLoadings++;
        }


        async void Click_Löschen_OtherChar(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            async void Delete()
            {
                try
                {
                    await CharHolderIO.Remove(((FileInfoClass)((Button)sender).DataContext));
                }
                catch (Exception ex)
                {
                    ViewModel.NewNotification(res.GetString("Notification_Error_DelFail"), ex);
                }
            }
            await ShowMessageDialog("TITLE", "Wirklich Löschen?", "Wirklich Löschen", "Ach nein doch nicht", Delete);
            await Summorys_Aktualisieren();
            SettingsModel.I.CountDeletions++;
        }

        public async static Task ShowMessageDialog(string Title, string Message, string strOK, string strCancel, Action OK, Action Cancel = null)
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

        async void Click_Löschen_Alles(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            async void Delete_All()
            {
                ChangeProgress(true);
                bool bIsFail = false;
                List<Task> temp = new List<Task>();
                List<FileInfoClass> tempSums = new List<FileInfoClass>();
                lock (Summorys)
                {
                    foreach (var item in Summorys)
                    {
                        tempSums.Add(item);
                    }
                }
                foreach (var item in tempSums)
                {
                    try
                    {
                        await CharHolderIO.Remove(item);
                        SettingsModel.I.CountDeletions++;
                    }
                    catch (Exception ex)
                    {
                        ViewModel.NewNotification(res.GetString("Notification_Error_DelFail"), ex);
                        bIsFail = true;
                    }
                }
                if (bIsFail)
                {
                    ViewModel.NewNotification(res.GetString("Notification_Error_DelAllFail"));
                }
                ChangeProgress(false);
                await Summorys_Aktualisieren();
            }
            await ShowMessageDialog("TITLE", "Damit zerstörst du die Existenz von ... JEDEM!, Chummer! Bist du sicher, dass du das machen willst?", "Wirklich Löschen", "Ach nein doch nicht", Delete_All);
        }

        void Click_Löschen_CurrentChar(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.MainObject = null;
            }
            catch (Exception ex)
            {
                if (ViewModel.MainObject != null)
                {
                    ViewModel.NewNotification(res.GetString("Notification_Error_DelCurrentFail"), ex);
                }
            }
            finally
            {
                ChangeCurrentCharUI(ViewModel.MainObject == null ? false : true);
            }
        }

        void Click_Datei_Export_CurrentChar(object sender, RoutedEventArgs e)
        {
            Click_Datei_Export(ViewModel.MainObject);
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

        async void Click_Datei_Export(CharHolder CharToSave)
        {
            try
            {
                await CharHolderIO.Save(CharToSave, Info: new FileInfoClass() { Fileplace = Place.Extern, FolderToken="Export" });
            }
            catch (Exception ex)
            {
                ViewModel.NewNotification(res.GetString("Notification_Error_FileExportFail"), ex);
            }
        }
        // CSV #######

        void Click_CSV_Export_CurrentChar(object sender, RoutedEventArgs e)
        {
            CSV_Export(ViewModel.MainObject);
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
            //TODO with Foldertoken = Export
            //StorageFolder Folder = null;

            try
            {
                List < (string, string) >  ContentList = new List<(string, string)>();
                foreach (var item in ViewModel.MainObject.ToCSV(";"))
                {
                    ContentList.Add((item.ThingType + Constants.DATEIENDUNG_CSV, item.Content));
                }
                SharedIO.SaveTextesToFilees(ContentList, new FileInfoClass (){Fileplace=Place.Extern, FolderToken = "CSV_TEMP" });
            }
            catch (Exception ex)
            {
                ViewModel.NewNotification(res.GetString("Notification_Error_CSVExportFail") + "2", ex);
            }
        }

        async void Click_CSV_Import(object sender, RoutedEventArgs e)
        {
            if (IsOperationInProgres)
            {
                return;
            }
            //TODO with Foldertoken = import
            //StorageFile File = null;
            //string strRead = "";
            //try
            //{
            //    File = await WinIO.GetFile(Place.Extern, null, null, Constants.LST_FILETYPES_CSV);
            //    strRead = await FileIO.ReadTextAsync(File);

            //}
            //catch (Exception ex)
            //{
            //    ViewModel.NewNotification(res.GetString("Notification_Error_CSVImportFail") + "1", ex);
            //}
            //try
            //{
            //    if (ViewModel.MainObject == null)
            //    {
            //        ViewModel.MainObject = new CharHolder();
            //    }
            //    ViewModel.MainObject.CTRLCyberDeck.MultipleCSVImport(';', '\n', strRead);
            //}
            //catch (Exception ex)
            //{
            //    ViewModel.NewNotification(res.GetString("Notification_Error_CSVImportFail") + "2", ex);
            //}
        }

        void Exception(object sender, RoutedEventArgs e)
        {
            throw new System.Exception();
        }

        void Click_Repair_CurrentChar(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.MainObject?.Repair();
            }
            catch (Exception ex)
            {
                ViewModel.NewNotification(res.GetString("Notification_Error_RepairFail"), ex);
            }
        }

        // NEW VIEW ###########################################################

        private async void Click_NewView(object sender, RoutedEventArgs e)
        {
            CoreApplicationView newView = CoreApplication.CreateNewView();
            int newViewId = 0;
            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame frame = new Frame();
                frame.Navigate(typeof(MainPage), null);
                Window.Current.Content = frame;
                // You have to activate the window in order to show it later.
                Window.Current.Activate();

                newViewId = ApplicationView.GetForCurrentView().Id;
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
        }

        private void UI_CharVerwaltung_Btn_CSV_Export_Loaded(object sender, RoutedEventArgs e)
        {
#if DEBUG
            (sender as AppBarButton).Visibility = Visibility.Visible;
#else
            (sender as AppBarButton).Visibility = Visibility.Collapsed;
#endif

        }
    }
}