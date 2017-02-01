using System;
using ShadowRunHelper.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Primitives;
using System.Collections.Generic;
using Windows.Storage;

namespace ShadowRunHelper
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Char_Verwaltung : Page
    {
        private CharViewModel ViewModel { get; set; }
        private IO.CharVerwaltung Verwaltung { get; set; }

        public Char_Verwaltung()
        {
            this.InitializeComponent();
            ProgressRing_Char.IsActive = true;
            this.Verwaltung = new IO.CharVerwaltung();
            
            ProgressRing_Char.IsActive = false;
        }

        ~Char_Verwaltung()
        {
            this.Verwaltung = null;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            ViewModel = (CharViewModel)e.Parameter;
            Verwaltung.Summorys_Aktualisieren();
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
           ViewModel.Current = new CharHolder();
            //ViewModel.currentState = TCharState.IN_USE;
           Frame.Navigate(typeof(Char), ViewModel);
        }

        private void Click_Löschen(object sender, RoutedEventArgs e)
        {
            string id = ((CharSummory)((Button)sender).DataContext).strFileName;
            Verwaltung.Lösche(id);
        }

        private void Click_Löschen_Alles(object sender, RoutedEventArgs e)
        {
            ViewModel.Current = null;

            //Verwaltung.LöscheAlles();
        }

        private async void Click_Laden(object sender, RoutedEventArgs e)
        {
            ProgressRing_Char.IsActive = true;
            string id = ((CharSummory)((Button)sender).DataContext).strFileName;
            //ViewModel.Current = null;
            
            ViewModel.Current = await Verwaltung.LadenIntern(id); //todo try catch?
            //ViewModel.currentState = TCharState.IN_USE;
            ProgressRing_Char.IsActive = false;
            Frame.Navigate(typeof(Char), ViewModel);
        }

        private async void Click_Speichern(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Current!=null)
            {
                await Verwaltung.SpeichernIntern(ViewModel.Current);
            }
        }

        private void Click_Laden_Datei(object sender, RoutedEventArgs e)
        {
            Verwaltung.LadenExtern();
        }

        private async void Click_Speichern_Datei(object sender, RoutedEventArgs e)
        {
            string id = ((CharSummory)((Button)sender).DataContext).strFileName;
            await Verwaltung.SpeichernExtern(id);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder Folder = await IO.CharVerwaltung.getExternFolder();
            foreach (var item in ViewModel.Current.TOCSV(";"))
            {
                StorageFile File = await Folder.CreateFileAsync(item.Value+".csv", CreationCollisionOption.ReplaceExisting);
                //System.ArgumentException
                await FileIO.WriteTextAsync(File, item.Key,Windows.Storage.Streams.UnicodeEncoding.Utf8);
            }
        }
    }
}