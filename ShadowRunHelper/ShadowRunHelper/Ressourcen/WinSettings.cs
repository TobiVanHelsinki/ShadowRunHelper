using System;

namespace ShadowRunHelper
{
    public class WinSettings : ISettings
    {
        Windows.Storage.ApplicationDataContainer Settings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public void set(string place, object value)
        {
            try
            {
                Settings.Values[place] = value;
            }
            catch (Exception)
            {
            }
        }
        public bool getBool(string place)
        {
            try
            {
                return bool.Parse(Settings.Values[place].ToString());
            }
            catch (Exception)
            {
            }
            return false;
        }

        public string getString(string place)
        {
            try
            {
                return Settings.Values[place].ToString();
            }
            catch (Exception)
            {
            }
            return "";
        }
        // ####################################################################
        public string GetStrLastChar()
        {
            return getString(Konstanten.LAST_CHAR);
        }
        public void SetStrLastChar(string value)
        {
            set(Konstanten.LAST_CHAR, value);
        }

        // ####################################################################
        public bool GetBSaveCharOnExit()
        {
            return getBool(Konstanten.CONTAINER_SETTINGS_SAVE_CHAR_ON_EXIT);
        }
        public void SetBSaveCharOnExit(bool value)
        {
            set(Konstanten.CONTAINER_SETTINGS_SAVE_CHAR_ON_EXIT, value);
        }
        // ####################################################################
        public bool GetBLoadCharOnStart()
        {
            return getBool(Konstanten.CONTAINER_SETTINGS_LOAD_CHAR_ON_START);
        }
        public void SetBLoadCharOnStart(bool value)
        {
            set(Konstanten.CONTAINER_SETTINGS_LOAD_CHAR_ON_START, value);
        }
        // ####################################################################
        public bool GetBIsFileInProgress()
        {
            return getBool(Konstanten.CONTAINER_SETTINGS_IS_FILE_IN_PROGRESS);
        }
        public void SetBIsFileInProgress(bool value)
        {
            set(Konstanten.CONTAINER_SETTINGS_IS_FILE_IN_PROGRESS, value);
        }
        // ####################################################################
        public bool GetBFolderMode()
        {
            return getBool(Konstanten.CONTAINER_SETTINGS_ORDNERMODE);
        }
        public void SetBFolderMode(bool value)
        {
            set(Konstanten.CONTAINER_SETTINGS_ORDNERMODE, value);
        }
        // ####################################################################
        public string GetStrFolderModePath()
        {
            return getString(Konstanten.CONTAINER_SETTINGS_ORDNERMODE_PFAD);
        }
        public void SetStrFolderModePath(string value)
        {
            set(Konstanten.CONTAINER_SETTINGS_ORDNERMODE_PFAD, value);
        }
        // ####################################################################
        public bool GetBStartEditAfterAdd()
        {
            return getBool(Konstanten.CONTAINER_SETTINGS_bStartEditAfterAdd);
        }
        public void SetBStartEditAfterAdd(bool value)
        {
            set(Konstanten.CONTAINER_SETTINGS_bStartEditAfterAdd, value);
        }
        // ####################################################################
        public bool GetBDisplayRequest()
        {
            return getBool(Konstanten.CONTAINER_SETTINGS_bDisplayRequest);
        }
        public void SetBDisplayRequest(bool value)
        {
            set(Konstanten.CONTAINER_SETTINGS_bDisplayRequest, value);
        }
    }
}
