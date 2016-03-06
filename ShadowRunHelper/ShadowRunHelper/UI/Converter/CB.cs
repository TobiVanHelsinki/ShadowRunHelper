using System;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper
{
    // Custom class implements the IValueConverter interface. 
    public class CB : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            double retvalue = 0;
            try
            {
                retvalue = ((IConvertible)value).ToDouble(null);
            }
            catch (Exception)
            {
               
            }
            return retvalue;
        }
        #endregion
    }
}
