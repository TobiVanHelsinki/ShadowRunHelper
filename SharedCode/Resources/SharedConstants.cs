namespace ShadowRunHelper
{
    public class SharedConstants
    {
        #region TOKEN
        public const string STRING_APP_VERSION_NUMBER = "APP_VERSION_NUMBER";
        public const string STRING_FILE_VERSION_NUMBER = "FILE_VERSION_NUMBER";
        public const string ERROR_TOKEN = "ERROR";
        #endregion

        #region Speicher Container
        public const string INTERN_SAVE_CONTAINER = "Char_Store";
        public const string CONTAINER_SETTINGS = "Char_Settings";
        #endregion

        #region AppStore Constants
        public static string APP_STORE_LINK { get => APP_STORE_LINK_SHAPE + APP_STORE_ID ?? ""; }
        public static string APP_STORE_REVIEW_LINK { get => APP_STORE_REVIEW_LINK_SHAPE + APP_STORE_ID ?? ""; }
        public static string APP_MORE_APPS { get => APP_MORE_APPS_SHAPE + APP_PUBLISHER; }
        public static string APP_PUBLISHER_MAILTO { get => APP_CONTACT_MAILTO_SHAPE + APP_PUBLISHER_MAIL; }

        #region Shapes
        public const string APP_MORE_APPS_SHAPE = "ms-windows-store://publisher/?name=";
        public const string APP_STORE_LINK_SHAPE = "ms-windows-store://pdp/?productid=";
        public const string APP_STORE_REVIEW_LINK_SHAPE = "ms-windows-store://review/?ProductId=";
        public const string APP_CONTACT_MAILTO_SHAPE = "mailto:";
        public const string SettingsPrefix = "CONTAINER_SETTINGS_";
        public const string SettingsSTDPostfix = "_STD";
        #endregion

        #region SetByApp
        public static string APP_VERSION_BUILD_DELIM { get; set; }
        public static string APP_STORE_ID { get; set; }
        public static string APP_PUBLISHER_MAIL { get; set; }
        public static string APP_PUBLISHER { get; set; }
        #endregion
        #endregion

    }
}
