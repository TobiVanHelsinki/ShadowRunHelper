using System;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class o_FileSize : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            dynamic number;
            if (value is ulong ul)
            {
                number = ul;
            }
            else if (value is long l)
            {
                number = l;
            }
            else if (value is uint ui)
            {
                number = ui;
            }
            else if (value is int i)
            {
                number = i;
            }
            else
            {
                number = 0;
            }
            if (number > 1000000)
            {
                return (number / 1000000) + " MB   ";
            }
            if (number > 1000)
            {
                return (number / 1000) + " KB   ";
            }
            return number + " B   ";
            //return String.Format("{0:N}", number)+" Byte ";
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
}
