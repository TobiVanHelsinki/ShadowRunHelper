using ShadowRunHelper.CharController;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelper.UI.Edit;
using System;
using System.Collections.Generic;
using System.Linq;
using TLIB_UWPFRAME.IO;
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
                    (item.ContentTemplateRoot as CategoryEntry).ExpandedTemplate();
                }
                var RemovedItems = (sender as ListView).ItemsPanelRoot.Children.Cast<ListViewItem>().Where(c => e.RemovedItems.Contains(c.Content));
                foreach (var item in RemovedItems)
                {
                    (item.ContentTemplateRoot as CategoryEntry).DefaultTemplate();
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
        #endregion

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

    }
}
