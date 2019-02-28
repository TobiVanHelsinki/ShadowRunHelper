using ShadowRunHelper.CharController;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.IO;
using System.Linq;
using TAPPLICATION.IO;
using TLIB;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper.UI
{
    public sealed partial class CategoryBlock : UserControl
    {
        readonly AppModel Model = AppModel.Instance;
        public static readonly DependencyProperty ControllerProperty =
                   DependencyProperty.Register("Controller", typeof(IController), typeof(CategoryBlock), new PropertyMetadata(null));
        public IController Controller
        {
            get { return (IController)GetValue(ControllerProperty); }
            set { SetValue(ControllerProperty, value); OnControllerChanged(); }
        }

        CategoryOption CTRLOption { get; set; }
        public void CheckVisibility()
        {
            Visibility = (CTRLOption?.Visibility == false) ? Visibility.Collapsed : Visibility.Visible;
        }
        private void OnControllerChanged()
        {
            try
            {
                ListView.Tag = (int)Controller.eDataTyp;
                DataContext = Controller;

                CTRLOption = AppModel.Instance?.MainObject.Settings.CategoryOptions.FirstOrDefault(x => x.ThingType == Controller.eDataTyp);
                if (CTRLOption != null)
                {
                    CTRLOption.PropertyChanged += (o, e) => { if (e.PropertyName == nameof(CTRLOption.Visibility)) CheckVisibility(); };
                }
                CheckVisibility();

                var Current = TypeHelper.ThingTypeProperties.FirstOrDefault(t => t.ThingType == Controller.eDataTyp);

                CategoryName.Text = CustomManager.GetString(Current.DisplayNamePlural);
              
                if (Controller.eDataTyp == ThingDefs.Vorteil || Controller.eDataTyp == ThingDefs.Nachteil)
                {
                    HeadLine.ContentTemplate = Eigenschaft_E;
                }
                else if (Controller.eDataTyp == ThingDefs.Stroemung || Controller.eDataTyp == ThingDefs.Wandlung ||
                    Controller.eDataTyp == ThingDefs.Tradition || Controller.eDataTyp == ThingDefs.Initiation)
                {
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

        public CategoryBlock()
        {
            InitializeComponent();
        }

        #region CategoryStuff
        private async void UI_TxT_CSV_Cat_Import_Click(object sender, RoutedEventArgs e)
        {
            string strRead = "";
            var CTRL = (sender as FrameworkElement).DataContext as IController;
            try
            {
                var file = await SharedIO.CurrentIO.PickFile(Constants.LST_FILETYPES_CSV, Constants.ACCESSTOKEN_IMPORT);
                strRead = await SharedIO.CurrentIO.LoadFileContent(file);
            }
            catch (IsOKException ex)
            {
                return;
            }
            catch (Exception ex)
            {
                Model.NewNotification(CustomManager.GetString("Notification_Error_CSVImportFail") + "1", ex);
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
                Model.NewNotification(CustomManager.GetString("Notification_Error_CSVImportFail") + "2", ex);
            }
            Features.Analytics.TrackEvent("Char_UI_TxT_CSV_Cat_Import");
        }

        async void UI_TxT_CSV_Cat_Export_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string output = Controller.Data2CSV(';', '\n');
                var targetdir = await SharedIO.CurrentIO.PickFolder(Constants.ACCESSTOKEN_EXPORT);
                var targetfile = new FileInfo(Path.Combine(targetdir.FullName, TypeHelper.ThingDefToString(Controller.eDataTyp, true) + Constants.DATEIENDUNG_CSV));
                SharedIO.CurrentIO.SaveFileContent(output, targetfile);
            }
            catch (IsOKException ex)
            {
                return;
            }
            catch (Exception ex)
            {
                Model.NewNotification(CustomManager.GetString("Notification_Error_CSVExportFail") + "2", ex);
            }
            Features.Analytics.TrackEvent("Char_UI_TxT_CSV_Cat_Export");
        }
        async void UI_TxT_CSV_Cat_Export_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected2 = ListView.SelectedItems.Select(i => i as Thing);
                string output = CSV_Converter.Data2CSV(';', '\n', selected2);
                var targetdir = await SharedIO.CurrentIO.PickFolder(Constants.ACCESSTOKEN_EXPORT);
                var targetfile = new FileInfo(Path.Combine(targetdir.FullName, TypeHelper.ThingDefToString(Controller.eDataTyp, true) + Constants.DATEIENDUNG_CSV));
                SharedIO.CurrentIO.SaveFileContent(output, targetfile);
            }
            catch (IsOKException ex)
            {
                return;
            }
            catch (Exception ex)
            {
                Model.NewNotification(CustomManager.GetString("Notification_Error_CSVExportFail") + "2", ex);
            }
            Features.Analytics.TrackEvent("Char_UI_TxT_CSV_Cat_Export_Selected");
        }

        void UI_TxT_Cat_UncheckAll(object sender, RoutedEventArgs e)
        {
            ListView.SelectedIndex = -1;
        }
        private void UI_TxT_Cat_Truncate(object sender, RoutedEventArgs e)
        {
            try
            {
                Controller.ClearData();
            }
            catch (Exception ex)
            {
                Model.NewNotification(CustomManager.GetString("Notification_Error_LogicFail"), ex);
            }
        }
        #endregion
        #region Ordering
        void UI_TxT_Cat_Order_Type(object sender, RoutedEventArgs e)
        {
            Features.Analytics.TrackEvent("Char_UI_TxT_Cat_Order_Type");
            var CTRL = ((sender as FrameworkElement).DataContext as IController);
            CTRL.OrderData(Ordering.Type);
        }

        void UI_TxT_Cat_Order_ABC(object sender, RoutedEventArgs e)
        {
            Features.Analytics.TrackEvent("Char_UI_TxT_Cat_Order_ABC");
            var CTRL = ((sender as FrameworkElement).DataContext as IController);
            CTRL.OrderData(Ordering.ABC);
        }
        private void UI_TxT_Cat_Order_Orig(object sender, RoutedEventArgs e)
        {
            Features.Analytics.TrackEvent("Char_UI_TxT_Cat_Order_Orig");
            var CTRL = ((sender as FrameworkElement).DataContext as IController);
            CTRL.OrderData(Ordering.Original);
        }
        private void UI_TxT_Cat_Order_Save(object sender, RoutedEventArgs e)
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

        async void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Thing newThing = Model.MainObject.Add(Controller.eDataTyp);
                if (SettingsModel.I.START_AFTER_EDIT)
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

        #region DnD
        private void ListView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            e.Data.RequestedOperation = DataPackageOperation.Move | DataPackageOperation.Copy;
            e.Data.Properties.Title = "Thing";

            CharHolder ToSend = new CharHolder();
            foreach (Thing item in e.Items)
            {
                ToSend.Add(item);
                Model.MainObject.PrepareToMoveOrCopy(item as Thing);
            }
            var txt = CharHolderIO.Serialize(ToSend);
            e.Data.SetText(txt);
            e.Data.Properties.ApplicationName = Features.InstanceHandling.InstanceKey;
        }

        private void ListView_DragOver(object sender, DragEventArgs e)
        {
            if (e?.DataView?.Properties?.Title == "Thing")
            {
                if (e.DataView.Properties.ApplicationName == Features.InstanceHandling.InstanceKey)
                {
                    e.AcceptedOperation = DataPackageOperation.Move;
                }
                else
                {
                    e.AcceptedOperation = DataPackageOperation.Copy;
                }
            }
            else
            {
                e.AcceptedOperation = DataPackageOperation.None;
            }
        }

        private async void ListView_Drop(object sender, DragEventArgs e) // 3
        {
            if (e?.DataView?.Properties?.ApplicationName == Features.InstanceHandling.InstanceKey) // bin ich selber
            {
                Model.MainObject.MovePreparedItems(Controller.eDataTyp);
            }
            else // kommt von einer anderen app
            {
                var txt = await e.DataView.GetTextAsync();
                var newch = CharHolderIO.Deserialize(txt);
                foreach (var item in newch.ThingList)
                {
                    Model.MainObject.Add(item);
                }
                Model.MainObject.Repair();
            }
            e.Handled = true;
        }
        //public static void ErrorHandler(object o, ErrorEventArgs a)
        //{
        //    a.ErrorContext.Handled = true;
        //}
        #endregion

        private void UI_TxT_Cat_AddSep(object sender, RoutedEventArgs e)
        {
            try
            {
                Thing newThing = Model.MainObject.Add(Controller.eDataTyp);
                newThing.Bezeichner = "";
                newThing.Wert = 1337;
            }
            catch (Exception ex)
            {
                Model.NewNotification("", ex);
            }
        }
    }
}
