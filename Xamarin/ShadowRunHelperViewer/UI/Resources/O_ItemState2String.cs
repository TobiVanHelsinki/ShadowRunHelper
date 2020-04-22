//Author: Tobi van Helsinki

using System;
using System.Globalization;
using Humanizer;
using SharedCode.Resources;
using Xamarin.Forms;

namespace ShadowRunHelperViewer.UI.Resources
{
    internal class O_ItemState2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return b ? ModelResources.Item_Aktiv : ModelResources.Item_Lager;
            }
            else
            {
                return ModelResources.Item_Besitz;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}