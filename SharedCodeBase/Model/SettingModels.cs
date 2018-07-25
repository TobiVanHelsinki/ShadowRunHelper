using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using TAMARIN.IO;
using TAPPLICATION;
using TAPPLICATION.IO;
using TAPPLICATION.Model;
using TLIB;

namespace ShadowRunHelper
{
    public class SettingsModel : SharedSettingsModel
    {
        #region Settings
        [UsedSetting]
        public bool IAP_HIDEADS
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_IAP_HIDEADS);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_IAP_HIDEADS, value);
                Instance.NotifyPropertyChanged();
            }
        }
       
        [UsedSetting]
        public string LAST_APP_VERSION
        {
            get => PlatformSettings.getString(Constants.CONTAINER_SETTINGS_LAST_APP_VERSION);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_LAST_APP_VERSION, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public ProjectPages LAST_PAGE
        {
            get => (ProjectPages)PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_LAST_PAGE);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_LAST_PAGE, (int)value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public bool FORCE_LOAD_CHAR_ON_START
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_FORCE_LOAD_CHAR_ON_START);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_FORCE_LOAD_CHAR_ON_START, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public bool AUTO_SAVE
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_AUTO_SAVE);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_AUTO_SAVE, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public bool TUT_SHOWN_1
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_TUT_SHOWN_1);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_TUT_SHOWN_1, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public int START_COUNT_DB
        {
            get => PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_START_COUNT_DB);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_START_COUNT_DB, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public int START_COUNT
        {
            get => PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_START_COUNT);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_START_COUNT, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public int COUNT_LOADINGS
        {
            get => PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_COUNT_LOADINGS);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_COUNT_LOADINGS, value);
                Instance.NotifyPropertyChanged();
            }
        }
        [UsedSetting]
        public int COUNT_SAVINGS
        {
            get => PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_COUNT_SAVINGS);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_COUNT_SAVINGS, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public int COUNT_DELETIONS
        {
            get => PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_COUNT_DELETIONS);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_COUNT_DELETIONS, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public int COUNT_CREATIONS
        {
            get => PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_COUNT_CREATIONS);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_COUNT_CREATIONS, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public int AUTO_SAVE_INTERVAL_MS
        {
            get => PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_AUTO_SAVE_INTERVAL_MS);
            set
            {
                if (value > 100000)
                {
                    value = 100000;
                }
                else if (value < 1000)
                {
                    value = 1000;
                }
                SystemHelper.WriteLine("value: " + value);
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_AUTO_SAVE_INTERVAL_MS, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public bool LOAD_CHAR_ON_START
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_LOAD_CHAR_ON_START);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_LOAD_CHAR_ON_START, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public bool START_AFTER_EDIT
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_START_AFTER_EDIT);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_START_AFTER_EDIT, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public bool BETA_FEATURES
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_BETA_FEATURES);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_BETA_FEATURES, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public FileInfoClass LAST_SAVE_INFO
        {
            get
            {
                return new FileInfoClass()
                {
                    Filename = PlatformSettings.getString(Constants.CONTAINER_SETTINGS_LAST_CHAR_NAME),
                    Filepath = PlatformSettings.getString(Constants.CONTAINER_SETTINGS_LAST_SAVE_PATH),
                    Fileplace = (Place)PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_LAST_SAVE_PLACE)
                };
            }
            set
            {
                if (value == null)
                {
                    PlatformSettings.set(Constants.CONTAINER_SETTINGS_LAST_CHAR_NAME, Constants.CONTAINER_SETTINGS_LAST_CHAR_NAME_STD);
                    PlatformSettings.set(Constants.CONTAINER_SETTINGS_LAST_SAVE_PATH, Constants.CONTAINER_SETTINGS_LAST_SAVE_PATH_STD);
                    PlatformSettings.set(Constants.CONTAINER_SETTINGS_LAST_SAVE_PLACE, Constants.CONTAINER_SETTINGS_LAST_SAVE_PLACE_STD);
                }
                else
                {
                    PlatformSettings.set(Constants.CONTAINER_SETTINGS_LAST_CHAR_NAME, value.Filename);
                    PlatformSettings.set(Constants.CONTAINER_SETTINGS_LAST_SAVE_PATH, value.Filepath);
                    PlatformSettings.set(Constants.CONTAINER_SETTINGS_LAST_SAVE_PLACE, (int)value.Fileplace);
                }
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public bool FILENAME_USEPROGRESS
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_FILENAME_USEPROGRESS);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_FILENAME_USEPROGRESS, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public bool FILENAME_USEDATE
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_FILENAME_USEDATE);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_FILENAME_USEDATE, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [UsedSetting]
        public bool CHARINTEMPSTORE
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_CHARINTEMPSTORE);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_CHARINTEMPSTORE, value);
                NotifyPropertyChanged();
            }
        }

        #endregion Settings

        #region Start, Konstruktor, Listener
        public static new SettingsModel Initialize()
        {
            if (instance == null)
            {
                SharedIO.CurrentIO.CreateSaveContainer();
                instance = new SettingsModel();
                instance.UsedConstants = typeof(Constants);
            }
            instance.PropertyChanged += SettingsChanged;

            return Instance;
        }

        private static void SettingsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "InternSync":
                    Intern_Sync_Toggled();
                    break;
                case "ORDNERMODE":
                    FolderMode_Toggled();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Singleton Model Thigns
        public static new SettingsModel Instance
        {
            get
            {
                return (SettingsModel)instance;
            }
        }
        public static new SettingsModel I
        {
            get
            {
                return (SettingsModel)instance;
            }
        }
        #endregion

        #region Constraints
        static async void Intern_Sync_Toggled()
        {
            try
            {
                var b = SharedSettingsModel.Instance.INTERN_SYNC;
                var localpath = SharedIO.CurrentIO.GetCompleteInternPath(Place.Local);
                var roampath = SharedIO.CurrentIO.GetCompleteInternPath(Place.Roaming);
                var t = new FileInfoClass(b ? Place.Roaming : Place.Local, "", (b ? roampath : localpath) + SharedConstants.INTERN_SAVE_CONTAINER + @"\");
                var s = new FileInfoClass(b ? Place.Local : Place.Roaming, "", (b ? localpath : roampath) + SharedConstants.INTERN_SAVE_CONTAINER + @"\");
                await SharedIO.CurrentIO.MoveAllFiles(t, s);
            }
            catch (Exception ex)
            {
                AppModel.Instance.NewNotification(StringHelper.GetString("Error_CopyFiles"), ex);
            }
        }
        static async void FolderMode_Toggled()
        {
            var FolderMode = SharedSettingsModel.Instance.FOLDERMODE;
            if (FolderMode)
            {
                try
                {
                    SharedSettingsModel.I.FOLDERMODE_PATH = (await UwpIO.GetFolder(new FileInfoClass() { Fileplace = Place.Extern, FolderToken = Constants.ACCESSTOKEN_FOLDERMODE }, UserDecision.AskUser)).Path;
                }
                catch (Exception ex)
                {
                    SharedSettingsModel.Instance.FOLDERMODE = false;
                    return;
                }
            }
            try
            {
                var InternRoam = SharedSettingsModel.Instance.INTERN_SYNC;

                var internpath = SharedIO.CurrentIO.GetCompleteInternPath(InternRoam ? Place.Roaming : Place.Local) + SharedConstants.INTERN_SAVE_CONTAINER + @"\";
                var externpath = SharedSettingsModel.Instance.FOLDERMODE_PATH;
                var t = new FileInfoClass(FolderMode ? Place.Extern : (InternRoam ? Place.Roaming : Place.Local), "", (FolderMode ? externpath : internpath));
                var s = new FileInfoClass(!FolderMode ? Place.Extern : (InternRoam ? Place.Roaming : Place.Local), "", (!FolderMode ? externpath : internpath));
                await SharedIO.CurrentIO.MoveAllFiles(t, s, Constants.LST_FILETYPES_CHAR);
            }
            catch (Exception ex)
            {
                AppModel.Instance.NewNotification(StringHelper.GetString("Error_CopyFiles"), ex);
            }
        }
        #endregion

    }
}
