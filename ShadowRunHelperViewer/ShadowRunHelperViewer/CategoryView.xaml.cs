using ShadowRunHelper;
using ShadowRunHelper.Model;
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
            //BindingContextChanged += CategoryView_BindingContextChanged;
            InitializeComponent();
        }

        private void CategoryView_BindingContextChanged(object sender, System.EventArgs e)
        {
            if (BindingContext is CharHolder charHolder)
            {
                int count = TableView.ItemSource.Count();
                foreach (var Pivot in TypeHelper.ThingTypeProperties.Where(x => x.Usable).Select(x => x.Pivot).Distinct().OrderBy(x => x)) // Pivot
                {
                    var Stack = new StackLayout();
                    Stack.SetBinding(GController.BindingContextProperty, "this");

                    TabItem tab = new TabItem(Pivot.ToString(), Stack);
                    tab.SetBinding(GController.BindingContextProperty, "this");
                    TableView.AddTab(tab);
                    foreach (var Category in TypeHelper.ThingTypeProperties.Where(x => x.Usable).Where(x => x.Pivot == Pivot).OrderBy(x => x.Order))
                    {
                        GController item = new GController();
                        Stack.Children.Add(item);
                        var ctrl = charHolder.CTRLList.FirstOrDefault(x => x.eDataTyp == Category.ThingType);
                        var prop = charHolder.GetType().GetProperties().Where(x => x.GetValue(charHolder) == ctrl).FirstOrDefault();
                        item.SetBinding(GController.BindingContextProperty, prop?.Name);
                        //{ BindingContext = charHolder.CTRLList.FirstOrDefault(x => x.eDataTyp == Category.ThingType) };
                    }
                }
                for (int i = 0; i < count; i++) // need to remove old items at bottom, because view cannot have zero items
                {
                    TableView.RemoveTab();
                }
            }
        }
    }
}