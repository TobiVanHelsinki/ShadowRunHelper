using ShadowRunHelper.Model;
using System;
using TAPPLICATION;
using TAPPLICATION.IO;
using TAPPLICATION.Model;
using TLIB;
using TLIB.IO;
using TLIB.PlatformHelper;

namespace ShadowRunHelper
{
    public class SettingsModel : SharedSettingsModel
    {
        #region Settings
        [RoamingSettingAttribute]
        public bool DISABLE_TIPS
        {
            get => PlatformSettings.GetBoolRoaming(Constants.CONTAINER_SETTINGS_DISABLE_TIPS);
            set
            {
                PlatformSettings.SetRoaming(Constants.CONTAINER_SETTINGS_DISABLE_TIPS, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [LocalSettingAttribute]
        public bool IAP_HIDEADS
        {
            get => PlatformSettings.GetBoolLocal(Constants.CONTAINER_SETTINGS_IAP_HIDEADS);
            set
            {
                PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_IAP_HIDEADS, value);
                Instance.NotifyPropertyChanged();
            }
        }
       
        [LocalSettingAttribute]
        public string LAST_APP_VERSION
        {
            get => PlatformSettings.GetStringLocal(Constants.CONTAINER_SETTINGS_LAST_APP_VERSION);
            set
            {
                PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_LAST_APP_VERSION, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [LocalSettingAttribute]
        public ProjectPages LAST_PAGE
        {
            get => (ProjectPages)PlatformSettings.GetIntLocal(Constants.CONTAINER_SETTINGS_LAST_PAGE);
            set
            {
                PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_LAST_PAGE, (int)value);
                Instance.NotifyPropertyChanged();
            }
        }

        [LocalSettingAttribute]
        public bool FORCE_LOAD_CHAR_ON_START
        {
            get => PlatformSettings.GetBoolLocal(Constants.CONTAINER_SETTINGS_FORCE_LOAD_CHAR_ON_START);
            set
            {
                PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_FORCE_LOAD_CHAR_ON_START, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [RoamingSettingAttribute]
        public bool AUTO_SAVE
        {
            get => PlatformSettings.GetBoolRoaming(Constants.CONTAINER_SETTINGS_AUTO_SAVE);
            set
            {
                PlatformSettings.SetRoaming(Constants.CONTAINER_SETTINGS_AUTO_SAVE, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [LocalSettingAttribute]
        public bool IAP_PREMIUM_BADGE
        {
            get => PlatformSettings.GetBoolLocal(Constants.CONTAINER_SETTINGS_IAP_PREMIUM_BADGE);
            set
            {
                PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_IAP_PREMIUM_BADGE, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [LocalSettingAttribute]
        public bool TUT_SHOWN_1
        {
            get => PlatformSettings.GetBoolLocal(Constants.CONTAINER_SETTINGS_TUT_SHOWN_1);
            set
            {
                PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_TUT_SHOWN_1, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [RoamingSettingAttribute]
        public int START_COUNT_DB
        {
            get => PlatformSettings.GetIntRoaming(Constants.CONTAINER_SETTINGS_START_COUNT_DB);
            set
            {
                PlatformSettings.SetRoaming(Constants.CONTAINER_SETTINGS_START_COUNT_DB, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [RoamingSettingAttribute]
        public int START_COUNT
        {
            get => PlatformSettings.GetIntRoaming(Constants.CONTAINER_SETTINGS_START_COUNT);
            set
            {
                PlatformSettings.SetRoaming(Constants.CONTAINER_SETTINGS_START_COUNT, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [RoamingSettingAttribute]
        public int COUNT_LOADINGS
        {
            get => PlatformSettings.GetIntRoaming(Constants.CONTAINER_SETTINGS_COUNT_LOADINGS);
            set
            {
                PlatformSettings.SetRoaming(Constants.CONTAINER_SETTINGS_COUNT_LOADINGS, value);
                Instance.NotifyPropertyChanged();
            }
        }
        [RoamingSettingAttribute]
        public int COUNT_SAVINGS
        {
            get => PlatformSettings.GetIntRoaming(Constants.CONTAINER_SETTINGS_COUNT_SAVINGS);
            set
            {
                PlatformSettings.SetRoaming(Constants.CONTAINER_SETTINGS_COUNT_SAVINGS, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [RoamingSettingAttribute]
        public int COUNT_DELETIONS
        {
            get => PlatformSettings.GetIntRoaming(Constants.CONTAINER_SETTINGS_COUNT_DELETIONS);
            set
            {
                PlatformSettings.SetRoaming(Constants.CONTAINER_SETTINGS_COUNT_DELETIONS, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [RoamingSettingAttribute]
        public int COUNT_CREATIONS
        {
            get => PlatformSettings.GetIntRoaming(Constants.CONTAINER_SETTINGS_COUNT_CREATIONS);
            set
            {
                PlatformSettings.SetRoaming(Constants.CONTAINER_SETTINGS_COUNT_CREATIONS, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [RoamingSettingAttribute]
        public int AUTO_SAVE_INTERVAL_MS
        {
            get => PlatformSettings.GetIntRoaming(Constants.CONTAINER_SETTINGS_AUTO_SAVE_INTERVAL_MS);
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
                System.Diagnostics.Debug.WriteLine("value: " + value);
                PlatformSettings.SetRoaming(Constants.CONTAINER_SETTINGS_AUTO_SAVE_INTERVAL_MS, value);
                Instance.NotifyPropertyChanged();
            }
        }
        

        [LocalSettingAttribute]
        public bool FIRST_START
        {
            get => PlatformSettings.GetBoolLocal(Constants.CONTAINER_SETTINGS_FIRST_START, true);
            set
            {
                PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_FIRST_START, value);
                Instance.NotifyPropertyChanged();
            }
        }
        [LocalSettingAttribute]
        public bool LOAD_CHAR_ON_START
        {
            get => PlatformSettings.GetBoolLocal(Constants.CONTAINER_SETTINGS_LOAD_CHAR_ON_START);
            set
            {
                PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_LOAD_CHAR_ON_START, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [RoamingSettingAttribute]
        public bool START_AFTER_EDIT
        {
            get => PlatformSettings.GetBoolRoaming(Constants.CONTAINER_SETTINGS_START_AFTER_EDIT);
            set
            {
                PlatformSettings.SetRoaming(Constants.CONTAINER_SETTINGS_START_AFTER_EDIT, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [LocalSettingAttribute]
        public FileInfoClass LAST_SAVE_INFO
        {
            get
            {
                return new FileInfoClass()
                {
                    Filename = PlatformSettings.GetStringLocal(Constants.CONTAINER_SETTINGS_LAST_CHAR_NAME),
                    Filepath = PlatformSettings.GetStringLocal(Constants.CONTAINER_SETTINGS_LAST_SAVE_PATH),
                    Fileplace = (Place)PlatformSettings.GetIntLocal(Constants.CONTAINER_SETTINGS_LAST_SAVE_PLACE)
                };
            }
            set
            {
                if (value == null)
                {
                    PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_LAST_CHAR_NAME, Constants.CONTAINER_SETTINGS_LAST_CHAR_NAME_STD);
                    PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_LAST_SAVE_PATH, Constants.CONTAINER_SETTINGS_LAST_SAVE_PATH_STD);
                    PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_LAST_SAVE_PLACE, Constants.CONTAINER_SETTINGS_LAST_SAVE_PLACE_STD);
                    PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_LAST_SAVE_TOKEN, Constants.CONTAINER_SETTINGS_LAST_SAVE_PLACE_STD);
                }
                else
                {
                    PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_LAST_CHAR_NAME, value.Filename);
                    PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_LAST_SAVE_PATH, value.Filepath);
                    PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_LAST_SAVE_PLACE, (int)value.Fileplace);
                    PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_LAST_SAVE_TOKEN, value.Token);
                }
                Instance.NotifyPropertyChanged();
            }
        }

        [RoamingSettingAttribute]
        public bool FILENAME_USEPROGRESS
        {
            get => PlatformSettings.GetBoolRoaming(Constants.CONTAINER_SETTINGS_FILENAME_USEPROGRESS);
            set
            {
                PlatformSettings.SetRoaming(Constants.CONTAINER_SETTINGS_FILENAME_USEPROGRESS, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [RoamingSettingAttribute]
        public bool FILENAME_USEDATE
        {
            get => PlatformSettings.GetBoolRoaming(Constants.CONTAINER_SETTINGS_FILENAME_USEDATE);
            set
            {
                PlatformSettings.SetRoaming(Constants.CONTAINER_SETTINGS_FILENAME_USEDATE, value);
                Instance.NotifyPropertyChanged();
            }
        }

        [LocalSettingAttribute]
        public bool CHARINTEMPSTORE
        {
            get => PlatformSettings.GetBoolLocal(Constants.CONTAINER_SETTINGS_CHARINTEMPSTORE);
            set
            {
                PlatformSettings.SetLocal(Constants.CONTAINER_SETTINGS_CHARINTEMPSTORE, value);
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
                case nameof(INTERN_SYNC):
                    Intern_Sync_Toggled();
                    break;
                case nameof(FOLDERMODE):
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
                    SharedSettingsModel.I.FOLDERMODE_PATH = (await SharedIO.CurrentIO.GetFolderInfo(new FileInfoClass() { Fileplace = Place.Extern, Token = Constants.ACCESSTOKEN_FOLDERMODE }, UserDecision.AskUser)).Filepath;
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
            catch (IsOKException ex)
            {
            }
            catch (Exception ex)
            {
                AppModel.Instance.NewNotification(StringHelper.GetString("Error_CopyFiles"), ex);
            }
        }
        #endregion

    }
}
