using ShadowRunHelper.Controller;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;

namespace ShadowRunHelper.IO
{
    class CharVerwaltung : INotifyPropertyChanged
    {
        private enum Ort
        {
            InternSpeichern = 0,
            ExternSpeichern = 1
        }

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
            CharFolder = await getInternFolder();

            foreach (var item in await IO.CharIO.getListofChars(CharFolder))
            {
                item.Summory = item.ID.Replace('_', ' ');
                item.Summory = item.Summory.Replace(Variablen.DATEIENDUNG_CHAR, "");
                item.Summory += " (" + item.DateCreated.Day + "." + item.DateCreated.Month +"."+ item.DateCreated.Year + " " + item.DateCreated.Hour + ":" + item.DateCreated.Minute +")";
                Summorys.Add(item);
            }

        }

        private void noob(object state)
        {
            //System.Diagnostics.Debug.WriteLine("{0} Timer kommt.", DateTime.Now.ToString("h:mm:ss.fff"));
        }

        private string makeName(CharHolder SaveChar, Ort SpeicherOrt)
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

            if (SpeicherOrt == Ort.InternSpeichern)
            {
                return temp_Alias + "_" + temp_Char_Typ + "_Karma_" + temp_Karma + "_Runs_" + temp_Runs + Variablen.DATEIENDUNG_CHAR;
            }
            else
            {
                return temp_Alias + "_" + temp_Char_Typ + "_Karma_" + temp_Karma + "_Runs_" + temp_Runs + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + Variablen.DATEIENDUNG_CHAR;
            }
        }

        private static async Task<StorageFolder> getInternFolder()
        {
            StorageFolder RoamingFolder = ApplicationData.Current.RoamingFolder;
            StorageFolder CharFolder = null;
            string path = "";
            if (Optionen.ORDNERMODE)
            {
                path = Optionen.ORDNERMODE_PFAD; 
                CharFolder = await Windows.Storage.StorageFolder.GetFolderFromPathAsync(path);
            }
            else
            {
                path = Variablen.CONTAINER_CHAR;
                CharFolder = await RoamingFolder.GetFolderAsync(path);
            }

            return CharFolder;

        }

        public static async Task<StorageFolder> getExternFolder()
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            folderPicker.FileTypeFilter.Add(Variablen.DATEIENDUNG_CHAR);
            //Ordner Auswähler rufen
            StorageFolder CharFolder;
            try
            {
                CharFolder = await folderPicker.PickSingleFolderAsync();
            }
            catch (Exception)
            {
                throw;
            }
            
            Windows.Storage.AccessCache.StorageApplicationPermissions.
                FutureAccessList.AddOrReplace("PickedFolderToken", CharFolder);
            return CharFolder;
        }

        public async Task<CharHolder> LadenIntern(string id)
        {
            StorageFolder CharFolder;
            StorageFile CharFile;
            CharFolder = await getInternFolder();
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
            StorageFolder SaveFolder = await getInternFolder();
            String SaveName = makeName(SaveChar, Ort.InternSpeichern);
            return await Speichern(SaveChar, SaveFolder, SaveName);
        }

        public async Task<string> SpeichernExtern(string id)
        {
            CharHolder SaveChar = await LadenIntern(id);
            String SaveName = "";
            try
            {
                SaveName = makeName(SaveChar, Ort.ExternSpeichern);
            }
            catch (Exception)
            {
                throw;
            }
            StorageFolder SaveFolder = await getExternFolder();
            return await Speichern(SaveChar, SaveFolder, SaveName);
        }

        private async Task<string> Speichern(CharHolder SaveChar, StorageFolder SaveFolder, String SaveName )
        {
            StorageFile Save_File = await SaveFolder.CreateFileAsync(SaveName, CreationCollisionOption.ReplaceExisting);
            Save_File = await SaveFolder.GetFileAsync(SaveName);

            IO.CharIO.Speichern(SaveChar, Save_File);
            this.Summorys_Aktualisieren();
            return SaveName;
        }

        public async void Lösche(string id)
        {
            StorageFolder CharFolder;
            CharFolder = await getInternFolder();
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
