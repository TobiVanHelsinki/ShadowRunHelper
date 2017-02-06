using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper
{
    public sealed partial class Char_Verwaltung : Page
    {
        ViewModel ViewModel { get; set; }
        ObservableCollection<CharSummory> Summorys;
        event PropertyChangedEventHandler PropertyChanged;
        ResourceLoader res;

        void NotifySummoryChanged([CallerMemberName] String summoryName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(summoryName));
        }
        
        // Start Stuff ########################################################
        public Char_Verwaltung()
        {
            InitializeComponent();
            Summorys = new ObservableCollection<CharSummory>();
            res = ResourceLoader.GetForCurrentView();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = (ViewModel)e.Parameter;
            Summorys_Aktualisieren();
            ViewModel.PropertyChanged += (x, y) => { ChangeCurrentButtons(ViewModel.CurrentChar==null?false:true); };
            ChangeCurrentButtons(ViewModel.CurrentChar == null ? false : true);
        }

        void CommandBar_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsPropertyPresent("Windows.UI.Xaml.Controls.CommandBar", "DefaultLabelPosition"))
            {
                (sender as CommandBar).DefaultLabelPosition = CommandBarDefaultLabelPosition.Right;
            }
        }

        // GUI Stuff ##########################################################

        void ChangeCurrentButtons(bool bHow)
        {
            CurrentCharBtn_Save.IsEnabled = bHow;
            CurrentCharBtn_Del.IsEnabled = bHow;
            CurrentCharBtn_FileExp.IsEnabled = bHow;
            CurrentCharBtn_CSVExp.IsEnabled = bHow;
        }

        void Char_Sum_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        void Char_Sum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.RemovedItems)
            {
                // Set the DataTemplate of the deselected ListViewItems
                ((ListViewItem)(sender as ListView).ContainerFromItem(item)).ContentTemplate = Normal;
            }

            /* Then we set all the items that has been selected
            to be expanded.
            We should probably throw an Exception if more than 1 was found,
            because it's unwanted behavior, but we'll ignore that for now.
            */
            foreach (var item in e.AddedItems)
            {
                ((ListViewItem)(sender as ListView).ContainerFromItem(e.AddedItems[0])).ContentTemplate = Active;
            }

        }

        async void Summorys_Aktualisieren()
        {
            Summorys.Clear();
            StorageFolder CharFolder = await GeneralIO.GetFolder(CharIO.GetCurrentSavePlace(), await CharIO.GetCurrentSavePath());
            foreach (var item in await GeneralIO.GetListofFiles(CharFolder, new List<string>(new string[] { Konstanten.DATEIENDUNG_CHAR })))
            {
                Summorys.Add(new CharSummory(item.Name, item.DateCreated));
            }
        }

        // Handle Stuff #######################################################
        void Click_Erstellen(object sender, RoutedEventArgs e)
        {
            ViewModel.CurrentChar = new CharHolder();
            ViewModel.RequestedNavigation(ProjectPages.Char);
        }

        async void Click_Speichern(object sender, RoutedEventArgs e)
        {
            try
            {
                await CharIO.SaveCharAtCurrentPlace(ViewModel.CurrentChar);
                Summorys_Aktualisieren();
            }
            catch (Exception ex)
            {
                ViewModel.lstNotifications.Add(new Notification(res.GetString("Notification_Error_SaveFail"), ex));
            }
        }

        async void Click_Laden(object sender, RoutedEventArgs e)
        {
            ProgressRing_Char.IsActive = true;
            try
            {
                ViewModel.CurrentChar = await CharIO.LoadCharAtCurrentPlace(((CharSummory)((Button)sender).DataContext).strFileName);
            }
            catch (Exception ex)
            {
                ViewModel.lstNotifications.Add(new Notification(res.GetString("Notification_Error_LoadFail"), ex));
            }
            ProgressRing_Char.IsActive = false;
            if (ViewModel.CurrentChar != null)
            {
                ViewModel.RequestedNavigation(ProjectPages.Char);
            }
        }

        async void Click_Löschen_OtherChar(object sender, RoutedEventArgs e)
        {
            try
            {
                await CharIO.RemoveCharAtCurrentPlace(((CharSummory)((Button)sender).DataContext).strFileName);
            }
            catch (Exception ex)
            {
                ViewModel.lstNotifications.Add(new Notification(res.GetString("Notification_Error_DelFail"), ex));
            }
            Summorys_Aktualisieren();
        }

        async void Click_Löschen_Alles(object sender, RoutedEventArgs e)
        {

            var messageDialog = new MessageDialog("Damit zerstörst du die Existenz von ... JEDEM!, Chummer! Bist du sicher, dass du das machen willst?");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "Wirklich Löschen",
                new UICommandInvokedHandler((x) => Delete_All())));
            messageDialog.Commands.Add(new UICommand(
                "Ach nein doch nicht"));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        async void Delete_All()
        {
            foreach (var item in Summorys)
            {
                try
                {
                    await CharIO.RemoveCharAtCurrentPlace(item.strFileName);
                }
                catch (Exception ex)
                {
                    ViewModel.lstNotifications.Add(new Notification(res.GetString("Notification_Error_DelAllFail"), ex));
                }
            }
            Summorys_Aktualisieren();
        }

        void Click_Löschen_CurrentChar(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.CurrentChar = null;
            }
            catch (Exception ex)
            {
                ViewModel.lstNotifications.Add(new Notification(res.GetString("Notification_Error_DelCurrentFail"), ex));
            }
        }

        void Click_Datei_Export_CurrentChar(object sender, RoutedEventArgs e)
        {
            Click_Datei_Export(ViewModel.CurrentChar);
        }

        async void Click_Datei_Export_OtherChar(object sender, RoutedEventArgs e)
        {
            Click_Datei_Export(await CharIO.LoadCharAtCurrentPlace(((CharSummory)((Button)sender).DataContext).strFileName));
        }

        async void Click_Datei_Export(CharHolder CharToSave)
        {
            try
            {
                StorageFile FileToSave = await GeneralIO.GetFile(Place.Extern, CharToSave.MakeName());
                CharIO.SaveCharToFile(CharToSave, FileToSave);
            }
            catch (Exception ex)
            {
                ViewModel.lstNotifications.Add(new Notification(res.GetString("Notification_Error_FileExportFail"), ex));
            }
        }

        async void Click_Datei_Import(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.CurrentChar = await CharIO.LoadCharFromFile(await GeneralIO.FilePicker(new List<string>(new string[] { Konstanten.DATEIENDUNG_CHAR})));
            }
            catch (Exception ex)
            {
                ViewModel.lstNotifications.Add(new Notification(res.GetString("Notification_Error_FileImportFail"), ex));
            }
        }

        void Click_CSV_Export_CurrentChar(object sender, RoutedEventArgs e)
        {
            Click_CSV_Export(ViewModel.CurrentChar);
        }

        async void Click_CSV_Export_OtherChar(object sender, RoutedEventArgs e)
        {
            Click_CSV_Export(await CharIO.LoadCharAtCurrentPlace(((CharSummory)((Button)sender).DataContext).strFileName));

            Click_CSV_Export(await CharIO.LoadCharAtCurrentPlace(
                ((sender as Button).DataContext as CharSummory).strFileName
                ));
        }

        async void Click_CSV_Export(CharHolder CharToSave)
        {
            StorageFolder Folder = null;
           
            try
            {
                Folder = await GeneralIO.GetFolder(Place.Extern);
            }
            catch (Exception ex)
            {
                ViewModel.lstNotifications.Add(new Notification(res.GetString("Notification_Error_CSVExportFail")+"1", ex));
            }
            try
            {
                foreach (var item in ViewModel.CurrentChar.ToCSV(";"))
                {
                    StorageFile File = await GeneralIO.GetFile(Place.Extern, item.Value + Konstanten.DATEIENDUNG_CSV, Folder.Path);
                    GeneralIO.Write(File, item.Key);
                }
            }
            catch (Exception ex)
            {
                ViewModel.lstNotifications.Add(new Notification(res.GetString("Notification_Error_CSVExportFail") + "2", ex));
            }
        }

        async void Click_CSV_Import(object sender, RoutedEventArgs e)
        {
            StorageFile File = null;
            string strRead = "";
            try
            {
                File = await GeneralIO.GetFile(Place.Extern, null, null, new List<string>(new string[] { Konstanten.DATEIENDUNG_CHAR, Konstanten.DATEIENDUNG_CSV }));
                strRead = await FileIO.ReadTextAsync(File);

            }
            catch (Exception ex)
            {
                ViewModel.lstNotifications.Add(new Notification(res.GetString("Notification_Error_CSVImportFail") + "1", ex));
            }
            try
            {
                if (ViewModel.CurrentChar == null)
                {
                    ViewModel.CurrentChar = new CharHolder();
                }
                ViewModel.CurrentChar.CTRLCyberDeck.MultipleCSVImport(';', '\n', strRead);
            }
            catch (Exception ex)
            {
                ViewModel.lstNotifications.Add(new Notification(res.GetString("Notification_Error_CSVImportFail") + "2", ex));
            }
        }

    }

}