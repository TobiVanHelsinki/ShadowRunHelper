
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    class CharList
    {
        public static bool Add(int id)
        {
            if (id == 0) { return false; }
            List<int> Char_ID_List = ReadSeperatedtoList();
            if (!Char_ID_List.Contains(id))
            {
                Char_ID_List.Add(id);
            }
            Write(Char_ID_List);
            return true;
        }

        public static void Write(List<int> Char_ID_List)
        {
            string Char_ID_String = "";
            for (int i = 0; i < Char_ID_List.Count(); i++)
            {
                Char_ID_String += Char_ID_List[i].ToString();

                if (Char_ID_List.Count() - 1 != i)
                {
                    Char_ID_String += ",";
                }
            }

            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["CharList"] = Char_ID_String;
        }

        public static List<int> ReadSeperatedtoList()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            List<string> temp = new List<string>();
            List<int> inte = new List<int>();
            try
            {
                temp = ((string)localSettings.Values["CharList"]).Split(',').OfType<string>().ToList();
            }
            catch (NullReferenceException)
            {
                return inte;
            }
            
            for (int i = 0; i < temp.Count(); i++)
            {
                try
                {
                    inte.Add(Int32.Parse(temp[i]));
                }
                catch
                {

                }
            }

            return inte;

        }

        public static bool Clear()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["CharList"] = null;

            return true;
        }

        public static bool Delete(int id)
        {
            List<int> Char_ID_List = ReadSeperatedtoList();
            if (Char_ID_List.Contains(id))
            {
                Char_ID_List.Remove(id);
                Write(Char_ID_List);
                return true;
            }
            return false;
        }

        internal static bool Beinhaltet(int iD_Char)
        {
            List<int> Char_ID_List = ReadSeperatedtoList();
            if (Char_ID_List.Contains(iD_Char))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        internal static int freieID()
        {
            List<int> List_of_IDs = ReadSeperatedtoList();

            for (int i=1;i<=List_of_IDs.Count();i++)
            {
                if (!List_of_IDs.Contains(i))
                {
                    return i;
                }    
            }
            return List_of_IDs.Count()+1;
        }
    }
}

