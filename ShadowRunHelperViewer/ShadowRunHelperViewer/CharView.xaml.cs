using ShadowRunHelper;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TAPPLICATION.IO;
using TLIB;
using Xam.Plugin.TabView;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharView : ContentView
    {
        public CharView()
        {
            //BindingContextChanged += CategoryView_BindingContextChanged_AddCategoriesProgramamticly;
            BindingContextChanged += CategoryView_BindingContextChanged_AddCategoriesButtonsProgramamticly;
            InitializeComponent();
            ButtonsPanels = new List<StackLayout>() { s1, s2, s3, s4 };
        }

        IEnumerable<StackLayout> ButtonsPanels;
        IEnumerable<Button> Buttons => ButtonsPanels.SelectMany(x => x.Children).OfType<Button>();

        private void CategoryView_BindingContextChanged_AddCategoriesButtonsProgramamticly(object sender, EventArgs e)
        {
            foreach (var item in Controllers.Children)
            {
                item.IsVisible = false;
            }
            if (this is ContentView Content)
            {
                foreach (var item in Buttons)
                {
                    item.Clicked -= B_Clicked;
                }
                foreach (var item in ButtonsPanels)
                {
                    item.Children.Clear();
                }
                var bs1 = new Button
                {
                    Text = "Favorites" + " (" + (BindingContext as CharHolder)?.Favorites?.Count() + ")"
                };
                bs1.Clicked += Bs1_Clicked;
                s1.Children.Add(bs1);
                var bs2 = new Button
                {
                    Text = "Additional Info"
                };
                bs2.Clicked += Bs2_Clicked;
                s1.Children.Add(bs2);
                foreach (var Category in TypeHelper.ThingTypeProperties.Where(x => x.Usable).OrderBy(x => x.Pivot).ThenBy(x => x.Order))
                {
                    var gc = Controllers.Children.OfType<GController>().FirstOrDefault(x=>x.Controller.eDataTyp == Category.ThingType);
                    var b = new Button
                    {
                        Text = CustomManager.GetString(Category.DisplayNamePlural) + " (" + gc?.Controller?.GetElements().Count() + ")"
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
                    b.Resources.Add("GC",gc);
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
            object gc = null;
            (sender as Button)?.Resources?.TryGetValue("GC", out gc);
            if (gc is GController GC)
            {
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