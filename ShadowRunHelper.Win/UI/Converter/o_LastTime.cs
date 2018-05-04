using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    public class o_LastTime : IValueConverter
    {
        static TimeSpan span = TimeSpan.FromSeconds(1);
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var DT = (DateTimeOffset)value;

            //long ticks = (DT.Ticks + (span.Ticks / 2) + 1) / span.Ticks;
            //DT = new DateTime(ticks * span.Ticks);

            string retdate = DT.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
            string rettime = DT.ToString(CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern);

            return retdate + rettime;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
}
