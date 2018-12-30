using ShadowRunHelper;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TLIB;
using Xam.Plugin.TabView;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryView : ContentView
    {
        public CategoryView()
        {
            //BindingContextChanged += CategoryView_BindingContextChanged_AddCategoriesProgramamticly;
            BindingContextChanged += CategoryView_BindingContextChanged_AddCategoriesButtonsProgramamticly;
            InitializeComponent();
            Buttons = new List<StackLayout>() { s1, s2, s3, s4 };
        }

        IEnumerable<StackLayout> Buttons;

        private void CategoryView_BindingContextChanged_AddCategoriesButtonsProgramamticly(object sender, EventArgs e)
        {
            foreach (var item in Controllers.Children)
            {
                item.IsVisible = false;
            }
            if (this is ContentView Content)
            {
                foreach (var item in Buttons.SelectMany(x=>x.Children).OfType<Button>())
                {
                    item.Clicked -= B_Clicked;
                }
                foreach (var item in Buttons)
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

        //private void CategoryView_BindingContextChanged_AddCategoriesProgramamticly(object sender, System.EventArgs e)
        //{
        //    if (BindingContext is CharHolder charHolder)
        //    {
        //        int count = TableView.ItemSource.Count();
        //        foreach (var Pivot in TypeHelper.ThingTypeProperties.Where(x => x.Usable).Select(x => x.Pivot).Distinct().OrderBy(x => x)) // Pivot
        //        {
        //            var Stack = new StackLayout();
        //            Stack.SetBinding(GController.BindingContextProperty, "this");

        //            TabItem tab = new TabItem(Pivot.ToString(), Stack);
        //            tab.SetBinding(GController.BindingContextProperty, "this");
        //            TableView.AddTab(tab);
        //            foreach (var Category in TypeHelper.ThingTypeProperties.Where(x => x.Usable).Where(x => x.Pivot == Pivot).OrderBy(x => x.Order))
        //            {
        //                GController item = new GController();
        //                Stack.Children.Add(item);
        //                var ctrl = charHolder.CTRLList.FirstOrDefault(x => x.eDataTyp == Category.ThingType);
        //                var prop = charHolder.GetType().GetProperties().Where(x => x.GetValue(charHolder) == ctrl).FirstOrDefault();
        //                item.SetBinding(GController.BindingContextProperty, prop?.Name);
        //                //{ BindingContext = charHolder.CTRLList.FirstOrDefault(x => x.eDataTyp == Category.ThingType) };
        //            }
        //        }
        //        for (int i = 0; i < count; i++) // need to remove old items at bottom, because view cannot have zero items
        //        {
        //            TableView.RemoveTab();
        //        }
        //    }
        //}
    }
}