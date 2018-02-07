using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelper.UI;
using ShadowRunHelper.UI.Edit;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Resources;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper
{
    public sealed partial class CharPage : Page
    {
        void Click_Save(object sender, RoutedEventArgs e)
        {
            ViewModel?.MainObject?.SetSaveTimerTo();
        }
        // Variables ##########################################################
        readonly AppModel ViewModel = AppModel.Instance;
        public Windows.System.Display.DisplayRequest Char_DisplayRequest;
        CharHolder MainObject;
        ResourceLoader res;
        public CharPage()
        {
            if (MainObject == null)
            {
                MainObject = ViewModel.MainObject;
            }
            res = ResourceLoader.GetForCurrentView();
            InitializeComponent();
            PrepareBlockList();
        }
        // Navigation Stuff####################################################
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (SettingsModel.I.DISPLAY_REQUEST)
            {
                try
                {
                    Char_DisplayRequest = new Windows.System.Display.DisplayRequest();
                    Char_DisplayRequest.RequestActive();
                }
                catch (Exception)
                {
                }
            }
            ViewModel.TutorialStateChanged += TutorialStateChanged;
            if (!SettingsModel.I.TutorialCharShown)
            {
#pragma warning disable CS4014
                new Tutorial(20, 23).ShowAsync();
#pragma warning restore CS4014
                SettingsModel.I.TutorialCharShown = true;
            }
        }

        private void TutorialStateChanged(int StateNumber, bool Highlight)
        {
            Style StyleToBeApplied = Highlight ? Tutorial.HighlightBorderStyle_XAML : Tutorial.UnhighlightBorderStyle_XAML;
            switch (StateNumber)
            {
                case 22:
                    PivotHeader1Border.Style = StyleToBeApplied;
                    PivotHeader2Border.Style = StyleToBeApplied;
                    PivotHeader3Border.Style = StyleToBeApplied;
                    PivotHeader4Border.Style = StyleToBeApplied;
                    PivotHeader5Border.Style = StyleToBeApplied;
                    break;
                case 23:
                    MainContentBorder.Style = StyleToBeApplied;
                    break;
                default:
                    //PivotHeader1Border.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    //PivotHeader2Border.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    //PivotHeader3Border.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    //PivotHeader4Border.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    //PivotHeader5Border.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    //MainContentBorder.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    break;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (SettingsModel.I.DISPLAY_REQUEST)
            {
                try
                {
                    Char_DisplayRequest.RequestRelease();
                }
                catch (Exception)
                {
                }
            }
            base.OnNavigatedFrom(e);
        }

        // Gui-Model Handler Stuff#############################################
        async void Add_Click(object sender, RoutedEventArgs e)
        {
            ThingDefs Controller = 0;
            long test = Int64.Parse((((Button)sender).Tag).ToString());
            Controller = (ThingDefs)test;
            Thing newThing = null;
            try
            {
                newThing = ViewModel.MainObject.Add(Controller);
                if (SettingsModel.I.StartEditAfterAdd)
                {
                    await new Edit_Dialog(newThing).ShowAsync();
                }

            }
            catch (Exception ex)
            {
                ViewModel.NewNotification("", ex);
            }
        }


        async void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (((String)((Button)sender).Name).Contains("Person1"))
            {
                await new Edit_Person(ViewModel.MainObject.Person).ShowAsync();
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

        async void Edit_Attribut(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            try
            {
                await new Edit_Dialog(((Thing)((Grid)sender).DataContext)).ShowAsync();
            }
            catch (Exception)
            {
            }
        }

        void Del_Click(object sender, RoutedEventArgs e)
        {
            if ((Thing)((Button)sender).DataContext != null)
            {
                ViewModel.MainObject.Remove((Thing)((Button)sender).DataContext);
            }
        }

        async void HandlungEditZusDialog_Click(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(((Handlung)((Button)sender).DataContext).WertZusammensetzung, ViewModel.MainObject.LinkList);
            await dialog.ShowAsync();

        }

        async void HandlungEditGrenzeZusDialog_Click(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(((Handlung)((Button)sender).DataContext).GrenzeZusammensetzung, ViewModel.MainObject.LinkList);
            var ergebnis = await dialog.ShowAsync();
        }

        async void HandlungEditGegenZusDialog_Click(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(((Handlung)((Button)sender).DataContext).GegenZusammensetzung, ViewModel.MainObject.LinkList);
            await dialog.ShowAsync();
        }

        async void FertigkeitenZusammensetzungBearbeiten(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(((Fertigkeit)((Button)sender).DataContext).PoolZusammensetzung, ViewModel.MainObject.LinkList);
            await dialog.ShowAsync();
        }

        // Gui Handler Stuff ##################################################

        void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //(sender as ListView).ScrollIntoView(, ScrollIntoViewAlignment.Leading);
            DataTemplate NewTemplate = null;
            DataTemplate NewTemplateX = null;
            switch (int.Parse(((sender as ListView).Tag as string)))
            {
                case (int)ThingDefs.Handlung:
                    NewTemplate = HandlungItem;
                    NewTemplateX = HandlungItemX;
                    if (!SettingsModel.I.TutorialHandlungShown)
                    {
#pragma warning disable CS4014
                        new Tutorial(30, 31).ShowAsync();
#pragma warning restore CS4014
                        SettingsModel.I.TutorialHandlungShown = true;
                    }
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
                case (int)ThingDefs.Stroemung_Wandlung:
                    NewTemplate = Stroemung_WandlungItem;
                    NewTemplateX = Stroemung_WandlungItemX;
                    break;
                case (int)ThingDefs.Tradition_Initiation:
                    NewTemplate = Tradition_InitiationItem;
                    NewTemplateX = Tradition_InitiationItemX;
                    break;
                case (int)ThingDefs.Zaubersprueche:
                    NewTemplate = ZauberspruecheItem;
                    NewTemplateX = ZauberspruecheItemX;
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

        void ContentControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Temp Vars
            TextBlock U = ((((sender as ContentControl).ContentTemplateRoot as StackPanel).Children[0] as StackPanel).Children[1] as TextBlock);
            ContentPresenter E = (((sender as ContentControl).ContentTemplateRoot as StackPanel).Children[1] as ContentPresenter);
            ListView LV = (((sender as ContentControl).ContentTemplateRoot as StackPanel).Children[2] as ListView);
            // Global Things
            ((((sender as ContentControl).ContentTemplateRoot as StackPanel).Children[0] as StackPanel).Children[0] as Button).Tag = (sender as ContentControl).Tag;
            LV.Tag = (sender as ContentControl).Tag;

            //Search Things
            //try
            {
                Blocklist.TryGetValue((ThingDefs)int.Parse((sender as ContentControl).Tag as string), out (int PivotItem, ContentControl Block, ListView ListView, ScrollViewer sv) NewBlock);
                NewBlock.ListView = LV;
                NewBlock.Block = (ContentControl)sender;
                NewBlock.sv = (ScrollViewer)((sender as ContentControl).Parent as FrameworkElement).Parent;
                Blocklist.Remove((ThingDefs)int.Parse((sender as ContentControl).Tag as string));
                Blocklist.Add((ThingDefs)int.Parse((sender as ContentControl).Tag as string), NewBlock);
            }
            //catch (Exception ex)
            {
            }
            
            //Local things
            switch (int.Parse(((sender as ContentControl).Tag as string)))
            {
                case (int)ThingDefs.Handlung:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_HandlungM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLHandlung.Data;
                    E.ContentTemplate = this.Handlung_E;
                    LV.ItemTemplate = HandlungItem;
                    //NewBlock.PivotItem = 0;
                    break;
                case (int)ThingDefs.Fertigkeit:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_FertigkeitM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLFertigkeit.Data;
                    E.ContentTemplate = this.Fertigkeit_E;
                    LV.ItemTemplate = FertigkeitItem;
                    break;
                case (int)ThingDefs.Item:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ItemM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLItem.Data;
                    E.ContentTemplate = this.Item_E;
                    LV.ItemTemplate = ItemItem;
                    break;
                case (int)ThingDefs.Programm:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ProgrammM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLProgramm.Data;
                    E.ContentTemplate = this.Programm_E;
                    LV.ItemTemplate = ProgrammItem;
                    break;
                case (int)ThingDefs.Munition:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_MunitionM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLMunition.Data;
                    E.ContentTemplate = this.Munition_E;
                    LV.ItemTemplate = MunitionItem;
                    break;
                case (int)ThingDefs.Implantat:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ImplantatM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLImplantat.Data;
                    E.ContentTemplate = this.Implantat_E;
                    LV.ItemTemplate = ImplantatItem;
                    break;
                case (int)ThingDefs.Vorteil:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_VorteilM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLVorteil.Data;
                    E.ContentTemplate = this.Eigenschaft_E;
                    LV.ItemTemplate = EigenschaftItem;
                    break;
                case (int)ThingDefs.Nachteil:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_NachteilM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLNachteil.Data;
                    E.ContentTemplate = this.Eigenschaft_E;
                    LV.ItemTemplate = EigenschaftItem;
                    break;
                case (int)ThingDefs.Connection:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ConnectionM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLConnection.Data;
                    E.ContentTemplate = this.Connection_E;
                    LV.ItemTemplate = ConnectionItem;
                    //NewBlock.PivotItem = 3;
                    break;
                case (int)ThingDefs.Sin:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_SinM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLSin.Data;
                    E.ContentTemplate = this.Sin_E;
                    LV.ItemTemplate = SinItem;
                    break;
                case (int)ThingDefs.Nahkampfwaffe:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_NahkampfwaffeM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLNahkampfwaffe.Data;
                    E.ContentTemplate = this.Nahkampfwaffe_E;
                    LV.ItemTemplate = NahkampfwaffeItem;
                    break;
                case (int)ThingDefs.Fernkampfwaffe:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_FernkampfwaffeM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLFernkampfwaffe.Data;
                    E.ContentTemplate = this.Fernkampfwaffe_E;
                    LV.ItemTemplate = FernkampfwaffeItem;
                    break;
                case (int)ThingDefs.Kommlink:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_KommlinkM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLKommlink.Data;
                    E.ContentTemplate = this.Kommlink_E;
                    LV.ItemTemplate = KommlinkItem;
                    break;
                case (int)ThingDefs.CyberDeck:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_CyberDeckM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLCyberDeck.Data;
                    E.ContentTemplate = this.CyberDeck_E;
                    LV.ItemTemplate = CyberDeckItem;
                    break;
                case (int)ThingDefs.Vehikel:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_VehikelM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLVehikel.Data;
                    E.ContentTemplate = this.Vehikel_E;
                    LV.ItemTemplate = VehikelItem;
                    break;
                case (int)ThingDefs.Panzerung:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_PanzerungM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLPanzerung.Data;
                    E.ContentTemplate = this.Panzerung_E;
                    LV.ItemTemplate = PanzerungItem;
                    break;
                case (int)ThingDefs.Adeptenkraft_KomplexeForm:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Adeptenkraft_KomplexeFormM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLAdeptenkraft_KomplexeForm.Data;
                    E.ContentTemplate = this.Adeptenkraft_KomplexeForm_E;
                    LV.ItemTemplate = Adeptenkraft_KomplexeFormItem;
                    break;
                case (int)ThingDefs.Geist_Sprite:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Geist_SpriteM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLGeist_Sprite.Data;
                    E.ContentTemplate = this.Geist_Sprite_E;
                    LV.ItemTemplate = Geist_SpriteItem;
                    break;
                case (int)ThingDefs.Foki_Widgets:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Foki_WidgetsM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLFoki_Widgets.Data;
                    E.ContentTemplate = this.Foki_Widgets_E;
                    LV.ItemTemplate = Foki_WidgetsItem;
                    break;
                case (int)ThingDefs.Stroemung_Wandlung:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Stroemung_WandlungM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLStroemung_Wandlung.Data;
                    LV.ItemTemplate = Stroemung_WandlungItem;
                    break;
                case (int)ThingDefs.Tradition_Initiation:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Tradition_InitiationM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLTradition_Initiation.Data;
                    LV.ItemTemplate = Tradition_InitiationItem;
                    break;
                case (int)ThingDefs.Zaubersprueche:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ZauberspruecheM_/Text");
                    LV.ItemsSource = ViewModel.MainObject.CTRLZaubersprueche.Data;
                    E.ContentTemplate = this.Zaubersprueche_E;
                    LV.ItemTemplate = ZauberspruecheItem;
                    break;
                default:
                    return;
            }
        }

        // instant search Stuff ##################################################
        Dictionary<ThingDefs, (int PivotItem, ContentControl Block, ListView ListView, ScrollViewer SV)> Blocklist = new Dictionary<ThingDefs, (int PivotItem, ContentControl Block, ListView ListView, ScrollViewer SV)>();
        Thing PendingScrollEntry;

        void PrepareBlockList()
        {
            Blocklist.Add(ThingDefs.Handlung, (0, null, null, null));
            Blocklist.Add(ThingDefs.Fertigkeit, (0, null, null, null));
            Blocklist.Add(ThingDefs.Adeptenkraft_KomplexeForm, (0, null, null, null));
            Blocklist.Add(ThingDefs.Item, (1, null, null, null));
            Blocklist.Add(ThingDefs.Kommlink, (1, null, null, null));
            Blocklist.Add(ThingDefs.CyberDeck, (1, null, null, null));
            Blocklist.Add(ThingDefs.Programm, (1, null, null, null));
            Blocklist.Add(ThingDefs.Foki_Widgets, (1, null, null, null));
            Blocklist.Add(ThingDefs.Zaubersprueche, (1, null, null, null));
            Blocklist.Add(ThingDefs.Geist_Sprite, (1, null, null, null));
            Blocklist.Add(ThingDefs.Fernkampfwaffe, (2, null, null, null));
            Blocklist.Add(ThingDefs.Nahkampfwaffe, (2, null, null, null));
            Blocklist.Add(ThingDefs.Panzerung, (2, null, null, null));
            Blocklist.Add(ThingDefs.Vehikel, (2, null, null, null));
            Blocklist.Add(ThingDefs.Munition, (2, null, null, null));
            Blocklist.Add(ThingDefs.Attribut, (3, null, null, null));
            Blocklist.Add(ThingDefs.Connection, (3, null, null, null));
            Blocklist.Add(ThingDefs.Implantat, (3, null, null, null));
            Blocklist.Add(ThingDefs.Tradition_Initiation, (3, null, null, null));
            Blocklist.Add(ThingDefs.Stroemung_Wandlung, (3, null, null, null));
            Blocklist.Add(ThingDefs.Sin, (3, null, null, null));
            Blocklist.Add(ThingDefs.Vorteil, (3, null, null, null));
            Blocklist.Add(ThingDefs.Nachteil, (3, null, null, null));
        }

        void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            (sender as AutoSuggestBox).ItemsSource = MainObject.ThingList.Where((x) => x.SimilaritiesTo((sender as AutoSuggestBox).Text.ToLower()) > 0.3f);
        }

        void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                if (args.ChosenSuggestion != null)
                {
                    PendingScrollEntry = (args.ChosenSuggestion as Thing);
                }
                else
                {
                    PendingScrollEntry = MainObject.ThingList.Find((x) => x.SimilaritiesTo((sender as AutoSuggestBox).Text.ToLower()) > 0.3f);
                }

                if (!Blocklist.TryGetValue(PendingScrollEntry.ThingType, out var Choosen))
                {
                    return;
                }
                Pivot.SelectedIndex = Choosen.PivotItem;
            }
            catch { return; }
            ScrollIntoBlock();
        }
        private void Grid_Entry_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollIntoBlock();
            (sender as Grid).Loaded -= Grid_Entry_Loaded;
        }

        void ScrollIntoBlock()
        {
            if (PendingScrollEntry == null)
            {
                return;
            }
            try
            {
                Blocklist.TryGetValue(PendingScrollEntry.ThingType, out (int PivotItem, ContentControl Block, ListView ListView, ScrollViewer sv) Choosen);
                // Listenauswahl
                double offset = 0;
                foreach (var item in ((Choosen.sv as ScrollViewer).Content as Panel).Children)
                {
                    if (item.Equals(Choosen.Block))
                    {
                        if ((Choosen.sv.Content as Panel).Children.Last() == item)
                        {
                            throw new IndexOutOfRangeException();
                        }
                        break;
                    }
                    else
                    {
                        offset += item.DesiredSize.Height;
                    }
                }
                // Scroll into ListView
                foreach (ListViewItem item in Choosen.ListView.ItemsPanelRoot.Children)
                {
                    if ((item.Content).Equals(PendingScrollEntry))
                    {
                        break;
                    }
                    else
                    {
                        if (Choosen.ListView.ItemsPanelRoot.Children.Last() == item)
                        {
                            throw new IndexOutOfRangeException();
                        }
                        offset += item.DesiredSize.Height;
                    }
                }
                Choosen.ListView.SelectedItem = PendingScrollEntry; 
                Choosen.sv.ChangeView(null, offset - 100, null);
            }
            catch (Exception)
            {
                return;
            }
        PendingScrollEntry = null;
        }
        #region ApplyNewStyles
        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.RevealBrush"))
            {
                (sender as Button).Style = (Style)Resources["ButtonRevealStyle"];
            }
        }

        #endregion

    }
}
