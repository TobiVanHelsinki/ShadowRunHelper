using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using ShadowRunHelper.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TAMARIN.IO;
using TAPPLICATION;
using TAPPLICATION.IO;
using TLIB;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper
{
    static class AppHolder
    {
        public static AppInstance Instance;
        private static string instanceKey = "";
        public static string InstanceKey { get { return Instance == null ? instanceKey : Instance.Key; } set => instanceKey = value; }
        static bool FirstStart = true;
        static AppModel Model;
        static SettingsModel Settings;
        static Task CheckLicence;
        #region Init

        internal static void Init()
        {

            //Debug_TimeAnalyser.Start("Overall");
            //Debug_TimeAnalyser.Start("App()");
            Settings = SettingsModel.Initialize();
            CheckLicence = Task.Run(() => IAP.CheckLicence());
            SetConstantStuff();
            Model = AppModel.Initialize();
            if (Settings.START_COUNT < 1)
            {
                Settings.ResetAllSettings();
            }


            Settings.START_COUNT++;
            Task.Run(AppCenterConfiguration);
            Task.Run(RegisterAppInstance);
            //Debug_TimeAnalyser.Stop("App()");
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

        internal static async void LeavingBackground()
        {
            //Debug_TimeAnalyser.Start("LeavingBackground");

            //Debug_TimeAnalyser.Start("CharLoadingHandling");
            Task Loading = CharLoadingHandling();
            //Debug_TimeAnalyser.Stop("CharLoadingHandling");
            // App-Initialisierung nicht wiederholen, wenn das Fenster bereits Inhalte enthaelt.
            // Nur sicherstellen, dass das Fenster aktiv ist.
            if (!(Window.Current.Content is Frame rootFrame))
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                Window.Current.Content = rootFrame;
            }
            Window.Current.Activate();
            //Debug_TimeAnalyser.Start("await Loading");
            await Loading;
            //Debug_TimeAnalyser.Stop("await Loading");
            //Debug_TimeAnalyser.Start("await CheckLicence");
            //await CheckLicence;
            //Debug_TimeAnalyser.Stop("await CheckLicence");
            if (rootFrame.Content == null)
            {
                // Wenn der Navigationsstapel nicht wiederhergestellt wird, zur ersten Seite navigieren
                //Debug_TimeAnalyser.Start("NavigatetoMP");
                rootFrame.Navigate(typeof(MainPage));
                //Debug_TimeAnalyser.Stop("NavigatetoMP");
            }
            else
            {
                // Seite ist aktiv, wir versuchen, den Char anzuzeigen
                Model.RequestNavigation(Settings.LAST_PAGE);
            }
            // Sicherstellen, dass das aktuelle Fenster aktiv ist
            //Debug_TimeAnalyser.Stop("LeavingBackground");
            //Debug_TimeAnalyser.Stop("Overall");
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
                    //Debug_TimeAnalyser.Start("CharLoadingNow");
                    var TMPChar = await CharHolderIO.Load(info, eUD: UserDecision.ThrowError);
                    //Debug_TimeAnalyser.Stop("CharLoadingNow");

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
                Settings.FORCE_LOAD_CHAR_ON_START = false;
                FirstStart = false;
                Settings.LAST_SAVE_INFO = null;
                Settings.CHARINTEMPSTORE = false;
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
