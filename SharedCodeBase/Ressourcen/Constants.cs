using ShadowRunHelper.Model;
using System.Collections.Generic;
using TAPPLICATION;
using TLIB;

namespace ShadowRunHelper
{
    internal class Constants : SharedConstants
    {
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
        public static readonly List<string> LST_FILETYPES_ALL = new List<string>(new string[] { Constants.DATEIENDUNG_CHAR, Constants.DATEIENDUNG_CSV });
        public static readonly List<string> LST_FILETYPES_CSV = new List<string>(new string[] { Constants.DATEIENDUNG_CSV });
        public static readonly List<string> LST_FILETYPES_CHAR = new List<string>(new string[] { Constants.DATEIENDUNG_CHAR });
        #endregion
        #region Version Numbers
        public const string APP_VERSION_NUMBER_1_3 = "1.3";
        public const string APP_VERSION_NUMBER_1_5 = "1.5";
        public const string APP_VERSION_NUMBER_1_7 = "1.7";
        public const string APP_VERSION_NUMBER = APP_VERSION_NUMBER_1_7;

        public const string CHARFILE_VERSION_1_3 = "1130"; //old version
        public const string CHARFILE_VERSION_1_5 = "1.5";
        public const string CHARFILE_VERSION_1_6 = "1.6";
        public const string CHARFILE_VERSION = CHARFILE_VERSION_1_6;
        #endregion
        #region Settings
        public const string CONTAINER_SETTINGS_BLOCKLISTOPTIONS = "SETTINGS_BLOCKLISTOPTIONS";
        public const string CONTAINER_SETTINGS_AUTO_SAVE = "SETTINGS_AUTO_SAVE";
        public const bool CONTAINER_SETTINGS_AUTO_SAVE_STD = false;
        public const string CONTAINER_SETTINGS_TUT_SHOWN_1 = "SETTINGS_TUT_SHOWN_1";
        public const bool CONTAINER_SETTINGS_TUT_SHOWN_1_STD = false;
        public const string CONTAINER_SETTINGS_TUT_SHOWN_2 = "SETTINGS_TUT_SHOWN_2";
        public const bool CONTAINER_SETTINGS_TUT_SHOWN_2_STD = false;
        public const string CONTAINER_SETTINGS_TUT_SHOWN_3 = "SETTINGS_TUT_SHOWN_3";
        public const bool CONTAINER_SETTINGS_TUT_SHOWN_3_STD = false;
        public const string CONTAINER_SETTINGS_TUT_SHOWN_4 = "SETTINGS_TUT_SHOWN_4";
        public const bool CONTAINER_SETTINGS_TUT_SHOWN_4_STD = false;
        public const string CONTAINER_SETTINGS_TUT_SHOWN_5 = "SETTINGS_TUT_SHOWN_5";
        public const bool CONTAINER_SETTINGS_TUT_SHOWN_5_STD = false;
        public const string CONTAINER_SETTINGS_AUTO_SAVE_INTERVAL_MS = "SETTINGS_AUTO_SAVE_INTERVAL";
        public const int CONTAINER_SETTINGS_AUTO_SAVE_INTERVAL_MS_STD = 5000; //5 sec
        public const string CONTAINER_SETTINGS_LOAD_CHAR_ON_START = "SETTINGS_LOAD_CHAR_ON_START";
        public const bool CONTAINER_SETTINGS_LOAD_CHAR_ON_START_STD = false;
        public const string CONTAINER_SETTINGS_FORCE_LOAD_CHAR_ON_START = "SETTINGS_FORCE_LOAD_CHAR_ON_START";
        public const bool CONTAINER_SETTINGS_FORCE_LOAD_CHAR_ON_START_STD = false;
        public const string CONTAINER_SETTINGS_START_AFTER_EDIT = "SETTINGS_START_AFTER_EDIT";
        public const bool CONTAINER_SETTINGS_START_AFTER_EDIT_STD = true;
        public const string CONTAINER_SETTINGS_START_COUNT = "SETTINGS_START_COUNT";
        public const int CONTAINER_SETTINGS_START_COUNT_STD = 0;
        public const string CONTAINER_SETTINGS_START_COUNT_DB = "SETTINGS_START_COUNT_DB";
        public const int CONTAINER_SETTINGS_START_COUNT_DB_STD = 0;
        public const string CONTAINER_SETTINGS_COUNT_LOADINGS = "ETTINGS_COUNT_LOADINGS";
        public const int CONTAINER_SETTINGS_COUNT_LOADINGS_STD = 0;
        public const string CONTAINER_SETTINGS_COUNT_SAVINGS = "SETTINGS_COUNT_SAVINGS";
        public const int CONTAINER_SETTINGS_COUNT_SAVINGS_STD = 0;
        public const string CONTAINER_SETTINGS_COUNT_DELETIONS = "SETTINGS_COUNT_DELETIONS";
        public const int CONTAINER_SETTINGS_COUNT_DELETIONS_STD = 0;
        public const string CONTAINER_SETTINGS_COUNT_CREATIONS = "SETTINGS_COUNT_CREATIONS";
        public const int CONTAINER_SETTINGS_COUNT_CREATIONS_STD = 0;


