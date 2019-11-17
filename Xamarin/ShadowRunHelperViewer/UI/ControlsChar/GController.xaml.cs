//Author: Tobi van Helsinki

using Rg.Plugins.Popup.Services;
using ShadowRunHelper;
using ShadowRunHelper.CharController;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.UI.ControlsOther;
using ShadowRunHelperViewer.UI.Resources;
using SharedCode.Ressourcen;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TLIB;
using Xam.Plugin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GController : ContentView, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

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

        #region Control MyController, Items Visual and Header Visual

        private void GController_BindingContextChanged(object sender, EventArgs e) => MyController = BindingContext as IController;

        private void OnControllerChanged()
        {
            if (MyController != null)
            {
                MyController.RegisterEventAtData(ControlerDataChanged);
                CreateItems(MyController.eDataTyp);
                CreateHeadline(MyController.eDataTyp);

                MyControllerSettings = MyChar.Settings.CategoryOptions.FirstOrDefault(x => x.ThingType == MyController.eDataTyp);
                IsVisible = MyControllerSettings.Visibility;
                Headline.Text = TypeHelper.ThingDefToString(MyController.eDataTyp, true);
                var att = MyController.GetType().GetCustomAttributes(typeof(ShadowRunHelperControllerAttribute), true).FirstOrDefault() as ShadowRunHelperControllerAttribute;
                if (att?.SupportsEdit == false)
                {
                    CatAddButton.IsVisible = false;
                    CatMoreButton.IsVisible = false;
                }
            }
            else
            {
                IsVisible = false;
            }
        }

        /// <summary>
        /// Searches for a Resource with a mathing name and try to create this as headline
        /// </summary>
        /// <param name="key"></param>
        private void CreateHeadline(ThingDefs key)
        {
            var CustomTemplate = key.HierarchieUpSearch(s => { Resources.TryGetValue(s + "_H", out var CustomTemplate); return CustomTemplate; });
            if (CustomTemplate is ControlTemplate HL)
            {
                Items_H.ControlTemplate = HL;
            }
        }

        /// <summary>
        /// Searches for a Resource with a mathing name and try to create all entries with that resource as template
        /// Fallback: ThingTempalte
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private void CreateItems(ThingDefs key)
        {
            var CustomTemplate = key.HierarchieUpSearch(s => { Resources.TryGetValue(s, out var CustomTemplate); return CustomTemplate; });
            if (CustomTemplate is DataTemplate DT)
            {
                var section = new TableSection("");
                Items.Root = new TableRoot() { section };
                foreach (var item in MyController.GetData())
                {
                    var content = DT.CreateContent();
                    ViewCell vc;
                    if (content is ViewCell vcn)
                    {
                        vc = vcn;
                    }
                    else if (content is View v)
                    {
                        vc = new ViewCell() { View = v };
                    }
                    else
                    {
                        break;
                    }
                    //vc.Appearing += Vc_Appearing;
                    //vc.Disappearing += Vc_Disappearing;
                    vc.BindingContext = item;
                    vc.Tapped += ItemCell_Tapped;
                    section.Add(vc);
                }
            }
        }

        private void ControlerDataChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is INotifyCollectionChanged)
            {
                CreateItems(MyController.eDataTyp);
            }
        }

        /// <summary>
        /// Toggles between normal and expandet state
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ItemCell_Tapped(object sender, EventArgs e)
        {
            if (sender is ViewCell vc && vc.BindingContext is Thing item)
            {
                var CustomTemplate = item.ThingType.HierarchieUpSearch(s => { Resources.TryGetValue(s + "X", out var CustomTemplate); return CustomTemplate; });
                if (CustomTemplate is null)
                {
                    Resources.TryGetValue(nameof(Thing) + "X", out CustomTemplate);
                }
                if (CustomTemplate is DataTemplate DT)
                {
                    if (vc.View.FindByName("Extended") is ContentView XView)
                    {
                        if (XView.Content is null)
                        {
                            if (DT.CreateContent() is View v)
                            {
                                XView.Content = v;
                            }
                        }
                        else
                        {
                            XView.Content = null;
                        }
                        vc.ForceUpdateSize();
                    }
                }
            }
        }
        #endregion Control MyController, Items Visual and Header Visual

        public GController(CharHolder myChar)
        {
            MyChar = myChar;
            InitializeComponent();
            CreateStyle();
            OnControllerChanged();
            BindingContextChanged += GController_BindingContextChanged;
            SetHeaderVisible(!SettingsModel.I.MINIMIZED_HEADER);
        }

        private void CreateStyle()
        {
            foreach (var (name, type, setter) in new[] {
                ("TemplateStack", typeof(StackLayout), new (BindableProperty, object)[] {
                    (StackLayout.SpacingProperty, 0),
                    (StackLayout.OrientationProperty, StackOrientation.Horizontal),
                    (MarginProperty, SettingsModel.I.CurrentSpacingStrategy),
                    (VerticalOptionsProperty, LayoutOptions.Center ),
                }),
                ("TemplateGrid", typeof(Grid), new (BindableProperty, object)[] {
                    (Grid.ColumnSpacingProperty, 0),
                    (Grid.RowSpacingProperty, 0),
                    (MarginProperty, SettingsModel.I.CurrentSpacingStrategy),
                    (VerticalOptionsProperty, LayoutOptions.Center ),
                }),
                ("SeparatorLine", typeof(BoxView), new (BindableProperty, object)[] {
                    (HeightRequestProperty, 1),
                    (MarginProperty, new Thickness(-5,5,-5,0)),
                    (BackgroundColorProperty, StyleManager.ForegroundColor),
                }),
                })
            {
                try
                {
                    var style = new Style(type);
                    foreach (var (prop, value) in setter)
                    {
                        style.Setters.Add(new Setter() { Property = prop, Value = value });
                    }
                    Resources.Add(name, style);
                }
                catch (Exception ex)
                {
                    Log.Write("Could not create resource", ex, logType: LogType.Error);
                }
            }
        }

        #region Controller Actions

        private void Add(object sender, EventArgs e)
        {
            try
            {
                var thing = MyChar.Add(MyController.eDataTyp);
                if (SettingsModel.I.START_AFTER_EDIT && thing.ThingType != ThingDefs.Note)
                {
                    PopupNavigation.Instance.PushAsync(new DetailsPage(thing, MyChar));
                }
            }
            catch (NotSupportedException) { }
            catch (Exception ex)
            {
                Log.Write("Could not add object", ex, logType: LogType.Error);
            }
        }

        private (string, Action)[] MenuItems => new (string, Action)[] {
                        (UiResources.Cat_AddSep,()=>{ }),
                        (UiResources.Cat_UncheckAll,()=>{ }),
                        (UiResources.Cat_Order_ABC,()=>{ }),
                        (UiResources.Cat_Order_Type,()=>{ }),
                        (UiResources.Cat_Order_Save,()=>{ }),
                        (UiResources.Cat_Order_Orig,()=>{ }),
                        (UiResources.CSV_Cat_ExportX,()=>{ }),
                        (UiResources.CSV_Cat_Export_Selected,()=>{ }),
                        (UiResources.CSV_Cat_ImportX,()=>{ }),
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

        private async void Thing_Edit(object sender, EventArgs e)
        {
            if (sender is BindableObject b && b.BindingContext is Thing t)
            {
                await PopupNavigation.Instance.PushAsync(new DetailsPage(t, MyChar));
            }
        }

        private void Thing_Delete(object sender, EventArgs e)
        {
            if (sender is BindableObject b && b.BindingContext is Thing t)
            {
                MyChar.Remove(t);
            }
        }

        #endregion Items Actions

        #region Dynamic Header

        public void SetHeaderVisible(bool visible)
        {
            Items_H.IsVisible = visible;
            CatAddButton.IsVisible = visible;
            CatMoreButton.IsVisible = visible;
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