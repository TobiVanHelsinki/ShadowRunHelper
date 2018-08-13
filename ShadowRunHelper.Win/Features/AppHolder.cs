using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TAMARIN.IO;
using TAPPLICATION;
using TAPPLICATION.IO;
using TLIB;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper
{
    static class AppHolder
    {
    
        static AppModel Model;
        static SettingsModel Settings;

        public static AppInstance Instance;
        private static string instanceKey = "";
        public static string InstanceKey { get { return Instance == null ? instanceKey : Instance.Key; } set => instanceKey = value; }

        static bool FirstStart = true;

        #region Init
        internal static void InitModel()
        {
            Settings = SettingsModel.Initialize();
            Model = AppModel.Initialize();
        }
        internal static void Init()
        {
            if (Settings.FIRST_START)
            {
                Settings.InitSettings();
            }
            Settings.START_COUNT++;

            Task.WaitAll(
                Task.Run(() => IAP.CheckLicence()),
                Task.Run(AppCenterConfiguration),
                Task.Run(SetConstantStuff),
                Task.Run(RegisterAppInstance)
                );
            Task.WaitAll(
                Task.Run(CharLoadingHandling)
                );
        }

        internal static async void StartInit()
        {
            await Task.Run(Init);
        }

        static void RegisterAppInstance()
        {
            if (!Windows.Foundation.Metadata.ApiInformation.IsMethodPresent("AppInstance", "FindOrRegisterInstanceForKey") && Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 5))
            {
                string key = Guid.NewGuid().ToString();
                try
                {
                    Instance = AppInstance.FindOrRegisterInstanceForKey(key);
                }
                catch (Exception ex)
                {
                    InstanceKey = key;
                }
            }

        }

        internal static void FileActivated(string Name, string Path)
        {
            Settings.FORCE_LOAD_CHAR_ON_START = true;
            Settings.LAST_SAVE_INFO = new FileInfoClass(Place.Extern, Name, Path)
            {
                Token = SharedConstants.ACCESSTOKEN_FILEACTIVATED
            };
            if (!FirstStart)
            {
                CharLoadingHandling();
            }
        }

        static void AppCenterConfiguration()
        {
            try
            {
                AppCenter.Start(Constants.AppCenterID, typeof(Crashes), typeof(Analytics)); // zu lange, nach mainwindow creation
            }
            catch (Exception)
            {
            }
        }

        static void SetConstantStuff()
        {
            SharedConstants.APP_VERSION_BUILD_DELIM = String.Format("{0}.{1}.{2}.{3}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor, Package.Current.Id.Version.Build, Package.Current.Id.Version.Revision);
            SharedConstants.APP_PUBLISHER_MAIL = Constants.APP_PUBLISHER_MAIL_TvH;
            SharedConstants.APP_PUBLISHER = Constants.APP_PUBLISHER_TvH;
            SharedConstants.APP_STORE_ID = Constants.APP_STORE_ID_SRE;
        }
        #endregion

        internal static void LeavingBackground()
        {
            CharLoadingHandling();
        }

        internal static async void EnteredBackground()
        {
            if (Model.MainObject != null)
            {
                try
                {
                    FileInfoClass SaveInfo;
                    if (Settings.AUTO_SAVE)
                    {
                        SaveInfo = await SharedIO.SaveAtOriginPlace(Model.MainObject, SaveType.Auto, UserDecision.ThrowError);
                        Settings.COUNT_SAVINGS++;
                    }
                    else
                    {
                        SaveInfo = await SharedIO.SaveAtTempPlace(Model.MainObject);
                    }
                    Settings.CHARINTEMPSTORE = true;
                    Settings.LAST_SAVE_INFO = SaveInfo;
                }
                catch (Exception) { }
            }
        }

        static async Task CharLoadingHandling()
        {
            try
            {
                if ((Settings.CHARINTEMPSTORE && !FirstStart || Settings.LOAD_CHAR_ON_START && FirstStart) && Model.MainObject == null || Settings.FORCE_LOAD_CHAR_ON_START)
                {
                    var info = Settings.LAST_SAVE_INFO;
                    Model.CharInProgress = info;
                    var TMPChar = await CharHolderIO.Load(info, eUD: UserDecision.ThrowError);

                    if (TMPChar.FileInfo.Fileplace == Place.Temp)
                    {
#pragma warning disable CS4014
                        CharHolderIO.SaveAtCurrentPlace(TMPChar, SaveType.Auto, UserDecision.ThrowError);
#pragma warning restore CS4014
                    }
                    var OldChar = Model.MainObject;
                    Model.MainObject = TMPChar;
                    Settings.COUNT_LOADINGS++;
                    if (OldChar != null)
                    {
                        try
                        {
#pragma warning disable CS4014
                            CharHolderIO.SaveAtOriginPlace(OldChar, SaveType.Auto, UserDecision.ThrowError);
#pragma warning restore CS4014
                        }
                        catch (Exception ex)
                        {
                            Model.NewNotification(StringHelper.GetString("Notification_Error_FileActivation"), ex);
                        }
                    }
                    if (Settings.FORCE_LOAD_CHAR_ON_START)
                    {
                        Model.NewNotification(StringHelper.GetString("Notification_Char_Loaded_File"));
                    }
                    else
                    {
                        Model.NewNotification(StringHelper.GetString("Notification_Char_Loaded_Start"));
                    }
                }
            }
            catch (Exception) { }
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

        internal static void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Try to save current char at unhandled exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        internal static async Task App_UnhandledExceptionAsync(object sender, UnhandledExceptionEventArgs e)
        {
            Settings.LAST_SAVE_INFO = null;
            try
            {
                await SharedIO.SaveAtOriginPlace(Model.MainObject, SaveType.Emergency);
                var res = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
                Model.NewNotification(res.GetString("Notification_Error_Unknown"), e.Exception);
            }
            catch (Exception ex)
            {
            }
            if (!e.Message.Contains(Constants.TESTEXCEPTIONTEXT))
            {
                e.Handled = true;
                var param = new Dictionary<string, string>();
                param.Add("Message", e.Message);
                param.Add("EXMessage", e.Exception.Message);
                param.Add("StackTrace", e.Exception.StackTrace);
                param.Add("InnerException", e.Exception.InnerException.Message);
                Analytics.TrackEvent("App_UnhandledExceptionAsync", param);
            }
        }
        #endregion
    }
}
