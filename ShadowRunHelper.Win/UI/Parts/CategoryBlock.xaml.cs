using Microsoft.AppCenter.Analytics;
using ShadowRunHelper.CharController;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelper.UI.Edit;
using System;
using System.Collections.Generic;
using System.Linq;
using TAMARIN.IO;
using TAPPLICATION.IO;
using TLIB;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
                strRead = (await SharedIO.CurrentIO.LoadFileContent(new FileInfoClass() { FolderToken = "import", Fileplace = Place.Extern }, Constants.LST_FILETYPES_CSV, UserDecision.AskUser)).strFileContent;
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
            Analytics.TrackEvent("Char_CSV_IN");
        }

        private void CSV_EX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string output = CTRL.Data2CSV(';', '\n');
                SharedIO.CurrentIO.SaveFileContent(output, new FileInfoClass() { Filename = TypeHelper.ThingDefToString(CTRL.eDataTyp, true) + Constants.DATEIENDUNG_CSV, Fileplace = Place.Extern, FolderToken = "CSV_TEMP" });
            }
            catch (IsOKException ex)
            {
                return;
            }
            catch (Exception ex)
            {
                Model.NewNotification(res.GetString("Notification_Error_CSVExportFail") + "2", ex);
            }
            Analytics.TrackEvent("Char_CSV_EX");
        }
        private void CSV_EX_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected2 = ListView.SelectedItems.Select(i => i as Thing);
                string output = IO.CSV_Converter.Data2CSV(';', '\n', selected2);
                SharedIO.CurrentIO.SaveFileContent(output, new FileInfoClass() { Filename = TypeHelper.ThingDefToString(CTRL.eDataTyp, true) + Constants.DATEIENDUNG_CSV, Fileplace = Place.Extern, FolderToken = "CSV_TEMP" });
            }
            catch (IsOKException ex)
            {
                return;
            }
            catch (Exception ex)
            {
                Model.NewNotification(res.GetString("Notification_Error_CSVExportFail") + "2", ex);
            }
            Analytics.TrackEvent("Char_CSV_EX_Selected");
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
            Analytics.TrackEvent("Char_Order_Type");
            var CTRL = ((sender as FrameworkElement).DataContext as IController);
            CTRL.OrderData(Ordering.Type);
        }

        void Order_ABC(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Char_Order_ABC");
            var CTRL = ((sender as FrameworkElement).DataContext as IController);
            CTRL.OrderData(Ordering.ABC);
        }
        private void Order_Orig(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Char_Order_Orig");
            var CTRL = ((sender as FrameworkElement).DataContext as IController);
            CTRL.OrderData(Ordering.Original);
        }
        private void Order_Save(object sender, RoutedEventArgs e)
        {
            ((sender as FrameworkElement).DataContext as IController).SaveCurrentOrdering();
        }


        #endregion

        void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var AddedItems = (sender as ListView).ItemsPanelRoot.Children.Cast<ListViewItem>().Where(c => e.AddedItems.Contains(c.Content));
                foreach (var item in AddedItems)
                {
                    (item.ContentTemplateRoot as CategoryEntry).SetExpandedTemplate();
                }
                var RemovedItems = (sender as ListView).ItemsPanelRoot.Children.Cast<ListViewItem>().Where(c => e.RemovedItems.Contains(c.Content));
                foreach (var item in RemovedItems)
                {
                    (item.ContentTemplateRoot as CategoryEntry).SetDefaultTemplate();
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
            try
            {
                CTRL = Model.MainObject.ThingDef2CTRL(ThingType);
                ListView.Tag = (int)ThingType;
                DataContext = CTRL;

                //Search Things
                var entry = lokalCategoryOptions.FirstOrDefault(x => x.ThingType == ThingType);

                if (entry != null && !entry.Visibility)
                {
                    Visibility = Visibility.Collapsed;
                }
                var Current = TypeHelper.ThingTypeProperties.FirstOrDefault(t => t.ThingType == CTRL.eDataTyp);

                CategoryName.Text = ResourceLoader.GetForCurrentView().GetString(Current.DisplayNamePlural);
                switch (CTRL.eDataTyp)
                {
                    case ThingDefs.Handlung:
                        ListView.ItemsSource = Model.MainObject.CTRLHandlung.Data;
                        break;
                    case ThingDefs.Fertigkeit:
                        ListView.ItemsSource = Model.MainObject.CTRLFertigkeit.Data;
                        break;
                    case ThingDefs.Item:
                        ListView.ItemsSource = Model.MainObject.CTRLItem.Data;
                        break;
                    case ThingDefs.Programm:
                        ListView.ItemsSource = Model.MainObject.CTRLProgramm.Data;
                        break;
                    case ThingDefs.Munition:
                        ListView.ItemsSource = Model.MainObject.CTRLMunition.Data;
                        break;
                    case ThingDefs.Implantat:
                        ListView.ItemsSource = Model.MainObject.CTRLImplantat.Data;
                        break;
                    case ThingDefs.Vorteil:
                        ListView.ItemsSource = Model.MainObject.CTRLVorteil.Data;
                        break;
                    case ThingDefs.Nachteil:
                        ListView.ItemsSource = Model.MainObject.CTRLNachteil.Data;
                        break;
                    case ThingDefs.Connection:
                        ListView.ItemsSource = Model.MainObject.CTRLConnection.Data;
                        break;
                    case ThingDefs.Sin:
                        ListView.ItemsSource = Model.MainObject.CTRLSin.Data;
                        break;
                    case ThingDefs.Nahkampfwaffe:
                        ListView.ItemsSource = Model.MainObject.CTRLNahkampfwaffe.Data;
                        break;
                    case ThingDefs.Fernkampfwaffe:
                        ListView.ItemsSource = Model.MainObject.CTRLFernkampfwaffe.Data;
                        break;
                    case ThingDefs.Kommlink:
                        ListView.ItemsSource = Model.MainObject.CTRLKommlink.Data;
                        break;
                    case ThingDefs.CyberDeck:
                        ListView.ItemsSource = Model.MainObject.CTRLCyberDeck.Data;
                        break;
                    case ThingDefs.Vehikel:
                        ListView.ItemsSource = Model.MainObject.CTRLVehikel.Data;
                        break;
                    case ThingDefs.Panzerung:
                        ListView.ItemsSource = Model.MainObject.CTRLPanzerung.Data;
                        break;
                    case ThingDefs.Adeptenkraft:
                        ListView.ItemsSource = Model.MainObject.CTRLAdeptenkraft.Data;
                        break;
                    case ThingDefs.Geist:
                        ListView.ItemsSource = Model.MainObject.CTRLGeist.Data;
                        break;
                    case ThingDefs.Foki:
                        ListView.ItemsSource = Model.MainObject.CTRLFoki.Data;
                        break;
                    case ThingDefs.Stroemung:
                        ListView.ItemsSource = Model.MainObject.CTRLStroemung.Data;
                        break;
                    case ThingDefs.Tradition:
                        ListView.ItemsSource = Model.MainObject.CTRLTradition.Data;
                        break;
                    case ThingDefs.Zaubersprueche:
                        ListView.ItemsSource = Model.MainObject.CTRLZaubersprueche.Data;
                        break;
                    case ThingDefs.KomplexeForm:
                        ListView.ItemsSource = Model.MainObject.CTRLKomplexeForm.Data;
                        break;
                    case ThingDefs.Sprite:
                        ListView.ItemsSource = Model.MainObject.CTRLSprite.Data;
                        break;
                    case ThingDefs.Widgets:
                        ListView.ItemsSource = Model.MainObject.CTRLWidgets.Data;
                        break;
                    case ThingDefs.Wandlung:
                        ListView.ItemsSource = Model.MainObject.CTRLWandlung.Data;
                        break;
                    case ThingDefs.Initiation:
                        ListView.ItemsSource = Model.MainObject.CTRLInitiation.Data;
                        break;
                    default:
                        break;
                }
                if (ThingType == ThingDefs.Vorteil || ThingType == ThingDefs.Nachteil)
                {
                    HeadLine.ContentTemplate = Eigenschaft_E;
                }
                else
                {
                    HeadLine.ContentTemplate = (DataTemplate)Resources[Current.DisplayName + "_E"];
                }
            }
            catch (Exception ex)
            {

            }
      
        }

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


        internal double GetPositionAtListView(Thing PendingScrollEntry)
        {
            double offset = 0;
            foreach (ListViewItem item in ListView.ItemsPanelRoot.Children)
            {
                if ((item.Content).Equals(PendingScrollEntry))
                {
                    break;
                }
                else
                {
                    if (ListView.ItemsPanelRoot.Children.Last() == item)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    offset += item.DesiredSize.Height;
                }
            }
            return offset;
        }

        internal void Select(Thing PendingScrollEntry)
        {
            ListView.SelectedItem = PendingScrollEntry;
        }
    }
}
