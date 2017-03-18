using TLIB.Model;

namespace ShadowRunHelper
{
    public class AppSettings : SharedAppSettings
    {
        internal Settings Settings = new Settings();

        public bool SAVE_CHAR_ON_EXIT
        {
            get { return Settings.GetBSaveCharOnExit(); }
            set
            {
                Settings.SetBSaveCharOnExit(value);
                NotifyPropertyChanged();
            }
        }
        public bool LOAD_CHAR_ON_START
        {
            get { return Settings.GetBLoadCharOnStart(); }
            set
            {
                Settings.SetBLoadCharOnStart(value);
                NotifyPropertyChanged();
            }
        }
        public bool bStartEditAfterAdd
        {
            get { return Settings.GetBStartEditAfterAdd(); }
            set
            {
                Settings.SetBStartEditAfterAdd(value);
                NotifyPropertyChanged();
            }
        }

        public static new AppSettings Instance
        {
            get
            {
                return (AppSettings)instance;
            }
        }

    }
}