        public const string CONTAINER_SETTINGS_FILENAME_USEPROGRESS = "SETTINGS_FILENAME_USEPROGRESS";
        public const bool CONTAINER_SETTINGS_FILENAME_USEPROGRESS_STD = false;
        public const string CONTAINER_SETTINGS_FILENAME_USEDATE = "SETTINGS_FILENAME_USEDATE";
        public const bool CONTAINER_SETTINGS_FILENAME_USEDATE_STD = false;

        public const string CONTAINER_SETTINGS_LAST_CHAR_NAME = "SETTINGS_LAST_CHAR_NAME";
        public const string CONTAINER_SETTINGS_LAST_CHAR_NAME_STD = "";
        public const string CONTAINER_SETTINGS_LAST_SAVE_PATH = "SETTINGS_LAST_SAVE_PATH";
        public const string CONTAINER_SETTINGS_LAST_SAVE_PATH_STD = "";
        public const string CONTAINER_SETTINGS_LAST_SAVE_PLACE = "SETTINGS_LAST_SAVE_PLACE";
        public const string CONTAINER_SETTINGS_LAST_SAVE_PLACE_STD = "";

        public const string CONTAINER_SETTINGS_CHARINTEMPSTORE = "CHARINTEMPSTORE";
        public const bool CONTAINER_SETTINGS_CHARINTEMPSTORE_STD = false;

        public const string CONTAINER_SETTINGS_LAST_PAGE = "SETTINGS_LAST_PAGE";
        public const ProjectPages CONTAINER_SETTINGS_LAST_PAGE_STD = ProjectPages.Char;
        

        #endregion
        #region Help
        public static List<HelpEntry> HelpList = new List<HelpEntry>() {
            new HelpEntry() { Paragraph = StringHelper.GetString("Help4_CharAdministration"), Text = StringHelper.GetString("Help4") },
            new HelpEntry() { Paragraph = StringHelper.GetString("Help5_CharAdministration_FileName"), Text = StringHelper.GetString("Help5") },
            new HelpEntry() { Paragraph = StringHelper.GetString("Help3_LinkedItems"), Text = StringHelper.GetString("Help3") },
            new HelpEntry() { Paragraph = StringHelper.GetString("Help1_ActiveItems"), Text = StringHelper.GetString("Help1") },
            new HelpEntry() { Paragraph = StringHelper.GetString("Help2_AutoCalc"), Text = StringHelper.GetString("Help2") },
        };
        #endregion
        #region IAPs
        public static List<string> IAP_STORE_LIST_ADDON_TYPES = new List<string>() { "Durable", "Consumable", "UnmanagedConsumable" };

#if BETA
        public const string IAP_FEATUREID_ADFREE_365 = "--";
        public const string IAP_FEATUREID_ADFREE = "--";
        public const string IAP_FEATUREID_TEE = "9N6G5Z236BTH";
#elif RELEASE
        public const string IAP_FEATUREID_ADFREE_365 = "9NKHSRWSBMRD";
        public const string IAP_FEATUREID_ADFREE = "9NMBBTFVKW84";
        public const string IAP_FEATUREID_TEE = "9PJF3SD71T40";
#endif
#if BETA
        public const string AD_APPID = "9ncxwgx1kr8s";
        public const string AD_ADID_MainPageRight = "1100021541";
        public const string AD_ADID_MainPageBottom = "1100021012";
#elif RELEASE
        public const string AD_APPID = "9nblggh4rhvx";
        public const string AD_ADID_MainPageRight = "1100022060";
        public const string AD_ADID_MainPageBottom = "1100022061";
#endif

        public static bool IAP_HIDEADS { get; internal set; }
        #endregion
        #region Diagnostics
        public const string AppCenterID =
#if BETA
            "cea0f814-f9f7-46b1-ba58-760607a60559";
#elif RELEASE
            "ef331e8e-b76a-41c1-a93a-a1a9d475b141";
#endif

        #endregion
    }
}
