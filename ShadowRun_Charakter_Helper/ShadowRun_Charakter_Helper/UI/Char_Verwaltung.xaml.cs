using System;
using ShadowRun_Charakter_Helper.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Primitives;

namespace ShadowRun_Charakter_Helper
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
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = (CharViewModel)e.Parameter;
            this.Verwaltung = new IO.CharVerwaltung(1);
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
           ViewModel = new CharViewModel();
           Frame.Navigate(typeof(Char), ViewModel);
        }

        private void Click_Löschen(object sender, RoutedEventArgs e)
        {
            string id = ((CharSummory)((Button)sender).DataContext).ID;
            Verwaltung.Lösche(id);
        }

        private void Click_Löschen_Alles(object sender, RoutedEventArgs e)
        {
            Verwaltung.LöscheAlles();
        }

        private async void Click_Laden(object sender, RoutedEventArgs e)
        {
            string id = ((CharSummory)((Button)sender).DataContext).ID;
            ViewModel.Current = await Verwaltung.LadenIntern(id);
            Frame.Navigate(typeof(Char), ViewModel);
        }

        private void Click_Speichern(object sender, RoutedEventArgs e)
        {
            Verwaltung.SpeichernIntern(ViewModel.Current);

        }

        private void Click_Laden_Datei(object sender, RoutedEventArgs e)
        {
            Verwaltung.LadenExtern();
        }

        private void Click_Speichern_Datei(object sender, RoutedEventArgs e)
        {
            string id = ((CharSummory)((Button)sender).DataContext).ID;
            Verwaltung.SpeichernExtern(id);
        }
    }
}