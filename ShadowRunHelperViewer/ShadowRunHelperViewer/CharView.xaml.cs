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
            ButtonsPanels = new List<StackLayout>() { s1, s2, s3, s4 };
            InitButtons();
            BindingContext = this;

            foreach (var item in Controllers)
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
                s1.Children.Add(bs1);
                var bs2 = new Button
                {
                    Text = "Additional Info"
                };
                bs2.Clicked += Bs2_Clicked;
                s1.Children.Add(bs2);
                foreach (var Category in Model.MainObject.Settings.CategoryOptions.OrderBy(x => x.Pivot).ThenBy(x => x.Order))
                {
                    var b = new Button
                    {
                        BindingContext = Category.ThingType // TODO wohl doch die ressources nehmen
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

        private void Toggle(object sender, EventArgs e)
        {
            foreach (var item in Buttons)
            {
                item.IsVisible = !item.IsVisible;
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