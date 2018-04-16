using System;
using System.Collections.Generic;
using TAMARIN;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class o_ThingDef2String: IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return TypeHelper.ThingDefToString((ThingDefs)value, true);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
        #endregion
    }
}
