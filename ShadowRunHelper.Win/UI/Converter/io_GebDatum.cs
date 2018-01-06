using System;
using System.Globalization;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class io_GebDatum : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var cultInfo = CultureInfo.CurrentCulture;
            cultInfo.DateTimeFormat.LongTimePattern = "";
            cultInfo.DateTimeFormat.ShortTimePattern = "";

            try
            {
                DateTimeOffset dateTemp = (DateTimeOffset)value;
                return dateTemp.Date.ToString(cultInfo).Trim();
            }
            catch (Exception)
            {
                return Constants.ERROR_TOKEN;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
}
