using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ShadowRunHelper.Model;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Primitives;
using System;
using ShadowRunHelper.UI.Edit;
using ShadowRunHelper.Ressourcen;
using static ShadowRunHelper.Ressourcen.TypNamen;

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
            ThingDefs Controller = 0;
            long test = Int64.Parse((((Button)sender).Tag).ToString()); //TODO Add Tag with the correospedenting number
            Controller = (ThingDefs)test;
            try
            {
                ViewModel.Current.Add(Controller);
            }
            catch (WrongTypeException)
            {
            }    
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            string Controller_Name = ((String)((Button)sender).Name);

            if (Controller_Name.Contains("Person1"))
            {
                await new Edit_Person(ViewModel.Current.Person).ShowAsync();
            }
            else
            {
                await new Edit_Dialog(((CharModel.Thing)((Button)sender).DataContext)).ShowAsync();
            }

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