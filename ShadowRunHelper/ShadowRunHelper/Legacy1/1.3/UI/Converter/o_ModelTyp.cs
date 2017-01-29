using System;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper1_3.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class o_ModelTyp : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string strTemp = value.ToString();
            if (strTemp != "")
            {
                strTemp += ": ";
            }
            return strTemp;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
}
