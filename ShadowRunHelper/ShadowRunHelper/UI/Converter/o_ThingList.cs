using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class o_ThingList : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var lst = (List<KeyValuePair<string, double>>)value;
            switch ((string)parameter)
            {
                case "Wert":
                    if (lst.Count == 1)
                    {
                        return lst[0].Value;
                    }
                    return "-";
                case "Bezeichner":
                    if (lst.Count == 1)
                    {
                        return lst[0].Key;
                    }
                    return "";
                default:
                    return value;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
        #endregion
    }
}
