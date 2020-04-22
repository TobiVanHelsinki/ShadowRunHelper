//Author: Tobi van Helsinki

///Author: Tobi van Helsinki

using System.Collections.Generic;
using ShadowRunHelper.Model;
using SharedCode.Ressourcen;
using TAPPLICATION;

namespace ShadowRunHelper
{
    public class Constants : SharedConstants
    {
        public const string ObsoleteCalcProperty = "Obsolete with Version 1.8 and the new CalcProperty System";

        public const string ACCESSTOKEN_FOLDERMODE = "ACCESSTOKEN_FOLDERMODE";
        public const string ACCESSTOKEN_FILEACTIVATED = "ACCESSTOKEN_FILEACTIVATED";
        public const string ACCESSTOKEN_EXPORT = "Export";
        public const string ACCESSTOKEN_IMPORT = "Import";

        public const string THING_DELETED_TOKEN = "THING_DELETED_TOKEN";
        public const string TESTEXCEPTIONTEXT = "TESTEXCEPTIONTEXT";

        public static string[] AVAILIBLE_DB_LANGUAGES = new string[] { "de" };
        public const string DEFAULT_DB_LANGUAGE = "de";
        public static string[] AVAILIBLE_EXAMPLE_LANGUAGES = new string[] { "de", "en" };
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
        #endregion File Endings

        #region Version Numbers

        public const string CHARFILE_VERSION_1_3 = "1130"; //old version
        public const string CHARFILE_VERSION_1_5 = "1.5";
        public const string CHARFILE_VERSION_1_6 = "1.6";
        public const string CHARFILE_VERSION_1_7 = "1.7";
        public const string CHARFILE_VERSION_1_8 = "1.8";
        public const string CHARFILE_VERSION = CHARFILE_VERSION_1_8;
        #endregion Version Numbers

        #region Tips
        public static List<string> TipList = Helper.StringHelper.GetStrings("Tip");
        #endregion Tips

        #region Help

        public static List<HelpEntry> HelpList = new List<HelpEntry>() {
            new HelpEntry() { Paragraph = AppResources.Help4_CharAdministration, Text = AppResources.Help4 },
            new HelpEntry() { Paragraph = AppResources.Help5_CharAdministration_FileName, Text = AppResources.Help5},
            new HelpEntry() { Paragraph = AppResources.Help3_LinkedItems, Text = AppResources.Help3},
            new HelpEntry() { Paragraph = AppResources.Help1_ActiveItems, Text = AppResources.Help1},
            new HelpEntry() { Paragraph = AppResources.Help2_AutoCalc, Text = AppResources.Help2},
        };
        #endregion Help

        #region Orga-Stuff
#if BETA
        public const string APP_STORE_ID_SRE = "9ncxwgx1kr8s";
#elif RELEASE

        //public const string APP_STORE_ID_SRC = "9nblggh4rhvx";
        public const string APP_STORE_ID_SRE = "9n7sf3p3fp5j";
#endif
        public const string APP_PUBLISHER_MAIL_TvH = "TobiVanHelsinki@live.de";
        public const string APP_PUBLISHER_TvH = "Tobi van Helsinki";

        #endregion Orga-Stuff

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
        public const string StyleDark = "Dark";
        public const string StyleBrigth = "Brigth";
        public const string StyleScaryGreen = "ScaryGreen";
        public static IEnumerable<string> StyleNames = new[] { StyleDark, StyleBrigth, StyleScaryGreen };
        public const string SpacingCompact = "Compact";
        public const int SpacingCompactValue = 0;
        public const string SpacingMedium = "Medium";
        public const int SpacingMediumValue = 2;
        public const string SpacingWide = "Wide";
        public const int SpacingWideValue = 4;
        public static IEnumerable<string> Spacings = new[] { SpacingCompact, SpacingMedium, SpacingWide };

        #endregion IAPs

        #region Diagnostics

        public const string AppCenterID =
#if BETA
            "cea0f814-f9f7-46b1-ba58-760607a60559";
#elif RELEASE
            "20914a32-ddd2-4d7d-93af-97206a32f332";
#else
        "----";
#endif
        #endregion Diagnostics

        public const string NoResourceFallback = "---";
    }
}