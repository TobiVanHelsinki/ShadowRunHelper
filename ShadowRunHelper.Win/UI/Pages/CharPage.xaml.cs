using ShadowRunHelper.CharController;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelper.UI;
using ShadowRunHelper.UI.Edit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TLIB_UWPFRAME.IO;
using Windows.ApplicationModel.Resources;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper
{
    public sealed partial class CharPage : Page
    {


        #region Variables
        readonly AppModel Model = AppModel.Instance;
        public Windows.System.Display.DisplayRequest Char_DisplayRequest;
        CharHolder MainObject;
        ResourceLoader res;
        #endregion

        public CharPage()
        {
            if (MainObject == null)
            {
                MainObject = Model.MainObject;
            }
            res = ResourceLoader.GetForCurrentView();
            InitializeComponent();
        }

        #region Navigation stuff
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
            Model.TutorialStateChanged += TutorialStateChanged;
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

        #endregion
        #region Gui-Model Handler Stuff
        async void Add_Click(object sender, RoutedEventArgs e)
        {
            ThingDefs Controller = 0;
            long test = Int64.Parse((((Button)sender).Tag).ToString());
            Controller = (ThingDefs)test;
            Thing newThing = null;
            try
            {
                newThing = Model.MainObject.Add(Controller);
                if (SettingsModel.I.StartEditAfterAdd)
                {
                    await new EditThingDialog(newThing).ShowAsync();
                }

            }
            catch (Exception ex)
            {
                Model.NewNotification("", ex);
            }
        }
        
        async void Edit_Person_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await new Edit_Person_Detail(Model.MainObject.Person).ShowAsync();
            }
            catch (Exception)
            {
            }
        }
        async void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await new EditThingDialog(((Thing)((Button)sender).DataContext)).ShowAsync();
            }
            catch (Exception)
            {
            }
        }

        async void Edit_Attribut(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            try
            {
                await new EditThingDialog(((Thing)((Grid)sender).DataContext)).ShowAsync();
            }
            catch (Exception)
            {
            }
        }

        void Del_Click(object sender, RoutedEventArgs e)
        {
            if ((Thing)((Button)sender).DataContext != null)
            {
                Model.MainObject.Remove((Thing)((Button)sender).DataContext);
            }
        }

        async void HandlungEditZusDialog_Click(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(Model.MainObject.LinkList, ((Handlung)((Button)sender).DataContext).WertZusammensetzung, Filter: Handlung.Filter);
            await dialog.ShowAsync();

        }

        async void HandlungEditGrenzeZusDialog_Click(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(Model.MainObject.LinkList, ((Handlung)((Button)sender).DataContext).GrenzeZusammensetzung, Filter: Handlung.Filter);
            var ergebnis = await dialog.ShowAsync();
        }

        async void HandlungEditGegenZusDialog_Click(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(Model.MainObject.LinkList, ((Handlung)((Button)sender).DataContext).GegenZusammensetzung, Filter: Handlung.Filter);
            await dialog.ShowAsync();
        }

        async void FertigkeitenZusammensetzungBearbeiten(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(Model.MainObject.LinkList, ((Fertigkeit)((Button)sender).DataContext).PoolZusammensetzung, Filter:Fertigkeit.Filter);
            await dialog.ShowAsync();
        }

        async void Edit_LinkedThings(object sender, RoutedEventArgs e)
        {
            ThingDefs type = (ThingDefs)int.Parse((sender as FrameworkElement).Tag as string);
            IEnumerable<ThingDefs> FilterToUse = null;
            switch (type)
            {
                case ThingDefs.Handlung:
                    FilterToUse = Handlung.Filter;
                    break;
                case ThingDefs.Fertigkeit:
                    FilterToUse = Fertigkeit.Filter;
                    break;
                case ThingDefs.Item:
                    break;
                case ThingDefs.Programm:
                    break;
                case ThingDefs.Munition:
                    break;
                case ThingDefs.Implantat:
                    break;
                case ThingDefs.Vorteil:
                    break;
                case ThingDefs.Nachteil:
                    break;
                case ThingDefs.Attribut:
                    FilterToUse = Attribut.Filter;
                    break;
                case ThingDefs.Nahkampfwaffe:
                    break;
                case ThingDefs.Fernkampfwaffe:
                    FilterToUse = Fernkampfwaffe.Filter;
                    break;
                case ThingDefs.Kommlink:
                    break;
                case ThingDefs.CyberDeck:
                    break;
                case ThingDefs.Vehikel:
                    break;
                case ThingDefs.Panzerung:
                    FilterToUse = Panzerung.Filter;
                    break;
                case ThingDefs.Eigenschaft:
                    break;
                case ThingDefs.Adeptenkraft_KomplexeForm:
                    break;
                case ThingDefs.Geist_Sprite:
                    break;
                case ThingDefs.Foki_Widgets:
                    break;
                case ThingDefs.Stroemung_Wandlung:
                    break;
                case ThingDefs.Tradition_Initiation:
                    break;
                case ThingDefs.Zaubersprueche:
                    break;
                case ThingDefs.Berechnet:
                    break;
                case ThingDefs.KomplexeForm:
                    break;
                case ThingDefs.Sprite:
                    break;
                case ThingDefs.Widgets:
                    break;
                case ThingDefs.Wandlung:
                    break;
                case ThingDefs.Initiation:
                    break;
                default:
                    break;
            }
            var dialog = new Auswahl(Model.MainObject.LinkList, ((Thing)((Button)sender).DataContext).LinkedThings, Filter: FilterToUse);
            await dialog.ShowAsync();
        }
        //async void MunitionBearbeiten(object sender, RoutedEventArgs e)
        //{
        //    var TemList = new ObservableCollection<AllListEntry> ();
        //    Auswahl dialog = new Auswahl(Model.MainObject.LinkList, TemList, false, Fernkampfwaffe.Filter);
        //    await dialog.ShowAsync();
        //    ((Fernkampfwaffe)((Button)sender).DataContext).CurrentMunition = TemList.FirstOrDefault();
        //}
        #endregion
        #region Display Categoriy Stuff

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
                case (int)ThingDefs.KomplexeForm:
                    NewTemplate = KomplexeFormItem;
                    NewTemplateX = KomplexeFormItemX;
                    break;
                case (int)ThingDefs.Sprite:
                    NewTemplate = SpriteItem;
                    NewTemplateX = SpriteItemX;
                    break;
                case (int)ThingDefs.Widgets:
                    NewTemplate = WidgetsItem;
                    NewTemplateX = WidgetsItemX;
                    break;
                case (int)ThingDefs.Wandlung:
                    NewTemplate = WandlungItem;
                    NewTemplateX = WandlungItemX;
                    break;
                case (int)ThingDefs.Initiation:
                    NewTemplate = InitiationItem;
                    NewTemplateX = InitiationItemX;
                    break;

                default:
                    return;
            }            if (NewTemplate == null || NewTemplateX == null)
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

        /// <summary>
        /// for fast Accces
        /// </summary>
        public readonly IEnumerable<CategoryOption> lokalCategoryOptions = AppModel.Instance.MainObject.Settings.CategoryOptions;

        void ContentControl_Loaded(object sender, RoutedEventArgs e)
        {
            var ControlBlock = sender as ContentControl;
            ThingDefs Type = TypeHelper.Obj2ThingDef(ControlBlock.Tag);

            //Temp Vars
            var U = ((((ControlBlock.ContentTemplateRoot as Panel).Children[0] as Panel)).Children.First(c => c.GetType() == typeof(TextBlock)) as TextBlock);
            var E = ((ControlBlock.ContentTemplateRoot as Panel).Children.First(c => c.GetType() == typeof(ContentPresenter)) as ContentPresenter);
            var LV = ((ControlBlock.ContentTemplateRoot as Panel).Children.First(c => c.GetType() == typeof(ListView)) as ListView);
            // Global Things
            ((((ControlBlock.ContentTemplateRoot as Panel).Children[0] as Panel)).Children[0] as Button).Tag = ControlBlock.Tag;
            LV.Tag = ControlBlock.Tag;
            ControlBlock.DataContext = Model.MainObject.ThingDef2CTRL(Type);

            //Search Things
            #region SearchStuff
            var Search_LV = LV;
            var Search_Block = ControlBlock;
            var Search_SV = (ScrollViewer)(ControlBlock.Parent as FrameworkElement).Parent;

            GlobalSearchCache.Add(Type, (Search_Block, Search_LV, Search_SV));
            #endregion

            var entry = lokalCategoryOptions.FirstOrDefault(x => x.ThingType == Type);
            if (entry != null && !entry.Visibility)
            {
                ControlBlock.Visibility = Visibility.Collapsed;
                //return;
            }
            //Local things
            switch (Type)
            {
                case ThingDefs.Handlung:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_HandlungM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLHandlung.Data;
                    E.ContentTemplate = this.Handlung_E;
                    LV.ItemTemplate = HandlungItem;
                    break;
                case ThingDefs.Fertigkeit:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_FertigkeitM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLFertigkeit.Data;
                    E.ContentTemplate = this.Fertigkeit_E;
                    LV.ItemTemplate = FertigkeitItem;
                    break;
                case ThingDefs.Item:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ItemM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLItem.Data;
                    E.ContentTemplate = this.Item_E;
                    LV.ItemTemplate = ItemItem;
                    break;
                case ThingDefs.Programm:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ProgrammM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLProgramm.Data;
                    E.ContentTemplate = this.Programm_E;
                    LV.ItemTemplate = ProgrammItem;
                    break;
                case ThingDefs.Munition:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_MunitionM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLMunition.Data;
                    E.ContentTemplate = this.Munition_E;
                    LV.ItemTemplate = MunitionItem;
                    break;
                case ThingDefs.Implantat:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ImplantatM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLImplantat.Data;
                    E.ContentTemplate = this.Implantat_E;
                    LV.ItemTemplate = ImplantatItem;
                    break;
                case ThingDefs.Vorteil:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_VorteilM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLVorteil.Data;
                    E.ContentTemplate = this.Eigenschaft_E;
                    LV.ItemTemplate = EigenschaftItem;
                    break;
                case ThingDefs.Nachteil:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_NachteilM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLNachteil.Data;
                    E.ContentTemplate = this.Eigenschaft_E;
                    LV.ItemTemplate = EigenschaftItem;
                    break;
                case ThingDefs.Connection:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ConnectionM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLConnection.Data;
                    E.ContentTemplate = this.Connection_E;
                    LV.ItemTemplate = ConnectionItem;
                    break;
                case ThingDefs.Sin:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_SinM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLSin.Data;
                    E.ContentTemplate = this.Sin_E;
                    LV.ItemTemplate = SinItem;
                    break;
                case ThingDefs.Nahkampfwaffe:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_NahkampfwaffeM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLNahkampfwaffe.Data;
                    E.ContentTemplate = this.Nahkampfwaffe_E;
                    LV.ItemTemplate = NahkampfwaffeItem;
                    break;
                case ThingDefs.Fernkampfwaffe:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_FernkampfwaffeM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLFernkampfwaffe.Data;
                    E.ContentTemplate = this.Fernkampfwaffe_E;
                    LV.ItemTemplate = FernkampfwaffeItem;
                    break;
                case ThingDefs.Kommlink:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_KommlinkM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLKommlink.Data;
                    E.ContentTemplate = this.Kommlink_E;
                    LV.ItemTemplate = KommlinkItem;
                    break;
                case ThingDefs.CyberDeck:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_CyberDeckM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLCyberDeck.Data;
                    E.ContentTemplate = this.CyberDeck_E;
                    LV.ItemTemplate = CyberDeckItem;
                    break;
                case ThingDefs.Vehikel:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_VehikelM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLVehikel.Data;
                    E.ContentTemplate = this.Vehikel_E;
                    LV.ItemTemplate = VehikelItem;
                    break;
                case ThingDefs.Panzerung:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_PanzerungM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLPanzerung.Data;
                    E.ContentTemplate = this.Panzerung_E;
                    LV.ItemTemplate = PanzerungItem;
                    break;
                case ThingDefs.Adeptenkraft_KomplexeForm:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Adeptenkraft_KomplexeFormM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLAdeptenkraft_KomplexeForm.Data;
                    E.ContentTemplate = this.Adeptenkraft_KomplexeForm_E;
                    LV.ItemTemplate = Adeptenkraft_KomplexeFormItem;
                    break;
                case ThingDefs.Geist_Sprite:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Geist_SpriteM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLGeist_Sprite.Data;
                    E.ContentTemplate = this.Geist_Sprite_E;
                    LV.ItemTemplate = Geist_SpriteItem;
                    break;
                case ThingDefs.Foki_Widgets:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Foki_WidgetsM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLFoki_Widgets.Data;
                    E.ContentTemplate = this.Foki_Widgets_E;
                    LV.ItemTemplate = Foki_WidgetsItem;
                    break;
                case ThingDefs.Stroemung_Wandlung:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Stroemung_WandlungM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLStroemung_Wandlung.Data;
                    LV.ItemTemplate = Stroemung_WandlungItem;
                    break;
                case ThingDefs.Tradition_Initiation:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Tradition_InitiationM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLTradition_Initiation.Data;
                    LV.ItemTemplate = Tradition_InitiationItem;
                    break;
                case ThingDefs.Zaubersprueche:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ZauberspruecheM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLZaubersprueche.Data;
                    E.ContentTemplate = this.Zaubersprueche_E;
                    LV.ItemTemplate = ZauberspruecheItem;
                    break;
                case ThingDefs.KomplexeForm:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_KomplexeFormM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLKomplexeForm.Data;
                    E.ContentTemplate = this.KomplexeForm_E;
                    LV.ItemTemplate = KomplexeFormItem;
                    break;
                case ThingDefs.Sprite:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_SpriteM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLSprite.Data;
                    E.ContentTemplate = this.Sprite_E;
                    LV.ItemTemplate = SpriteItem;
                    break;
                case ThingDefs.Widgets:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_WidgetsM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLWidgets.Data;
                    E.ContentTemplate = this.Widgets_E;
                    LV.ItemTemplate = WidgetsItem;
                    break;
                case ThingDefs.Wandlung:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_WandlungM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLWandlung.Data;
                    LV.ItemTemplate = WandlungItem;
                    break;
                case ThingDefs.Initiation:
                    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_InitiationM_/Text");
                    LV.ItemsSource = Model.MainObject.CTRLInitiation.Data;
                    LV.ItemTemplate = InitiationItem;
                    break;
                default:
                    return;
            }
        }
        #endregion
        #region Options
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Model.MainObject.Settings.ResetCategoryOptions();
        }
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            Model.RequestNavigation(this, ProjectPages.Char);
        }
        #endregion
        #region  instant search Stuff

        Dictionary<ThingDefs, (ContentControl Block, ListView ListView, ScrollViewer SV)> GlobalSearchCache = new Dictionary<ThingDefs, (ContentControl Block, ListView ListView, ScrollViewer SV)>();
        Thing PendingScrollEntry;

        void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            switch (args.Reason)
            {
                case AutoSuggestionBoxTextChangeReason.UserInput:
                    (sender as AutoSuggestBox).ItemsSource = MainObject.ThingList.Where(x => lokalCategoryOptions.First(y => y.ThingType == x.ThingType).Visibility).Where((x) => x.SimilaritiesTo((sender as AutoSuggestBox).Text.ToLower()) > 0.3f);
                    break;
                case AutoSuggestionBoxTextChangeReason.ProgrammaticChange:
                    break;
                case AutoSuggestionBoxTextChangeReason.SuggestionChosen:
                default:
                    break;
            }
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
                    PendingScrollEntry = MainObject.ThingList.Where(x => lokalCategoryOptions.First(y => y.ThingType == x.ThingType).Visibility).ToList().Find((x) => x.SimilaritiesTo((sender as AutoSuggestBox).Text.ToLower()) > 0.3f);
                }
                if (PendingScrollEntry == null)
                {
                    return;
                }
                Pivot.SelectedIndex = TypeHelper.ThingTypeProperties.Find(x => x.ThingType == PendingScrollEntry.ThingType).Pivot;
            }
            catch { return; }
            ScrollIntoBlock();
            sender.Text = "";
            sender.IsSuggestionListOpen = false;
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
                GlobalSearchCache.TryGetValue(PendingScrollEntry.ThingType, out (ContentControl Block, ListView ListView, ScrollViewer sv) Choosen);
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
        #endregion

        #region Button Handlers
        void Click_Save(object sender, RoutedEventArgs e)
        {
            Model?.MainObject?.SetSaveTimerTo();
        }
        private async void CSV_IN_Click(object sender, RoutedEventArgs e)
        {
            string strRead = "";
            var CTRL = ((sender as FrameworkElement).DataContext as IController);
            try
            {
                strRead = await SharedIO.ReadTextFromFile(new FileInfoClass() { FolderToken = "import", Fileplace = Place.Extern }, Constants.LST_FILETYPES_CSV, UserDecision.AskUser);
            }
            catch (IsOKException ex)
            {
                return;
            }
            catch (Exception ex)
            {
                Model.NewNotification(res.GetString("Notification_Error_CSVImportFail") + "1", ex);
            }
            try
            {
                CTRL.CSV2Data(';', '\n', strRead);
            }
            catch (IsOKException ex)
            {
                return;
            }
            catch (Exception ex)
            {
                Model.NewNotification(res.GetString("Notification_Error_CSVImportFail") + "2", ex);
            }
        }

        private void CSV_EX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var CTRL = ((sender as FrameworkElement).DataContext as IController);
                string output = CTRL.Data2CSV(';', '\n');
                SharedIO.SaveTextToFile(new FileInfoClass() { Filename = TypeHelper.ThingDefToString(CTRL.eDataTyp, true) + Constants.DATEIENDUNG_CSV, Fileplace = Place.Extern, FolderToken = "CSV_TEMP" }, output);
            }
            catch (IsOKException ex)
            {
                return;
            }
            catch (Exception ex)
            {
                Model.NewNotification(res.GetString("Notification_Error_CSVExportFail") + "2", ex);
            }
        }
        private void CSV_EX_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                var CTRL = ((sender as FrameworkElement).DataContext as IController);
                var block = GlobalSearchCache.First(x => x.Key == CTRL.eDataTyp);
                var selected2 = block.Value.ListView.SelectedItems.Select(i=>i as Thing);
                string output = IO.CSV_Converter.Data2CSV(';', '\n', selected2);


                SharedIO.SaveTextToFile(new FileInfoClass() { Filename = TypeHelper.ThingDefToString(CTRL.eDataTyp, true) + Constants.DATEIENDUNG_CSV, Fileplace = Place.Extern, FolderToken = "CSV_TEMP" }, output);
            }
            catch (IsOKException ex)
            {
                return;
            }
            catch (Exception ex)
            {
                Model.NewNotification(res.GetString("Notification_Error_CSVExportFail") + "2", ex);
            }
        }
        
        private void DeleteCategoryContent(object sender, RoutedEventArgs e)
        {
            try
            {
                var CTRL = ((sender as FrameworkElement).DataContext as IController);
                CTRL.ClearData();
            }
            catch (Exception ex)
            {
                Model.NewNotification(res.GetString("Notification_Error_LogicFail"), ex);
            }
        }

        private void FurtherSettings(object sender, RoutedEventArgs e)
        {
            AppModel.Instance.RequestNavigation(this,ProjectPages.Settings, ProjectPagesOptions.SettingsCategories);
        }


        #endregion
        #region Ordering
        void Order_Type(object sender, RoutedEventArgs e)
        {
            var CTRL = ((sender as FrameworkElement).DataContext as IController);
            CTRL.OrderData(Ordering.Type);
        }

        void Order_ABC(object sender, RoutedEventArgs e)
        {
            var CTRL = ((sender as FrameworkElement).DataContext as IController);
            CTRL.OrderData(Ordering.ABC);
        }
        private void Order_Orig(object sender, RoutedEventArgs e)
        {
            var CTRL = ((sender as FrameworkElement).DataContext as IController);
            CTRL.OrderData(Ordering.Original);
        }
        private void Order_Save(object sender, RoutedEventArgs e)
        {
            ((sender as FrameworkElement).DataContext as IController).SaveCurrentOrdering();
        }


        #endregion



    }
}
