using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;

namespace ShadowRun_Charakter_Helper.Controller
{
    class CharVerwaltung : INotifyPropertyChanged
    {
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
        /// Nutzen für ohne Liste
        /// </summary>
        public CharVerwaltung()
        {
        }
        /// <summary>
        /// Nutzen für mit Liste
        /// </summary>
        /// <param name="id"></param>
        public CharVerwaltung(int id)
        {
            summorys = new ObservableCollection<Model.CharSummory>();
            // List erstellen, entweder aus dem App Container oder aus dem Ordner oder beidem 
            Summorys_Aktualisieren();
        }
        private async void Summorys_Aktualisieren()
        {
            Summorys.Clear();
            // system unauthorized abfangen
            foreach (var item in await IO.CharIO.getListofChars())
            {

                summorys.Add(item);
            }
            //summorys = await IO.CharIO.getListofChars();
        }

        public async Task<CharHolder> Laden(int id)
        {
            return await IO.CharIO.Lade(id);
        }

        public async void LadenDatei()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            picker.FileTypeFilter.Add(".SRWin");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            CharHolder temp = await IO.CharIO.Lade(file);
            //todo freie id finden
            this.Speichern(temp);
        }

        public void Speichern(CharHolder Char)
        {
            int newID = this.Summorys_getFreeID();
            IO.CharIO.Speichern(Char, newID);
            this.Summorys_Aktualisieren();
        }

        private int Summorys_getFreeID()
        {
            List<int> templist = new List<int>();

            foreach (var item in Summorys)
            {
                templist.Add(item.ID);
            }

            int i;

            for (i = 0; i < templist.Count; i++)
            {
                if (!templist.Contains(i))
                {
                    return i;
                }
            }
            return i;

        }

        public async void SpeichernDatei(int id)
        {
            CharHolder SaveChar = await Laden(id);
            //Ordner Auswähler vorbereiten
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            folderPicker.FileTypeFilter.Add(".SRWin");
            //Ordner Auswähler rufen
            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                // Application now has read/write access to all contents in the picked folder
                // (including other sub-folder contents)
                Windows.Storage.AccessCache.StorageApplicationPermissions.
                FutureAccessList.AddOrReplace("PickedFolderToken", folder);

                //Dateiname und Datei vorbereiten
                String filename = SaveChar.Person.Alias + "_" + SaveChar.Person.Karma_Gesamt + "_Karma_" + SaveChar.Person.Runs + "_Runs_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".SRWin";
                StorageFile Save_File = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                Save_File = await folder.GetFileAsync(filename);

                IO.CharIO.Speichern(SaveChar, Save_File);
                this.Summorys_Aktualisieren();
            }
        }

        public void Lösche(int id)
        {
            IO.CharIO.Löschen(id);
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
