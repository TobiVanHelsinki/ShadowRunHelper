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
        public Char()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = (CharViewModel)e.Parameter;
            this.InitializeComponent();

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
            String test = ((String)((Button)sender).Name);
            ViewModel.Current.Add(test, 0);
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
                //todo converter für zahlen einbinden in werte 
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

            if (Controller_Name.Contains("Handlung"))
            {
                ViewModel.Current.HandlungController.Remove(((CharController.Handlung)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("Fertigkeit"))
            {
                ViewModel.Current.FertigkeitController.Remove(((CharController.Fertigkeit)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("Attribut"))
            {
                ViewModel.Current.AttributController.Remove(((CharController.Attribut)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("Item"))
            {
                ViewModel.Current.ItemController.Remove(((CharController.Item)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("Programm"))
            {
                ViewModel.Current.ProgrammController.Remove(((CharController.Programm)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("Munition"))
            {
                ViewModel.Current.MunitionController.Remove(((CharController.Munition)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("Implantat"))
            {
                ViewModel.Current.ImplantatController.Remove(((CharController.Implantat)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("Vorteil"))
            {
                ViewModel.Current.VorteilController.Remove(((CharController.Vorteil)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("Nachteil"))
            {
                ViewModel.Current.NachteilController.Remove(((CharController.Nachteil)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("Connection"))
            {
                ViewModel.Current.ConnectionController.Remove(((CharController.Connection)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("Sin"))
            {
                ViewModel.Current.SinController.Remove(((CharController.Sin)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("Nahkampfwaffe"))
            {
                ViewModel.Current.NahkampfwaffeController.remove(((CharModel.Nahkampfwaffe)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("Fernkampfwaffe"))
            {
                ViewModel.Current.FernkampfwaffeController.remove(((CharModel.Fernkampfwaffe)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("Kommlink"))
            {
                ViewModel.Current.KommlinkController.remove(((CharModel.Kommlink)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("CyberDeck"))
            {
                ViewModel.Current.CyberDeckController.remove(((CharModel.CyberDeck)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("Vehikel"))
            {
                ViewModel.Current.VehikelController.remove(((CharModel.Vehikel)((Button)sender).DataContext));
            }
            else if (Controller_Name.Contains("Panzerung"))
            {
                ViewModel.Current.PanzerungController.remove(((CharModel.Panzerung)((Button)sender).DataContext));
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