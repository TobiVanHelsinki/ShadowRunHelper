using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using ShadowRun_Charakter_Helper.Models;
using ShadowRun_Charakter_Helper.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRun_Charakter_Helper
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Frame_Char_Change : Page, INotifyPropertyChanged
    {
        public CharViewModel ViewModel { get; set; }
        private ObservableCollection<Char_Summory> Char_List_Sum = new ObservableCollection<Char_Summory>();
        private Char_Summory selected_Char_Summory;

        public Frame_Char_Change()
        {
            createSummorys();
            this.ViewModel = new CharViewModel();
            this.InitializeComponent();
        }


        private void createSummorys() {
            String[] Char_List = CharList.ReadSeperated();
            try {  
                for (int i = 0; i < Char_List.Length; i++)
                {
                    Char_List_Sum.Add(Char_Summory.get_char_summory_by_id(Int32.Parse(Char_List[i])));
                }
            }
            catch
            {
                return;
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = (CharViewModel)e.Parameter;
        }

        private void CharSum_Auswahl(object sender, ItemClickEventArgs e)
        {
            selected_Char_Summory = (Char_Summory)e.ClickedItem;
        }



        private void Char_Auswahl(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ViewModel.DefaultChar.ID_Char != selected_Char_Summory.id)
                {
                    Store_Data.Store_Char(ViewModel.DefaultChar);
                }
                Load_Data.Clear_Char(ViewModel.DefaultChar);


                ViewModel.DefaultChar.ID_Char = selected_Char_Summory.id;
            }
            catch (NullReferenceException)
            {
                return;
            }
            Load_Data.Load_Char(ViewModel.DefaultChar);
            Frame.Navigate(typeof(Frame_Char), ViewModel);
        }


        private void Neu_Click(object sender, RoutedEventArgs e)
        {
            Load_Data.Clear_Char(ViewModel.DefaultChar);

            ViewModel.DefaultChar.ID_Char = CharList.Add();

            Store_Data.Store_Char(ViewModel.DefaultChar);
            CharList.Add(ViewModel.DefaultChar.ID_Char);
            Load_Data.Clear_Char(ViewModel.DefaultChar);
            Load_Data.Load_Char(ViewModel.DefaultChar);

            Frame.Navigate(typeof(Frame_Char_Edit), ViewModel);
        }

        private async void Löschen_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Damit zerstörst du die Existenz von " + selected_Char_Summory.char_summory + ", Chummer! Bist du sicher, dass du das machen willst?");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "Wirklich Löschen",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.Commands.Add(new UICommand(
                "Ach nein doch nicht"));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private async void CommandInvokedHandler(IUICommand command)
        {
            // Display message showing the label of the command that was invoked
            CharList.Delete(selected_Char_Summory.id);
            Char_List_Sum.Remove(selected_Char_Summory);

            if (ViewModel.DefaultChar.ID_Char == selected_Char_Summory.id)
            {
                Load_Data.Clear_Char(ViewModel.DefaultChar);
            }

            MessageDialog dialog = new MessageDialog("Er ist tot, Jim.", "Nachricht");
            await dialog.ShowAsync();
        }

        private void Speichern_Click(object sender, RoutedEventArgs e)
        {
            Store_Data.Store_Char(ViewModel.DefaultChar);
            //CharList.Add(ViewModel.DefaultChar.ID_Char);
            createSummorys();
        }


        private async void Datei_Laden_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            picker.FileTypeFilter.Add(".SRWin");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                // File -> string, Method in Load aufrufen, neuen char speichern, liste aktualisieren, 
                string fileContent = await FileIO.ReadTextAsync(file);

                Models.Char temp_char = new Models.Char();

                Load_Data.Load_Char_from_String(fileContent, temp_char);
                if (CharList.Beinhaltet(temp_char.ID_Char))
                {
                    temp_char.ID_Char = CharList.freieID();   
                }
                //id kann beibehalten werden, wenn es keine andere gibt
                Store_Data.Store_Char(temp_char);
                CharList.Add(temp_char.ID_Char);
                Char_List_Sum.Add(Char_Summory.get_char_summory_by_id(temp_char.ID_Char));
                temp_char = null;
            }
        }

        private async void Datei_Speichern_Click(object sender, RoutedEventArgs e)
        {
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
                
                //Richtigen Char auswählen
                Models.Char temp_char = new Models.Char();
                temp_char.ID_Char = selected_Char_Summory.id;
                Load_Data.Load_Char(temp_char);

                //Dateiname und Datei vorbereiten
                String filename = temp_char.ID_Char + "_" + temp_char.Alias + "_" + temp_char.Karma_Gesamt + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second+ ".SRWin";
                StorageFile Save_File = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                Save_File = await folder.GetFileAsync(filename);

                
                //Ausgewählten Char auf schreiben
                await FileIO.WriteTextAsync(Save_File, Store_Data.Store_Char_to_String(temp_char));
                //Variable Befreien
                temp_char = null;
            }
        }
    }
}
