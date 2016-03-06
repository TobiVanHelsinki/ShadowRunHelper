using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRunHelper
{
    public static class Optionen
    {
        public static bool SAVE_CHAR_ON_EXIT()
        {
            Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

            bool temp = false;
            try
            {
                temp = (bool)container.Values[Variablen.CONTAINER_SETTINGS_SAVE_CHAR_ON_EXIT];
            }
            catch (Exception)
            {
                temp = false;
            }
            return temp;
        }
        public static void SAVE_CHAR_ON_EXIT(bool obj)
        {
            Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

            container.Values[Variablen.CONTAINER_SETTINGS_SAVE_CHAR_ON_EXIT] = obj;
            Optionen.LOAD_CHAR_ON_START(true);
        }

        public static bool LOAD_CHAR_ON_START()
        {
            Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

            bool temp = false;
            try
            {
                temp = (bool)container.Values[Variablen.CONTAINER_SETTINGS_LOAD_CHAR_ON_START];
            }
            catch (Exception)
            {
                temp = false;
            }
            return temp;
        }

        public static void LOAD_CHAR_ON_START(bool obj)
        {
            Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

            container.Values[Variablen.CONTAINER_SETTINGS_LOAD_CHAR_ON_START] = obj;
        }

        public static bool IS_FILE_IN_PROGRESS()
        {
            Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

            bool temp = false;
            try
            {
                temp = (bool)container.Values[Variablen.CONTAINER_SETTINGS_IS_FILE_IN_PROGRESS];
            }
            catch (Exception)
            {
                temp = false;
            }
            return temp;
        }

        public static void IS_FILE_IN_PROGRESS(bool obj)
        {
            Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

            container.Values[Variablen.CONTAINER_SETTINGS_IS_FILE_IN_PROGRESS] = obj;
        }
    }
}
