using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ShadowRunHelper.Model;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls.Primitives;
using System;
using ShadowRunHelper.UI.Edit;
using ShadowRunHelper.CharModel;
using Windows.Foundation.Metadata;
using Windows.ApplicationModel.Resources;
using System.Collections.Generic;

namespace ShadowRunHelper
{
    public sealed partial class Char : Page
    {
        // Variables ##########################################################
        public ViewModel ViewModel { get; set; }
        public Windows.System.Display.DisplayRequest Char_DisplayRequest;

        public Char()
        {
            InitializeComponent();
        }
        // Navigation Stuff####################################################
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = (ViewModel)e.Parameter;
            if (Optionen.bDisplayRequest)
            {
                try
                {
                    Char_DisplayRequest = new Windows.System.Display.DisplayRequest();
                    Char_DisplayRequest.RequestActive();
                }
                catch (Exception)
                {
                    var res = ResourceLoader.GetForCurrentView();
                    ViewModel.Instance.lstNotifications.Add(new Notification(
                        res.GetString("Notification_Error_DisplayRequest/Text")
                        ));
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (Optionen.bDisplayRequest)
            {
                try
                {
                    //Char_DisplayRequest = new Windows.System.Display.DisplayRequest();
                    //Char_DisplayRequest.RequestRelease();
                }
                catch (Exception)
                {
                    //var res = ResourceLoader.GetForCurrentView();
                    //ViewModel.Instance.lstNotifications.Add(new Notification(
                    //    res.GetString("Notification_Error_DisplayRequest/Text")
                    //    ));
                }
            }
            base.OnNavigatedFrom(e);
        }

        // Gui-Model Handler Stuff#############################################
        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            ThingDefs Controller = 0;
            long test = Int64.Parse((((Button)sender).Tag).ToString()); //TODO Add Tag with the correospedenting number
            Controller = (ThingDefs)test;
            Thing newThing = null;
            try
            {
                newThing = ViewModel.CurrentChar.Add(Controller);
                if (Optionen.bStartEditAfterAdd)
                {
                    await new Edit_Dialog(newThing).ShowAsync();
                }

            }
            catch (WrongTypeException)
            {
            }
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (((String)((Button)sender).Name).Contains("Person1"))
            {
                await new Edit_Person(ViewModel.CurrentChar.Person).ShowAsync();
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

        private async void Attribut_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            string Tag = ((String)((Grid)sender).Tag);
            if (Tag != null)
            {
                Thing Attribute = null;
                switch (Tag)
                {
                    case "Konsti":
                        Attribute = ViewModel.CurrentChar.CTRLAttribut.Konsti;
                        break;
                    case "Reaktion":
                        Attribute = ViewModel.CurrentChar.CTRLAttribut.Reaktion;
                        break;
                    case "Intuition":
                        Attribute = ViewModel.CurrentChar.CTRLAttribut.Intuition;
                        break;
                    case "Staerke":
                        Attribute = ViewModel.CurrentChar.CTRLAttribut.Staerke;
                        break;
                    case "Willen":
                        Attribute = ViewModel.CurrentChar.CTRLAttribut.Willen;
                        break;
                    case "Logik":
                        Attribute = ViewModel.CurrentChar.CTRLAttribut.Logik;
                        break;
                    case "Geschick":
                        Attribute = ViewModel.CurrentChar.CTRLAttribut.Geschick;
                        break;
                    case "Charisma":
                        Attribute = ViewModel.CurrentChar.CTRLAttribut.Charisma;
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
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if ((Thing)((Button)sender).DataContext != null)
            {
                ViewModel.CurrentChar.Remove((Thing)((Button)sender).DataContext);
            }
        }
        
        private async void HandlungEditZusDialog_Click(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(((Handlung)((Button)sender).DataContext).WertZusammensetzung, ViewModel.CurrentChar.lstThings);
            await dialog.ShowAsync();

        }

        private async void HandlungEditGrenzeZusDialog_Click(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(((Handlung)((Button)sender).DataContext).GrenzeZusammensetzung, ViewModel.CurrentChar.lstThings);
            var ergebnis = await dialog.ShowAsync();
        }

        private async void HandlungEditGegenZusDialog_Click(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(((Handlung)((Button)sender).DataContext).GegenZusammensetzung, ViewModel.CurrentChar.lstThings);
            await dialog.ShowAsync();
        }

        private async void FertigkeitenZusammensetzungBearbeiten(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(((Fertigkeit)((Button)sender).DataContext).PoolZusammensetzung, ViewModel.CurrentChar.lstThings);
            await dialog.ShowAsync();
        }

        // Gui Handler Stuff ##################################################

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTemplate NewTemplate = null;
            DataTemplate NewTemplateX = null;
            
            switch (int.Parse(((sender as ListView).Tag as string)))
            {
                case (int)ThingDefs.Handlung:
                    NewTemplate = HandlungItem;
                    NewTemplateX = HandlungItemX;
                    break;
                case (int)ThingDefs.Fertigkeit:
                    NewTemplate = FertigkeitItem;
                    NewTemplateX = FertigkeitItemX;
                    break;
                case (int)ThingDefs.Item:
                    NewTemplate = ItemItem;
                    NewTemplateX = ItemItemX;
                    break;
                case (int)ThingDefs.Programm:
                    NewTemplate = ProgrammItem;
                    NewTemplateX = ProgrammItemX;
                    break;
                case (int)ThingDefs.Munition:
                    NewTemplate = MunitionItem;
                    NewTemplateX = MunitionItemX;
                    break;
                case (int)ThingDefs.Implantat:
                    NewTemplate = ImplantatItem;
                    NewTemplateX = ImplantatItemX;
                    break;
                case (int)ThingDefs.Vorteil:
                    NewTemplate = EigenschaftItem;
                    NewTemplateX = EigenschaftItemX;
                    break;
                case (int)ThingDefs.Nachteil:
                    NewTemplate = EigenschaftItem;
                    NewTemplateX = EigenschaftItemX;
                    break;
                case (int)ThingDefs.Connection:
                    NewTemplate = ConnectionItem;
                    NewTemplateX = ConnectionItemX;
                    break;
                case (int)ThingDefs.Sin:
                    NewTemplate = SinItem;
                    NewTemplateX = SinItemX;
                    break;
                case (int)ThingDefs.Nahkampfwaffe:
                    NewTemplate = NahkampfwaffeItem;
                    NewTemplateX = NahkampfwaffeItemX;
                    break;
                case (int)ThingDefs.Fernkampfwaffe:
                    NewTemplate = FernkampfwaffeItem;
                    NewTemplateX = FernkampfwaffeItemX;
                    break;
                case (int)ThingDefs.Kommlink:
                    NewTemplate = KommlinkItem;
                    NewTemplateX = KommlinkItemX;
                    break;
                case (int)ThingDefs.CyberDeck:
                    NewTemplate = CyberDeckItem;
                    NewTemplateX = CyberDeckItemX;
                    break;
                case (int)ThingDefs.Vehikel:
                    NewTemplate = VehikelItem;
                    NewTemplateX = VehikelItemX;
                    break;
                case (int)ThingDefs.Panzerung:
                    NewTemplate = PanzerungItem;
                    NewTemplateX = PanzerungItemX;
                    break;
                default:
                    return;
            }
            if (NewTemplate == null || NewTemplateX == null)
            {
                return;
            }
            foreach (var item in e.RemovedItems)
            {
                try
                {
                    ((ListViewItem)(sender as ListView).ContainerFromItem(item)).ContentTemplate = NewTemplate;
                }
                catch (Exception)
                {
                }
            }
            foreach (var item in e.AddedItems)
            {
                try
                {
                    ((ListViewItem)(sender as ListView).ContainerFromItem(item)).ContentTemplate = NewTemplateX;
                }
                catch (Exception)
                {
                }
            }
        }

    }
}