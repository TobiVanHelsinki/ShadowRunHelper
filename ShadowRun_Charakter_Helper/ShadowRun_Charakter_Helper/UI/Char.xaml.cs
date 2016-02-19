using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ShadowRun_Charakter_Helper.Model;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Primitives;
using System;
using AppUIBasics.ControlPages;
using System.Collections.Generic;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRun_Charakter_Helper
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Char : Page
    {
        protected List<DictionaryCharEntry> ZusTemp;
        public CharViewModel ViewModel { get; set; }
        private int CurrentOpenHandlung = 0;
        public Char()
        {
            InitializeComponent();

            //todo testdaten
            ZusTemp = new List<DictionaryCharEntry>();
            //ZusTemp.Add(new DictionaryCharEntry);
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = (CharViewModel)e.Parameter;
            this.InitializeComponent();

        }

        private void Add(object sender, RoutedEventArgs e)
        {
            String test = ((String)((Button)sender).Name);
            if (test.Contains("Handlung"))
            {
                ViewModel.Current.HandlungController.Add(new CharController.Handlung());
            }
        }

        private void Item_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                FlyoutBase.ShowAttachedFlyout(element);
            }
        }

        private async void Zus_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            HD_Wahl dialog = new HD_Wahl(((KeyValuePair<int, DictionaryCharEntry>)(((Grid)sender).DataContext)), ViewModel.Current.HD);
            await dialog.ShowAsync();
            //delete old
            ViewModel.Current.HandlungController[CurrentOpenHandlung].Data.Zusammensetzung.Remove(dialog.ActiveElement_old.Key);
            //create new
            ViewModel.Current.HandlungController[CurrentOpenHandlung].Data.Zusammensetzung.Add(dialog.ActiveElement_new.Key, dialog.ActiveElement_new.Value);
        }

        private async void HandlungEditDialog_Click(object sender, RoutedEventArgs e)
        {
            Edit_Handlung dialog = new Edit_Handlung(((CharController.Handlung)((Button)sender).DataContext).Data, ViewModel.Current.HD);
            await dialog.ShowAsync();
            
        }

        private void FlyoutHandlung_Opened(object sender, object e)
        {
            CurrentOpenHandlung = ((CharController.Handlung)((StackPanel)((Flyout)sender).Content).DataContext).HD_ID;
        }

        private void FlyoutHandlung_Closed(object sender, object e)
        {
            CurrentOpenHandlung = 0;
        }
    }
}