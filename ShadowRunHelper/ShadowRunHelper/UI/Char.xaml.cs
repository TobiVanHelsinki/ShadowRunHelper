using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ShadowRun_Charakter_Helper.Model;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Primitives;
using System;
using AppUIBasics.ControlPages;
using ShadowRun_Charakter_Helper.UI.Edit;

namespace ShadowRun_Charakter_Helper
{
    public sealed partial class Char : Page
    {
        public CharViewModel ViewModel { get; set; }
        public Windows.System.Display.DisplayRequest Char_DisplayRequest;

        public Char()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = (CharViewModel)e.Parameter;
            Char_DisplayRequest = new Windows.System.Display.DisplayRequest();
            Char_DisplayRequest.RequestActive();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Char_DisplayRequest.RequestRelease();
            base.OnNavigatedFrom(e);
        }

        private void Item_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
                if (element != null)
                {
                    FlyoutBase.ShowAttachedFlyout(element);
                }
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            String Controller_Name = ((String)((Button)sender).Name);
            ViewModel.Current.Add(Controller_Name, 0);
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
                Edit_Fertigkeit dialog = new Edit_Fertigkeit(((CharController.Fertigkeit)((Button)sender).DataContext).Data, ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Attribut"))
            {
                Edit_Attribut dialog = new Edit_Attribut(((CharController.Attribut)((Button)sender).DataContext).Data, ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Item"))
            {
                Edit_Item dialog = new Edit_Item(((CharController.Item)((Button)sender).DataContext).Data, ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Programm"))
            {
                Edit_Programm dialog = new Edit_Programm(((CharController.Programm)((Button)sender).DataContext).Data, ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Munition"))
            {
                Edit_Munition dialog = new Edit_Munition(((CharController.Munition)((Button)sender).DataContext).Data, ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Implantat"))
            {
                Edit_Implantat dialog = new Edit_Implantat(((CharController.Implantat)((Button)sender).DataContext).Data, ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Vorteil"))
            {
                Edit_Vorteil dialog = new Edit_Vorteil(((CharController.Vorteil)((Button)sender).DataContext).Data, ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Nachteil"))
            {
                Edit_Nachteil dialog = new Edit_Nachteil(((CharController.Nachteil)((Button)sender).DataContext).Data, ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Connection"))
            {
                Edit_Connection dialog = new Edit_Connection(((CharController.Connection)((Button)sender).DataContext).Data, ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Sin"))
            {
                Edit_Sin dialog = new Edit_Sin(((CharController.Sin)((Button)sender).DataContext).Data, ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Nahkampfwaffe"))
            {
                Edit_Nahkampfwaffe dialog = new Edit_Nahkampfwaffe(((CharModel.Nahkampfwaffe)((Button)sender).DataContext), ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Fernkampfwaffe"))
            {
                Edit_Fernkampfwaffe dialog = new Edit_Fernkampfwaffe(((CharModel.Fernkampfwaffe)((Button)sender).DataContext), ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Kommlink"))
            {
                Edit_Kommlink dialog = new Edit_Kommlink(((CharModel.Kommlink)((Button)sender).DataContext), ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("CyberDeck"))
            {
                Edit_CyberDeck dialog = new Edit_CyberDeck(((CharModel.CyberDeck)((Button)sender).DataContext), ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Vehikel"))
            {
                Edit_Vehikel dialog = new Edit_Vehikel(((CharModel.Vehikel)((Button)sender).DataContext), ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Panzerung"))
            {
                Edit_Panzerung dialog = new Edit_Panzerung(((CharModel.Panzerung)((Button)sender).DataContext), ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Person1"))
            {
                Edit_Person dialog = new Edit_Person(ViewModel.Current.Person, ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
            else if (Controller_Name.Contains("Person2"))
            {
                Edit_Person2 dialog = new Edit_Person2(ViewModel.Current.Person, ViewModel.Current.HD);
                await dialog.ShowAsync();
            }
        }


        private void Del_Click(object sender, RoutedEventArgs e)
        {
            String Controller_Name = ((String)((Button)sender).Name);
            object Controller_Item = ((Button)sender).DataContext;
            ViewModel.Current.Remove(Controller_Name, 0, Controller_Item);
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