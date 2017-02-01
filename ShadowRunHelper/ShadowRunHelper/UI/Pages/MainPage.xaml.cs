using ShadowRunHelper.Model;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace ShadowRunHelper
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CharViewModel ViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            RefreshGui();
            ViewModel = (CharViewModel)e.Parameter;
            ViewModel.PropertyChanged += (x, y) => RefreshGui();

            if (ViewModel.CurrentChar != null)
            {
                MyFrame.Navigate(typeof(Char), ViewModel);
            }
            else
            {
                MyFrame.Navigate(typeof(Char_Verwaltung), ViewModel);
            }
        }

        private void RefreshGui()
        {
            ChangeUI( (ViewModel == null || ViewModel.CurrentChar == null) ? false : true) ;
        }

        void ChangeUI(bool bState) {
            Header_Kontostand.IsEnabled = bState;
            XAML_Header_Schaden_G_Slider.IsEnabled = bState;
            XAML_Header_Schaden_K_Slider.IsEnabled = bState;
            XAML_Header_Schaden_M_Slider.IsEnabled = bState;
            Karma_Aktuell_Plus.IsEnabled = bState;
            Karma_Aktuell_Minus.IsEnabled = bState;
            Karma_Gesamt_Plus.IsEnabled = bState;
            Karma_Gesamt_Minus.IsEnabled = bState;
            Edge_Aktuell_Plus.IsEnabled = bState;
            Edge_Aktuell_Minus.IsEnabled = bState;
            Edge_Gesamt_Plus.IsEnabled = bState;
            Edge_Gesamt_Minus.IsEnabled = bState;
            Runs_Minus.IsEnabled = bState;
            Runs_Plus.IsEnabled = bState;
            Initiative_Plus.IsEnabled = bState;
            Initiative_Minus.IsEnabled = bState;
            Person2_Edit.IsEnabled = bState;
        }

        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Char.IsSelected)
            {
                if (ViewModel.CurrentChar != null)
                {
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
            if (ViewModel.CurrentChar != null)
            {
                string Controller_Name = ((string)((Button)sender).Name);

                if (Controller_Name.Contains("Karma_Gesamt"))
                {
                    ViewModel.CurrentChar.Person.Karma_Gesamt++;
                }
                if (Controller_Name.Contains("Karma_Aktuell"))
                {
                    ViewModel.CurrentChar.Person.Karma_Aktuell++;
                }
                if (Controller_Name.Contains("Edge_Gesamt"))
                {
                    ViewModel.CurrentChar.Person.Edge_Gesamt++;
                }
                if (Controller_Name.Contains("Edge_Aktuell"))
                {
                    ViewModel.CurrentChar.Person.Edge_Aktuell++;
                }
                if (Controller_Name.Contains("Initiative"))
                {
                    ViewModel.CurrentChar.Person.Initiative++;
                }
                if (Controller_Name.Contains("Runs"))
                {
                    ViewModel.CurrentChar.Person.Runs++;
                }
            }
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CurrentChar != null)
            {
                string Controller_Name = ((string)((Button)sender).Name);

                if (Controller_Name.Contains("Karma_Gesamt"))
                {
                    ViewModel.CurrentChar.Person.Karma_Gesamt--;
                }
                if (Controller_Name.Contains("Karma_Aktuell"))
                {
                    ViewModel.CurrentChar.Person.Karma_Aktuell--;
                }
                if (Controller_Name.Contains("Edge_Gesamt"))
                {
                    ViewModel.CurrentChar.Person.Edge_Gesamt--;
                }
                if (Controller_Name.Contains("Edge_Aktuell"))
                {
                    ViewModel.CurrentChar.Person.Edge_Aktuell--;
                }
                if (Controller_Name.Contains("Initiative"))
                {
                    ViewModel.CurrentChar.Person.Initiative--;
                }
                if (Controller_Name.Contains("Runs"))
                {
                    ViewModel.CurrentChar.Person.Runs--;
                }
            }
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            String Controller_Name = ((String)((Button)sender).Name);

            if (Controller_Name.Contains("Person2"))
            {
                UI.Edit.Edit_Person2 dialog = new UI.Edit.Edit_Person2(ViewModel.CurrentChar.Person);
                await dialog.ShowAsync();
            }
        }
    }
}
