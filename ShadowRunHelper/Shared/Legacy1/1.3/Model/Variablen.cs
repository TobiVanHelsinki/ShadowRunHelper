using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace ShadowRunHelper1_3
{
    public enum FolderMode
    {
        Intern, Extern
    }

    public static class Variablen
    {
        public static string DATEIENDUNG_CHAR_1 = ".SRWin";
        public static string DATEIENDUNG_CHAR_2 = ".SRCHChar";
        public static string DATEIENDUNG_CHAR_3 = ".SRHChar";
        public static string DATEIENDUNG_CHAR = DATEIENDUNG_CHAR_3;
        public static String CONTAINER_CHAR = "Char_Store";

        private static PackageVersion version = Package.Current.Id.Version;
        public static string APP_VERSION_NUMBER = "" + version.Build + version.Major + version.Minor + version.Revision;

        public static string LAST_CHAR = "Last_Char";

        public static string CONTAINER_SETTINGS = "Char_Settings";

        public static string CONTAINER_SETTINGS_SAVE_CHAR_ON_EXIT = "SAVE_CHAR_ON_EXIT";

        public static string CONTAINER_SETTINGS_LOAD_CHAR_ON_START = "LOAD_CHAR_ON_START";

        public static string CONTAINER_SETTINGS_IS_FILE_IN_PROGRESS = "IS_FILE_IN_PROGRESS";
        public static string CONTAINER_SETTINGS_ORDNERMODE = "ORDNERMODE_ZWEI";
        public static string CONTAINER_SETTINGS_ORDNERMODE_PFAD = "ORDNERMODE_PFAD";
    }
}
