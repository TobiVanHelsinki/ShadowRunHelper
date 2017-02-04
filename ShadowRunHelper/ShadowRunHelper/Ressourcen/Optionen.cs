﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRunHelper
{
    public static class Optionen
    {
        public static string strLastChar
        {
            get
            {
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    return Settings.Values[Konstanten.LAST_CHAR].ToString();
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
                    Settings.Values[Konstanten.LAST_CHAR] = value;
                }
                catch (Exception)
                {
                }
            }
        }

        public static bool bSaveCharOnExit
        {
            get
            {


                bool temp = false;
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Konstanten.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);
                    temp = (bool)container.Values[Konstanten.CONTAINER_SETTINGS_SAVE_CHAR_ON_EXIT];
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
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Konstanten.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

                    container.Values[Konstanten.CONTAINER_SETTINGS_SAVE_CHAR_ON_EXIT] = value;
                    Optionen.bLoadCharOnStart = true;
                }
                catch (Exception)
                {
                }
            }
        }
        
        public static bool bLoadCharOnStart
        {
            get
            {
                bool temp = false;
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Konstanten.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);
                    temp = (bool)container.Values[Konstanten.CONTAINER_SETTINGS_LOAD_CHAR_ON_START];
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
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Konstanten.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

                    container.Values[Konstanten.CONTAINER_SETTINGS_LOAD_CHAR_ON_START] = value;
                }
                catch (Exception)
                {
                }
            }
        }

        public static bool bIsFIleInProgress
        {
            get
            {
                bool temp = false;
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Konstanten.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);
                    temp = (bool)container.Values[Konstanten.CONTAINER_SETTINGS_IS_FILE_IN_PROGRESS];
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
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Konstanten.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);
                    container.Values[Konstanten.CONTAINER_SETTINGS_IS_FILE_IN_PROGRESS] = value;
                }
                catch (Exception)
                {
                }
            }
        }

        public static bool bORDNERMODE
        {
            get
            {
                bool temp = false;
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Konstanten.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);
                    temp = (bool)container.Values[Konstanten.CONTAINER_SETTINGS_ORDNERMODE];
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
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Konstanten.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

                    container.Values[Konstanten.CONTAINER_SETTINGS_ORDNERMODE] = value;
                }
                catch (Exception)
                {
                }
            }
        }

        public static string strORDNERMODE_PFAD
        {
            get
            {
                string temp = "";
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Konstanten.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);
                    temp = container.Values[Konstanten.CONTAINER_SETTINGS_ORDNERMODE_PFAD].ToString();
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
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Konstanten.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

                    container.Values[Konstanten.CONTAINER_SETTINGS_ORDNERMODE_PFAD] = value;
                }
                catch (Exception)
                {
                }
            }
        }

        internal static bool bStartEditAfterAdd
        {
            get
            {
                bool temp = false;
                try
                {
                    Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Konstanten.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);
                    temp = (bool)container.Values[Konstanten.CONTAINER_SETTINGS_bStartEditAfterAdd];
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
                    Windows.Storage.ApplicationDataContainer container = Settings.CreateContainer(Konstanten.CONTAINER_SETTINGS, Windows.Storage.ApplicationDataCreateDisposition.Always);

                    container.Values[Konstanten.CONTAINER_SETTINGS_bStartEditAfterAdd] = value;
                }
                catch (Exception)
                {
                }
            }
        }

    }
    
}
