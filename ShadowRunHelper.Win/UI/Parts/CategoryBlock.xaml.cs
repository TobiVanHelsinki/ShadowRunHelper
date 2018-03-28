using ShadowRunHelper.CharController;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelper.UI;
using ShadowRunHelper.UI.Edit;
using System;
using System.Collections.Generic;
using System.Linq;
using TLIB_UWPFRAME.IO;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace ShadowRunHelper.Win.UI
{
    public sealed partial class CategoryBlock : UserControl
    {
        #region Variables
        readonly AppModel Model = AppModel.Instance;
        CharHolder MainObject;
        ResourceLoader res;
        #endregion
        IController CTRL;
        //ThingDefs ThingType;

        public ThingDefs ThingType
        {
            get { return (ThingDefs)GetValue(ThingTypeProperty); }
            set { SetValue(ThingTypeProperty, value); OnThingTypeChanged(); }
        }

        // Using a DependencyProperty as the backing store for ThingType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThingTypeProperty =
            DependencyProperty.Register("ThingType", typeof(ThingDefs), typeof(CategoryBlock), new PropertyMetadata(0));

        public CategoryBlock()
        {
            if (MainObject == null)
            {
                MainObject = Model.MainObject;
            }
            res = ResourceLoader.GetForCurrentView();
            this.InitializeComponent();
        }

        #region CategoryStuff
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
                var selected2 = ListView.SelectedItems.Select(i => i as Thing);
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
                CTRL.ClearData();
            }
            catch (Exception ex)
            {
                Model.NewNotification(res.GetString("Notification_Error_LogicFail"), ex);
            }
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

        #region Display Categoriy Stuff

        void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var AddedItems = (sender as ListView).ItemsPanelRoot.Children.Cast<ListViewItem>().Where(c => e.AddedItems.Contains(c.Content));
                foreach (var item in AddedItems)
                {
                    (item.ContentTemplateRoot as CategoryEntry).Expand();
                }
                var RemovedItems = (sender as ListView).ItemsPanelRoot.Children.Cast<ListViewItem>().Where(c => e.RemovedItems.Contains(c.Content));
                foreach (var item in RemovedItems)
                {
                    (item.ContentTemplateRoot as CategoryEntry).Shrink();
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// for fast Accces
        /// </summary>
        public readonly IEnumerable<CategoryOption> lokalCategoryOptions = AppModel.Instance.MainObject.Settings.CategoryOptions;
        void OnThingTypeChanged()
        {
            CTRL = Model.MainObject.ThingDef2CTRL(ThingType);
            ListView.Tag = (int)ThingType;
            DataContext = CTRL;

            //Search Things
            var entry = lokalCategoryOptions.FirstOrDefault(x => x.ThingType == ThingType);

            if (entry != null && !entry.Visibility)
            {
                Visibility = Visibility.Collapsed;
                //return;
            }

            CategoryName.Text = ResourceLoader.GetForCurrentView().GetString("Model_ItemM_/Text");
            ListView.ItemsSource = Model.MainObject.CTRLItem.Data;
            //HeadLine.ContentTemplate = Item_E;
            HeadLine.ContentTemplate = (DataTemplate)Resources["Item_E"];
        }
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
            var entry = lokalCategoryOptions.FirstOrDefault(x => x.ThingType == Type);
            if (entry != null && !entry.Visibility)
            {
                ControlBlock.Visibility = Visibility.Collapsed;
                //return;
            }
            //Local things
            switch (Type)
            {
                //case ThingDefs.Handlung:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_HandlungM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLHandlung.Data;
                //    E.ContentTemplate = this.Handlung_E;
                //    LV.ItemTemplate = HandlungItem;
                //    break;
                //case ThingDefs.Fertigkeit:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_FertigkeitM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLFertigkeit.Data;
                //    E.ContentTemplate = this.Fertigkeit_E;
                //    LV.ItemTemplate = FertigkeitItem;
                //    break;
                //case ThingDefs.Item:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ItemM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLItem.Data;
                //    E.ContentTemplate = this.Item_E;
                //    LV.ItemTemplate = ItemItem;
                //    break;
                //case ThingDefs.Programm:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ProgrammM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLProgramm.Data;
                //    E.ContentTemplate = this.Programm_E;
                //    LV.ItemTemplate = ProgrammItem;
                //    break;
                //case ThingDefs.Munition:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_MunitionM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLMunition.Data;
                //    E.ContentTemplate = this.Munition_E;
                //    LV.ItemTemplate = MunitionItem;
                //    break;
                //case ThingDefs.Implantat:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ImplantatM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLImplantat.Data;
                //    E.ContentTemplate = this.Implantat_E;
                //    LV.ItemTemplate = ImplantatItem;
                //    break;
                //case ThingDefs.Vorteil:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_VorteilM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLVorteil.Data;
                //    E.ContentTemplate = this.Eigenschaft_E;
                //    LV.ItemTemplate = EigenschaftItem;
                //    break;
                //case ThingDefs.Nachteil:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_NachteilM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLNachteil.Data;
                //    E.ContentTemplate = this.Eigenschaft_E;
                //    LV.ItemTemplate = EigenschaftItem;
                //    break;
                //case ThingDefs.Connection:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ConnectionM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLConnection.Data;
                //    E.ContentTemplate = this.Connection_E;
                //    LV.ItemTemplate = ConnectionItem;
                //    break;
                //case ThingDefs.Sin:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_SinM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLSin.Data;
                //    E.ContentTemplate = this.Sin_E;
                //    LV.ItemTemplate = SinItem;
                //    break;
                //case ThingDefs.Nahkampfwaffe:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_NahkampfwaffeM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLNahkampfwaffe.Data;
                //    E.ContentTemplate = this.Nahkampfwaffe_E;
                //    LV.ItemTemplate = NahkampfwaffeItem;
                //    break;
                //case ThingDefs.Fernkampfwaffe:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_FernkampfwaffeM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLFernkampfwaffe.Data;
                //    E.ContentTemplate = this.Fernkampfwaffe_E;
                //    LV.ItemTemplate = FernkampfwaffeItem;
                //    break;
                //case ThingDefs.Kommlink:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_KommlinkM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLKommlink.Data;
                //    E.ContentTemplate = this.Kommlink_E;
                //    LV.ItemTemplate = KommlinkItem;
                //    break;
                //case ThingDefs.CyberDeck:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_CyberDeckM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLCyberDeck.Data;
                //    E.ContentTemplate = this.CyberDeck_E;
                //    LV.ItemTemplate = CyberDeckItem;
                //    break;
                //case ThingDefs.Vehikel:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_VehikelM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLVehikel.Data;
                //    E.ContentTemplate = this.Vehikel_E;
                //    LV.ItemTemplate = VehikelItem;
                //    break;
                //case ThingDefs.Panzerung:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_PanzerungM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLPanzerung.Data;
                //    E.ContentTemplate = this.Panzerung_E;
                //    LV.ItemTemplate = PanzerungItem;
                //    break;
                //case ThingDefs.Adeptenkraft_KomplexeForm:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Adeptenkraft_KomplexeFormM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLAdeptenkraft_KomplexeForm.Data;
                //    E.ContentTemplate = this.Adeptenkraft_KomplexeForm_E;
                //    LV.ItemTemplate = Adeptenkraft_KomplexeFormItem;
                //    break;
                //case ThingDefs.Geist_Sprite:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Geist_SpriteM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLGeist_Sprite.Data;
                //    E.ContentTemplate = this.Geist_Sprite_E;
                //    LV.ItemTemplate = Geist_SpriteItem;
                //    break;
                //case ThingDefs.Foki_Widgets:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Foki_WidgetsM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLFoki_Widgets.Data;
                //    E.ContentTemplate = this.Foki_Widgets_E;
                //    LV.ItemTemplate = Foki_WidgetsItem;
                //    break;
                //case ThingDefs.Stroemung_Wandlung:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Stroemung_WandlungM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLStroemung_Wandlung.Data;
                //    LV.ItemTemplate = Stroemung_WandlungItem;
                //    break;
                //case ThingDefs.Tradition_Initiation:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_Tradition_InitiationM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLTradition_Initiation.Data;
                //    LV.ItemTemplate = Tradition_InitiationItem;
                //    break;
                //case ThingDefs.Zaubersprueche:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_ZauberspruecheM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLZaubersprueche.Data;
                //    E.ContentTemplate = this.Zaubersprueche_E;
                //    LV.ItemTemplate = ZauberspruecheItem;
                //    break;
                //case ThingDefs.KomplexeForm:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_KomplexeFormM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLKomplexeForm.Data;
                //    E.ContentTemplate = this.KomplexeForm_E;
                //    LV.ItemTemplate = KomplexeFormItem;
                //    break;
                //case ThingDefs.Sprite:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_SpriteM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLSprite.Data;
                //    E.ContentTemplate = this.Sprite_E;
                //    LV.ItemTemplate = SpriteItem;
                //    break;
                //case ThingDefs.Widgets:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_WidgetsM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLWidgets.Data;
                //    E.ContentTemplate = this.Widgets_E;
                //    LV.ItemTemplate = WidgetsItem;
                //    break;
                //case ThingDefs.Wandlung:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_WandlungM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLWandlung.Data;
                //    LV.ItemTemplate = WandlungItem;
                //    break;
                //case ThingDefs.Initiation:
                //    U.Text = ResourceLoader.GetForCurrentView().GetString("Model_InitiationM_/Text");
                //    LV.ItemsSource = Model.MainObject.CTRLInitiation.Data;
                //    LV.ItemTemplate = InitiationItem;
                //    break;
                default:
                    return;
            }
        }
        #endregion

        #region Gui-Model Handler Stuff
        async void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Thing newThing = Model.MainObject.Add(ThingType);
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
            Auswahl dialog = new Auswahl(Model.MainObject.LinkList, ((Fertigkeit)((Button)sender).DataContext).PoolZusammensetzung, Filter: Fertigkeit.Filter);
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
    }
}
