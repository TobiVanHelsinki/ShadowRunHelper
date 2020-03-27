//Author: Tobi van Helsinki

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using Syncfusion.DataSource.Extensions;
using Syncfusion.ListView.XForms;
using TLIB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Controls
{
    public class O_GetCalcPropertiesConvertert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Thing t)
            {
                return t.GetPropertiesConnects().Select(x => x.GetValue(t));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class ThingDefGroupComparer : IComparer<GroupResult>
    {
        //TODO custom Sort for this case

        public int Compare(GroupResult x, GroupResult y)
        {
            if ((ThingDefs)x.Key > (ThingDefs)y.Key)
            {
                //GroupResult y is stacked into top of the group i.e., Ascending.
                //GroupResult x is stacked at the bottom of the group i.e., Descending.
                return 1;
            }
            else if ((ThingDefs)x.Key < (ThingDefs)y.Key)
            {
                //GroupResult x is stacked into top of the group i.e., Ascending.
                //GroupResult y is stacked at the bottom of the group i.e., Descending.
                return -1;
            }

            return 0;
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LinkListChooser : ContentView
    {
        /// <summary>
        /// Public for ui binding
        /// </summary>
        public CharHolder MyChar { get; }

        /// <summary>
        /// Gets my property.
        /// </summary>
        /// <value>My property.</value>
        private readonly ConnectProperty MyProperty;

        /// <summary>
        /// Current Selected Items
        /// </summary>
        private readonly List<ConnectProperty> Selected = new List<ConnectProperty>();

        public LinkListChooser(CharHolder myChar, ConnectProperty property)
        {
            MyChar = myChar;
            MyProperty = property;
            Selected.AddRange(MyProperty.Connected);
            InitializeComponent();
            BindingContext = this;
        }

        public event EventHandler ClosingRequested;

        private void Cancel_Clicked(object sender, System.EventArgs e)
        {
            //TODO in Backbutton kette aufnehmen
            //Closing fehler untersuchen
            ClosingRequested?.Invoke(this, new EventArgs());
        }

        private void Finish_Clicked(object sender, EventArgs e)
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
            if (sender is CheckBox box && box.Parent is Layout panel)
            {
                var entry = box.BindingContext switch
                {
                    ConnectProperty p => p,
                    Thing t => t.Value,
                    _ => throw new ArgumentException(),
                };
                if (box.IsChecked)
                {
                    //panel.BackgroundColor = Color.Accent;
                    if (!Selected.Contains(entry))
                    {
                        Selected.Add(entry);
                    }
                }
                else
                {
                    //panel.BackgroundColor = Color.Transparent;
                    if (Selected.Contains(entry))
                    {
                        Selected.Remove(entry);
                    }
                }
            }
        }

        private void SfItems_GroupExpanding(object sender, GroupExpandCollapseChangedEventArgs e)
        {
            if (e.Groups.Count > 0)
            {
                foreach (var otherGroup in SfItems.DataSource.Groups)
                {
                    if (e.Groups[0].Key != otherGroup.Key)
                    {
                        SfItems.CollapseGroup(otherGroup);
                    }
                }
            }
        }

        private void SfItems_Loaded(object sender, ListViewLoadedEventArgs e)
        {
            (sender as SfListView).CollapseAll();
        }

        private void CheckBoxLoaded(object sender, EventArgs e)
        {
            if (sender is CheckBox v && v.BindingContext is ConnectProperty cp)
            {
                v.IsChecked = Selected.Contains(cp);
            }
        }

        private void ItemLoaded(object sender, EventArgs e)
        {
            if (sender is ContentPresenter cp && cp.BindingContext is Thing t)
            {
                if (t.GetPropertiesConnects().Count() > 1)
                {
                    cp.Content = (Resources["LinkListThingTemplateExtendet"] as DataTemplate).CreateContent() as View;
                    cp.Content.BindingContext = t;
                }
                else
                {
                    cp.Content = (Resources["LinkListThingTemplateSimple"] as DataTemplate).CreateContent() as View;
                    cp.Content.BindingContext = t;
                }
            }
        }
    }
}