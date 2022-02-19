using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class io_GebDatum : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                return ((DateTimeOffset)value).Date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern).Trim();
            }
            catch (Exception ex)
            {
                return SharedConstants.ERROR_TOKEN;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
}
