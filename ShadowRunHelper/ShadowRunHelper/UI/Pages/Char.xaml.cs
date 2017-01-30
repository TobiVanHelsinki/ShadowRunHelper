using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ShadowRunHelper.Model;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Primitives;
using System;
using ShadowRunHelper.UI.Edit;
using ShadowRunHelper.CharModel;

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
            string Name = ((String)((Button)sender).Name);
            string Tag = ((String)((Button)sender).Tag);

            if (Name.Contains("Person1"))
            {
                await new Edit_Person(ViewModel.Current.Person).ShowAsync();
            }
            else if (Tag != "")
            {
                Thing Attribute = null;
                switch (Tag)
                {
                    case "Konsti":
                        Attribute = ViewModel.Current.CTRLAttribut.Konsti;
                        break;
                    case "Reaktion":
                        Attribute = ViewModel.Current.CTRLAttribut.Reaktion;
                        break;
                    case "Intuition":
                        Attribute = ViewModel.Current.CTRLAttribut.Intuition;
                        break;
                    case "Staerke":
                        Attribute = ViewModel.Current.CTRLAttribut.Staerke;
                        break;
                    case "Willen":
                        Attribute = ViewModel.Current.CTRLAttribut.Willen;
                        break;
                    case "Logik":
                        Attribute = ViewModel.Current.CTRLAttribut.Logik;
                        break;
                    case "Geschick":
                        Attribute = ViewModel.Current.CTRLAttribut.Geschick;
                        break;
                    case "Charisma":
                        Attribute = ViewModel.Current.CTRLAttribut.Charisma;
                        break;
                    default:
                        break;
                }
                try
                {
                    if (Attribute != null)
                    {
                        await new Edit_Dialog(Attribute).ShowAsync();
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                try
                {
                    await new Edit_Dialog(((Thing)((Button)sender).DataContext)).ShowAsync();

                }
                catch (Exception)
                {
                }
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if ((Thing)((Button)sender).DataContext != null)
            {
                ViewModel.Current.Remove((Thing)((Button)sender).DataContext);
            }
        }


        private async void HandlungEditZusDialog_Click(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(((Handlung)((Button)sender).DataContext), ViewModel.Current.lstThings, CharModel.Handlung.Mode.Wert);
            await dialog.ShowAsync();

        }

        private async void HandlungEditGrenzeZusDialog_Click(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(((Handlung)((Button)sender).DataContext), ViewModel.Current.lstThings, CharModel.Handlung.Mode.Grenze);
            await dialog.ShowAsync();
        }

        private async void HandlungEditGegenZusDialog_Click(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(((Handlung)((Button)sender).DataContext), ViewModel.Current.lstThings, CharModel.Handlung.Mode.Gegen);
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

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}