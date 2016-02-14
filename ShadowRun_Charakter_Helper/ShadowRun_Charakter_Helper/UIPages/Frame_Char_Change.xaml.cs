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
using ShadowRun_Charakter_Helper.IO;

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


        private void createSummorys()
        {
            List<int> Char_List = CharList.ReadSeperatedtoList();
            Char_List_Sum.Clear();
            try
            {
                for (int i = 0; i < Char_List.Count; i++)
                {
                    Char_List_Sum.Add(Char_Summory.get_char_summory_by_id((Char_List[i])));
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

        
        private async void Löschen(IUICommand command)
        {
            // Char Löschen
            CharList.Delete(selected_Char_Summory.id);
            Char_List_Sum.Remove(selected_Char_Summory);

            if (ViewModel.DefaultChar.ID_Char == selected_Char_Summory.id)
            {
                ViewModel.DefaultChar.Säubern();
            }
            // Nachicht Anzeigen
            MessageDialog dialog = new MessageDialog("Er ist tot, Jim.", "Nachricht");
            await dialog.ShowAsync();
        }

        private async void Löschen_Alles(IUICommand command)
        {
            // Char Löschen
            CharList.Clear();
            createSummorys();
           
            ViewModel.DefaultChar.Säubern();
            // Nachicht Anzeigen
            MessageDialog dialog = new MessageDialog("Alle sind tot, Jim.", "Nachricht");
            await dialog.ShowAsync();
        }

        private void Click_Erstellen(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.ID_Char = CharList.freieID();
            CharIO.Save_JSONChar_to_Data(ViewModel.DefaultChar);
            CharList.Add(ViewModel.DefaultChar.ID_Char);

            Frame.Navigate(typeof(Frame_Char_Edit), ViewModel);

        }

        private async void Click_Löschen(object sender, RoutedEventArgs e)
        {
            if (selected_Char_Summory == null) { return; }
            var messageDialog = new MessageDialog("Damit zerstörst du die Existenz von " + selected_Char_Summory.char_summory + ", Chummer! Bist du sicher, dass du das machen willst?");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "Wirklich Löschen",
                new UICommandInvokedHandler(this.Löschen)));
            messageDialog.Commands.Add(new UICommand(
                "Ach nein doch nicht"));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private async void Click_Löschen_Alles(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("Damit zerstörst du die Existenz von ... JEDEM!, Chummer! Bist du sicher, dass du das machen willst?");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "Wirklich Löschen",
                new UICommandInvokedHandler(this.Löschen_Alles)));
            messageDialog.Commands.Add(new UICommand(
                "Ach nein doch nicht"));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 1;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private void Click_Laden(object sender, RoutedEventArgs e)
        {
            if (selected_Char_Summory != null) {
                ViewModel.DefaultChar = CharIO.Load_JSONChar_from_Data(ViewModel.DefaultChar.ID_Char);
                Frame.Navigate(typeof(Frame_Char), ViewModel);
            }
        }

        private void Click_Speichern(object sender, RoutedEventArgs e)
        {
            CharIO.Save_JSONChar_to_Data(ViewModel.DefaultChar);
            createSummorys();
        }

        private async void Click_Laden_Datei(object sender, RoutedEventArgs e)
        {

             ViewModel.DefaultChar = await CharIO.Load_JSONChar_from_IO();
            Frame.Navigate(typeof(Frame_Char), ViewModel);
        }

        private void Click_Speichern_Datei(object sender, RoutedEventArgs e)
        {
            if (selected_Char_Summory != null) { 
 
                //Richtigen Char auswählen
                Models.Char temp_char = new Models.Char();

                temp_char = CharIO.Load_JSONChar_from_Data(selected_Char_Summory.id);
                CharIO.Save_JSONChar_to_IO(temp_char);

                 //var befreien
                temp_char = null;
            }
        }
    }
}
