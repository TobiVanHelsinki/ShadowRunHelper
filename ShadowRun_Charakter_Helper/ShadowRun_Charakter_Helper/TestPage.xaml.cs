using ShadowRun_Charakter_Helper.Models;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace ShadowRun_Charakter_Helper
{
    public sealed partial class TestPage : Page
    {
        public CharViewModel ViewModel { get; set; }
        public TestPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = (CharViewModel)e.Parameter;
            CB1.SelectedValue = 2;
            //   this.DataContext = ViewModel.DefaultChar;
        }

        private void Handlungen_Del(object sender, RoutedEventArgs e)
        {

          //  ViewModel.Current.removeHandlung((Handlung)((Button)sender).DataContext);
        }
        private void Handlungen_Add(object sender, RoutedEventArgs e)
        {
            // ViewModel.Current.addHandlung();
            ViewModel.Current.PanzerungsController.add();
        }
        //private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    TextBlock txtBlock = ((Windows.UI.Xaml.RoutedEventArgs)(e)).OriginalSource as TextBlock;
        //    if (txtBlock != null)
        //    {
        //        txtSubMenuTapped.Text = txtSubMenuTapped.Tag.ToString() + txtBlock.Text;
        //    }
        //    e.Handled = true;
        //}
    }
}
