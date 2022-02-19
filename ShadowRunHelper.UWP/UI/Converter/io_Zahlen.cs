using System;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    public class io_ZahlenDouble : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!Double.TryParse(value.ToString(), out double dValue))
            {
                return Constants.ERROR_TOKEN;
            }
            string retval = dValue.ToString();
            return retval;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string strOrigin = (value as string);
            double dRetVal = TLIB.NumberHelper.CalcToDouble(strOrigin, true);
            return dRetVal;
        }

        #endregion
    }
}
