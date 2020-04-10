//Author: Tobi van Helsinki

using System;
using System.Globalization;
using TLIB;
using Xamarin.Forms;

namespace ShadowRunHelperViewer.UI.Resources
{
    internal class IO_Calculation : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!double.TryParse(value.ToString(), out var dValue))
            {
                return TAPPLICATION.SharedConstants.ERROR_TOKEN;
            }
            return dValue.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strOrigin)
            {
                return NumberHelper.CalcToDouble(strOrigin, true);
            }
            return default;
        }
    }
}