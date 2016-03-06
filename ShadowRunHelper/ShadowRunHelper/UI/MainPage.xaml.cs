using ShadowRunHelper.Model;
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
        public CharViewModel ViewModel { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel = (CharViewModel)e.Parameter;
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
            else if (App_Settings.IsSelected) { MyFrame.Navigate(typeof(Settings)); }
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

        private void Runs_Minus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Current.Person.Runs--;
        }
        private void Runs_Plus_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Current.Person.Runs++;
        }
    }
}
