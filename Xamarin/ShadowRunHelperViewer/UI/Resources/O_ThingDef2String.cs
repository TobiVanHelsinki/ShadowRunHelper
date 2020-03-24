using System;
using System.Globalization;
using ShadowRunHelper;
using Xamarin.Forms;

namespace ShadowRunHelperViewer.UI.Resources
{
    internal class O_ThingDef2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TypeHelper.ThingDefToString((ThingDefs)value, true);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}