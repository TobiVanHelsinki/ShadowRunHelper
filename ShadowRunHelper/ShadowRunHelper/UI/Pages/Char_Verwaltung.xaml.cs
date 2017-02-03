using System;
using ShadowRunHelper.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Primitives;
using System.Collections.Generic;
using Windows.Storage;
using ShadowRunHelper.IO;
//using static ShadowRunHelper.IO.CharIO;
//using static ShadowRunHelper.IO.GeneralIO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace ShadowRunHelper
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Char_Verwaltung : Page
    {
        ViewModel_Char ViewModel { get; set; }
        //ViewModel_CharVerwaltung ViewModel_IO { get; set; }

        ObservableCollection<CharSummory> summorys;

        public ObservableCollection<CharSummory> Summorys
        {
            get
            {
                return summorys;
            }
            private set
            {
                if (value != this.summorys)
                {
                    this.summorys = value;
                    NotifySummoryChanged();
                }
            }
        }

        public Task<IEnumerable<object>> GenralIO { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifySummoryChanged([CallerMemberName] String summoryName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(summoryName));
        }


        private async void Summorys_Aktualisieren()
        {
            Summorys.Clear();
            StorageFolder CharFolder = await GeneralIO.GetFolder(CharIO.GetCurrentSavePlace(), await CharIO.GetCurrentSavePath());
            foreach (var item in await GeneralIO.GetListofFiles(CharFolder, new List<string>(new string[] { Konstanten.DATEIENDUNG_CHAR })))
            {
                Summorys.Add(new CharSummory(item.Name, item.DateCreated));
            }
        }


        public Char_Verwaltung()
        {
            InitializeComponent();
            summorys = new ObservableCollection<Model.CharSummory>();

            //ViewModel_IO = new ViewModel_CharVerwaltung();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = (ViewModel_Char)e.Parameter;
            Summorys_Aktualisieren();
        }

        private void Item_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                FlyoutBase.ShowAttachedFlyout(element);
            }
        }

        private void Click_Erstellen(object sender, RoutedEventArgs e)
        {
            ViewModel.CurrentChar = new CharHolder();
            Frame.Navigate(typeof(Char), ViewModel);
        }

        private async void Click_Löschen(object sender, RoutedEventArgs e)
        {
            try
            {
                await CharIO.RemoveCharAtCurrentPlace(((CharSummory)((Button)sender).DataContext).strFileName);
            }
            catch (Exception)
            {
                //TODO notify user
                throw; //then remove throw
            }
            Summorys_Aktualisieren();
        }

        private async void Click_Löschen_Alles(object sender, RoutedEventArgs e)
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

        private async void Delete_All()
        {
            foreach (var item in Summorys)
            {
                try
                {
                    await CharIO.RemoveCharAtCurrentPlace(item.strFileName);
                }
                catch (Exception)
                {
                    //TODO notify user
                    throw; //then remove throw
                }
            }
            Summorys_Aktualisieren();
        }

        private async void Click_Laden(object sender, RoutedEventArgs e)
        {
            ProgressRing_Char.IsActive = true;
            try
            {
                ViewModel.CurrentChar = await CharIO.LoadCharAtCurrentPlace(((CharSummory)((Button)sender).DataContext).strFileName);
            }
            catch (Exception)
            {
                //TODO notify user
                throw; //then remove throw
            }
            ProgressRing_Char.IsActive = false;
            if (ViewModel.CurrentChar != null)
            {
                Frame.Navigate(typeof(Char), ViewModel);
            }
        }

        private async void Click_Speichern(object sender, RoutedEventArgs e)
        {
            try
            {
                await CharIO.SaveCharAtCurrentPlace(ViewModel.CurrentChar);
            }
            catch (Exception)
            {
                //TODO notify user
                throw; //then remove throw
            }
        }

        private async void Click_Laden_Datei(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.CurrentChar = await CharIO.LoadCharFromFile(await GeneralIO.FilePicker(new List<string>(new string[] { Konstanten.DATEIENDUNG_CHAR})));
            }
            catch (Exception)
            {
                //TODO notify user
                throw; //then remove throw
            }
        }

        private async void Click_Speichern_Datei(object sender, RoutedEventArgs e)
        {
            try
            {
                CharHolder CharToSave = await CharIO.LoadCharAtCurrentPlace(((CharSummory)((Button)sender).DataContext).strFileName);
                StorageFile FileToSave = await GeneralIO.GetFile(Place.Extern, CharToSave.MakeName());
                CharIO.SaveCharToFile(CharToSave, FileToSave);
            }
            catch (Exception)
            {
                //TODO notify user
                throw; //then remove throw
            }
        }

        private async void Export_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder Folder = null;
            try
            {
                Folder = await GeneralIO.GetFolder(Place.Extern);
            }
            catch (Exception)
            {
                //TODO notify user
            }
            try
            {
                foreach (var item in ViewModel.CurrentChar.ToCSV(";"))
                {
                    StorageFile File = await GeneralIO.GetFile(Place.Extern, item.Value + Konstanten.DATEIENDUNG_CSV, Folder.Path);
                    GeneralIO.Write(File, item.Key);
                }
            }
            catch (Exception)
            {
                //TODO notify user
                //throw;
            }
        }

        private async void Import_Click(object sender, RoutedEventArgs e)
        {
            StorageFile File = null;
            string strRead = "";
            try
            {
                File = await GeneralIO.GetFile(Place.Extern, null, null, new List<string>(new string[] { Konstanten.DATEIENDUNG_CHAR, Konstanten.DATEIENDUNG_CSV }));
                strRead = await FileIO.ReadTextAsync(File);

            }
            catch (Exception)
            {
                //TODO
            }
            try
            {
                if (ViewModel.CurrentChar == null)
                {
                    ViewModel.CurrentChar = new CharHolder();
                }
                ViewModel.CurrentChar.CTRLCyberDeck.MultipleCSVImport(';', '\n', strRead);
            }
            catch (Exception)
            {//todo
                //throw;
            }

        }
    }

}