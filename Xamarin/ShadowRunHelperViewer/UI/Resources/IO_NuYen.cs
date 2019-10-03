using System;
using System.Globalization;
using Xamarin.Forms;

namespace ShadowRunHelperViewer.UI.Resources
{
    class IO_NuYen : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() + " ¥";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                return s.Replace(" ¥", "").Replace("¥", "");
            }
            return value;
        }
    }
}
