using ShadowRunHelper.CharModel;
using System;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class o_ModelValue : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return string.Empty;
            }
            switch (parameter as string)
            {
                case "HandlungWert":
                    var Wert = (value as Handlung).WertCalced;
                    var Limit = (value as Handlung).Grenze;
                    var Gegen = (value as Handlung).Gegen;
                    return Wert + (Limit != 0 ? " [" + Limit + "] " : "") + (Gegen != 0 ? " - " + Gegen : "");
                case "Wert":
                    break;
                default:
                    break;
            }
            return string.Empty;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
}
