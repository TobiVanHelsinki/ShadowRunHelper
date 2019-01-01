using ShadowRunHelper;
using ShadowRunHelper.CharController;
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
        public CharView()
        {
            InitializeComponent();
            SizeChanged += Page_SizeChanged;
            ButtonsPanels = new List<StackLayout>() { s1, s2, s3, s4 };
            InitButtons();
            BindingContext = this;

            foreach (var item in ControllerPanel.Children)
            {
                item.IsVisible = false;
            }
        }
        IEnumerable<StackLayout> ButtonsPanels;
        IEnumerable<Button> Buttons => ButtonsPanels.SelectMany(x => x.Children).OfType<Button>();
        IEnumerable<GController> Controllers => ControllerPanel.Children.OfType<GController>();

        private void RefreshButton(object sender, EventArgs e)
        {
            if (sender is GController gc && gc.Controller is IController controller)
            {
                var B = Buttons.FirstOrDefault(x => (x.BindingContext is ThingDefs d) ? controller.eDataTyp == d : false);
                if (B != null)
                {
                    B.Text = TypeHelper.ThingDefToString(controller.eDataTyp, true);
                    B.IsVisible = gc.Setting.Visibility;
                }
            }
        }

        private void InitButtons()
        {
            if (this is ContentView Content)
            {
                var bs1 = new Button
                {
                    Text = "Favorites"
                };
                bs1.Clicked += Bs1_Clicked;
                s5.Children.Add(bs1);
                var bs2 = new Button
                {
                    Text = "Additional Info"
                };
                bs2.Clicked += Bs2_Clicked;
                s5.Children.Add(bs2);
                foreach (var Category in Model.MainObject.Settings.CategoryOptions.OrderBy(x => x.Pivot).ThenBy(x => x.Order))
                {
                    var b = new Button
                    {
                        Padding = new Thickness(2),
                        BindingContext = Category.ThingType
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
                    b.Clicked += B_Clicked;
                }
            }
        }

        private void Bs1_Clicked(object sender, EventArgs e)
        {
            Favs.IsVisible = !Favs.IsVisible;
        }

        private void Bs2_Clicked(object sender, EventArgs e)
        {
            Notes.IsVisible = !Notes.IsVisible;
        }

        private void B_Clicked(object sender, EventArgs e)
        {
            if (sender is Button b && b.BindingContext is ThingDefs type)
            {
                var GC = Controllers.First(x=>x.Controller.eDataTyp == type);
                GC.IsVisible = !GC.IsVisible;
            }
        }
        private void Page_SizeChanged(object sender, EventArgs e)
        {
            if (Width > 550)
            {
                if (Open)
                {
                    ChangeUi_Wide_Open();
                }
                else
                {
                    ChangeUi_Wide_Close();
                }
            }
            else
            {
                if (Open)
                {
                    ChangeUi_Narrow_Open();
                }
                else
                {
                    ChangeUi_Narrow_Close();
                }
            }
        }
        bool Open = true;
        bool Narrow = false;

        private void ChangeUi_Narrow_Open()
        {
            Narrow = true;
            Open = true;
            LayerContent_Col0.Width = new GridLength(1, GridUnitType.Star);
            LayerContent_Col1.Width = new GridLength(0, GridUnitType.Absolute);
            LayerContent_Col2.Width = new GridLength(1, GridUnitType.Star);
            PaintFrame.BackgroundColor = Color.White;
        }
        private void ChangeUi_Narrow_Close()
        {
            Narrow = true;
            Open = false;
            LayerContent_Col0.Width = new GridLength(0, GridUnitType.Absolute);
            LayerContent_Col1.Width = new GridLength(1, GridUnitType.Star);
            LayerContent_Col2.Width = new GridLength(0, GridUnitType.Absolute);
            PaintFrame.BackgroundColor = Color.Transparent;
        }
        private void ChangeUi_Wide_Open()
        {
            Narrow = false;
            Open = true;
            LayerContent_Col0.Width = new GridLength(1, GridUnitType.Auto);
            LayerContent_Col1.Width = new GridLength(1, GridUnitType.Star);
            LayerContent_Col2.Width = new GridLength(1, GridUnitType.Auto);
            PaintFrame.BackgroundColor = Color.Transparent;
        }
        private void ChangeUi_Wide_Close()
        {
            Narrow = false;
            Open = false;
            LayerContent_Col0.Width = new GridLength(0, GridUnitType.Absolute);
            LayerContent_Col1.Width = new GridLength(1, GridUnitType.Star);
            LayerContent_Col2.Width = new GridLength(0, GridUnitType.Absolute);
            PaintFrame.BackgroundColor = Color.Transparent;
        }

        private void Toggle(object sender, EventArgs e)
        {
            if (Open)
            {
                if (Narrow)
                {
                    ChangeUi_Narrow_Close();
                }
                else
                {
                    ChangeUi_Wide_Close();
                }
            }
            else
            {
                if (Narrow)
                {
                    ChangeUi_Narrow_Open();
                }
                else
                {
                    ChangeUi_Wide_Open();
                }
            }
        }
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