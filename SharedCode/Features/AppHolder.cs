using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Threading.Tasks;
using TAPPLICATION;
using TAPPLICATION.IO;
using TAPPLICATION.Model;
using TLIB;

[assembly: NeutralResourcesLanguageAttribute("en")]
namespace ShadowRunHelper
{
    public static class AppHolder
    {
    
        static AppModel Model;
        static SettingsModel Settings;

        static bool FirstStart = true;

        #region Init
        public static void InitModel()
        {
            Settings = SettingsModel.Initialize();
            Model = AppModel.Initialize();
        }
        public static void Init()
        {
//#if !DEBUG
            if (Settings.FIRST_START)
//#endif
            {
                Settings.InitSettings();
            }
            Settings.START_COUNT++;

            Task.WaitAll(
                Task.Run(() => Features.IAP.CheckLicence()),
                Task.Run(AppCenterConfiguration),
                Task.Run(SetConstantStuff),
                Task.Run(CreateFolder),
                Task.Run(RegisterAppInstance)
                );
            Task.WaitAll(
                Task.Run(CharLoadingHandling)
                );
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

        public static void FileActivated(string Name, string Path)
        {
            Settings.FORCE_LOAD_CHAR_ON_START = true;
            AppModel.Instance.IsFileActivated = true;
            Settings.LAST_SAVE_INFO = new FileInfo(Path + Name);
            if (!FirstStart)
            {
                CharLoadingHandling();
            }
        }

        static void AppCenterConfiguration()
        {
            try
            {
                Features.Analytics.Init();
            }
            catch (Exception ex)
 { TAPPLICATION.Debugging.TraceException(ex); TAPPLICATION.Debugging.TraceException(ex);
                TAPPLICATION.Debugging.TraceException(ex);
            }
        }

        static void SetConstantStuff()
        {
            SharedConstants.APP_VERSION_BUILD_DELIM = String.Format("{0}.{1}.{2}.{3}", Features.AppInformation.Version_Major, Features.AppInformation.Version_Minor, Features.AppInformation.Version_Build, Features.AppInformation.Version_Revision);
            SharedConstants.APP_PUBLISHER_MAIL = Constants.APP_PUBLISHER_MAIL_TvH;
            SharedConstants.APP_PUBLISHER = Constants.APP_PUBLISHER_TvH;
            SharedConstants.APP_STORE_ID = Constants.APP_STORE_ID_SRE;
        }

        static async void CreateFolder()
        {
            try
            {
                SharedIO.CurrentIO.CreateFolder(new DirectoryInfo(Path.Combine(await SharedIO.CurrentIO.GetCompleteInternPath(Place.Local), SharedConstants.INTERN_SAVE_CONTAINER)));
                SharedIO.CurrentIO.CreateFolder(new DirectoryInfo(Path.Combine(await SharedIO.CurrentIO.GetCompleteInternPath(Place.Roaming), SharedConstants.INTERN_SAVE_CONTAINER)));
            }
            catch (Exception)
            {
            }
        }
        #endregion

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
 { TAPPLICATION.Debugging.TraceException(ex); }
            }
        }

        static async Task CharLoadingHandling()
        {
            //TODO This is still Single MainObject Orienteded
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
                            Model.NewNotification(CustomManager.GetString("Notification_Error_FileActivation"), ex);
                        }
                    }
                    if (Settings.FORCE_LOAD_CHAR_ON_START)
                    {
                        Model.NewNotification(CustomManager.GetString("Notification_Char_Loaded_File"));
                    }
                    else
                    {
                        Model.NewNotification(CustomManager.GetString("Notification_Char_Loaded_Start"));
                    }
                }
            }
            catch (Exception ex)
 { TAPPLICATION.Debugging.TraceException(ex); }
            finally
            {
                Model.CharInProgress = null;
                Settings.FORCE_LOAD_CHAR_ON_START = false;
                FirstStart = false;
                Settings.LAST_SAVE_INFO = null;
                Settings.CHARINTEMPSTORE = false;
                Model.RequestNavigation(ProjectPages.Char); //TODO ThreadSave 
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
        #endregion
    }
}
