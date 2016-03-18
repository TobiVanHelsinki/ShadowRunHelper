using System;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class o_ZahlenYen : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.ToString()+ " ¥";
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double retvalue = 0;
            return retvalue;
        }
        #endregion
    }
}
