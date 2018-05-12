﻿using ShadowRunHelper.Model;
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
        //LastAppVersion
        public string LastAppVersion
        {
            get => PlatformSettings.getString(Constants.CONTAINER_SETTINGS_LAST_APP_VERSION);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_LAST_APP_VERSION, value);
                Instance.NotifyPropertyChanged();
            }
        }

        public void LastAppVersionReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_LAST_PAGE, (int)Constants.CONTAINER_SETTINGS_LAST_PAGE_STD);
        }

        public ProjectPages LastPage
        {
            get => (ProjectPages)PlatformSettings.getInt(Constants.CONTAINER_SETTINGS_LAST_PAGE);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_LAST_PAGE, (int)value);
                Instance.NotifyPropertyChanged();
            }
        }

        public void LastPageReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_LAST_PAGE, (int)Constants.CONTAINER_SETTINGS_LAST_PAGE_STD);
        }

        public bool ForceLoadCharOnStart
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_FORCE_LOAD_CHAR_ON_START);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_FORCE_LOAD_CHAR_ON_START, value);
                Instance.NotifyPropertyChanged();
            }
        }

        public void ForceLoadCharOnStartReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_FORCE_LOAD_CHAR_ON_START, Constants.CONTAINER_SETTINGS_FORCE_LOAD_CHAR_ON_START_STD);
        }
        public bool AutoSave
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_AUTO_SAVE);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_AUTO_SAVE, value);
                Instance.NotifyPropertyChanged();
            }
        }

        public void AutoSaveReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_AUTO_SAVE, Constants.CONTAINER_SETTINGS_AUTO_SAVE_STD);
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
        public void TutorialMainShownReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_TUT_SHOWN_1, Constants.CONTAINER_SETTINGS_TUT_SHOWN_1_STD);
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
        public void StartCountDBReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_START_COUNT_DB, Constants.CONTAINER_SETTINGS_START_COUNT_DB_STD);
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
        public void StartCountReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_START_COUNT, Constants.CONTAINER_SETTINGS_START_COUNT_STD);
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
        public void CountLoadingsReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_COUNT_LOADINGS, Constants.CONTAINER_SETTINGS_COUNT_LOADINGS_STD);
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
        public void CountSavingsReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_COUNT_SAVINGS, Constants.CONTAINER_SETTINGS_COUNT_SAVINGS_STD);
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
        public void CountDeletionsReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_COUNT_DELETIONS, Constants.CONTAINER_SETTINGS_COUNT_DELETIONS_STD);
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
        public void CountCreationsReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_COUNT_CREATIONS, Constants.CONTAINER_SETTINGS_COUNT_CREATIONS_STD);
        }

        public int AutoSaveInterval
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
        public void AutoSaveIntervalReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_AUTO_SAVE_INTERVAL_MS, Constants.CONTAINER_SETTINGS_AUTO_SAVE_INTERVAL_MS_STD);
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
        public void LoadCharOnStartReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_LOAD_CHAR_ON_START, Constants.CONTAINER_SETTINGS_LOAD_CHAR_ON_START_STD);
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
        public void StartEditAfterAddReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_START_AFTER_EDIT, Constants.CONTAINER_SETTINGS_START_AFTER_EDIT_STD);
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
        public void BetaFeaturesReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_BETA_FEATURES, Constants.CONTAINER_SETTINGS_BETA_FEATURES_STD);
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
        public void LastSaveInfoReset()
        {
            LastSaveInfo = null;
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
        public void FileNameUseProgresReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_FILENAME_USEPROGRESS, Constants.CONTAINER_SETTINGS_FILENAME_USEPROGRESS_STD);
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
        public void FileNameUseDateReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_FILENAME_USEDATE, Constants.CONTAINER_SETTINGS_FILENAME_USEDATE_STD);
        }

        public bool CharInTempStore
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_CHARINTEMPSTORE);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_CHARINTEMPSTORE, value);
                NotifyPropertyChanged();
            }
        }
        public void CharInTempStoreReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_CHARINTEMPSTORE, Constants.CONTAINER_SETTINGS_CHARINTEMPSTORE_STD);
        }



        #endregion Settings

        #region Singleton Model Thigns
        //================================================
        public static new SettingsModel Initialize()
        {
            if (instance == null)
            {
                SharedIO.CurrentIO.CreateSaveContainer();
                instance = new SettingsModel();
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

        #region Contraints
        static async void Intern_Sync_Toggled()
        {
            try
            {
                var b = SharedSettingsModel.Instance.InternSync;
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
            var FolderMode = SharedSettingsModel.Instance.ORDNERMODE;
            if (FolderMode)
            {
                try
                {
                    SharedSettingsModel.I.ORDNERMODE_PFAD = (await UwpIO.GetFolder(new FileInfoClass() { Fileplace = Place.Extern, FolderToken = Constants.ACCESSTOKEN_FOLDERMODE }, UserDecision.AskUser)).Path;
                }
                catch (Exception ex)
                {
                    SharedSettingsModel.Instance.ORDNERMODE = false;
                    return;
                }
            }
            try
            {
                var InternRoam = SharedSettingsModel.Instance.InternSync;

                var internpath = SharedIO.CurrentIO.GetCompleteInternPath(InternRoam ? Place.Roaming : Place.Local) + SharedConstants.INTERN_SAVE_CONTAINER + @"\";
                var externpath = SharedSettingsModel.Instance.ORDNERMODE_PFAD;
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
