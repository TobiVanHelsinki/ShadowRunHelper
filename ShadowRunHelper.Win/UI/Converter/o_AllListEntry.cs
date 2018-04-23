using System;
using System.Collections.Generic;
using TLIB;
using TAPPLICATION;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class o_AllListEntry : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var item = (Model.AllListEntry)value;
            switch ((string)parameter)
            {
                case "Wert":
                    return item.Object.ValueOf(item.PropertyID).ToString();
                case "Bezeichner":
                    if (item?.PropertyID == "")
                    {
                        return item.Object.Bezeichner;
                    }
                    else
                    {
                        return StringHelper.GetString("Model__Active/Text") + StringHelper.GetString(item.DisplayName);
                    }
                case "BezeichnerLang":
                    if (item?.PropertyID == "")
                    {
                        return item.Object.Bezeichner;
                    }
                    else
                    {
                        return TypeHelper.ThingDefToString(item.Object.ThingType, false) + " " + StringHelper.GetString(item.DisplayName);
                    }
                case "Zusatz":
                    return item.Object.ValueOf("Zusatz").ToString();
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
