using System;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    public class io_Mult1K : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (int.TryParse(value.ToString(), out int number))
            {
                return (number / 1000).ToString();
            }
            else
            {
                return "--";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (int.TryParse(value.ToString(), out int number))
            {
                return number * 1000;
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
