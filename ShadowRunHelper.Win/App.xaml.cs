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
    sealed partial class App : Application
    {
        public static AppInstance Instance;
        private static string instanceKey = "";
        public static string InstanceKey { get { return Instance == null ? instanceKey : Instance.Key; } set => instanceKey = value; }
        bool FirstStart = true;
        readonly AppModel Model;
        readonly SettingsModel Settings;
        Task CheckLicence;

        #region App Startup and Init
        public App()
        {
            Debug_TimeAnalyser.Start("Overall");
            Debug_TimeAnalyser.Start("App()");
            UnhandledException += async (x, y) => { await App_UnhandledExceptionAsync(x, y); };
            CheckLicence = Task.Run(IAP.CheckLicence);
            SetConstantStuff();
            Model = AppModel.Initialize();
            Settings = SettingsModel.Initialize();
            if (Settings.StartCount < 1)
            {
                Settings.ResetAllSettings();
            }

            EnteredBackground += App_EnteredBackground;
            LeavingBackground += App_LeavingBackground;

            InitializeComponent();
            Settings.StartCount++;
            Task.Run(AppCenterConfiguration);
            Task.Run(RegisterAppInstance);
            Debug_TimeAnalyser.Stop("App()");
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
                AppCenter.Start(Constants.AppCenterID, typeof(Crashes), typeof(Analytics)); // zu lange
            }
            catch (Exception)
            {
            }
        }

        public void SetConstantStuff()
        {
            SharedConstants.APP_VERSION_BUILD_DELIM = String.Format("{0}.{1}.{2}.{3}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor, Package.Current.Id.Version.Build, Package.Current.Id.Version.Revision);
            SharedConstants.APP_PUBLISHER_MAIL = "TobiVanHelsinki@live.de";
            SharedConstants.APP_PUBLISHER = "Tobi van Helsinki";
#if BETA
            SharedConstants.APP_STORE_ID = "9NCXWGX1KR8S";
#elif RELEASE
            //SharedConstants.APP_STORE_ID = "9NBLGGH4RHVX";
            SharedConstants.APP_STORE_ID = "---"; //TODO
#endif
        }

        #endregion

        #region Entry-Points
        protected override async void OnActivated(IActivatedEventArgs args)
        {
            Debug_TimeAnalyser.Start("Entry Protocol");
            if (args.Kind == ActivationKind.Protocol && args is ProtocolActivatedEventArgs uriArgs)
            {
                Settings.ForceLoadCharOnStart = true;
                string name = uriArgs.Uri.Segments[uriArgs.Uri.Segments.Length - 1];
                string path = uriArgs.Uri.LocalPath.Remove(uriArgs.Uri.LocalPath.Length - name.Length);
                name = name.Remove(name.Length - 1);
                Settings.LastSaveInfo = new FileInfoClass(Place.Extern, name, path)
                {
                    FolderToken = SharedConstants.ACCESSTOKEN_FILEACTIVATED
                };
            }
            if (!FirstStart)
            {
                await CharLoadingHandling();
                Model.RequestNavigation(ProjectPages.Char, ProjectPagesOptions.Char_Action);
            }
            Debug_TimeAnalyser.Stop("Entry Protocol");
        }
        protected override async void OnFileActivated(FileActivatedEventArgs args)
        {
            Debug_TimeAnalyser.Start("Entry File");
            if (args.Files[0].Name.EndsWith(".SRHChar"))
            {
                try
                {
                    Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace(SharedConstants.ACCESSTOKEN_FILEACTIVATED, args.Files[0]);
                }
                catch (Exception ex)
                {
                }
                Settings.ForceLoadCharOnStart = true;
                Settings.LastSaveInfo = new FileInfoClass(Place.Extern, args.Files[0].Name, args.Files[0].Path.Substring(0, args.Files[0].Path.Length - args.Files[0].Name.Length))
                {
                    FolderToken = SharedConstants.ACCESSTOKEN_FILEACTIVATED
                };
                if (!FirstStart)
                {
                    await CharLoadingHandling();
                    Model.RequestNavigation(ProjectPages.Char, ProjectPagesOptions.Char_Action);
                }
            }
            else if (args.Files[0].Name.EndsWith(".SRHApp1"))
            {
                AppDataPorter.Loading = AppDataPorter.LoadAppPacket(args.Files[0]);
            }
            if (!FirstStart)
            {
                Model.RequestNavigation(ProjectPages.Administration, ProjectPagesOptions.Import);
            }
            Debug_TimeAnalyser.Stop("Entry File");
        }
        #endregion

        async void App_LeavingBackground(object sender, LeavingBackgroundEventArgs e)
        {
            Debug_TimeAnalyser.Start("LeavingBackground");
            var def = e.GetDeferral();

            Debug_TimeAnalyser.Start("CharLoadingHandling");
            Task Loading = CharLoadingHandling();
            Debug_TimeAnalyser.Stop("CharLoadingHandling");
            // App-Initialisierung nicht wiederholen, wenn das Fenster bereits Inhalte enthaelt.
            // Nur sicherstellen, dass das Fenster aktiv ist.
            if (!(Window.Current.Content is Frame rootFrame))
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                Window.Current.Content = rootFrame;
            }
            Debug_TimeAnalyser.Start("await Loading");
            await Loading;
            Debug_TimeAnalyser.Stop("await Loading");
            //Debug_TimeAnalyser.Start("await CheckLicence");
            //await CheckLicence;
            //Debug_TimeAnalyser.Stop("await CheckLicence");
            if (rootFrame.Content == null)
            {
                // Wenn der Navigationsstapel nicht wiederhergestellt wird, zur ersten Seite navigieren
                Debug_TimeAnalyser.Start("NavigatetoMP");
                rootFrame.Navigate(typeof(MainPage));
                Debug_TimeAnalyser.Stop("NavigatetoMP");
            }
            else
            {
                // Seite ist aktiv, wir versuchen, den Char anzuzeigen
                Model.RequestNavigation(Settings.LastPage);
            }
            // Sicherstellen, dass das aktuelle Fenster aktiv ist
            Window.Current.Activate();
            def.Complete();
            Debug_TimeAnalyser.Stop("LeavingBackground");
            Debug_TimeAnalyser.Stop("Overall");
        }

        async Task CharLoadingHandling()
        {
            try
            {
                if ((Settings.CharInTempStore && !FirstStart || Settings.LoadCharOnStart && FirstStart) && Model.MainObject == null || Settings.ForceLoadCharOnStart)
                {
                    var info = Settings.LastSaveInfo;
                    Debug_TimeAnalyser.Start("CharLoadingNow");
                    var TMPChar = await CharHolderIO.Load(info, eUD: UserDecision.ThrowError);
                    Debug_TimeAnalyser.Stop("CharLoadingNow");

                    if (TMPChar.FileInfo.Fileplace == Place.Temp)
                    {
#pragma warning disable CS4014
                        CharHolderIO.SaveAtCurrentPlace(TMPChar, SaveType.Auto, UserDecision.ThrowError);
#pragma warning restore CS4014
                    }
                    var OldChar = Model.MainObject;
                    Settings.CountLoadings++;
                    Model.MainObject = TMPChar;
                    if (OldChar != null)
                    {
                        try
                        {
#pragma warning disable CS4014
                            CharHolderIO.SaveAtCurrentPlace(TMPChar, SaveType.Auto, UserDecision.ThrowError);
#pragma warning restore CS4014
                        }
                        catch (Exception ex)
                        {
                            Model.NewNotification(StringHelper.GetString("Notification_Error_FileActivation"), ex);
                        }
                    }
                    if (Settings.ForceLoadCharOnStart)
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
                Settings.ForceLoadCharOnStart = false;
                FirstStart = false;
                Settings.LastSaveInfo = null;
                Settings.CharInTempStore = false;
            }
        }

        async void App_EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
            var def = e.GetDeferral();
            try
            {
                FileInfoClass SaveInfo;
                if (Settings.AutoSave)
                {
                    SaveInfo = await SharedIO.SaveAtOriginPlace(Model.MainObject, SaveType.Auto, UserDecision.ThrowError);
                    Settings.CountSavings++;
                }
                else
                {
                    SaveInfo = await SharedIO.SaveAtTempPlace(Model.MainObject);
                }
                Settings.CharInTempStore = true;
                Settings.LastSaveInfo = SaveInfo;
            }
            catch (Exception) { }
            def.Complete();
        }

        #region Exception Handling

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Try to save current char at unhandled exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        async Task App_UnhandledExceptionAsync(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            Settings.LastSaveInfo = null;
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
                Analytics.TrackEvent("App_UnhandledExceptionAsync");
                e.Handled = true;
            }
        }
        #endregion

    }
}
