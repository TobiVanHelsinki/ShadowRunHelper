using System;
using System.Collections.Generic;
using TLIB;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class o_ThingList : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var item = (Model.AllListEntry)value;
            switch ((string)parameter)
            {
                case "Wert":
                    if (item?.strProperty == "")
                    {
                        return item.Object.Wert.ToString();
                    }
                    else
                    {
                        return item.Object.GetValue(item.strProperty).ToString();
                    }
                case "Bezeichner":
                    if (item?.strProperty == "")
                    {
                        return item.Object.Bezeichner;
                    }
                    else
                    {
                        return CrossPlatformHelper.GetString(item.strPropertyName);
                    }
                case "BezeichnerLang":
                    if (item?.strProperty == "")
                    {
                        return item.Object.Bezeichner;
                    }
                    else
                    {
                        return TypenHelper.ThingDefToString(item.Object.ThingType, false)+" "+CrossPlatformHelper.GetString(item.strPropertyName);
                    }
                case "Zusatz":
                    return item.Object.Zusatz;
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
