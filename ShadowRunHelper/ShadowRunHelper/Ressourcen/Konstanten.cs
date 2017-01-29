using System;
using Windows.ApplicationModel;

namespace ShadowRunHelper
{
    internal static class Konstanten
    {
        public const string DATEIENDUNG_CHAR_1 = ".SRWin";
        public const string DATEIENDUNG_CHAR_2 = ".SRCHChar";
        public const string DATEIENDUNG_CHAR_3 = ".SRHChar";
        public const string DATEIENDUNG_CHAR = DATEIENDUNG_CHAR_3;
        public const String CONTAINER_CHAR = "Char_Store";

        private static PackageVersion version = Package.Current.Id.Version;
        public static string APP_VERSION_NUMBER = "" + version.Build + version.Major + version.Minor + version.Revision;
        public const string CHARFILE_VERSION_1_3 = "1.3";
        public const string CHARFILE_VERSION_1_5 = "1.5";

        public const string LAST_CHAR = "Last_Char";

        public const string CONTAINER_SETTINGS = "Char_Settings";

        public const string CONTAINER_SETTINGS_SAVE_CHAR_ON_EXIT = "SAVE_CHAR_ON_EXIT";

        public const string CONTAINER_SETTINGS_LOAD_CHAR_ON_START = "LOAD_CHAR_ON_START";

        public const string CONTAINER_SETTINGS_IS_FILE_IN_PROGRESS = "IS_FILE_IN_PROGRESS";
        public const string CONTAINER_SETTINGS_ORDNERMODE = "ORDNERMODE_ZWEI";
        public const string CONTAINER_SETTINGS_ORDNERMODE_PFAD = "ORDNERMODE_PFAD";
    }
}
