﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRunHelper
{
    public static class Optionen
    {
        public static string LAST_CHAR_IS
        {
            get
            {
                Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                return Settings.Values[Variablen.LAST_CHAR].ToString();
            }
            set
            {
                Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                Settings.Values[Variablen.LAST_CHAR] = value;
            }
        }

        public static bool SAVE_CHAR_ON_EXIT
        {
            get
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
            set
            {
                Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
                Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

                container.Values[Variablen.CONTAINER_SETTINGS_SAVE_CHAR_ON_EXIT] = value;
                Optionen.LOAD_CHAR_ON_START = true;
            }
        }
        

        public static bool LOAD_CHAR_ON_START
        {
            get
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
            set
            {
                Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
                Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

                container.Values[Variablen.CONTAINER_SETTINGS_LOAD_CHAR_ON_START] = value;
            }
        }


        public static bool IS_FILE_IN_PROGRESS
        {
            get
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
            set
            {
                Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
                Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Variablen.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

                container.Values[Variablen.CONTAINER_SETTINGS_IS_FILE_IN_PROGRESS] = value;
            }
        }
    }
}
