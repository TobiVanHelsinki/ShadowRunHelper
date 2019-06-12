using ShadowRunHelper.Model;
using System.Collections.Generic;
using TAPPLICATION;
using TLIB;

namespace ShadowRunHelper
{
    public class Constants : SharedConstants
    {
        public const string ACCESSTOKEN_FOLDERMODE = "ACCESSTOKEN_FOLDERMODE";
        public const string ACCESSTOKEN_FILEACTIVATED = "ACCESSTOKEN_FILEACTIVATED";
        public const string ACCESSTOKEN_EXPORT = "Export";
        public const string ACCESSTOKEN_IMPORT = "Import";

        public const string THING_DELETED_TOKEN = "THING_DELETED_TOKEN";
        public const string TESTEXCEPTIONTEXT = "TESTEXCEPTIONTEXT";

        public static string[] AVAILIBLE_DB_LANGUAGES = new string[] { "de" };
        public const string DEFAULT_DB_LANGUAGE = "de";
        public static string[] AVAILIBLE_EXAMPLE_LANGUAGES = new string[] { "de" , "en" };
        public const string DEFAULT_EXAMPLE_LANGUAGE = "en";

        public const string PROTOCOL_CHAR = "srch://";

        #region File Endings
        public const string DATEIENDUNG_CHAR_1 = ".SRWin";
        public const string DATEIENDUNG_CHAR_2 = ".SRCHChar";
        public const string DATEIENDUNG_CHAR_3 = ".SRHChar";
        public const string DATEIENDUNG_CHAR = DATEIENDUNG_CHAR_3;
        public const string DATEIENDUNG_CSV = ".csv";
        public static readonly List<string> LST_FILETYPES_ALL = new List<string>() { DATEIENDUNG_CHAR, DATEIENDUNG_CSV };
        public static readonly List<string> LST_FILETYPES_CSV = new List<string>() { DATEIENDUNG_CSV };
        public static readonly List<string> LST_FILETYPES_CHAR = new List<string>() { DATEIENDUNG_CHAR_1, DATEIENDUNG_CHAR_2, DATEIENDUNG_CHAR_3 };
        #endregion
        #region Version Numbers
        public const string APP_VERSION_NUMBER_1_3 = "1.3";
        public const string APP_VERSION_NUMBER_1_5 = "1.5";
        public const string APP_VERSION_NUMBER_1_7 = "1.7";
        public const string APP_VERSION_NUMBER = APP_VERSION_NUMBER_1_7;

        public const string CHARFILE_VERSION_1_3 = "1130"; //old version
        public const string CHARFILE_VERSION_1_5 = "1.5";
        public const string CHARFILE_VERSION_1_6 = "1.6";
        public const string CHARFILE_VERSION_1_7 = "1.7";
        public const string CHARFILE_VERSION = CHARFILE_VERSION_1_7;
        #endregion
        #region Tips
        public static List<string> TipList = Helper.StringHelper.GetStrings("Tip");
        #endregion
        #region Help
        public static List<HelpEntry> HelpList = new List<HelpEntry>() {
            new HelpEntry() { Paragraph = CustomManager.GetString("Help4_CharAdministration"), Text = CustomManager.GetString("Help4") },
            new HelpEntry() { Paragraph = CustomManager.GetString("Help5_CharAdministration_FileName"), Text = CustomManager.GetString("Help5") },
            new HelpEntry() { Paragraph = CustomManager.GetString("Help3_LinkedItems"), Text = CustomManager.GetString("Help3") },
            new HelpEntry() { Paragraph = CustomManager.GetString("Help1_ActiveItems"), Text = CustomManager.GetString("Help1") },
            new HelpEntry() { Paragraph = CustomManager.GetString("Help2_AutoCalc"), Text = CustomManager.GetString("Help2") },
        };
        #endregion
        #region Orga-Stuff
#if BETA
        public const string APP_STORE_ID_SRE = "9ncxwgx1kr8s";
#elif RELEASE
        //public const string APP_STORE_ID_SRC = "9nblggh4rhvx";
        public const string APP_STORE_ID_SRE = "9n7sf3p3fp5j";
#endif
        public const string APP_PUBLISHER_MAIL_TvH = "TobiVanHelsinki@live.de";
        public const string APP_PUBLISHER_TvH = "Tobi van Helsinki";

        #endregion
        #region IAPs
        public static List<string> IAP_STORE_LIST_ADDON_TYPES = new List<string>() { "Durable", "Consumable", "UnmanagedConsumable" };
#if BETA
        public const string AD_ADID_MainPageRight = "1100021541";
        public const string AD_ADID_MainPageBottom = "1100021012";
        public const string IAP_FEATUREID_ADFREE_365 = "--";
        public const string IAP_FEATUREID_ADFREE = "--";
        public const string IAP_FEATUREID_TEE = "9N6G5Z236BTH";
#elif RELEASE
        public const string AD_ADID_MainPageRight = "1100025839";
        public const string AD_ADID_MainPageBottom = "1100028115";
        
        //public const string IAP_FEATUREID_ADFREE_365 = "9NKHSRWSBMRD";
        //public const string IAP_FEATUREID_ADFREE = "9NMBBTFVKW84";
        //public const string IAP_FEATUREID_TEE = "9PJF3SD71T40";
        public const string IAP_FEATUREID_ADFREE_365 = "9N3GGT14P8C2";
        public const string IAP_FEATUREID_ADFREE = "9P5W20WCS2C5";
        public const string IAP_FEATUREID_TEE = "9NL3081GB2F4";
#endif

        public static bool IAP_HIDEADS { get; set; }
        #endregion
        #region Diagnostics
        public const string AppCenterID =
#if BETA
            "cea0f814-f9f7-46b1-ba58-760607a60559";
#elif RELEASE
            "20914a32-ddd2-4d7d-93af-97206a32f332";
#else   
        "----";
#endif
        #endregion
    }
}
