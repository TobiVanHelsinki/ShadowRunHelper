﻿using System.Collections.Generic;
using TLIB;
using TLIB.Model;

namespace ShadowRunHelper
{
    internal class Constants : TLIB.SharedConstants
    {

        /// <summary>
        /// Dateiendungen
        /// </summary>
        public const string DATEIENDUNG_CHAR_1 = ".SRWin";
        public const string DATEIENDUNG_CHAR_2 = ".SRCHChar";
        public const string DATEIENDUNG_CHAR_3 = ".SRHChar";
        public const string DATEIENDUNG_CHAR = DATEIENDUNG_CHAR_3;
        public const string DATEIENDUNG_CSV = ".csv";
        public static readonly List<string> LST_FILETYPES_ALL = new List<string>(new string[] { Constants.DATEIENDUNG_CHAR, Constants.DATEIENDUNG_CSV });
        public static readonly List<string> LST_FILETYPES_CSV = new List<string>(new string[] { Constants.DATEIENDUNG_CSV });
        public static readonly List<string> LST_FILETYPES_CHAR = new List<string>(new string[] { Constants.DATEIENDUNG_CHAR });

        public const string APP_VERSION_NUMBER_1_3 = "1.3";
        public const string APP_VERSION_NUMBER_1_5 = "1.5";
        public const string APP_VERSION_NUMBER = APP_VERSION_NUMBER_1_5;

        /// <summary>
        /// Datei Versionen
        /// </summary>
        public const string CHARFILE_VERSION_1_3 = "1130"; //old version
        public const string CHARFILE_VERSION_1_5 = "1.5";

        /// <summary>
        /// Speicher Constants
        /// </summary>

        /// <summary>
        /// Speicher Einstellungen
        /// </summary>
        public const string CONTAINER_SETTINGS_AUTO_SAVE = "SETTINGS_AUTO_SAVE";
        public const string CONTAINER_SETTINGS_TUT_SHOWN_1 = "SETTINGS_TUT_SHOWN_1";
        public const string CONTAINER_SETTINGS_TUT_SHOWN_2 = "SETTINGS_TUT_SHOWN_2";
        public const string CONTAINER_SETTINGS_TUT_SHOWN_3 = "SETTINGS_TUT_SHOWN_3";
        public const string CONTAINER_SETTINGS_TUT_SHOWN_4 = "SETTINGS_TUT_SHOWN_4";
        public const string CONTAINER_SETTINGS_TUT_SHOWN_5 = "SETTINGS_TUT_SHOWN_5";
        public const string CONTAINER_SETTINGS_AUTO_SAVE_INTERVAL_MS = "SETTINGS_AUTO_SAVE_INTERVAL";
        public const int AUTO_SAVE_INTERVAL_MS_DEFAULT = 5000; //5 sec
        public const string CONTAINER_SETTINGS_LOAD_CHAR_ON_START = "SETTINGS_LOAD_CHAR_ON_START";
        public const string CONTAINER_SETTINGS_START_AFTER_EDIT = "SETTINGS_START_AFTER_EDIT";
        public const string CONTAINER_SETTINGS_START_COUNT = "SETTINGS_START_COUNT";
        public const string CONTAINER_SETTINGS_START_COUNT_DB = "SETTINGS_START_COUNT_DB";
        public const string CONTAINER_SETTINGS_COUNT_LOADINGS = "ETTINGS_COUNT_LOADINGS";
        public const string CONTAINER_SETTINGS_COUNT_SAVINGS = "SETTINGS_COUNT_SAVINGS";
        public const string CONTAINER_SETTINGS_COUNT_DELETIONS = "SETTINGS_COUNT_DELETIONS";
        public const string CONTAINER_SETTINGS_COUNT_CREATIONS = "SETTINGS_COUNT_CREATIONS";
        

        public const string CONTAINER_SETTINGS_FILENAME_USEPROGRESS = "SETTINGS_FILENAME_USEPROGRESS";
        public const string CONTAINER_SETTINGS_FILENAME_USEDATE = "SETTINGS_FILENAME_USEDATE";

        public const string CONTAINER_SETTINGS_LAST_CHAR_NAME = "SETTINGS_LAST_CHAR_NAME";
        public const string CONTAINER_SETTINGS_LAST_SAVE_PATH = "SETTINGS_LAST_SAVE_PATH";
        public const string CONTAINER_SETTINGS_LAST_SAVE_PLACE = "SETTINGS_LAST_SAVE_PLACE";


        public const string APP_STORE_LINK = "ms-windows-store://pdp/?productid=9nblggh4rhvx";
        public const string APP_STORE_REVIEW_LINK = "ms-windows-store://review/?ProductId=9nblggh4rhvx";

        public static List<HelpEntry> HelpList = new List<HelpEntry>() {
            new HelpEntry() { Paragraph = CrossPlatformHelper.GetString("Help4_CharAdministration"), Text = CrossPlatformHelper.GetString("Help4") },
            new HelpEntry() { Paragraph = CrossPlatformHelper.GetString("Help5_CharAdministration_FileName"), Text = CrossPlatformHelper.GetString("Help5") },
            new HelpEntry() { Paragraph = CrossPlatformHelper.GetString("Help3_LinkedItems"), Text = CrossPlatformHelper.GetString("Help3") },
            new HelpEntry() { Paragraph = CrossPlatformHelper.GetString("Help1_ActiveItems"), Text = CrossPlatformHelper.GetString("Help1") },
            new HelpEntry() { Paragraph = CrossPlatformHelper.GetString("Help2_AutoCalc"), Text = CrossPlatformHelper.GetString("Help2") },
       };
    }
}