using TLIB.IO;
using TLIB.Model;
using static TLIB.Model.SharedSettingsModel;

namespace ShadowRunHelper
{
    public class SettingsModel : SharedSettingsModel
    {
        public bool AutoSave
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_AUTO_SAVE);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_AUTO_SAVE, value);
                Instance.NotifyPropertyChanged();
            }
        }
        
        public bool TutorialHandlungShown
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_TUT_SHOWN_4);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_TUT_SHOWN_4, value);
                Instance.NotifyPropertyChanged();
            }
        }

        public bool TutorialMainShown
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_TUT_SHOWN_1);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_TUT_SHOWN_1, value);
                Instance.NotifyPropertyChanged();
            }
        }

        public bool TutorialCharListShown
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_TUT_SHOWN_3);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_TUT_SHOWN_3, value);
                Instance.NotifyPropertyChanged();
            }
        }


        public bool TutorialCharShown
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_TUT_SHOWN_2);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_TUT_SHOWN_2, value);
                Instance.NotifyPropertyChanged();
            }
        }

        public int StartCountDB
        {
            get => PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_START_COUNT_DB);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_START_COUNT_DB, value);
                Instance.NotifyPropertyChanged();
            }
        }
        public int StartCount
        {
            get => PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_START_COUNT);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_START_COUNT, value);
                Instance.NotifyPropertyChanged();
            }
        }
        public int CountLoadings
        {
            get => PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_COUNT_LOADINGS);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_COUNT_LOADINGS, value);
                Instance.NotifyPropertyChanged();
            }
        }
        public int CountSavings
        {
            get => PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_COUNT_SAVINGS);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_COUNT_SAVINGS, value);
                Instance.NotifyPropertyChanged();
            }
        }
        public int CountDeletions
        {
            get => PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_COUNT_DELETIONS);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_COUNT_DELETIONS, value);
                Instance.NotifyPropertyChanged();
            }
        }
        public int CountCreations
        {
            get => PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_COUNT_CREATIONS);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_COUNT_CREATIONS, value);
                Instance.NotifyPropertyChanged();
            }
        }
        
        public int AutoSaveInterval
        {
            get => PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_AUTO_SAVE_INTERVAL_MS);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_AUTO_SAVE_INTERVAL_MS, value);
                Instance.NotifyPropertyChanged();
            }
        }
        public bool LoadCharOnStart
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_LOAD_CHAR_ON_START);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_LOAD_CHAR_ON_START, value);
                Instance.NotifyPropertyChanged();
            }
        }
        public bool StartEditAfterAdd
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_START_AFTER_EDIT);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_START_AFTER_EDIT, value);
                Instance.NotifyPropertyChanged();
            }
        }
        public bool BetaFeatures
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_BETA_FEATURES);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_BETA_FEATURES, value);
                Instance.NotifyPropertyChanged();
            }
        }
        public FileInfoClass LastSaveInfo
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
                    PlatformSettings.set(Constants.CONTAINER_SETTINGS_LAST_CHAR_NAME, string.Empty);
                    PlatformSettings.set(Constants.CONTAINER_SETTINGS_LAST_SAVE_PATH, string.Empty);
                    PlatformSettings.set(Constants.CONTAINER_SETTINGS_LAST_SAVE_PLACE, string.Empty);
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
        public bool FileNameUseProgres
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_FILENAME_USEPROGRESS);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_FILENAME_USEPROGRESS, value);
                Instance.NotifyPropertyChanged();
            }
        }
        public bool FileNameUseDate
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_FILENAME_USEDATE);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_FILENAME_USEDATE, value);
                Instance.NotifyPropertyChanged();
            }
        }
        //================================================
        public static new SettingsModel Initialize()
        {
            if (instance == null)
            {
                instance = new SettingsModel();
            }
            return Instance;
        }
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

    }
}
