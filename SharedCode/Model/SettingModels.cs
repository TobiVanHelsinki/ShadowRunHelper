using ShadowRunHelper.Model;
using System;
using TAPPLICATION;
using TAPPLICATION.IO;
using TAPPLICATION.Model;
using TLIB;

namespace ShadowRunHelper
{
    public class SettingsModel : SharedSettingsModel
    {
        #region Settings
        [Setting("SETTINGS_BACKUP_VERSIONING", false, SaveType.Roaming)]
        public bool BACKUP_VERSIONING { get => Get(); set => Set(value); }

        [Setting("SETTINGS_DISABLE_TIPS", false, SaveType.Roaming)]
        public bool DISABLE_TIPS { get => Get(); set => Set(value); }

        [Setting("SETTINGS_IAP_HIDEADS", false, SaveType.Local)]
        public bool IAP_HIDEADS { get => Get(); set => Set(value); }

        [Setting("SETTINGS_LAST_APP_VERSION", "", SaveType.Local)]
        public string LAST_APP_VERSION { get => Get(); set => Set(value); }

        [Setting("SETTINGS_LAST_PAGE", ProjectPages.Char, SaveType.Local, typeof(int))]
        public ProjectPages LAST_PAGE { get => (ProjectPages)Get(); set => Set(value); }

        [Setting("SETTINGS_FORCE_LOAD_CHAR_ON_START", false, SaveType.Local)]
        public bool FORCE_LOAD_CHAR_ON_START { get => Get(); set => Set(value); }

        [Setting("SETTINGS_AUTO_SAVE", false, SaveType.Roaming)]
        public bool AUTO_SAVE { get => Get(); set => Set(value); }

        [Setting("IAP_BADGE", false, SaveType.Local)]
        public bool IAP_PREMIUM_BADGE { get => Get(); set => Set(value); }

        [Setting("SETTINGS_TUT_SHOWN_1", false, SaveType.Local)]
        public bool TUT_SHOWN_1 { get => Get(); set => Set(value); }

        [Setting("SETTINGS_START_COUNT_DB", 0, SaveType.Roaming)]
        public int START_COUNT_DB { get => Get(); set => Set(value); }

        [Setting("SETTINGS_START_COUNT", 0, SaveType.Roaming)]
        public int START_COUNT { get => Get(); set => Set(value); }

        [Setting("SETTINGS_COUNT_LOADINGS", 0, SaveType.Roaming)]
        public int COUNT_LOADINGS { get => Get(); set => Set(value); }
        [Setting("SETTINGS_COUNT_SAVINGS", 0, SaveType.Roaming)]
        public int COUNT_SAVINGS { get => Get(); set => Set(value); }

        [Setting("SETTINGS_COUNT_DELETIONS", 0, SaveType.Roaming)]
        public int COUNT_DELETIONS { get => Get(); set => Set(value); }

        [Setting("SETTINGS_COUNT_CREATIONS", 0, SaveType.Roaming)]
        public int COUNT_CREATIONS { get => Get(); set => Set(value); }

        [Setting("SETTINGS_AUTO_SAVE_INTERVAL", 0, SaveType.Roaming)]
        public int AUTO_SAVE_INTERVAL_MS { get {  return Get(); }
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
                Set(value);
            }
        }

        [Setting("FIRST_START", true, SaveType.Local)]
        public bool FIRST_START { get => Get(); set => Set(value); }

        [Setting("SETTINGS_LOAD_CHAR_ON_START", false, SaveType.Local)]
        public bool LOAD_CHAR_ON_START { get => Get(); set => Set(value); }

        [Setting("SETTINGS_START_AFTER_EDIT", true, SaveType.Roaming)]
        public bool START_AFTER_EDIT { get => Get(); set => Set(value); }

        [Setting("CHARINTEMPSTORE", false, SaveType.Local)]
        public bool CHARINTEMPSTORE { get => Get(); set => Set(value); }

        #endregion Settings

        #region Start, Konstruktor, Listener
        public static new SettingsModel Initialize()
        {
            if (instance == null)
            {
                SharedIO.CurrentIO?.CreateSaveContainer();
                instance = new SettingsModel();
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
                AppModel.Instance?.NewNotification(PlatformHelper.GetString("Error_CopyFiles"), ex);
            }
        }
        static async void FolderMode_Toggled()
        {
            var FolderMode = SharedSettingsModel.Instance.FOLDERMODE;
            if (FolderMode)
            {
                try
                {
                    var info = new FileInfoClass(Place.Extern, "", "") { Token = Constants.ACCESSTOKEN_FOLDERMODE };
                    var folder = await SharedIO.CurrentIO.GetFolderInfo(info, UserDecision.AskUser);
                    SharedSettingsModel.I.FOLDERMODE_PATH = folder.Filepath;
                }
                catch (Exception ex)
 { TAPPLICATION.Debugging.TraceException(ex);
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
            catch (IsOKException)
            {
            }
            catch (Exception ex)
            {
                AppModel.Instance?.NewNotification(PlatformHelper.GetString("Error_CopyFiles"), ex);
            }
        }
        #endregion

    }

}
