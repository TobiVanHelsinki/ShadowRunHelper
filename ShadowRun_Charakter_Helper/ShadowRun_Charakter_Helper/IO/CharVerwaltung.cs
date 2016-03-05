using ShadowRun_Charakter_Helper.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;

namespace ShadowRun_Charakter_Helper.IO
{
    class CharVerwaltung : INotifyPropertyChanged
    {
        
        int Mode = (int)FolderMode.Intern;


        private ObservableCollection<Model.CharSummory> summorys;

        public ObservableCollection<Model.CharSummory> Summorys
        {
            get
            {
                return summorys;
            }
            set
            {
                if (value != this.summorys)
                {
                    this.summorys = value;
                    NotifySummoryChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifySummoryChanged([CallerMemberName] String summoryName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(summoryName));
            }
        }


        /// <summary>
        /// Nutzen für mit Liste
        /// </summary>
        /// <param name="id"></param>
        public CharVerwaltung()
        {
            summorys = new ObservableCollection<Model.CharSummory>();
            // List erstellen, entweder aus dem App Container oder aus dem Ordner oder beidem 
            Summorys_Aktualisieren();
        }
        private async void Summorys_Aktualisieren()
        {
            AutoResetEvent autoEvent = new AutoResetEvent(false);

            Timer stateTimer = new Timer(noob, autoEvent, 500, 0);

            System.Diagnostics.Debug.WriteLine("{0} Creating timer.", DateTime.Now.ToString("h:mm:ss.fff"));
            autoEvent.WaitOne(500);
            stateTimer.Dispose();
            System.Diagnostics.Debug.WriteLine("{0} Deleting timer.", DateTime.Now.ToString("h:mm:ss.fff"));

            Summorys.Clear();

            // system unauthorized abfangen
            StorageFolder CharFolder;
            if (Mode == (int)FolderMode.Intern)
            {
                CharFolder = await getInternFolder();
            }
            else if (Mode == (int)FolderMode.Extern)
            {
                CharFolder = await getExternFolder();
            }
            else
            {
                throw new Exception("");
            }
            foreach (var item in await IO.CharIO.getListofChars(CharFolder))
            {
                item.Summory.Replace('_', ' ');
                Summorys.Add(item);
            }

        }

        private void noob(object state)
        {
            System.Diagnostics.Debug.WriteLine("{0} Timer kommt.", DateTime.Now.ToString("h:mm:ss.fff"));
        }

        public string makeName(CharHolder SaveChar)
        {
            String temp_Alias = "";
            String temp_Char_Typ = "";
            String temp_Karma = "";
            String temp_Runs = "";
            try
            {
                temp_Alias = SaveChar.Person.Alias;
                if (temp_Alias == "")
                {
                    throw new NullReferenceException();
                }
            }
            catch (NullReferenceException) { temp_Alias = "$ohne Namen$"; }
            try
            {
                temp_Char_Typ = SaveChar.Person.Char_Typ;
                if (temp_Char_Typ == "")
                {
                    throw new NullReferenceException();
                }
            }
            catch (NullReferenceException) { temp_Char_Typ = "$ohne Beruf$"; }
            try
            {
                temp_Karma = SaveChar.Person.Karma_Gesamt.ToString();
                if (temp_Karma == "")
                {
                    throw new NullReferenceException();
                }
            }
            catch (NullReferenceException) { temp_Karma = "$ohne Erfolg$"; }
            try
            {
                temp_Runs = SaveChar.Person.Runs.ToString();
                if (temp_Runs == "")
                {
                    throw new NullReferenceException();
                }
            }
            catch (NullReferenceException) { temp_Runs = "$ohne Erfolg$"; }

            return temp_Alias + "_" + temp_Char_Typ + "_Karma_" + temp_Karma + "_Runs_" + temp_Runs + Variablen.DATEIENDUNG_CHAR;

        }

        private static async Task<StorageFolder> getInternFolder()
        {
            StorageFolder RoamingFolder = ApplicationData.Current.RoamingFolder;
            StorageFolder CharFolder = null;
            try
            {
                CharFolder = await RoamingFolder.GetFolderAsync(Variablen.CONTAINER_CHAR);
            }
            catch (System.IO.FileNotFoundException)
            {
                CharFolder = await RoamingFolder.CreateFolderAsync(Variablen.CONTAINER_CHAR);
            }
            return CharFolder;
        }

        private static async Task<StorageFolder> getExternFolder()
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            folderPicker.FileTypeFilter.Add(Variablen.DATEIENDUNG_CHAR);
            //Ordner Auswähler rufen
            StorageFolder CharFolder = await folderPicker.PickSingleFolderAsync();
            Windows.Storage.AccessCache.StorageApplicationPermissions.
                FutureAccessList.AddOrReplace("PickedFolderToken", CharFolder);
            return CharFolder;
        }

