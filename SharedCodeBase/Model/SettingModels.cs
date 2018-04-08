using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using TLIB_UWPFRAME.IO;
using TLIB_UWPFRAME.Model;
using static TLIB_UWPFRAME.Model.SharedSettingsModel;
using Newtonsoft.Json;
using Windows.Storage;

namespace ShadowRunHelper
{
    public class SettingsModel : SharedSettingsModel
    {
        #region Settings
        //static JsonSerializerSettings SerializationSettings = new JsonSerializerSettings() { Error = SerializationErrorHandler };
        //static void SerializationErrorHandler(object o, Newtonsoft.Json.Serialization.ErrorEventArgs a)
        //{
        //    o = null;
        //}
        //public IEnumerable<(ThingDefs ThingType, bool vis)> BlockListOptions
        //{
        //    get
        //    {
        //        try
        //        {
        //            var content = PlatformSettings.getString(Constants.CONTAINER_SETTINGS_BLOCKLISTOPTIONS);
        //            return JsonConvert.DeserializeObject<IEnumerable<(ThingDefs ThingType, bool vis)>>(content, SerializationSettings);
        //        }
        //        catch (System.Exception)
        //        {
        //            BlockListOptions = TypeHelper.ThingTypeProperties.Select(t => (t.ThingType, true));
        //            return BlockListOptions;
        //        }
        //    }
        //    set
        //    {
        //        var content = JsonConvert.SerializeObject(value, SerializationSettings);
        //        PlatformSettings.set(Constants.CONTAINER_SETTINGS_BLOCKLISTOPTIONS, content);
        //        Instance.NotifyPropertyChanged();
        //    }
        //}

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

        public bool TutorialHandlungShown
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_TUT_SHOWN_4);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_TUT_SHOWN_4, value);
                Instance.NotifyPropertyChanged();
            }
        }

        public void TutorialHandlungShownReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_TUT_SHOWN_4, Constants.CONTAINER_SETTINGS_TUT_SHOWN_4_STD);
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

        public bool TutorialCharListShown
        {
            get => PlatformSettings.getBool(Constants.CONTAINER_SETTINGS_TUT_SHOWN_3);
            set
            {
                PlatformSettings.set(Constants.CONTAINER_SETTINGS_TUT_SHOWN_3, value);
                Instance.NotifyPropertyChanged();
            }
        }
        public void TutorialCharListShownReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_TUT_SHOWN_3, Constants.CONTAINER_SETTINGS_TUT_SHOWN_3_STD);
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
        public void TutorialCharShownReset()
        {
            PlatformSettings.set(Constants.CONTAINER_SETTINGS_TUT_SHOWN_2, Constants.CONTAINER_SETTINGS_TUT_SHOWN_2_STD);
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

        #endregion Settings

        #region Singleton Model Thigns
        //================================================
        public static new SettingsModel Initialize()
        {
            if (instance == null)
            {
                ApplicationData.Current.LocalSettings.CreateContainer(Constants.CONTAINER_SETTINGS, ApplicationDataCreateDisposition.Always);
                instance = new SettingsModel();
            }
            //#region Custom Stuff
            //if (Instance.BlockListOptions == null)
            //{ // create
            //}
            //#endregion
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



        #endregion

    }
}
