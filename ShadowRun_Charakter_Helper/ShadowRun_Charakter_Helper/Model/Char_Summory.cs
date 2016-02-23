using System;

namespace ShadowRun_Charakter_Helper.Model
{
    public class Char_Summory
    {
        public int id { get; set; }
        public string char_summory { get; set; }

        public Char_Summory(int id, string sum)
        {
            this.id = id;
            this.char_summory = sum;
        }

        public static Char_Summory get_char_summory_by_id(int id)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            String Current_Char_Container_Name = "Char_ID_" + id;
            String temp_Alias = "";
            String temp_Char_Typ = "";
            String temp_Karma = "";
            try
            {
                temp_Alias = (string)localSettings.Containers[Current_Char_Container_Name].Values["Alias"];
                if (temp_Alias == "" || temp_Karma == null)
                {
                    temp_Alias = "$ohne Namen$";
                }

            }
            catch (NullReferenceException) { temp_Alias = "$ohne Namen$"; }
            try
            {
                temp_Char_Typ = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Typ"];
                if (temp_Char_Typ == "" || temp_Char_Typ == null)
                {
                    temp_Char_Typ = "$ohne Beruf$";
                }
            }
            catch (NullReferenceException) { temp_Char_Typ = "$ohne Beruf$"; }
            try
            {
                temp_Karma = (string)localSettings.Containers[Current_Char_Container_Name].Values["Karma_Gesamt"].ToString();
                if (temp_Karma == "" || temp_Karma == null)
                {
                    temp_Karma = "$ohne Erfolg$";
                }
            }
            catch (NullReferenceException) { temp_Karma = "$ohne Erfolg$"; }


            Char_Summory temp_summory = new Char_Summory(id, temp_Alias + ", " + temp_Char_Typ + ", Karma: " + temp_Karma);
            return temp_summory;
        }
    }
}


