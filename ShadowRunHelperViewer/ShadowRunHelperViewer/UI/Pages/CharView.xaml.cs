using ShadowRunHelper;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharView : ContentView
    {
        public AppModel Model => AppModel.Instance;
        IEnumerable<StackLayout> ButtonsPanels;
        static CharView Instance;
        public CharView()
        {
            Instance = this;
            InitializeComponent();
            ButtonsPanels = new List<StackLayout>() { s1, s2, s3, s4, s5 };

            SizeChanged += (a, b) => SetViewParameters();
            Model.PropertyChanged += (x, y) => {
                InitButtons(); SetViewParameters(); Open = true;
            };
            Open = false;
            BindingContext = this;
        }

        #region Menu Buttons
        private void InitButtons()
        {
            if (this is ContentView Content)
            {
                foreach (var item in ButtonsPanels)
                {
                    item.Children.Clear();
                }
                foreach (var item in new (string, Layout)[] {("Favorites", Favs), ("Notes", Notes), ("Person", null) })
                {
                    var btt = new Button
                    {
                        Padding = new Thickness(2),
                        Text = item.Item1,
                        BindingContext = item.Item2
                    };
                    btt.Clicked += B_More_Clicked;
                    s5.Children.Add(btt);
                }
                foreach (var Category in Model.MainObject.Settings.CategoryOptions.OrderBy(x => x.Pivot).ThenBy(x => x.Order))
                {
                    var b = new Button
                    {
                        Padding = new Thickness(2),
                        BindingContext = Category.ThingType,
                        Text = Category.ThingType.ThingDefToString(true),
                        IsVisible = Category.Visibility,
                    };
                    switch (Category.Pivot)
                    {
                        case 0:
                            s1.Children.Add(b);
                            break;
                        case 1:
                            s2.Children.Add(b);
                            break;
                        case 2:
                            s3.Children.Add(b);
                            break;
                        case 3:
                            s4.Children.Add(b);
                            break;
                        default:
                            break;
                    }
                    b.Clicked += B_CTRL_Clicked;
                }
            }
        }

        private void B_More_Clicked(object sender, EventArgs e)
        {
            if (sender is Button b)
            {
                if (b.BindingContext is Layout panel)
                {
                    panel.IsVisible = !panel.IsVisible;
                }
            }
            if (Narrow)
            {
                Open = false;
            }
        }

        private void B_CTRL_Clicked(object sender, EventArgs e)
        {
            if (sender is VisualElement b && b.BindingContext is ThingDefs type)
            {
                var gc = new GController();
                gc.SetBinding(BindingContextProperty, new Binding($"{nameof(Model)}.{nameof(Model.MainObject)}.CTRL{type.ThingDefToString(false)}"));
                ControllerPanel.Children.Clear();
                ControllerPanel.Children.Add(gc);
                if (Narrow)
                {
                    Open = false;
                }
            }
        }

        #endregion
        #region AdaptiveUI

        bool _Narrow;
        public bool Narrow
        {
            get { return _Narrow; }
            set { if (_Narrow != value) { _Narrow = value; ChangeUi(); } }
        }
        bool _Open = true;
        public bool Open
        {
            get { return _Open; }
            set { if (_Open != value) { _Open = value; ChangeUi(); } }
        }
        public static bool? StaticOpen
        {
            get { return Instance?.Open; }
            set
            {
                if (Instance != null && value is bool b)
                {
                    Instance.Open = b;
                }
            }
        }
        private void ChangeUi()
        {
            if (Narrow)
            {
                if (Open)
                {
                    LayerContent_Col0.Width = new GridLength(1, GridUnitType.Star);
                    LayerContent_Col1.Width = new GridLength(0, GridUnitType.Absolute);
                    LayerContent_Col2.Width = new GridLength(1, GridUnitType.Star);
                    ContentPanel.IsVisible = false;
                }
                else
                {
                    LayerContent_Col0.Width = new GridLength(0, GridUnitType.Absolute);
                    LayerContent_Col1.Width = new GridLength(1, GridUnitType.Star);
                    LayerContent_Col2.Width = new GridLength(0, GridUnitType.Absolute);
                    ContentPanel.IsVisible = true;
                }
            }
            else
            {
                if (Open)
                {
                    LayerContent_Col0.Width = new GridLength(1, GridUnitType.Auto);
                    LayerContent_Col1.Width = new GridLength(1, GridUnitType.Star);
                    LayerContent_Col2.Width = new GridLength(1, GridUnitType.Auto);
                    ContentPanel.IsVisible = true;
                }
                else
                {
                    LayerContent_Col0.Width = new GridLength(0, GridUnitType.Absolute);
                    LayerContent_Col1.Width = new GridLength(1, GridUnitType.Star);
                    LayerContent_Col2.Width = new GridLength(0, GridUnitType.Absolute);
                    ContentPanel.IsVisible = true;
                }
            }
        }
        void SetViewParameters()
        {
            Narrow = Width < 550;
            if (Width > 550 && Model.MainObject != null)
            {
                Open = true;
            }
            else
            {
                if (ControllerPanel.Children.Any(x=>x.IsVisible))
                {
                    Open = false;
                }
            }
        }


        #endregion
    }
}