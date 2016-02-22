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
        public CharViewModel ViewModel { get; set; }
        public Char()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = (CharViewModel)e.Parameter;
            this.InitializeComponent();

        }
        private void Add(object sender, RoutedEventArgs e)
        {
            String test = ((String)((Button)sender).Name);
            ViewModel.Current.Add(test);
        }
        private void Item_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                FlyoutBase.ShowAttachedFlyout(element);
            }
        }
        private async void HandlungEditDialog_Click(object sender, RoutedEventArgs e)
        {
            String Controller_Name = ((String)((Button)sender).Name);

            if (Controller_Name.Contains("Handlung"))
            {
                Edit_Handlung dialog = new Edit_Handlung(((CharController.Handlung)((Button)sender).DataContext).Data, ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Fertigkeit"))
            {
        //       Edit_Fertigkeit dialog = new Edit_Fertigkeit(((CharController.Fertigkeit)((Button)sender).DataContext).Data, ViewModel.Current.HD);
        //        await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Attribut"))
            {
            }
            else if (Controller_Name.Contains("Item"))
            {
            }
            else if (Controller_Name.Contains("Munition"))
            {
            }
            else if (Controller_Name.Contains("Implantat"))
            {
                
            }
            else if (Controller_Name.Contains("Vorteil"))
            {

            }
            else if (Controller_Name.Contains("Nachteil"))
            {
          
            }
            else if (Controller_Name.Contains("Connection"))
            {
      
            }
            else if (Controller_Name.Contains("Sin"))
            {
               
            }
            else if (Controller_Name.Contains("Nahkampfwaffe"))
            {
   
            }
            else if (Controller_Name.Contains("Fernkampfwaffe"))
            {
              
            }
            else if (Controller_Name.Contains("Kommlink"))
            {
              
            }
            else if (Controller_Name.Contains("CyberDeck"))
            {
              
            }
            else if (Controller_Name.Contains("Vehikel"))
            {
               
            }
            else if (Controller_Name.Contains("Panzerung"))
            {
               
            }
        }

        private async void HandlungEditZusDialog_Click(object sender, RoutedEventArgs e)
        {
            HD_Wahl dialog = new HD_Wahl(((CharController.Handlung)((Button)sender).DataContext).Data, ViewModel.Current.HD, 1);
            await dialog.ShowAsync();

        }

        private async void HandlungEditGrenzeZusDialog_Click(object sender, RoutedEventArgs e)
        {
            HD_Wahl dialog = new HD_Wahl(((CharController.Handlung)((Button)sender).DataContext).Data, ViewModel.Current.HD, 2);
            await dialog.ShowAsync();
        }
    }
}