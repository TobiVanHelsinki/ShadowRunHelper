using System;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper1_3.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class i_char : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            char retvalue = ' ';
            try
            {
                if ((value.ToString()).Length>0)
                {
                    retvalue = (value.ToString())[0];
                }
            }
            catch (Exception)
            {
                retvalue = 'X';
            }
            return retvalue;
        }
        #endregion
    }
}
