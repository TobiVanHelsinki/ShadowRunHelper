using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRunHelper1_3
{
    public static class Optionen
    {
        public static string LAST_CHAR_IS
        {
            get
            {
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    return Settings.Values[Variablen.LAST_CHAR].ToString();
                }
                catch (Exception)
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Settings.Values[Variablen.LAST_CHAR] = value;
                }
                catch (Exception)
                {
                }
            }
        }

        public static bool SAVE_CHAR_ON_EXIT
        {
            get
            {


                bool temp = false;
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);
                    temp = (bool)container.Values[Variablen.CONTAINER_SETTINGS_SAVE_CHAR_ON_EXIT];
                }
                catch (Exception)
                {
                    temp = false;
                }
                return temp;
            }
            set
            {
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

                    container.Values[Variablen.CONTAINER_SETTINGS_SAVE_CHAR_ON_EXIT] = value;
                    Optionen.LOAD_CHAR_ON_START = true;
                }
                catch (Exception)
                {
                }
            }
        }
        
        public static bool LOAD_CHAR_ON_START
        {
            get
            {
                bool temp = false;
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);
                    temp = (bool)container.Values[Variablen.CONTAINER_SETTINGS_LOAD_CHAR_ON_START];
                }
                catch (Exception)
                {
                    temp = false;
                }
                return temp;
            }
            set
            {
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

                    container.Values[Variablen.CONTAINER_SETTINGS_LOAD_CHAR_ON_START] = value;
                }
                catch (Exception)
                {
                }
            }
        }

        public static bool IS_FILE_IN_PROGRESS
        {
            get
            {
                bool temp = false;
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);
                    temp = (bool)container.Values[Variablen.CONTAINER_SETTINGS_IS_FILE_IN_PROGRESS];
                }
                catch (Exception)
                {
                    temp = false;
                }
                return temp;
            }
            set
            {
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);
                    container.Values[Variablen.CONTAINER_SETTINGS_IS_FILE_IN_PROGRESS] = value;
                }
                catch (Exception)
                {
                }
            }
        }

        public static bool ORDNERMODE
        {
            get
            {
                bool temp = false;
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);
                    temp = (bool)container.Values[Variablen.CONTAINER_SETTINGS_ORDNERMODE];
                }
                catch (Exception)
                {
                    temp = false;
                }
                return temp;
            }
            set
            {
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

                    container.Values[Variablen.CONTAINER_SETTINGS_ORDNERMODE] = value;
                }
                catch (Exception)
                {
                }
            }
        }

        public static string ORDNERMODE_PFAD
        {
            get
            {
                string temp = "";
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);
                    temp = container.Values[Variablen.CONTAINER_SETTINGS_ORDNERMODE_PFAD].ToString();
                }
                catch (Exception)
                {
                    temp = "";
                }
                return temp;
            }
            set
            {
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

                    container.Values[Variablen.CONTAINER_SETTINGS_ORDNERMODE_PFAD] = value;
                }
                catch (Exception)
                {
                }
            }
        }
    }
    
}
