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
                return ShadowRunHelper.SharedConstants.ERROR_TOKEN;
            }
            return dValue.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strOrigin)
            {
                try
                {
                    return NumberHelper.CalcToDouble(strOrigin, true);
                }
                catch (Exception)
                {
                    return 0.0;
                }
            }
            return default;
        }
    }
}