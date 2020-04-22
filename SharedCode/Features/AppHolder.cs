//Author: Tobi van Helsinki

using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Threading.Tasks;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using SharedCode.Ressourcen;
using TAPPLICATION;
using TAPPLICATION.IO;
using TAPPLICATION.Model;
using TLIB;

[assembly: NeutralResourcesLanguageAttribute("en")]

namespace ShadowRunHelper
{
    public static class AppHolder
    {
        private static AppModel Model;
        private static SettingsModel Settings;
        private static bool FirstStart = true;

        #region Init

        public static void InitModel()
        {
            if (Settings is null)
            {
                Settings = SettingsModel.Initialize();
            }
            if (Model is null)
            {
                Model = AppModel.Initialize();
            }
        }

        public static void Init()
        {
#if DEBUG
            Log.IsConsoleLogEnabled = true;
#endif
            Log.IsFileLogEnabled = true;
            Log.IsInMemoryLogEnabled = true;
            Log.InMemoryLogMaxCount = 1000;
            if (Settings.FIRST_START)
            {
                Settings.InitSettings();
            }
            Settings.START_COUNT++;
            try
            {
                Task.WaitAll(
                    Task.Run(() => Features.IAP.CheckLicence()),
                    Task.Run(AppCenterConfiguration),
                    Task.Run(SetConstantStuff),
                    Task.Run(CreateFolder),
                    Task.Run(RegisterAppInstance)
                    );
                Task.Run(CharLoadingHandling).Wait();
            }
            catch (ObjectDisposedException)
            {
            }
            catch (AggregateException)
            {
            }
            Test.DoSmt();
        }

        public static async void StartInit()
        {
            await Task.Run(Init);
        }

        public static void RegisterAppInstance()
        {
            Features.InstanceHandling.CreateInstance();
        }

        public static void FileActivated(string name, string path)
        {
            FileActivated(Path.Combine(path, name));
        }

        public static void FileActivated(string fullPath)
        {
            InitModel();
            Settings.FORCE_LOAD_CHAR_ON_START = true;
            AppModel.Instance.IsFileActivated = true;
            try
            {
                Settings.LAST_SAVE_INFO = new FileInfo(fullPath);
            }
            catch (Exception)
            {
                Settings.LAST_SAVE_INFO = null;
            }

            if (!FirstStart)
            {
                CharLoadingHandling();
            }
        }

        private static void AppCenterConfiguration()
        {
            try
            {
                Features.Analytics.Init();
            }
            catch (Exception ex)
            {
                Log.Write("Could not Analytics.Init", ex, logType: LogType.Error);
            }
        }

        private static void SetConstantStuff()
        {
            SharedConstants.APP_VERSION_BUILD_DELIM = string.Format("{0}.{1}.{2}.{3}", Features.AppInformation.Version_Major, Features.AppInformation.Version_Minor, Features.AppInformation.Version_Build, Features.AppInformation.Version_Revision);
            SharedConstants.APP_PUBLISHER_MAIL = Constants.APP_PUBLISHER_MAIL_TvH;
            SharedConstants.APP_PUBLISHER = Constants.APP_PUBLISHER_TvH;
            SharedConstants.APP_STORE_ID = Constants.APP_STORE_ID_SRE;
        }

        private static async void CreateFolder()
        {
            try
            {
                SharedIO.CurrentIO.CreateFolder(new DirectoryInfo(Path.Combine(await SharedIO.CurrentIO.GetCompleteInternPath(Place.Local), SharedConstants.INTERN_SAVE_CONTAINER)));
                SharedIO.CurrentIO.CreateFolder(new DirectoryInfo(Path.Combine(await SharedIO.CurrentIO.GetCompleteInternPath(Place.Roaming), SharedConstants.INTERN_SAVE_CONTAINER)));
            }
            catch (Exception ex)
            {
                TLIB.Log.Write("Cannot create intern folder", ex);
            }
        }
        #endregion Init

