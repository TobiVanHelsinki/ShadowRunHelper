using System;
using Windows.ApplicationModel;

namespace ShadowRunHelper
{
    internal static class Konstanten
    {
        /// <summary>
        /// Dateiendungen
        /// </summary>
        public const string DATEIENDUNG_CHAR_1 = ".SRWin";
        public const string DATEIENDUNG_CHAR_2 = ".SRCHChar";
        public const string DATEIENDUNG_CHAR_3 = ".SRHChar";
        public const string DATEIENDUNG_CHAR = DATEIENDUNG_CHAR_3;
        public const string DATEIENDUNG_CSV = ".csv";

        /// <summary>
        /// App Versionen
        /// </summary>
        private static PackageVersion version = Package.Current.Id.Version;
        public static string APP_VERSION_BUILD = "" + version.Major + version.Minor + version.Build + version.Revision;
        public static string APP_VERSION_BUILD_DELIM = "" + version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision;
        public const string APP_VERSION_NUMBER_1_3 = "1.3";
        public const string APP_VERSION_NUMBER_1_5 = "1.5";
        public const string APP_VERSION_NUMBER = APP_VERSION_NUMBER_1_5;
        
        /// <summary>
        /// Datei Versionen
        /// </summary>
        public const int STRING_CHARFILEVERSION_LENGTH = 3;
        public const string CHARFILE_VERSION_1_3 = "1130"; //old version
        public const string CHARFILE_VERSION_1_5 = "1.5";

        /// <summary>
        /// Variablen Namen für Versionen
        /// </summary>
        public const string STRING_APP_VERSION_NUMBER = "APP_VERSION_NUMBER";
        public const string STRING_FILE_VERSION_NUMBER = "FILE_VERSION_NUMBER";

        /// <summary>
        /// Speicher Konstanten
        /// </summary>

        /// <summary>
        /// Die Anzahl Zeichen Zwischen der Variable der Nummer und der Nummer ansich
        /// </summary>
        internal const int JSON_FILE_GAP = 3;
        /// <summary>
        /// Speicher Container
        /// </summary>
        public const String CONTAINER_CHAR = "Char_Store";
        public const string CONTAINER_SETTINGS = "Char_Settings";

        /// <summary>
        /// Speicher Einstellungen
        /// </summary>
        public const string CONTAINER_SETTINGS_SAVE_CHAR_ON_EXIT = "SAVE_CHAR_ON_EXIT";
        public const string CONTAINER_SETTINGS_LOAD_CHAR_ON_START = "LOAD_CHAR_ON_START";
        public const string CONTAINER_SETTINGS_IS_FILE_IN_PROGRESS = "IS_FILE_IN_PROGRESS";
        public const string CONTAINER_SETTINGS_bStartEditAfterAdd = "bStartEditAfterAdd";
        public const string CONTAINER_SETTINGS_bDisplayRequest = "bDisplayRequest";

        
        public const string CONTAINER_SETTINGS_ORDNERMODE = "ORDNERMODE_ZWEI";
        public const string CONTAINER_SETTINGS_ORDNERMODE_PFAD = "ORDNERMODE_PFAD";
        public const string LAST_CHAR = "Last_Char";
    }
}
