using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ShadowRun_Charakter_Helper.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ShadowRun_Charakter_Helper.CharModel;
using ShadowRun_Charakter_Helper.CharController;
using ShadowRun_Charakter_Helper.Controller;

// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace ShadowRun_Charakter_Helper
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public CharViewModel ViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = new CharViewModel();
            MyFrame.Navigate(typeof(Char), ViewModel);
        }

        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Char.IsSelected) { MyFrame.Navigate(typeof(Char), ViewModel); }
            else if (Char_Change.IsSelected) { MyFrame.Navigate(typeof(Char_Verwaltung), ViewModel); }
            else if (App_Settings.IsSelected) { MyFrame.Navigate(typeof(TestPage), ViewModel); }
            else
            {
                MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
                return;
            }
        }

        private void Karma_Gesamt_Plus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Current.Person.Karma_Gesamt++;
        }

        private void Karma_Gesamt_Minus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Current.Person.Karma_Gesamt--;
        }

        private void Karma_Aktuell_Plus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Current.Person.Karma_Aktuell++;
        }

        private void Karma_Aktuell_Minus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Current.Person.Karma_Aktuell--;
        }

        private void Edgne_Aktuell_Plus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Current.Person.Edge_Aktuell++;
        }

        private void Edgne_Aktuell_Minus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Current.Person.Edge_Aktuell--;
        }

        private void Edge_Gesamt_Plus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Current.Person.Edge_Gesamt++;
        }

        private void Edge_Gesamt_Minus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Current.Person.Edge_Gesamt--;
        }

        private void Essenz_Plus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Current.Person.Essenz++;
        }

        private void Essenz_Minus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Current.Person.Essenz--;
        }

        private void Initiative_Plus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Current.Person.Initiative++;
        }

        private void Initiative_Minus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Current.Person.Initiative--;
        }
    }
}
