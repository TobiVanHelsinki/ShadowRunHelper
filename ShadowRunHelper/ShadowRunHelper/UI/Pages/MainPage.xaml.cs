using ShadowRunHelper.Model;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.Specialized;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Windows.UI;

// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace ShadowRunHelper
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        readonly AppModel ViewModel = AppModel.Instance;

        public MainPage()
        {
            InitializeComponent();
        }
        CharModel.CyberDeck CurrentDeck;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.PropertyChanged += (x, y) => ChangeUI();
            ViewModel.PropertyChanged += (x, y) => GetCurrentDeck();
            ViewModel.NavigationRequested += NavigationRequested; ;
            ViewModel.lstNotifications.CollectionChanged += (x, y) => ShowError(y);
            ChangeUI();
            GetCurrentDeck();
            ViewModel.RequestedNavigation(ViewModel.CurrentChar == null ? ProjectPages.Verwaltung : ProjectPages.Char);
        }

        void GetCurrentDeck()
        {
            //var temp = ViewModel.CurrentChar?.lstThings.Find(x => x.Object.ThingType == ThingDefs.CyberDeck);
            CurrentDeck = (CharModel.CyberDeck)ViewModel.CurrentChar?.lstThings.Find(x => x.Object.ThingType == ThingDefs.CyberDeck).Object;

            if (CurrentDeck != null)
            {
                //CurrentDeck = (CharModel.CyberDeck)temp.Object;
                CurrentDeck.PropertyChanged += (x, y) => CurrentDeck_PropertyChanged();
                CurrentDeck_PropertyChanged();
            }
        }

        private void CurrentDeck_PropertyChanged()
        {
            XAML_Header_Schaden_M_Text.Text = CurrentDeck.dSchaden.ToString();
            XAML_Header_Schaden_M_Slider.Maximum = CurrentDeck.dSchadenMax;
            XAML_Header_Schaden_M_Slider.Value = CurrentDeck.dSchaden;
        }

        ProjectPages LastPage = ProjectPages.Verwaltung;
        private void NavigationRequested(object sender, ProjectPages e)
        {
            switch (e)
            {
                case ProjectPages.Char:
                    if (ViewModel.CurrentChar != null)
                    {
                        Char.IsSelected = true;
                        LastPage = e;
                        MyFrame.Navigate(typeof(Char), ViewModel);
                    }
                    else
                    {
                        ViewModel.RequestedNavigation(LastPage);
                    }
                    break;
                case ProjectPages.Verwaltung:
                    Char_Change.IsSelected = true;
                    LastPage = e;
                    MyFrame.Navigate(typeof(Char_Verwaltung), ViewModel);
                    break;
                case ProjectPages.Settings:
                    App_Settings.IsSelected = true;
                    LastPage = e;
                    MyFrame.Navigate(typeof(Settings));
                    break;
                default:
                    break;
            }
        }

        async void ShowError(NotifyCollectionChangedEventArgs y)
        {
            foreach (Notification item in y.NewItems)
            {
                if (!item.bIsRead)
                {
                    var messageDialog = new MessageDialog(item.strMessage);
                    messageDialog.Commands.Add(new UICommand(
                        "OK"));
                    messageDialog.DefaultCommandIndex = 0;
                    await messageDialog.ShowAsync();
                    item.bIsRead = true;
                }
            }
        }

        void ChangeUI(bool? bState = null) {
            if (bState == null)
            {
                bState = ((ViewModel == null || ViewModel.CurrentChar == null) ? false : true);

            }

            Header_Kontostand.IsEnabled = (bool)bState;
            XAML_Header_Schaden_G_Slider.IsEnabled = (bool)bState;
            XAML_Header_Schaden_K_Slider.IsEnabled = (bool)bState;
            XAML_Header_Schaden_M_Slider.IsEnabled = (bool)bState;
            Karma_Aktuell_Plus.IsEnabled = (bool)bState;
            Karma_Aktuell_Minus.IsEnabled = (bool)bState;
            Karma_Gesamt_Plus.IsEnabled = (bool)bState;
            Karma_Gesamt_Minus.IsEnabled = (bool)bState;
            Edge_Aktuell_Plus.IsEnabled = (bool)bState;
            Edge_Aktuell_Minus.IsEnabled = (bool)bState;
            Edge_Gesamt_Plus.IsEnabled = (bool)bState;
            Edge_Gesamt_Minus.IsEnabled = (bool)bState;
            Runs_Minus.IsEnabled = (bool)bState;
            Runs_Plus.IsEnabled = (bool)bState;
            Initiative_Plus.IsEnabled = (bool)bState;
            Initiative_Minus.IsEnabled = (bool)bState;
            Person2_Edit.IsEnabled = (bool)bState;
        }

        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Char.IsSelected)
            {
                ViewModel?.RequestedNavigation(ProjectPages.Char);
            }
            else if (Char_Change.IsSelected)
            {
                ViewModel?.RequestedNavigation(ProjectPages.Verwaltung);
            }
            else if (App_Settings.IsSelected)
            {
                ViewModel?.RequestedNavigation(ProjectPages.Settings);
            }
            MySplitView.IsPaneOpen = false;
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

        private void XAML_Header_Schaden_M_Slider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (CurrentDeck == null)
            {
                return;
            }
            CurrentDeck.dSchaden = XAML_Header_Schaden_M_Slider.Value;
            if (CurrentDeck.dSchaden <= 3)
            {
                XAML_Header_Schaden_M_Slider.Foreground = new SolidColorBrush(Colors.Green);
            }
            else if (CurrentDeck.dSchaden <= 7)
            {
                XAML_Header_Schaden_M_Slider.Foreground = new SolidColorBrush(Colors.Yellow);
            }
            else if (CurrentDeck.dSchaden <= 10)
            {
                XAML_Header_Schaden_M_Slider.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                XAML_Header_Schaden_M_Slider.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
    }
}
