//Author: Tobi van Helsinki

using System;
using System.Globalization;
using Humanizer;
using Xamarin.Forms;

namespace ShadowRunHelperViewer.UI.Resources
{
    internal class O_HumanizeTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is DateTime dt ? dt.Humanize(false).ToString() : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}