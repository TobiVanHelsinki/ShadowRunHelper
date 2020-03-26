//Author: Tobi van Helsinki

using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Rg.Plugins.Popup.Services;
using ShadowRunHelper;
using ShadowRunHelper.CharController;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.UI.Pages;
using SharedCode.Ressourcen;
using TLIB;
using Xam.Plugin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentView"/>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged"/>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GController : ContentView, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public new event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion NotifyPropertyChanged

        public CharHolder MyChar { get; set; }
        public CategoryOption MyControllerSettings { get; private set; }

        private IController _MyController;
        public IController MyController
        {
            get => _MyController;
            set { if (_MyController != value) { _MyController = value; NotifyPropertyChanged(); OnControllerChanged(); } }
        }

        public GController(CharHolder myChar)
        {
            MyChar = myChar;
            InitializeComponent();
            OnControllerChanged();
            BindingContextChanged += GController_BindingContextChanged;
            SetHeaderVisible(!SettingsModel.I.MINIMIZED_HEADER);
            MainPage.Instance.ViewModeChanged += MainPage_ViewModeChanged;
            Items.DragDropController.UpdateSource = true;
        }

        #region Control MyController, Items Visual and Header Visual

        private void GController_BindingContextChanged(object sender, EventArgs e)
        {
            MyController = BindingContext as IController;
        }

        private void OnControllerChanged()
        {
            DetailsOpen = false;
            if (MyController != null)
            {
                if (MyController.eDataTyp.HierarchieUpSearch(s => { _ = Resources.TryGetValue(s + "_H", out var CustomTemplate); return CustomTemplate; }) is ControlTemplate HL)
                {
                    Items_H.ControlTemplate = HL;
                }

                MyControllerSettings = MyChar.Settings.CategoryOptions.FirstOrDefault(x => x.ThingType == MyController.eDataTyp);
                IsVisible = MyControllerSettings.Visibility;
                Headline.Text = TypeHelper.ThingDefToString(MyController.eDataTyp, true);
                SetHeaderVisible(!SettingsModel.I.MINIMIZED_HEADER);
            }
            else
            {
                IsVisible = false;
            }
        }

        #endregion Control MyController, Items Visual and Header Visual

        #region Details
        private bool _DetailsOpen;
        /// <summary>
        /// Controls the UI State dependend at the Details Status
        /// </summary>
        public bool DetailsOpen
        {
            get => _DetailsOpen;
            set { _DetailsOpen = value; SetViewMode(MainPage.Instance.CurrentViewMode); }
        }

        public void SetViewMode(ViewModes newMode)
        {
            if (DetailsOpen)
            {
                switch (newMode)
                {
                    case ViewModes.NotSet:
                        break;
                    case ViewModes.Wide:
                        Items.IsVisible = true;
                        Header.IsVisible = true;
                        DetailRow.Height = new GridLength(0);
                        DetailColumn.Width = new GridLength(1, GridUnitType.Star);
                        Grid.SetColumn(DetailsPane, 1);
                        Grid.SetRow(DetailsPane, 1);
                        Grid.SetColumnSpan(DetailsPane, 1);
                        Grid.SetRowSpan(DetailsPane, 1);
                        DetailsPane.IsVisible = true;
                        DetailsPane.IsVisible = true;
                        break;
                    case ViewModes.Tall:
                        Items.IsVisible = true;
                        Header.IsVisible = true;
                        DetailRow.Height = new GridLength(1.5, GridUnitType.Star);
                        DetailColumn.Width = new GridLength(0);
                        Grid.SetColumn(DetailsPane, 0);
                        Grid.SetRow(DetailsPane, 2);
                        Grid.SetColumnSpan(DetailsPane, 1);
                        Grid.SetRowSpan(DetailsPane, 1);
                        DetailsPane.IsVisible = true;
                        DetailsPane.IsVisible = true;
                        break;
                    case ViewModes.Mobile:
                        Items.IsVisible = false;
                        Header.IsVisible = false;
                        Grid.SetColumn(DetailsPane, 0);
                        Grid.SetRow(DetailsPane, 0);
                        Grid.SetColumnSpan(DetailsPane, 2);
                        Grid.SetRowSpan(DetailsPane, 3);
                        DetailsPane.IsVisible = true;
                        DetailsPane.IsVisible = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Items.IsVisible = true;
                Header.IsVisible = true;
                DetailRow.Height = new GridLength(0);
                DetailColumn.Width = new GridLength(0);
                Grid.SetColumnSpan(DetailsPane, 1);
                Grid.SetRowSpan(DetailsPane, 2);
                DetailsPane.IsVisible = false;
                Items.SelectedItem = null;
                DetailsPane.IsVisible = false;
            }
        }

        private void MainPage_ViewModeChanged(ViewModes oldMode, ViewModes newMode) => SetViewMode(newMode);

        /// <summary>
        /// Handles the SelectionChanged event of the Items control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="Xamarin.Forms.SelectionChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void Items_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            if (e.AddedItems.FirstOrDefault() is Thing t)
            {
                ActivateDetails(t);
            }
        }

        public void ActivateDetails(Thing t)
        {
            DetailsOpen = true;
            DetailsPane.Activate(t, MyChar);
        }

        internal bool OnBackButtonPressed()
        {
            if (DetailsOpen)
            {
                DetailsOpen = false;
                return true;
            }
            return false;
        }

        private void DetailsPane_ClosingRequested(object sender, EventArgs e)
        {
            DetailsOpen = false;
        }

        #endregion Details

        #region Controller Actions

        private void Add(object sender, EventArgs e)
        {
            try
            {
                var thing = MyChar.Add(MyController.eDataTyp);
                if (SettingsModel.I.START_AFTER_EDIT)
                {
                    DetailsOpen = true;
                    DetailsPane.Activate(thing, MyChar);
                }
            }
            catch (NotSupportedException) { }
            catch (Exception ex)
            {
                Log.Write("Could not add object", ex, logType: LogType.Error);
            }
        }

        private (string, Action)[] MenuItems => new (string, Action)[] {
                        (UiResources.Cat_AddSep,()=>{try
                                                    {
                                                        var newThing  = Activator.CreateInstance(MyController.eDataTyp.ThingDefToType()) as Thing;
                                                        newThing.IsSeperator = true;
                                                        MyChar.Add(newThing);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Log.Write("", ex);
                                                    }}),
                        //(UiResources.Cat_UncheckAll,()=>{ }),
                        (UiResources.Cat_Order_ABC,()=>MyController.OrderData(Ordering.ABC)),
                        (UiResources.Cat_Order_Type,()=>MyController.OrderData(Ordering.Type)),
                        (UiResources.Cat_Order_Save,()=> MyController.SaveCurrentOrdering()),
                        (UiResources.Cat_Order_Orig,()=> MyController.OrderData(Ordering.Original)),
                        //(UiResources.CSV_Cat_ExportX,()=>{ }),
                        //(UiResources.CSV_Cat_Export_Selected,()=>{ }),
                        //(UiResources.CSV_Cat_ImportX,()=>{ }),
                    };

        private void Options(object sender, EventArgs e)
        {
            try
            {
                var Popup = new PopupMenu
                {
                    ItemsSource = MenuItems.Select(x => x.Item1).ToArray()
                };
                Popup.OnItemSelected += Popup_OnItemSelected;
                Popup?.ShowPopup(sender as Button);
            }
            catch (Exception)
            {
            }
        }

        private void Popup_OnItemSelected(string item)
        {
            MenuItems.FirstOrDefault(x => x.Item1 == item).Item2?.Invoke();
        }
        #endregion Controller Actions

        #region Items Actions

        private void Items_SwipeEnded(object sender, Syncfusion.ListView.XForms.SwipeEndedEventArgs e)
        {
            if (e.SwipeOffset > 70)
            {
                if (e.SwipeDirection == Syncfusion.ListView.XForms.SwipeDirection.Left && e.ItemData is Thing t)
                {
                    t.IsFavorite = !t.IsFavorite;
                }
                e.SwipeOffset = 0;
            }
        }
        #endregion Items Actions

        #region Dynamic Header

        public void SetHeaderVisible(bool visible)
        {
            bool supportsEdit = true;
            try
            {
                if (MyController?.GetType().GetCustomAttributes(typeof(ShadowRunHelperControllerAttribute), true)?.FirstOrDefault() is ShadowRunHelperControllerAttribute shadowRunHelperControllerAttribute)
                {
                    supportsEdit = shadowRunHelperControllerAttribute.SupportsEdit;
                }
            }
            catch (Exception ex)
            {
                Log.Write("Getting Controller Attribute failed", ex);
            }
            CatAddButton.IsVisible = visible && supportsEdit;
            CatMoreButton.IsVisible = visible && supportsEdit;
            Items_H.IsVisible = visible;
            Headline.FontSize = Device.GetNamedSize(visible ? NamedSize.Medium : NamedSize.Micro, typeof(Label));
            SettingsModel.I.MINIMIZED_HEADER = !visible;
        }

        private void HeaderSwitch(object sender, EventArgs e)
        {
            SetHeaderVisible(!Items_H.IsVisible);
        }

        private void HeaderHide(object sender, SwipedEventArgs e)
        {
            SetHeaderVisible(false);
        }

        private void HeaderShow(object sender, SwipedEventArgs e)
        {
            SetHeaderVisible(true);
        }

        private void HeaderShow(object sender, EventArgs e)
        {
            SetHeaderVisible(true);
        }
        #endregion Dynamic Header
    }
}