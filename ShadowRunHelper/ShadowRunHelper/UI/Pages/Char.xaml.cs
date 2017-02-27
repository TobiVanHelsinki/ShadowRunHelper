using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelper.UI.Edit;
using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper
{
    public sealed partial class Char : Page
    {
        // Variables ##########################################################
        //public ViewModel ViewModel { get; set; }
        readonly ViewModel ViewModel = ViewModel.Instance;
        public Windows.System.Display.DisplayRequest Char_DisplayRequest;

        public Char()
        {
            InitializeComponent();
        }
        // Navigation Stuff####################################################
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //ViewModel = (ViewModel)e.Parameter;
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
            long test = Int64.Parse((((Button)sender).Tag).ToString());
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

        private async void Edit_Attribut(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (
                ((Grid)sender).DataContext.Equals(ViewModel.CurrentChar.CTRLAttribut.Essenz) ||
                ((Grid)sender).DataContext.Equals(ViewModel.CurrentChar.CTRLAttribut.Limit_K) ||
                ((Grid)sender).DataContext.Equals(ViewModel.CurrentChar.CTRLAttribut.Limit_G) ||
                ((Grid)sender).DataContext.Equals(ViewModel.CurrentChar.CTRLAttribut.Limit_S)
            )
            {
                return;
            }
            try
            {
                await new Edit_Dialog(((Thing)((Grid)sender).DataContext)).ShowAsync();
            }
            catch (Exception)
            {
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
                case (int)ThingDefs.Adeptenkraft_KomplexeForm:
                    NewTemplate = Adeptenkraft_KomplexeFormItem;
                    NewTemplateX = Adeptenkraft_KomplexeFormItemX;
                    break;
                case (int)ThingDefs.Geist_Sprite:
                    NewTemplate = Geist_SpriteItem;
                    NewTemplateX = Geist_SpriteItemX;
                    break;
                case (int)ThingDefs.Foki_Widgets:
                    NewTemplate = Foki_WidgetsItem;
                    NewTemplateX = Foki_WidgetsItemX;
                    break;
                case (int)ThingDefs.Strömung_Wandlung:
                    NewTemplate = Strömung_WandlungItem;
                    NewTemplateX = Strömung_WandlungItemX;
                    break;
                case (int)ThingDefs.Tradition_Initiation:
                    NewTemplate = Tradition_InitiationItem;
                    NewTemplateX = Tradition_InitiationItemX;
                    break;
                case (int)ThingDefs.Zaubersprüche:
                    NewTemplate = ZaubersprücheItem;
                    NewTemplateX = ZaubersprücheItemX;
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
        
        private void Block_Loaded(object sender, RoutedEventArgs e)
        {
            //switch (int.Parse(((sender as ListView).Tag as string)))
            //{
            //    case ThingDefs.UndefTemp:
            //        break;
            //    case ThingDefs.Undef:
            //        break;
            //    case ThingDefs.Handlung:
            //        break;
            //    case ThingDefs.Fertigkeit:
            //        break;
            //    case ThingDefs.Item:
            //        break;
            //    case ThingDefs.Programm:
            //        break;
            //    case ThingDefs.Munition:
            //        break;
            //    case ThingDefs.Implantat:
            //        break;
            //    case ThingDefs.Vorteil:
            //        break;
            //    case ThingDefs.Nachteil:
            //        break;
            //    case ThingDefs.Connection:
            //        break;
            //    case ThingDefs.Sin:
            //        break;
            //    case ThingDefs.Attribut:
            //        break;
            //    case ThingDefs.Nahkampfwaffe:
            //        break;
            //    case ThingDefs.Fernkampfwaffe:
            //        break;
            //    case ThingDefs.Kommlink:
            //        break;
            //    case ThingDefs.CyberDeck:
            //        break;
            //    case ThingDefs.Vehikel:
            //        break;
            //    case ThingDefs.Panzerung:
            //        break;
            //    case ThingDefs.Eigenschaft:
            //        break;
            //    case ThingDefs.Adeptenkraft_KomplexeForm:
            //        break;
            //    case ThingDefs.Geist_Sprite:
            //        break;
            //    case ThingDefs.Foki_Widgets:
            //        break;
            //    case ThingDefs.Strömung_Wandlung:
            //        break;
            //    case ThingDefs.Tradition_Initiation:
            //        break;
            //    case ThingDefs.Zaubersprüche:
            //        break;
            //    default:
            //        break;
            //}

        }

        private void ContentControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Temp Vars
            TextBlock U = ((((sender as ContentControl).ContentTemplateRoot as StackPanel).Children[0] as StackPanel).Children[1] as TextBlock);
            ContentPresenter E = (((sender as ContentControl).ContentTemplateRoot as StackPanel).Children[1] as ContentPresenter);
            ListView LV = (((sender as ContentControl).ContentTemplateRoot as StackPanel).Children[2] as ListView);
            // Global Things
            ((((sender as ContentControl).ContentTemplateRoot as StackPanel).Children[0] as StackPanel).Children[0] as Button).Tag = (sender as ContentControl).Tag;
            LV.Tag = (sender as ContentControl).Tag;
            //Local things


            switch (int.Parse(((sender as ContentControl).Tag as string)))
            {
                case (int)ThingDefs.Handlung:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_HandlungM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLHandlung.Data;
                    E.ContentTemplate = this.Handlung_E;
                    LV.ItemTemplate = HandlungItem;
                    break;
                case (int)ThingDefs.Fertigkeit:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_FertigkeitM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLFertigkeit.Data;
                    E.ContentTemplate = this.Fertigkeit_E;
                    LV.ItemTemplate = FertigkeitItem;
                    break;
                case (int)ThingDefs.Item:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ItemM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLItem.Data;
                    E.ContentTemplate = this.Item_E;
                    LV.ItemTemplate = ItemItem;
                    break;
                case (int)ThingDefs.Programm:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ProgrammM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLProgramm.Data;
                    E.ContentTemplate = this.Programm_E;
                    LV.ItemTemplate = ProgrammItem;
                    break;
                case (int)ThingDefs.Munition:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_MunitionM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLMunition.Data;
                    E.ContentTemplate = this.Munition_E;
                    LV.ItemTemplate = MunitionItem;
                    break;
                case (int)ThingDefs.Implantat:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ImplantatM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLImplantat.Data;
                    E.ContentTemplate = this.Implantat_E;
                    LV.ItemTemplate = ImplantatItem;
                    break;
                case (int)ThingDefs.Vorteil:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_VorteilM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLVorteil.Data;
                    E.ContentTemplate = this.Eigenschaft_E;
                    LV.ItemTemplate = EigenschaftItem;
                    break;
                case (int)ThingDefs.Nachteil:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_NachteilM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLNachteil.Data;
                    E.ContentTemplate = this.Eigenschaft_E;
                    LV.ItemTemplate = EigenschaftItem;
                    break;
                case (int)ThingDefs.Connection:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ConnectionM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLConnection.Data;
                    E.ContentTemplate = this.Connection_E;
                    LV.ItemTemplate = ConnectionItem;
                    break;
                case (int)ThingDefs.Sin:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_SinM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLSin.Data;
                    E.ContentTemplate = this.Sin_E;
                    LV.ItemTemplate = SinItem;
                    break;
                case (int)ThingDefs.Nahkampfwaffe:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_NahkampfwaffeM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLNahkampfwaffe.Data;
                    E.ContentTemplate = this.Nahkampfwaffe_E;
                    LV.ItemTemplate = NahkampfwaffeItem;
                    break;
                case (int)ThingDefs.Fernkampfwaffe:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_FernkampfwaffeM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLFernkampfwaffe.Data;
                    E.ContentTemplate = this.Fernkampfwaffe_E;
                    LV.ItemTemplate = FernkampfwaffeItem;
                    break;
                case (int)ThingDefs.Kommlink:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_KommlinkM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLKommlink.Data;
                    E.ContentTemplate = this.Kommlink_E;
                    LV.ItemTemplate = KommlinkItem;
                    break;
                case (int)ThingDefs.CyberDeck:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_CyberDeckM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLCyberDeck.Data;
                    E.ContentTemplate = this.CyberDeck_E;
                    LV.ItemTemplate = CyberDeckItem;
                    break;
                case (int)ThingDefs.Vehikel:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_VehikelM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLVehikel.Data;
                    E.ContentTemplate = this.Vehikel_E;
                    LV.ItemTemplate = VehikelItem;
                    break;
                case (int)ThingDefs.Panzerung:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_PanzerungM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLPanzerung.Data;
                    E.ContentTemplate = this.Panzerung_E;
                    LV.ItemTemplate = PanzerungItem;
                    break;
                case (int)ThingDefs.Adeptenkraft_KomplexeForm:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Adeptenkraft_KomplexeFormM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLAdeptenkraft_KomplexeForm.Data;
                    E.ContentTemplate = this.Adeptenkraft_KomplexeForm_E;
                    LV.ItemTemplate = Adeptenkraft_KomplexeFormItem;
                    break;
                case (int)ThingDefs.Geist_Sprite:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Geist_SpriteM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLGeist_Sprite.Data;
                    E.ContentTemplate = this.Geist_Sprite_E;
                    LV.ItemTemplate = Geist_SpriteItem;
                    break;
                case (int)ThingDefs.Foki_Widgets:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Foki_WidgetsM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLFoki_Widgets.Data;
                    E.ContentTemplate = this.Foki_Widgets_E;
                    LV.ItemTemplate = Foki_WidgetsItem;
                    break;
                case (int)ThingDefs.Strömung_Wandlung:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Strömung_WandlungM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLStrömung_Wandlung.Data;
                    LV.ItemTemplate = Strömung_WandlungItem;
                    break;
                case (int)ThingDefs.Tradition_Initiation:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Tradition_InitiationM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLTradition_Initiation.Data;
                    LV.ItemTemplate = Tradition_InitiationItem;
                    break;
                case (int)ThingDefs.Zaubersprüche:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ZaubersprücheM_/Text");
                    LV.ItemsSource = ViewModel.CurrentChar.CTRLZaubersprüche.Data;
                    E.ContentTemplate = this.Zaubersprüche_E;
                    LV.ItemTemplate = ZaubersprücheItem;
                    break;
                default:
                    return;
            }
        }
    }
}