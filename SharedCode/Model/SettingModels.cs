﻿//Author: Tobi van Helsinki

using System;
using System.IO;
using SharedCode.Resources;
using ShadowRunHelper;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using TLIB;

namespace ShadowRunHelper
{
    public class SettingsModel : SharedSettingsModel
    {
        #region Settings

        [Setting("SETTINGS_CURRENT_SPACING_STRATEGY", Constants.SpacingMedium, SaveType.Roaming)]
        public string CurrentSpacingStrategy { get => Get(); set => Set(value); }

        [Setting("SETTINGS_CURRENT_STYLE_NAME", Constants.StyleDark, SaveType.Roaming)]
        public string CurrentStyleName { get => Get(); set => Set(value); }

        [Setting("SETTINGS_MINIMIZED_HEADER", false, SaveType.Roaming)]
        public bool MINIMIZED_HEADER { get => Get(); set => Set(value); }

        [Setting("SETTINGS_BACKUP_VERSIONING", false, SaveType.Roaming)]
        public bool BACKUP_VERSIONING { get => Get(); set => Set(value); }

        [Setting("SETTINGS_DISABLE_TIPS", false, SaveType.Roaming)]
        public bool DISABLE_TIPS { get => Get(); set => Set(value); }

        [Setting("SETTINGS_IAP_HIDEADS", false, SaveType.Local)]
        public bool IAP_HIDEADS { get => Get(); set => Set(value); }

        [Setting("SETTINGS_LAST_APP_VERSION", "", SaveType.Local)]
        public string LAST_APP_VERSION { get => Get(); set => Set(value); }

        [Setting("SETTINGS_LAST_PAGE", ProjectPages.Char, SaveType.Local)]
        public ProjectPages LAST_PAGE { get => Get(); set => Set(value); }

        [Setting("SETTINGS_FORCE_LOAD_CHAR_ON_START", false, SaveType.Local)]
        public bool FORCE_LOAD_CHAR_ON_START { get => Get(); set => Set(value); }

        [Setting("SETTINGS_AUTO_SAVE", true, SaveType.Roaming)]
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
        public int AUTO_SAVE_INTERVAL_MS
        {
            get { return Get(); }
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
        public bool START_AFTER_EDIT
        {
            get => Get();
            set => Set(value);
        }

        [Setting("CHARINTEMPSTORE", false, SaveType.Local)]
        public bool CHARINTEMPSTORE { get => Get(); set => Set(value); }

        #endregion Settings

        #region Start, Konstruktor, Listener

        public static new SettingsModel Initialize()
        {
            if (instance == null)
            {
                instance = new SettingsModel();
                try
                {
                    PlatformSettings.PrepareSettingsSavePlace();
                }
                catch (Exception ex)
                {
                    Log.Write("Could not init Settingsmodel", ex, logType: LogType.Error);
                }
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
        #endregion Start, Konstruktor, Listener

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

        #endregion Singleton Model Thigns

        #region Constraints

        private static async void Intern_Sync_Toggled()
        {
            try
            {
                var b = SharedSettingsModel.Instance.INTERN_SYNC;
                var localpath = await SharedIO.CurrentIO.GetCompleteInternPath(Place.Local);
                var roampath = await SharedIO.CurrentIO.GetCompleteInternPath(Place.Roaming);
                var t = new DirectoryInfo(Path.Combine(b ? roampath : localpath, SharedConstants.INTERN_SAVE_CONTAINER));
                var s = new DirectoryInfo(Path.Combine(b ? localpath : roampath, SharedConstants.INTERN_SAVE_CONTAINER));
                await SharedIO.CurrentIO.MoveAllFiles(s, t, Constants.LST_FILETYPES_CHAR);
            }
            catch (Exception ex)
            {
                Log.Write(AppResources.Error_CopyFiles, ex);
            }
        }

        private static async void FolderMode_Toggled()
        {
            try
            {
                if (SharedSettingsModel.Instance.FOLDERMODE && !await SharedIO.CurrentIO.HasAccess(new DirectoryInfo(SharedSettingsModel.Instance.FOLDERMODE_PATH)))
                {
                    SharedSettingsModel.Instance.FOLDERMODE = false;
                }
            }
            catch (Exception ex)
            {
                Log.Write("No access to extern folder", ex, logType: LogType.Error);
            }

            try //copy all data from prev folder to current
            {
                var InternRoam = SharedSettingsModel.Instance.INTERN_SYNC;
                var internpath = await SharedIO.CurrentIO.GetCompleteInternPath(InternRoam ? Place.Roaming : Place.Local) + SharedConstants.INTERN_SAVE_CONTAINER;
                var externpath = SharedSettingsModel.Instance.FOLDERMODE_PATH;
                var t = new DirectoryInfo(SharedSettingsModel.Instance.FOLDERMODE ? externpath : internpath);
                var s = new DirectoryInfo(!SharedSettingsModel.Instance.FOLDERMODE ? externpath : internpath);
                await SharedIO.CurrentIO.MoveAllFiles(s, t, Constants.LST_FILETYPES_CHAR);
            }
            catch (IsOKException)
            {
            }
            catch (Exception ex)
            {
                Log.Write(AppResources.Error_CopyFiles, ex);
            }
        }
        #endregion Constraints
    }
}