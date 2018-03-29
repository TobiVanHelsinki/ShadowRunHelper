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
                case "Round":
                    return " (" + value + ")";
                case "Rect":
                    return " [" + value + "]";
                case "Space":
                    return " " + value + " ";
                case "DashR":
                    return " - " + value;
                case "DashL":
                    return value + " - ";
                case "Points":
                    return value + " : ";
                default:
                    return value.ToString();
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
}
