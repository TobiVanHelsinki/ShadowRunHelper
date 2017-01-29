using ShadowRunHelper1_3.Controller;
using ShadowRunHelper1_3.Model;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace ShadowRunHelper1_3
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
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel = (CharViewModel)e.Parameter;

            if (ViewModel.currentState == TApp.TCharState.EMPTY_CHAR)
            {
                MyFrame.Navigate(typeof(Char_Verwaltung), ViewModel);
            }
            else if (ViewModel.currentState == TApp.TCharState.LOAD_CHAR || ViewModel.currentState == TApp.TCharState.NEW_CHAR)
            {
                MyFrame.Navigate(typeof(Char), ViewModel);
            }
            else
            {
                MyFrame.Navigate(typeof(Char), ViewModel);
            }
        }

        void disableUI() {
            Header_Kontostand.IsEnabled = false;
            XAML_Header_Schaden_G_Slider.IsEnabled = false;
            XAML_Header_Schaden_K_Slider.IsEnabled = false;
            XAML_Header_Schaden_M_Slider.IsEnabled = false;
        }

        void enableUI()
        {
            Header_Kontostand.IsEnabled = true;
            XAML_Header_Schaden_G_Slider.IsEnabled = true;
            XAML_Header_Schaden_K_Slider.IsEnabled = true;
            XAML_Header_Schaden_M_Slider.IsEnabled = true;
        }

        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Char.IsSelected)
            {
                if (ViewModel.currentState != TApp.TCharState.EMPTY_CHAR)
                {
                    enableUI();
                    MyFrame.Navigate(typeof(Char), ViewModel);
                }
                Char.IsSelected = false;
            }
            else if (Char_Change.IsSelected)
            {
                Char_Change.IsSelected = false;
                MyFrame.Navigate(typeof(Char_Verwaltung), ViewModel);
            }
            else if (App_Settings.IsSelected)
            {
                App_Settings.IsSelected = false;
                MyFrame.Navigate(typeof(Settings));
            }

            MySplitView.IsPaneOpen = false;
            return;
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Current != null)
            {
                string Controller_Name = ((string)((Button)sender).Name);

                if (Controller_Name.Contains("Karma_Gesamt"))
                {
                    ViewModel.Current.Person.Karma_Gesamt++;
                }
                if (Controller_Name.Contains("Karma_Aktuell"))
                {
                    ViewModel.Current.Person.Karma_Aktuell++;
                }
                if (Controller_Name.Contains("Edge_Gesamt"))
                {
                    ViewModel.Current.Person.Edge_Gesamt++;
                }
                if (Controller_Name.Contains("Edge_Aktuell"))
                {
                    ViewModel.Current.Person.Edge_Aktuell++;
                }
                if (Controller_Name.Contains("Initiative"))
                {
                    ViewModel.Current.Person.Initiative++;
                }
                if (Controller_Name.Contains("Runs"))
                {
                    ViewModel.Current.Person.Runs++;
                }
            }
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Current != null)
            {
                string Controller_Name = ((string)((Button)sender).Name);

                if (Controller_Name.Contains("Karma_Gesamt"))
                {
                    ViewModel.Current.Person.Karma_Gesamt--;
                }
                if (Controller_Name.Contains("Karma_Aktuell"))
                {
                    ViewModel.Current.Person.Karma_Aktuell--;
                }
                if (Controller_Name.Contains("Edge_Gesamt"))
                {
                    ViewModel.Current.Person.Edge_Gesamt--;
                }
                if (Controller_Name.Contains("Edge_Aktuell"))
                {
                    ViewModel.Current.Person.Edge_Aktuell--;
                }
                if (Controller_Name.Contains("Initiative"))
                {
                    ViewModel.Current.Person.Initiative--;
                }
                if (Controller_Name.Contains("Runs"))
                {
                    ViewModel.Current.Person.Runs--;
                }
            }
        }


        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            String Controller_Name = ((String)((Button)sender).Name);

            if (Controller_Name.Contains("Person2"))
            {
                UI.Edit.Edit_Person2 dialog = new UI.Edit.Edit_Person2(ViewModel.Current.Person, ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
        }

    }
}
