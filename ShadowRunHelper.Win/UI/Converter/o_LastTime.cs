using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    public class o_LastTime : IValueConverter
    {
        static string DatePattern;
        static string TimePattern;

        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTimeOffset DT;
            if (value is DateTime)
            {
                DT = (DateTime)value;
            }
            else if(value is DateTimeOffset)
            {
                DT = (DateTimeOffset)value;
            }
            else
            {
                return "---";
            }
          
            string retdate = DT.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
            string rettime = DT.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern);
            return retdate + " " +  rettime;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
}
