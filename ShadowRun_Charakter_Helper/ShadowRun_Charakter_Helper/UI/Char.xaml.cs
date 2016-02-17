using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ShadowRun_Charakter_Helper.Models;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Primitives;
using System;
using AppUIBasics.ControlPages;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRun_Charakter_Helper
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Char : Page
    {
        public CharViewModel ViewModel { get; set; }
        public Char()
        {
            InitializeComponent();

            //Windows.UI.Xaml.Data.Binding bindingOrig = BindingOperations.GetBinding(item, dp);


            //TextBlock Fähigkeiten_Zus_Attribute;
            //Fähigkeiten_Zus_Attribute.SetBinding(Text, new Binding("AnotherIntegerProperty")
            //{
            //    Converter = new UnitConverter()
            //});
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
            Edit_Handlung dialog = new Edit_Handlung(((CharController.Handlung)((Button)sender).DataContext).Data);
            await dialog.ShowAsync();
            dialog.data;
            if (dialog.Result == SignInResult.SignInOK)
            {
                //DialogResult.Text = "Dialog result successful.";
            }
            else if (dialog.Result == SignInResult.SignInCancel)
            {
                //DialogResult.Text = "Dialog result canceled.";
            }
            else if (dialog.Result == SignInResult.Nothing)
            {
                //DialogResult.Text = "Dialog dismissed.";
            }
        }
    }
}