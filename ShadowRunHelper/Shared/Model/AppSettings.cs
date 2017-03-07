using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper
{
    public class AppSettings : INotifyPropertyChanged
    {
        public ISettings Settings = null;

        public bool SAVE_CHAR_ON_EXIT
        {
            get { return Settings.GetBSaveCharOnExit(); }
            set
            {
                Settings.SetBSaveCharOnExit(value);
                NotifyPropertyChanged();
            }
        }
        public bool bDisplayRequest
        {
            get { return Settings.GetBDisplayRequest(); }
            set
            {
                Settings.SetBDisplayRequest(value);
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
        public bool ORDNERMODE
        {
            get { return Settings.GetBFolderMode(); }
            set
            {
                Settings.SetBFolderMode(value);
                NotifyPropertyChanged();
            }
        }
        public string ORDNERMODE_PFAD
        {
            get { return Settings.GetStrFolderModePath(); }
            set
            {
                Settings.SetStrFolderModePath(value);
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

        AppSettings()
        {
#if __ANDROID__
#else
            Settings = new WinSettings();
#endif
        }

        static readonly AppSettings instance = new AppSettings();

        public static AppSettings Instance
        {
            get
            {
                return instance;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
