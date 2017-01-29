using System;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper1_3.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class io_ZahlenDouble : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double retvalue = 0;
            try
            {
                string digitstr = new string(((string)value).Where(r => char.IsDigit(r)).ToArray());
                retvalue = ((IConvertible)digitstr).ToDouble(null);
            }
            catch (Exception)
            {
                retvalue = 0;
            }
            return retvalue;
        }
        #endregion
    }
}
