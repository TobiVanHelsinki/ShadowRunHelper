using System;
using Windows.Globalization;
using Windows.Globalization.NumberFormatting;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class o_ZahlenYen : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            CurrencyFormatter CurrencyFormatter = new CurrencyFormatter(CurrencyIdentifiers.JPY);
            CurrencyFormatter.IsGrouped = true;
            CurrencyFormatter.FractionDigits = 0;
            return Double.TryParse(value.ToString(), out double number) ? CurrencyFormatter.Format(number) : Constants.ERROR_TOKEN;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double retvalue = 0;
            return retvalue;
        }
        #endregion
    }
}
