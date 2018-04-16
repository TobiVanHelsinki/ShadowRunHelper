using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class o_CharAdmin_Path : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((string)value == TAMARIN.SharedConstants.INTERN_SAVE_CONTAINER || (string)value == TAMARIN.SharedConstants.INTERN_SAVE_CONTAINER + "\\") 
            {
                return ResourceLoader.GetForCurrentView().GetString("UI_CharVerwaltung_Intern_Save_Display_String");
            }
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Type)value;
        }
        #endregion
    }
}
