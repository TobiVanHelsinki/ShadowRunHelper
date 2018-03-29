using System;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    public class o_ValueOrNoting : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch (value)
            {
                case double d:
                    if (d == 0)
                        return string.Empty;
                    break;
                case int i:
                    if (i == 0)
                        return string.Empty;
                    break;
                case string s:
                    if (s == null || s == "")
                        return string.Empty;
                    break;
                case object o:
                    if (value == null)
                        return string.Empty;
                    break;
                default:
                    break;
            }
            switch (parameter as string)
            {
                case "KlammerRund":
                    return " (" + value + ")";
                case "KlammerEck":
                    return " [" + value + "]";
                case "Space":
                    return " " + value + " ";
                case "Bindestrich":
                    return " - " + value;
                case "Doppelpunkt":
                    return value + " : ";
                default:
                    return value;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
}
