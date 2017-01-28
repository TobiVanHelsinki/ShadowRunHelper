using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ShadowRunHelper.Model;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Primitives;
using System;
using ShadowRunHelper.UI.Edit;

namespace ShadowRunHelper
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
            Model.ThingDefs Controller = ((Model.ThingDefs)((Button)sender).Tag); //TODO Add Tag with the correospedenting number
            ViewModel.Current.Add(Controller);
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            //String Controller_Name = ((String)((Button)sender).Name);
            Edit_Dialog dialog = new Edit_Dialog(((CharModel.Thing)((Button)sender).DataContext));
                await dialog.ShowAsync();
            //if (Controller_Name.Contains("Handlung"))
            //{
            //    Edit_Handlung dialog = new Edit_Handlung(((CharModel.Handlung)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Fertigkeit"))
            //{
            //    Edit_Fertigkeit dialog = new Edit_Fertigkeit(((CharModel.Fertigkeit)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Attribut"))
            //{
            //    Edit_Dialog dialog = new Edit_Dialog(((CharModel.Attribut)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Item"))
            //{
            //    Edit_Item dialog = new Edit_Item(((CharModel.Item)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Programm"))
            //{
            //    Edit_Programm dialog = new Edit_Programm(((CharModel.Programm)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Munition"))
            //{
            //    Edit_Munition dialog = new Edit_Munition(((CharModel.Munition)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Implantat"))
            //{
            //    Edit_Implantat dialog = new Edit_Implantat(((CharModel.Implantat)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Vorteil"))
            //{
            //    Edit_Vorteil dialog = new Edit_Vorteil(((CharModel.Vorteil)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Nachteil"))
            //{
            //    Edit_Nachteil dialog = new Edit_Nachteil(((CharModel.Nachteil)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Connection"))
            //{
            //    Edit_Connection dialog = new Edit_Connection(((CharModel.Connection)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Sin"))
            //{
            //    Edit_Sin dialog = new Edit_Sin(((CharController.Sin)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Nahkampfwaffe"))
            //{
            //    Edit_Nahkampfwaffe dialog = new Edit_Nahkampfwaffe(((CharModel.Nahkampfwaffe)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Fernkampfwaffe"))
            //{
            //    Edit_Fernkampfwaffe dialog = new Edit_Fernkampfwaffe(((CharModel.Fernkampfwaffe)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Kommlink"))
            //{
            //    Edit_Kommlink dialog = new Edit_Kommlink(((CharModel.Kommlink)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("CyberDeck"))
            //{
            //    Edit_CyberDeck dialog = new Edit_CyberDeck(((CharModel.CyberDeck)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Vehikel"))
            //{
            //    Edit_Vehikel dialog = new Edit_Vehikel(((CharModel.Vehikel)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Panzerung"))
            //{
            //    Edit_Panzerung dialog = new Edit_Panzerung(((CharModel.Panzerung)((Button)sender).DataContext), ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
            //else if (Controller_Name.Contains("Person1"))
            //{
            //    Edit_Person dialog = new Edit_Person(ViewModel.Current.Person, ViewModel.Current.lstAll);
            //    await dialog.ShowAsync();
            //}
        }


        private void Del_Click(object sender, RoutedEventArgs e)
        {
            //String Controller_Name = ((String)((Button)sender).Name);
            CharModel.Thing Controller_Item = (CharModel.Thing)((Button)sender).DataContext;
            ViewModel.Current.Remove(Controller_Item);
        }


        private async void HandlungEditZusDialog_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).DataContext.GetType();
            HD_Wahl dialog = new HD_Wahl(((CharModel.Handlung)((Button)sender).DataContext), ViewModel.Current.lstAll, 1);
            await dialog.ShowAsync();

        }

        private async void HandlungEditGrenzeZusDialog_Click(object sender, RoutedEventArgs e)
        {
            HD_Wahl dialog = new HD_Wahl(((CharModel.Handlung)((Button)sender).DataContext), ViewModel.Current.lstAll, 2);
            await dialog.ShowAsync();
        }

        private void Item_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                FlyoutBase.ShowAttachedFlyout(element);
            }
        }

        private async void HandlungEditGegenZusDialog_Click(object sender, RoutedEventArgs e)
        {
            HD_Wahl dialog = new HD_Wahl(((CharModel.Handlung)((Button)sender).DataContext), ViewModel.Current.lstAll, 3);
            await dialog.ShowAsync();
        }
    }
}