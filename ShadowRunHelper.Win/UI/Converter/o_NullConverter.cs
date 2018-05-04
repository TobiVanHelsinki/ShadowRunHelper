using System;
using Windows.UI.Xaml;
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
    public class o_NullOrDefault2Bool : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                switch (value)
                {
                    case int i:
                        return i != 0;
                    case double d:
                        return d != 0;
                    case String s:
                        return !(string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s));
                    default:
                        return false;
                }
            }
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
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
    public class o_Bool2Visibility : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Visibility)value == Visibility.Visible;
        }
        #endregion
    }
}
