//Author: Tobi van Helsinki


using System;
using System.Globalization;
using Xamarin.Forms;

namespace ShadowRunHelperViewer.UI.Resources
{
    internal class IO_DebugConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}