using System;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class io_GebDatum : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            DateTimeOffset dateTemp = new DateTimeOffset();
            try
            {
                dateTemp = (DateTimeOffset)value;
            }
            catch (Exception)
            {
            }
            //TODO regionales datumsformat einführen

            return dateTemp.Date.ToString("dd.MM.yyyy");
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
}
