using ShadowRunHelper.Model;
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(summoryName));
        }

        /// <summary>
        /// Nutzen für mit Liste
        /// </summary>
        /// <param name="id"></param>
        public CharVerwaltung()
        {
            summorys = new ObservableCollection<Model.CharSummory>();
            // List erstellen, entweder aus dem App Container oder aus dem Ordner oder beidem 
            // Summorys_Aktualisieren();
        }

        public async void Summorys_Aktualisieren()
        {
            Summorys.Clear();
            StorageFolder CharFolder = await getInternFolder();
            foreach (var item in await CharIO.getListofChars(CharFolder))
            {
                Summorys.Add(item);
            }
            // system unauthorized abfangen?
        }

        private static async Task<StorageFolder> getInternFolder()
        {
            StorageFolder RoamingFolder = ApplicationData.Current.RoamingFolder;
            StorageFolder CharFolder = null;
            string path = "";
            try
            {
                if (Optionen.ORDNERMODE)
                {
                    path = Optionen.ORDNERMODE_PFAD;
                    CharFolder = await StorageFolder.GetFolderFromPathAsync(path);
                }
                else
                {
                    path = Konstanten.CONTAINER_CHAR;
                    CharFolder = await RoamingFolder.GetFolderAsync(path);
                }
            }
            catch (Exception)
            {
                if (Optionen.ORDNERMODE)
                {
                    CharFolder = null;
                }
                else
                {
                    path = Konstanten.CONTAINER_CHAR;
                    await RoamingFolder.CreateFolderAsync(path);
                    CharFolder = await RoamingFolder.GetFolderAsync(path);
                }
            }

            return CharFolder;

        }

        public static async Task<StorageFolder> getExternFolder()
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            folderPicker.FileTypeFilter.Add(Konstanten.DATEIENDUNG_CHAR);
            //Ordner Auswähler rufen
            StorageFolder CharFolder;
            try
            {
                CharFolder = await folderPicker.PickSingleFolderAsync();
            }
            catch (Exception)
            {
                CharFolder = null;
            }
            
            Windows.Storage.AccessCache.StorageApplicationPermissions.
                FutureAccessList.AddOrReplace("PickedFolderToken", CharFolder);
            return CharFolder;
        }

        public async Task<CharHolder> LadenIntern(string fileName)
        {
            StorageFolder CharFolder;
            StorageFile CharFile;
            CharFolder = await getInternFolder();
            try
            {
                CharFile = await CharFolder.GetFileAsync(fileName);
            }
            catch (Exception)
            {
                CharHolder temp = new CharHolder();
                return temp;
               // throw new Exception("Konnte Char nciht laden");
            }
            
            return await IO.CharIO.Laden(CharFile); //TODO introduce user notification system and surround with try catch

        }

        public async void LadenExtern()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            picker.FileTypeFilter.Add(Konstanten.DATEIENDUNG_CHAR);

            StorageFile file = await picker.PickSingleFileAsync();
            CharHolder temp = await IO.CharIO.Laden(file); //TODO introduce user notification system and surround with try catch
            await this.SpeichernIntern(temp);
        }

        public async void LadenExtern(StorageFile file)
        {
            CharHolder temp = await IO.CharIO.Laden(file); //TODO introduce user notification system and surround with try catch
            await this.SpeichernIntern(temp);
        }

        public async Task<string> SpeichernIntern(CharHolder SaveChar)
        {
            StorageFolder SaveFolder = await getInternFolder();
            String SaveName = SaveChar.MakeName(false);
            return await Speichern(SaveChar, SaveFolder, SaveName);
        }

        public async Task<string> SpeichernExtern(string filename)
        {
            CharHolder SaveChar = await LadenIntern(filename);
            String SaveName = "";
            try
            {
                SaveName = SaveChar.MakeName(true);
            }
            catch (Exception)
            {
                throw;
            }
            StorageFolder SaveFolder = await getExternFolder();
            if (SaveFolder == null)
            {
                return "";
            }
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

        public async void Lösche(string filename)
        {
            StorageFolder CharFolder;
            CharFolder = await getInternFolder();
            try
            {
                StorageFile toDelFile = await CharFolder.GetFileAsync(filename);
                IO.CharIO.Löschen(toDelFile);
            }
            catch (Exception)
            {
            }
           
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
                Lösche(item.strFileName);
            }
            this.Summorys_Aktualisieren();
        }
    }
}
