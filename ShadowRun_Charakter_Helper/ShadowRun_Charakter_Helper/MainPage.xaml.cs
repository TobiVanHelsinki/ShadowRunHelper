using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ShadowRun_Charakter_Helper.Models;
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
    /// Eine leere Seite, die eigenst├ñndig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public CharViewModel ViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = new CharViewModel();
            MyFrame.Navigate(typeof(Frame_Char_Change), ViewModel);

            CharHolder Char_new = new CharHolder();
            //Char_new.addHandlung();
            //Char_new.addHandlung();

        }

        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Char.IsSelected) { MyFrame.Navigate(typeof(Frame_Char), ViewModel);}
            else if (Char_Edit.IsSelected) { MyFrame.Navigate(typeof(Frame_Char_Edit), ViewModel); }
            else if (Database_Edit.IsSelected) { MyFrame.Navigate(typeof(Frame_Database_Edit)); }
            else if (Char_Change.IsSelected) { MyFrame.Navigate(typeof(Frame_Char_Change), ViewModel); }
            else if (App_Settings.IsSelected) { MyFrame.Navigate(typeof(TestPage), ViewModel); }
            else
            {
                MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
                return;
            }
        }

        private void Karma_Gesamt_Plus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Karma_Gesamt++;
        }

        private void Karma_Gesamt_Minus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Karma_Gesamt--;
        }

        private void Karma_Aktuell_Plus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Karma_Aktuell++;
        }

        private void Karma_Aktuell_Minus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Karma_Aktuell--;
        }

        private void Edgne_Aktuell_Plus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Edgne_Aktuell++;
        }

        private void Edgne_Aktuell_Minus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Edgne_Aktuell--;
        }

        private void Edge_Gesamt_Plus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Edge_Gesamt++;
        }

        private void Edge_Gesamt_Minus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Edge_Gesamt--;
        }

        private void Essenz_Plus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Essenz++;
        }

        private void Essenz_Minus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Essenz--;
        }

        private void Initiative_Plus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Initiative++;
        }

        private void Initiative_Minus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Initiative--;
        }
    }
}
