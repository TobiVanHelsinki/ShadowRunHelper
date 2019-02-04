using ShadowRunHelper;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TAPPLICATION.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharView : ContentView
    {
        public AppModel Model => AppModel.Instance;
        IEnumerable<StackLayout> ButtonsPanels;

        public CharView()
        {
            InitializeComponent();
            SizeChanged += Page_SizeChanged;
            ButtonsPanels = new List<StackLayout>() { s1, s2, s3, s4, s5 };

            Model.PropertyChanged += (x, y) => InitButtons();

            BindingContext = this;
            ChangeUi();
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
                var bs1 = new Button
                {
                    Text = "Favorites"
                };
                bs1.Clicked += B_Fav_Clicked;
                s5.Children.Add(bs1);
                var bs2 = new Button
                {
                    Text = "Additional Info"
                };
                bs2.Clicked += B_Notes_Clicked;
                s5.Children.Add(bs2);
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

        private void B_Fav_Clicked(object sender, EventArgs e)
        {
            Favs.IsVisible = !Favs.IsVisible;
            if (Narrow)
            {
                Open = false;
            }
        }

        private void B_Notes_Clicked(object sender, EventArgs e)
        {
            Notes.IsVisible = !Notes.IsVisible;
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
        private void ChangeUi()
        {
            if (Narrow)
            {
                if (Open)
                {
                    LayerContent_Col0.Width = new GridLength(1, GridUnitType.Star);
                    LayerContent_Col1.Width = new GridLength(0, GridUnitType.Absolute);
                    LayerContent_Col2.Width = new GridLength(1, GridUnitType.Star);
                    HideFrame.IsVisible = true;
                }
                else
                {
                    LayerContent_Col0.Width = new GridLength(0, GridUnitType.Absolute);
                    LayerContent_Col1.Width = new GridLength(1, GridUnitType.Star);
                    LayerContent_Col2.Width = new GridLength(0, GridUnitType.Absolute);
                    HideFrame.IsVisible = false;
                }
            }
            else
            {
                if (Open)
                {
                    LayerContent_Col0.Width = new GridLength(1, GridUnitType.Auto);
                    LayerContent_Col1.Width = new GridLength(1, GridUnitType.Star);
                    LayerContent_Col2.Width = new GridLength(1, GridUnitType.Auto);
                    HideFrame.IsVisible = false;
                }
                else
                {
                    LayerContent_Col0.Width = new GridLength(0, GridUnitType.Absolute);
                    LayerContent_Col1.Width = new GridLength(1, GridUnitType.Star);
                    LayerContent_Col2.Width = new GridLength(0, GridUnitType.Absolute);
                    HideFrame.IsVisible = false;
                }
            }
        }
        private void Page_SizeChanged(object sender, EventArgs e)
        {
            Narrow = Width < 550;
            if (Width > 550)
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

        private void Toggle(object sender, EventArgs e)
        {
            Open = !Open;
        }
        #endregion
        async void ChooseFile(object sender, EventArgs e)
        {
            try
            {
                var File = await SharedIO.CurrentIO.PickFile(Constants.LST_FILETYPES_CHAR, "NextChar");
                AppModel.Instance.MainObject = await CharHolderIO.Load(File);
            }
            catch (Exception)
            {
            }
        }
    }
}