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

        private void Zusammensetzung_A_CBB_Value(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ComboBox Zusammensetzung_A_CBB = new ComboBox();
            Zusammensetzung_A_CBB.SelectedIndex = 1;
        }

        private async void HandlungEditDialog_Click(object sender, RoutedEventArgs e)
        {
            Edit_Handlung dialog = new Edit_Handlung(((CharController.Handlung)((Button)sender).DataContext).Data, ViewModel.Current);
            await dialog.ShowAsync();
            
        }
    }
}