        internal static void LeavingBackground()
        {
            CharLoadingHandling();
        }

        public static async void EnteredBackground()
        {
            if (Model.MainObject != null)
            {
                try
                {
                    if (Settings.AUTO_SAVE)
                    {
                        Settings.LAST_SAVE_INFO = await SharedIO.SaveAtOriginPlace(Model.MainObject);
                        Settings.COUNT_SAVINGS++;
                    }
                    else
                    {
                        Settings.LAST_SAVE_INFO = await SharedIO.SaveAtTempPlace(Model.MainObject);
                    }
                    Settings.CHARINTEMPSTORE = true;
                }
                catch (Exception ex)
                { Log.Write("Could not EnteredBackground", ex, logType: LogType.Error); }
            }
        }

        private static async Task CharLoadingHandling()
        {
            try
            {
                if ((Settings.CHARINTEMPSTORE && !FirstStart || Settings.LOAD_CHAR_ON_START && FirstStart) && Model.MainObject == null || Settings.FORCE_LOAD_CHAR_ON_START)
                {
                    var info = Settings.LAST_SAVE_INFO;
                    Model.CharInProgress = info;
                    var TMPChar = await CharHolderIO.Load(info);

                    if (TMPChar.FileInfo.Directory.FullName.Contains(await SharedIO.CurrentIO.GetCompleteInternPath(Place.Temp)))
                    {
                        CharHolderIO.SaveAtCurrentPlace(TMPChar);
                    }
                    var OldChar = Model.MainObject;
                    Model.MainObject = TMPChar;
                    Settings.COUNT_LOADINGS++;
                    if (OldChar != null)
                    {
                        try
                        {
                            CharHolderIO.SaveAtOriginPlace(OldChar);
                        }
                        catch (Exception ex)
                        {
                            Log.Write(AppResources.Error_FileActivation, ex);
                        }
                    }
                    if (Settings.FORCE_LOAD_CHAR_ON_START)
                    {
                        Log.Write(AppResources.Info_Char_Loaded_File);
                    }
                    else
                    {
                        Log.Write(AppResources.Info_Char_Loaded_Start);
                    }
                }
            }
            catch (Exception ex)
            { Log.Write("Could not", ex, logType: LogType.Error); }
            finally
            {
                Model.CharInProgress = null;
                Settings.FORCE_LOAD_CHAR_ON_START = false;
                FirstStart = false;
                Settings.LAST_SAVE_INFO = null;
                Settings.CHARINTEMPSTORE = false;
                Model.RequestNavigation(ProjectPages.Char);
            }
        }

        public static void CheckVersion()
        {
            if (SettingsModel.I.LAST_APP_VERSION != SharedConstants.APP_VERSION_BUILD_DELIM)
            {
                Log.Write(AppResources.VersionHistory);
                Log.Write(string.Format(AppResources.Info_NewVersion, SharedConstants.APP_VERSION_BUILD_DELIM), true);
                SettingsModel.I.LAST_APP_VERSION = SharedConstants.APP_VERSION_BUILD_DELIM;
            }
        }

        #region Exception Handling

        public static void OnNavigationFailed(string Message)
        {
            throw new Exception("Failed to load Page " + Message);
        }

        /// <summary>
        /// Try to save current char at unhandled exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static void App_UnhandledException(string Message, Exception ex)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
            Settings.LAST_SAVE_INFO = null;
            if (Model?.MainObject is IMainType Main && Main?.FileInfo is FileInfo Info)
            {
                Info.ChangeName("EmergencySave" + Info.Name);
                SharedIO.SaveAtOriginPlace(Main).Wait();
            }
            var param = new Dictionary<string, string>
            {
                { "Message", Message },
                { "EXMessage", ex.Message },
                { "StackTrace", ex.StackTrace },
                { "InnerException", ex?.InnerException?.Message ?? "no inner exception"}
            };
            Features.Analytics.TrackEvent("App_UnhandledExceptionAsync", param);
        }
        #endregion Exception Handling
    }
}