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
                    var Limit = (value as Handlung).Grenze;
                    var Gegen = (value as Handlung).Gegen;
                    return (value as Handlung).WertCalced + (value as Handlung).Zusatz + 
                    (Limit != 0 ? " [" + Limit + "] " : "") + 
                    (Gegen != 0 ? " - " + Gegen : "");
                default:
                    var val = (value as Thing).ValueOf(parameter as string);
                    var raw = (value as Thing).RawValueOf(parameter as string);
                    return val == raw ? val.ToString() : val + " ("+ raw + ")";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
}
