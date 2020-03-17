//Author: Tobi van Helsinki

using System;
using System.Globalization;
using Humanizer;
using Xamarin.Forms;

namespace ShadowRunHelperViewer.UI.Resources
{
    internal class O_HumanizeFileSize : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is long i ? i.Bytes().Humanize("#") : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}