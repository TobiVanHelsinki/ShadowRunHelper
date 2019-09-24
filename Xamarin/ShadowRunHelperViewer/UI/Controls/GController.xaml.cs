using Rg.Plugins.Popup.Services;
using ShadowRunHelper;
using ShadowRunHelper.CharController;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        #endregion
        public CharHolder MyChar { get; set; }
        #region Hold IController
        IController _Controller;
        public IController Controller
        {
            get { return _Controller; }
            set { if (_Controller != value) { _Controller = value; NotifyPropertyChanged(); OnControllerChanged(); } }
        }
        private void GController_BindingContextChanged(object sender, EventArgs e)
        {
            Controller = BindingContext as IController;
        }

        private void OnControllerChanged()
        {
            if (Controller != null)
            {
                Controller.RegisterEventAtData(SmthChanged);
                CreateItems(Controller.eDataTyp);
                CreateHeadline(Controller.eDataTyp);

                Setting = MyChar.Settings.CategoryOptions.FirstOrDefault(x => x.ThingType == Controller.eDataTyp);
                IsVisible = Setting.Visibility;
                Headline.Text = TypeHelper.ThingDefToString(Controller.eDataTyp, true);
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
            var ancestor = key.ThingDefToType();
            object CustomTemplate;
            do
            {
                Resources.TryGetValue(ancestor.Name + "_H", out CustomTemplate);
                ancestor = key.ThingDefToType().BaseType;
            }
            while (CustomTemplate is null);
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
        private object CreateItems(ThingDefs key)
        {
            var ancestor = key.ThingDefToType();
            object CustomTemplate;
            do 
            {
                Resources.TryGetValue(ancestor.Name, out CustomTemplate);
                ancestor = key.ThingDefToType().BaseType;
            }
            while (CustomTemplate is null);
            if (CustomTemplate is DataTemplate DT)
            {
                var section = new TableSection();
                //var section = new List<ViewCell>();
                //Items.ItemsSource = section;
             
                Items.Root = new TableRoot() { section };
                Items.Margin = new Thickness(0, Device.OnPlatform(-35, -35, -40), 0, 0);
                foreach (var item in Controller.GetElements())
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
                    vc.BindingContext = item;
                    vc.Tapped += ItemCell_Tapped;
                    section.Add(vc);
                }
            }
            return CustomTemplate;
        }

        private void SmthChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is INotifyCollectionChanged list)
            {
                CreateItems(Controller.eDataTyp);
            }
        }

        private void ItemCell_Tapped(object sender, EventArgs e)
        {
            if (sender is ViewCell vc)
            {
                string key = TypeHelper.ThingDefToString(Controller.eDataTyp, false);
                Resources.TryGetValue(key + "X", out var CustomTemplate);
                if (CustomTemplate is null)
                {
                    Resources.TryGetValue(nameof(Thing)+ "X", out CustomTemplate);
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
        #endregion

        public CategoryOption Setting { get; private set; }

        public GController(CharHolder myChar)
        {
            MyChar = myChar;
            InitializeComponent();
            OnControllerChanged();
            BindingContextChanged += GController_BindingContextChanged;
        }

        private void Add(object sender, EventArgs e)
        {
            MyChar.Add(Controller.eDataTyp);
        }

        async void Thing_Edit(object sender, EventArgs e)
        {
            if (sender is BindableObject b && b.BindingContext is Thing t)
            {
                await PopupNavigation.Instance.PushAsync(new DetailsPage(t));
            }
        }

        private void Thing_Delete(object sender, EventArgs e)
        {
            if (sender is BindableObject b && b.BindingContext is Thing t)
            {
                MyChar.Remove(t);
            }
        }

        (string, Action)[] MenuItems = new (string, Action)[] {
                        (CustomManager.GetString("UI_TxT_Cat_AddSep/Text"),null),
                        (CustomManager.GetString("UI_TxT_Cat_UncheckAll/Text"),null),
                        (CustomManager.GetString("UI_TxT_Cat_Order_ABC/Text"),null),
                        (CustomManager.GetString("UI_TxT_Cat_Order_Type/Text"),null),
                        (CustomManager.GetString("UI_TxT_Cat_Order_Save/Text"),null),
                        (CustomManager.GetString("UI_TxT_Cat_Order_Orig/Text"),null),
                        (CustomManager.GetString("UI_TxT_CSV_Cat_Export/Text"),null),
                        (CustomManager.GetString("UI_TxT_CSV_Cat_Export_Selected/Text"),null),
                        (CustomManager.GetString("UI_TxT_CSV_Cat_Import/Text"),null),
                        (CustomManager.GetString("UI_TxT_Cat_Truncate/Text"),null),
                    };

        private void Options(object sender, EventArgs e)
        {
            try
            {
                PopupMenu Popup = new PopupMenu
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
    }
}