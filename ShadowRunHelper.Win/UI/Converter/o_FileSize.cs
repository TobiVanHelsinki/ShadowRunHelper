using System;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class o_FileSize : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (((ulong)value) > 1000000)
            {
                return (((ulong)value) / 1000000) + " MB   ";
            }
            if (((ulong)value) > 1000)
            {
                return (((ulong)value) / 1000) + " KB   ";
            }
            return ((ulong)value) + " B   ";
            //return String.Format("{0:N}", ((ulong)value))+" Byte ";
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
}
