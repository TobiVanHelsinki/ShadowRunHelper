///Author: Tobi van Helsinki

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using TLIB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LinkListChooser : PopupPage
    {
        /// <summary>
        /// Public for ui binding
        /// </summary>
        public CharHolder MyChar { get; }

        /// <summary>
        /// Gets my property.
        /// </summary>
        /// <value>
        /// My property.
        /// </value>
        readonly CharCalcProperty MyProperty;

        /// <summary>
        /// Current Selected Items
        /// </summary>
        readonly List<CharCalcProperty> Selected = new List<CharCalcProperty>();

        public LinkListChooser(CharHolder myChar, CharCalcProperty property)
        {
            MyChar = myChar;
            MyProperty = property;
            Selected.AddRange(MyProperty.Connected);
            InitializeComponent();
            BindingContext = this;
        }

        private void Cancel_Clicked(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        private void Finish_Clicked(object sender, System.EventArgs e)
        {
            MyProperty.Connected.Clear();
            MyProperty.Connected.AddRange(Selected);
            Cancel_Clicked(sender, e);
        }

        /// <summary>
        /// Addes or removes selected items from the return list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selected_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox box && box.BindingContext is CharCalcProperty entry && box.Parent is Layout panel)
            {
                if (box.IsChecked)
                {
                    panel.BackgroundColor = Color.Accent;
                    if (!Selected.Contains(entry))
                    {
                        Selected.Add(entry);
                    }
                }
                else
                {
                    panel.BackgroundColor = Color.Transparent;
                    if (Selected.Contains(entry))
                    {
                        Selected.Remove(entry);
                    }
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
                    var charCalcProperty = prop.GetValue(t) as CharCalcProperty;
                    view.BindingContext = charCalcProperty;
                    if (Selected.Contains(charCalcProperty))
                    {
                        if (view.FindByName("Selected") is CheckBox b)
                        {
                            b.IsChecked = true;
                        }
                    }
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

        private void PopupPage_SizeChanged(object sender, EventArgs e) => (MainFrame.WidthRequest, MainFrame.HeightRequest) = Common.MaximumDimensions(Width, Height);
    }
}