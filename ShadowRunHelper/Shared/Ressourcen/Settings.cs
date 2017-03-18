using TLIB;
namespace ShadowRunHelper
{
    public class Settings : TLIB.Settings.SharedSettings
    {
        // ####################################################################
        public string GetStrLastChar()
        {
            return PlatformSettings.getString(Constants.LAST_CHAR);
        }
        public void SetStrLastChar(string value)
        {
            PlatformSettings.set(Constants.LAST_CHAR, value);
        }
        // ####################################################################
        public bool GetBSaveCharOnExit()
        {
            return PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_SAVE_CHAR_ON_EXIT);
        }
        public void SetBSaveCharOnExit(bool value)
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_SAVE_CHAR_ON_EXIT, value);
        }
        // ####################################################################
        public bool GetBLoadCharOnStart()
        {
            return PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_LOAD_CHAR_ON_START);
        }
        public void SetBLoadCharOnStart(bool value)
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_LOAD_CHAR_ON_START, value);
        }
        // ####################################################################
        public bool GetBIsFileInProgress()
        {
            return PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_IS_FILE_IN_PROGRESS);
        }
        public void SetBIsFileInProgress(bool value)
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_IS_FILE_IN_PROGRESS, value);
        }
        // ####################################################################
        public bool GetBStartEditAfterAdd()
        {
            return PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_bStartEditAfterAdd);
        }
        public void SetBStartEditAfterAdd(bool value)
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_bStartEditAfterAdd, value);
        }
        // ####################################################################
    }
}
