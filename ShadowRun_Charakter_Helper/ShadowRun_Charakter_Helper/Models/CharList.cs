
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

            string Char_ID_String = CharList.Read();
            try
            {
                string[] Char_ID_Array = Char_ID_String.Split(',');
                List<string> Char_ID_List = Char_ID_Array.OfType<string>().ToList();

                if (!Char_ID_List.Contains(id.ToString()))
                {
                    Char_ID_String += ",";
                    Char_ID_String += id.ToString();
                }
            }
            catch (NullReferenceException)
            {
                Char_ID_String += id.ToString();
            }

            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["CharList"] = Char_ID_String;
            return true;
        }

        public static int Add()
        {
            string Char_ID_String = CharList.Read();
            int new_ID = 0;
            try
            {
                string[] Char_ID_Array = Char_ID_String.Split(',');
                List<string> Char_ID_List = Char_ID_Array.OfType<string>().ToList();

                if (Char_ID_List==null || Char_ID_List.Count == 0) {

                    Char_ID_String += 0;
                }
                else { 
                    Char_ID_String += ",";
                    new_ID = (Int32.Parse(Char_ID_List[Char_ID_List.Count-1]) + 1);
                    Char_ID_String += new_ID.ToString();
                }
            }
            catch (NullReferenceException)
            {
               
            }

            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["CharList"] = Char_ID_String;
            return new_ID;
        }

        public static string Read()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            string CharList = (string)localSettings.Values["CharList"];
            return CharList;

        }

        public static string[] ReadSeperated()
        {
            try { 
                return CharList.Read().Split(',');
            }
            catch(NullReferenceException)
            {
                return null;
            }
        }

        public static List<string> ReadSeperatedtoList()
        {
            try
            {
                return CharList.Read().Split(',').OfType<string>().ToList();
            }
            catch(NullReferenceException)
            {
                return null;
            }
        }

        public static bool Clear()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["CharList"] = null;

            return true;
        }

        public static bool Delete(int id)
        {
            string Char_ID_String = CharList.Read();
            try
            {
                string[] Char_ID_Array = Char_ID_String.Split(',');
                List<string> Char_ID_List = Char_ID_Array.OfType<string>().ToList();

                if (Char_ID_List.Contains(id.ToString()))
                {
                    Char_ID_List.Remove(id.ToString());

                    string Char_ID_NewString = "";
                    for (int i = 0; i < Char_ID_List.Count; i++)
                    {
                        Char_ID_NewString += Char_ID_List[i].ToString();

                        if (i+1!=Char_ID_List.Count) {
                            Char_ID_NewString += ",";
                        }
                        

                    }
                    Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    localSettings.Values["CharList"] = Char_ID_NewString;
                    return true;
                }
            }
            catch (NullReferenceException)
            {
                return false;
            }

            return false;
        }

        internal static bool Beinhaltet(int iD_Char)
        {
            List<string> Char_ID_List = ReadSeperatedtoList();
            if (Char_ID_List.Contains(iD_Char.ToString()))
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
            List<string> List_of_IDs = ReadSeperatedtoList();

            for (int i=0;i< List_of_IDs.Count();i++)
            {
                if (!List_of_IDs.Contains(i.ToString()))
                {
                    return i;
                }    
            }
            return List_of_IDs.Count();
        }
    }
}

