using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ShadowRun_Charakter_Helper.Model;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ShadowRun_Charakter_Helper.IO;
using System.ComponentModel;
using ShadowRun_Charakter_Helper.Models;
using Windows.UI.Xaml.Controls.Primitives;

namespace ShadowRun_Charakter_Helper
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Char_Verwaltung : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private CharViewModel ViewModel { get; set; }
        private Controller.Char_Verwaltung Verwaltung { get; set; }

        public Char_Verwaltung()
        {
            this.Verwaltung = new Controller.Char_Verwaltung();
            this.InitializeComponent();
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
            ViewModel.Current = new Controller.CharHolder();
            Frame.Navigate(typeof(Char), ViewModel);
        }

        private void Click_Löschen(object sender, RoutedEventArgs e)
        {
        }

        private void Click_Löschen_Alles(object sender, RoutedEventArgs e)
        {
        }

        private void Click_Laden(object sender, RoutedEventArgs e)
        {
            ViewModel.Current = Verwaltung.LadenApp(id);
            //todo id aus sender generieren
        }

        private void Click_Speichern(object sender, RoutedEventArgs e)
        {
        }

        private void Click_Laden_Datei(object sender, RoutedEventArgs e)
        {
        }

        private void Click_Speichern_Datei(object sender, RoutedEventArgs e)
        {
        }
    }
}