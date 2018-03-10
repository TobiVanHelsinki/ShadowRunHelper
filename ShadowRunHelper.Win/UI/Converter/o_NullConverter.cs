using System;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    public class o_Null2Bool : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null ? false : true;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
    public class o_Null2Visibility : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null ? Windows.UI.Xaml.Visibility.Collapsed : Windows.UI.Xaml.Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
}
