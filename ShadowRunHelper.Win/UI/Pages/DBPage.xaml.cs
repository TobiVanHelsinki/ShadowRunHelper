using ShadowRunHelper.CharModel;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using SharedCodeBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TLIB;
using TLIB.IO;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper
{
    public sealed partial class DBPage : Page
    {
        // Variables ##########################################################
        readonly AppModel ViewModel = AppModel.Instance;
        public Windows.System.Display.DisplayRequest Char_DisplayRequest;
        CharHolder CurrentChar;
        CharHolder ActiveChar;
        public DBPage()
        {
            ActiveChar = ViewModel.MainObject;
            PrepareBlockList();
        }
        // Navigation Stuff####################################################
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var SourceFileClass = new FileInfoClass() { Filename = CrossPlatformHelper.GetSimpleCountryCode() + Constants.DATEIENDUNG_CHAR, Filepath = CrossPlatformHelper.GetPrefix(CrossPlatformHelper.PrefixType.AppPackageData) + "Assets/PreDB/", Fileplace = Place.Assets };
            CurrentChar = await CharHolderIO.Load(SourceFileClass, eUD: UserDecision.ThrowError);
            InitializeComponent();

            if (SettingsModel.I.BetaFeatures)
            {
                FilterBoxPanel.Visibility = Visibility.Visible;
            }
            else
            {
                FilterBoxPanel.Visibility = Visibility.Collapsed;
            }
            if (SettingsModel.I.StartCountDB >= 3)
            {
                ToggleHelp(Visibility.Collapsed);
            }
        }

        // Gui Handler Stuff ##################################################

        void CommandBar_Loaded(object sender, RoutedEventArgs e)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsPropertyPresent("Windows.UI.Xaml.Controls.CommandBar", "DefaultLabelPosition"))
            {
                (sender as CommandBar).DefaultLabelPosition = CommandBarDefaultLabelPosition.Right;
            }
        }


        void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            TextBlock U = ((((sender as ContentControl).ContentTemplateRoot as StackPanel).Children[0] as StackPanel).Children[0] as TextBlock);
            ContentPresenter E = (((sender as ContentControl).ContentTemplateRoot as StackPanel).Children[1] as ContentPresenter);
            ListView LV = (((sender as ContentControl).ContentTemplateRoot as StackPanel).Children[2] as ListView);
            // Global Things
            LV.Tag = (sender as ContentControl).Tag;
            ListViews.Add(LV);
            //Search Things
            try
            {
                Blocklist.TryGetValue((ThingDefs)int.Parse((sender as ContentControl).Tag as string), out (int PivotItem, ContentControl Block, ListView ListView) NewBlock);
                NewBlock.ListView = LV;
                NewBlock.Block = (ContentControl)sender;
                Blocklist.Remove((ThingDefs)int.Parse((sender as ContentControl).Tag as string));
                Blocklist.Add((ThingDefs)int.Parse((sender as ContentControl).Tag as string), NewBlock);
            }
            catch (Exception)
            {
            }

            //Local things
            switch (int.Parse(((sender as ContentControl).Tag as string)))
            {
                case (int)ThingDefs.Handlung:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_HandlungM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLHandlung.Data;
                    E.ContentTemplate = this.Handlung_E;
                    LV.ItemTemplate = HandlungItem;
                    //NewBlock.PivotItem = 0;
                    break;
                case (int)ThingDefs.Fertigkeit:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_FertigkeitM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLFertigkeit.Data;
                    E.ContentTemplate = this.Fertigkeit_E;
                    LV.ItemTemplate = FertigkeitItem;
                    break;
                case (int)ThingDefs.Item:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ItemM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLItem.Data;
                    E.ContentTemplate = this.Item_E;
                    LV.ItemTemplate = ItemItem;
                    break;
                case (int)ThingDefs.Programm:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ProgrammM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLProgramm.Data;
                    E.ContentTemplate = this.Programm_E;
                    LV.ItemTemplate = ProgrammItem;
                    break;
                case (int)ThingDefs.Munition:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_MunitionM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLMunition.Data;
                    E.ContentTemplate = this.Munition_E;
                    LV.ItemTemplate = MunitionItem;
                    break;
                case (int)ThingDefs.Implantat:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ImplantatM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLImplantat.Data;
                    E.ContentTemplate = this.Implantat_E;
                    LV.ItemTemplate = ImplantatItem;
                    break;
                case (int)ThingDefs.Vorteil:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_VorteilM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLVorteil.Data;
                    E.ContentTemplate = this.Eigenschaft_E;
                    LV.ItemTemplate = EigenschaftItem;
                    break;
                case (int)ThingDefs.Nachteil:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_NachteilM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLNachteil.Data;
                    E.ContentTemplate = this.Eigenschaft_E;
                    LV.ItemTemplate = EigenschaftItem;
                    break;
                case (int)ThingDefs.Connection:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ConnectionM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLConnection.Data;
                    E.ContentTemplate = this.Connection_E;
                    LV.ItemTemplate = ConnectionItem;
                    //NewBlock.PivotItem = 3;
                    break;
                case (int)ThingDefs.Sin:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_SinM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLSin.Data;
                    E.ContentTemplate = this.Sin_E;
                    LV.ItemTemplate = SinItem;
                    break;
                case (int)ThingDefs.Nahkampfwaffe:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_NahkampfwaffeM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLNahkampfwaffe.Data;
                    E.ContentTemplate = this.Nahkampfwaffe_E;
                    LV.ItemTemplate = NahkampfwaffeItem;
                    break;
                case (int)ThingDefs.Fernkampfwaffe:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_FernkampfwaffeM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLFernkampfwaffe.Data;
                    E.ContentTemplate = this.Fernkampfwaffe_E;
                    LV.ItemTemplate = FernkampfwaffeItem;
                    break;
                case (int)ThingDefs.Kommlink:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_KommlinkM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLKommlink.Data;
                    E.ContentTemplate = this.Kommlink_E;
                    LV.ItemTemplate = KommlinkItem;
                    break;
                case (int)ThingDefs.CyberDeck:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_CyberDeckM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLCyberDeck.Data;
                    E.ContentTemplate = this.CyberDeck_E;
                    LV.ItemTemplate = CyberDeckItem;
                    break;
                case (int)ThingDefs.Vehikel:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_VehikelM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLVehikel.Data;
                    E.ContentTemplate = this.Vehikel_E;
                    LV.ItemTemplate = VehikelItem;
                    break;
                case (int)ThingDefs.Panzerung:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_PanzerungM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLPanzerung.Data;
                    E.ContentTemplate = this.Panzerung_E;
                    LV.ItemTemplate = PanzerungItem;
                    break;
                case (int)ThingDefs.Adeptenkraft_KomplexeForm:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Adeptenkraft_KomplexeFormM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLAdeptenkraft_KomplexeForm.Data;
                    E.ContentTemplate = this.Adeptenkraft_KomplexeForm_E;
                    LV.ItemTemplate = Adeptenkraft_KomplexeFormItem;
                    break;
                case (int)ThingDefs.Geist_Sprite:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Geist_SpriteM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLGeist_Sprite.Data;
                    E.ContentTemplate = this.Geist_Sprite_E;
                    LV.ItemTemplate = Geist_SpriteItem;
                    break;
                case (int)ThingDefs.Foki_Widgets:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Foki_WidgetsM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLFoki_Widgets.Data;
                    E.ContentTemplate = this.Foki_Widgets_E;
                    LV.ItemTemplate = Foki_WidgetsItem;
                    break;
                case (int)ThingDefs.Stroemung_Wandlung:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Stroemung_WandlungM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLStroemung_Wandlung.Data;
                    LV.ItemTemplate = Stroemung_WandlungItem;
                    break;
                case (int)ThingDefs.Tradition_Initiation:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Tradition_InitiationM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLTradition_Initiation.Data;
                    LV.ItemTemplate = Tradition_InitiationItem;
                    break;
                case (int)ThingDefs.Zaubersprueche:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ZauberspruecheM_/Text");
                    LV.ItemsSource = CurrentChar.CTRLZaubersprueche.Data;
                    E.ContentTemplate = this.Zaubersprueche_E;
                    LV.ItemTemplate = ZauberspruecheItem;
                    break;
                default:
                    return;
            }

            if (!(PendingScrollEntry == null) && (int)PendingScrollEntry.ThingType == int.Parse(((sender as ContentControl).Tag as string)))
            {
                ScrollIntoBlock();
            }
        }

        // instant search Stuff ##################################################
        Dictionary<ThingDefs, (int PivotItem, ContentControl Block, ListView ListView)> Blocklist = new Dictionary<ThingDefs, (int PivotItem, ContentControl Block, ListView ListView)>();
        Thing PendingScrollEntry;

        void PrepareBlockList()
        {
            Blocklist.Add(ThingDefs.Handlung, (0, null, null));
            Blocklist.Add(ThingDefs.Fertigkeit, (0, null, null));
            Blocklist.Add(ThingDefs.Adeptenkraft_KomplexeForm, (0, null, null));
            Blocklist.Add(ThingDefs.Item, (1, null, null));
            Blocklist.Add(ThingDefs.Kommlink, (1, null, null));
            Blocklist.Add(ThingDefs.CyberDeck, (1, null, null));
            Blocklist.Add(ThingDefs.Programm, (1, null, null));
            Blocklist.Add(ThingDefs.Foki_Widgets, (1, null, null));
            Blocklist.Add(ThingDefs.Zaubersprueche, (1, null, null));
            Blocklist.Add(ThingDefs.Geist_Sprite, (1, null, null));
            Blocklist.Add(ThingDefs.Fernkampfwaffe, (2, null, null));
            Blocklist.Add(ThingDefs.Nahkampfwaffe, (2, null, null));
            Blocklist.Add(ThingDefs.Panzerung, (2, null, null));
            Blocklist.Add(ThingDefs.Vehikel, (2, null, null));
            Blocklist.Add(ThingDefs.Munition, (2, null, null));
            Blocklist.Add(ThingDefs.Attribut, (3, null, null));
            Blocklist.Add(ThingDefs.Connection, (3, null, null));
            Blocklist.Add(ThingDefs.Implantat, (3, null, null));
            Blocklist.Add(ThingDefs.Tradition_Initiation, (3, null, null));
            Blocklist.Add(ThingDefs.Stroemung_Wandlung, (3, null, null));
            Blocklist.Add(ThingDefs.Sin, (3, null, null));
            Blocklist.Add(ThingDefs.Vorteil, (3, null, null));
            Blocklist.Add(ThingDefs.Nachteil, (3, null, null));
        }

        void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(PendingScrollEntry == null))
            {
                ScrollIntoBlock();
            }
        }

        void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            (sender as AutoSuggestBox).ItemsSource = CurrentChar.ThingList.Where((x) => x.Bezeichner.ToLower().Contains((sender as AutoSuggestBox).Text.ToLower()));
        }

        void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            (int PivotItem, ContentControl Block, ListView ListView) Choosen;
            try
            {
                if (args.ChosenSuggestion != null)
                {
                    PendingScrollEntry = (args.ChosenSuggestion as Thing);
                }
                else
                {
                    PendingScrollEntry = CurrentChar.ThingList.Find(x => args.QueryText.Contains(x.Bezeichner));
                }

                if (!Blocklist.TryGetValue(PendingScrollEntry.ThingType, out Choosen))
                {
                    return;
                }
                Pivot.SelectedIndex = Choosen.PivotItem;
            }
            catch { return; }
            // Pivot Auswahl
            if (Choosen.Block != null)
            {
                ScrollIntoBlock();
            }
        }

        void ScrollIntoBlock()
        {
            try
            {
                if (!Blocklist.TryGetValue(PendingScrollEntry.ThingType, out (int PivotItem, ContentControl Block, ListView ListView) Choosen))
                {
                    return;
                }
                // Listenauswahl
                double offset = 0;
                foreach (var item in (((Pivot.Items[Choosen.PivotItem] as PivotItem).Content as ScrollViewer).Content as StackPanel).Children)
                {
                    //TODO special vor und nachteile
                    if (item.Equals(Choosen.Block))
                    {
                        break;
                    }
                    else
                    {
                        offset += item.DesiredSize.Height;
                    }
                }
                // Scroll into ListView
                try
                {
                    foreach (var item in Choosen.ListView.Items)
                    {
                        if ((item).Equals(PendingScrollEntry))
                        {
                            break;
                        }
                        else
                        {
                            offset += 10;
                        }
                    }
                }
                catch (Exception)
                {
                }
                Choosen.ListView.SelectedItem = PendingScrollEntry;
                ((Pivot.Items[Choosen.PivotItem] as PivotItem).Content as ScrollViewer).ChangeView(null, offset, null);
                //((Pivot.Items[Choosen.PivotItem] as PivotItem).Content as ScrollViewer).ChangeView(null, 100, null);
                //Choosen.ListView.ScrollIntoView(PendingScrollEntry.Object, ScrollIntoViewAlignment.Leading); //TODO typen stimme nicht
                PendingScrollEntry = null;
            }
            catch (Exception)
            {
            }
        }
        private void ToggleHelp(object sender, RoutedEventArgs e)
        {
            HelpContainer.Visibility = HelpContainer.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }
        private void ToggleHelp(Visibility V)
        {
            HelpContainer.Visibility = V;
        }

        // Copy Stuff ##################################################
        readonly List<ListView> ListViews = new List<ListView>();
        readonly List<Thing> ToCopyList = new List<Thing>();
        void Finish_Click(object sender, RoutedEventArgs e)
        {
            DoCopy();
        }

        private void FinishClose_Click(object sender, RoutedEventArgs e)
        {
            DoCopy();
            CoreApplication.GetCurrentView().CoreWindow.Close();
        }

        void DoCopy()
        {
            ToCopyList.Clear();
            foreach (ListView item in ListViews)
            {
                foreach (Thing selected in item.SelectedItems)
                {
                    ToCopyList.Add(selected);
                }
            }
            List<Thing> NewToCopyList = new List<Thing>();
            foreach (Thing item in ToCopyList)
            {
                void Loop(ObservableThingListEntryCollection List)
                {
                    foreach (AllListEntry item2 in List)
                    {
                        if (!(ToCopyList.Contains(item2.Object)))
                        {
                            if (!ActiveChar.LinkList.Exists(x => Thing.HasSameIdentifiers(x.Object, item2.Object)))
                            {
                                NewToCopyList.Add(item2.Object);
                            }
                            else
                            {
                                item2.Object = ActiveChar.LinkList.Where(x => Thing.HasSameIdentifiers(x.Object, item2.Object)).First().Object;
                            }
                        }
                    }
                }
                switch (item.ThingType)
                {
                    case ThingDefs.Handlung:
                        Loop((item as Handlung).WertZusammensetzung);
                        Loop((item as Handlung).GegenZusammensetzung);
                        Loop((item as Handlung).GrenzeZusammensetzung);
                        break;
                    case ThingDefs.Fertigkeit:
                        Loop((item as Fertigkeit).PoolZusammensetzung);
                        break;
                    default:
                        break;
                }
            }
            ToCopyList.AddRange(NewToCopyList);
            foreach (Thing item in ToCopyList)
            {
                if (!ActiveChar.LinkList.Exists((x) => Thing.HasSameIdentifiers(x.Object, item)))
                {
                    ModelResources.AtGui(() => ActiveChar.Add(item.Copy()));
                }
            }
            ModelResources.AtGui(() => { ActiveChar.Repair(); });
        }
    }
}
