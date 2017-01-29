using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper1_3.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class io_ListView : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //return (System.Collections.ObjectModel.ObservableCollection<ShadowRunHelper.CharController.Handlung>)value;
            ListViewItem item = (ListViewItem)value;
            ListView listView =
                ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            // Get the index of a ListViewItem
            //int index =listView.ItemsControl.IndexFromContainer(item);

            //if (index % 2 == 0)
            //{
            //    return Brushes.LightBlue;
            //}
            //else
            //{
            //    return Brushes.Beige;
            //}
            return (Type)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
}
