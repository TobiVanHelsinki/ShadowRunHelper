using ShadowRun_Charakter_Helper.Models;
using System;
using Windows.UI.Xaml.Data;

namespace ShadowRun_Charakter_Helper
{
    // Custom class implements the IValueConverter interface. 
    public class UI_Converter_Attribute : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            //int i = 0;
            //Models.Char LoadChar = new Models.Char();
            ////Char_Attribute
            //try
            //{
            //    i = 0;
            //    Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            //    String Current_Char_Container_Name = "Char_ID_" + LoadChar.ID_Char;

            //    LoadChar.Char_Attribute = new System.Collections.ObjectModel.ObservableCollection<Char_Attribut>();
            //    for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Attribute_Count"]; i++)
            //    {
            //        Char_Attribut Char_Attribute_Temp = new Char_Attribut();
            //        Char_Attribute_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Attribut_" + i + "_ID_Char_Vorteil"];
            //        Char_Attribute_Temp.Bezeichnung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Attribut_" + i + "_Bezeichnung"];
            //        Char_Attribute_Temp.Stufe = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Attribut_" + i + "_Stufe"];
            //        Char_Attribute_Temp.Stufe_Modifier = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Attribut_" + i + "_Stufe_Modifier"];

            //        LoadChar.Char_Attribute.Add(Char_Attribute_Temp);
            //    }
            //}
            //catch (NullReferenceException)
            //{

            //}

            try
            {

                return Char_Attribut.getNAMEbyID(value);
            }
            catch (Exception)
            {
                return "<Fehler>";
            }
            
        }


        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {

            return 0;
        }
        #endregion
    }
}