        public async Task<CharHolder> LadenIntern(string id)
        {
            StorageFolder CharFolder;
            StorageFile CharFile;
            if (Mode == (int)FolderMode.Intern)
            {
                CharFolder = await getInternFolder();
            }
            else if (Mode == (int)FolderMode.Extern)
            {
                CharFolder = await getExternFolder();
            }
            else
            {
                throw new Exception("");
            }
            try
            {
                CharFile = await CharFolder.GetFileAsync(id);
            }
            catch (Exception)
            {

                throw new Exception("Konnte Char nciht laden");
            }
            
            return await IO.CharIO.Laden(CharFile);
        }

        public async void LadenExtern()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            picker.FileTypeFilter.Add(Variablen.DATEIENDUNG_CHAR);

            StorageFile file = await picker.PickSingleFileAsync();
            CharHolder temp = await IO.CharIO.Laden(file);
            await this.SpeichernIntern(temp);
        }

        public async void LadenExtern(StorageFile file)
        {
            CharHolder temp = await IO.CharIO.Laden(file);
            await this.SpeichernIntern(temp);
        }

        public async Task<string> SpeichernIntern(CharHolder SaveChar)
        {
            StorageFolder CharFolder = await getInternFolder();

            return await Speichern(SaveChar, CharFolder);

        }

        public async Task<string> SpeichernExtern(string id)
        {
            CharHolder SaveChar = await LadenIntern(id);
            StorageFolder CharFolder = await getExternFolder();

            return await Speichern(SaveChar, CharFolder);
        }

        private async Task<string> Speichern(CharHolder SaveChar, StorageFolder CharFolder)
        {
            String filename = makeName(SaveChar);
            StorageFile Save_File = await CharFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            Save_File = await CharFolder.GetFileAsync(filename);

            IO.CharIO.Speichern(SaveChar, Save_File);
            this.Summorys_Aktualisieren();
            return filename;
        }

        public async void Lösche(string id)
        {
            StorageFolder CharFolder;
            if (Mode == (int)FolderMode.Intern)
            {
                CharFolder = await getInternFolder();
            }
            else if (Mode == (int)FolderMode.Extern)
            {
                CharFolder = await getExternFolder();
            }
            else
            {
                throw new Exception("");
            }
            StorageFile toDelFile = await CharFolder.GetFileAsync(id);
            IO.CharIO.Löschen(toDelFile);
            this.Summorys_Aktualisieren();
        }

        public async void LöscheAlles()
        {
            var messageDialog = new MessageDialog("Damit zerstörst du die Existenz von ... JEDEM!, Chummer! Bist du sicher, dass du das machen willst?");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "Wirklich Löschen",
                new UICommandInvokedHandler(this.LöscheAlles_Löschen)));
            messageDialog.Commands.Add(new UICommand(
                "Ach nein doch nicht"));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();

        }

        private void LöscheAlles_Löschen(IUICommand command)
        {
            foreach (var item in Summorys)
            {
                Lösche(item.ID);
            }
            this.Summorys_Aktualisieren();
        }
    }
}
