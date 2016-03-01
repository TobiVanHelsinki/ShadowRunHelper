using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace ShadowRun_Charakter_Helper
{
    public enum FolderMode
    {
        Intern, Extern
    }

    public static class Variablen
    {
        public static string DATEIENDUNG_CHAR_1 = ".SRWin";
        public static string DATEIENDUNG_CHAR_2 = ".SRCHChar";
        public static String CONTAINER_CHAR = "Char_Store";

        private static PackageVersion version = Package.Current.Id.Version;
        public static string APP_VERSION_NUMBER = "" + version.Build + version.Major + version.Minor + version.Revision;
    }
}
