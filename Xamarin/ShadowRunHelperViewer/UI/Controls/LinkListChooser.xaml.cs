using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LinkListChooser : PopupPage
    {
        public CharHolder MyChar { get; }

        /// <summary>
        /// Was the Choice successfull
        /// </summary>
        public bool Result { get; set; }
        public LinkListChooser(CharHolder myChar, IEnumerable<CharCalcProperty> preSelection)
        {
            //https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/collectionview/
            //can grouping, multiple selection mode, scroll into. should be used evry where if possible
            MyChar = myChar;
            InitializeComponent();
            BindingContext = this;
            Select(preSelection);
        }

        private void Select(IEnumerable<CharCalcProperty> preSelection)
        {
        }

        public List<CharCalcProperty> Selected = new List<CharCalcProperty>();
        private void Cancel_Clicked(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        private void Finish_Clicked(object sender, System.EventArgs e)
        {
            Result = true;
            Cancel_Clicked(sender, e);
        }

        /// <summary>
        /// Addes or removes selected items from the return list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selected_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox box && box.BindingContext is DetailsModel entry && box.Parent is Layout panel)
            {
                if (Selected.Contains(entry.CalcProperty))
                {
                    panel.BackgroundColor = Color.Default;
                    Selected.Remove(entry.CalcProperty);
                }
                else
                {
                    panel.BackgroundColor = Color.Accent;
                    Selected.Add(entry.CalcProperty);
                }
            }
        }

        /// <summary>
        /// Populates the Item with matching Properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewCell_Appearing(object sender, EventArgs e)
        {
            if (sender is Element v && v.FindByName("CalcItemsList") is StackLayout lv && v.BindingContext is Thing t)
            {
                var template = lv.Resources["Template"] as DataTemplate;
                var list = from item in t.GetCalcProps(typeof(CharCalcProperty))
                           let a = template.CreateContent()
                           let view = template.CreateContent() as View
                           select (item, view);
                foreach (var (prop, view) in list)
                {
                    view.BindingContext = new DetailsModel(prop.GetValue(t, null) as CharCalcProperty, prop.Name);
                    lv.Children.Add(view);
                }
                if (!list.Any() && v is ViewCell vc)
                {
                    vc.View.IsVisible = false;
                }
                if (list.Count() == 1)
                {
                    //TODO maybe Introduce special UI for just this one element;
                }
            }
        }

        bool SizeUpdateInProgress;
        private void MainFrame_SizeChanged(object sender, EventArgs e)
        {
            if (sender is Frame f && !SizeUpdateInProgress)
            {
                SizeUpdateInProgress = true;
                if (f.Height > 500)
                {
                    f.HeightRequest = 500;
                }
                if (f.Width > 800)
                {
                    f.HeightRequest = 800;
                }
                SizeUpdateInProgress = false;
            }
        }
    }

    /// <summary>
    /// A special Model, just used at this view
    /// </summary>
    public class DetailsModel
    {
        public CharCalcProperty CalcProperty { get; set; }
        public string Name { get; set; }

        public DetailsModel(CharCalcProperty calcProperty, string name)
        {
            CalcProperty = calcProperty;
            Name = name;
        }
    }
}