using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Threading.Tasks;
using TLIB;
using TLIB_UWPFRAME;
using TLIB_UWPFRAME.IO;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Services.Store;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper
{
    /// <summary>
    /// Stellt das anwendungsspezifische Verhalten bereit, um die Standardanwendungsklasse zu ergaenzen.
    /// </summary>
    sealed partial class App : Application
    {
        readonly AppModel Model;
        readonly SettingsModel Settings;

        #region App Startup

        /// <summary>
        /// Initialisiert das Singletonanwendungsobjekt.  Dies ist die erste Zeile von erstelltem Code
        /// und daher das logische aequivalent von main() bzw. WinMain().
        /// </summary>
        public App()
        {
            UnhandledException += async (x, y) => { await App_UnhandledExceptionAsync(x, y); };
            SetConstantStuff();
            IAP.CheckLicence();
            Model = AppModel.Initialize();
            Settings = SettingsModel.Initialize();
            if (Settings.StartCount < 1)
            {
                Settings.ResetAllSettings();
            }
            EnteredBackground += App_EnteredBackground;
            LeavingBackground += App_LeavingBackground;
            Suspending += App_Suspending;
            Resuming += App_Resuming;

            InitializeComponent();
            Settings.StartCount++;

            //Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
            //    //Microsoft.ApplicationInsights.WindowsCollectors.UnhandledException |
            //    Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
            //    Microsoft.ApplicationInsights.WindowsCollectors.Session);
            //AppCenter.LogLevel = LogLevel.Verbose;
            AppCenter.Start("cea0f814-f9f7-46b1-ba58-760607a60559", typeof(Crashes), typeof(Analytics));
        }

        public async void SetConstantStuff()
        {
            var SP = (await StoreContext.GetDefault().GetStoreProductForCurrentAppAsync()).Product;
            
            var json = Windows.Data.Json.JsonObject.Parse(SP.ExtendedJsonData);
            SharedConstants.APP_STORE_ID = SP.StoreId;
            SharedConstants.APP_VERSION_BUILD_DELIM = String.Format("{0}.{1}.{2}.{3}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor, Package.Current.Id.Version.Build, Package.Current.Id.Version.Revision);

            var arr = json.GetNamedArray("LocalizedProperties");
            if (arr.Count == 0)
            {
                return;
            }
            var json2 = Windows.Data.Json.JsonObject.Parse(arr[0].Stringify());
            SharedConstants.APP_PUBLISHER_MAILTO = json2.GetNamedString("SupportUri", SharedConstants.ERROR_TOKEN);
            SharedConstants.APP_PUBLISHER = json2.GetNamedString("PublisherName", SharedConstants.ERROR_TOKEN);
        }

        #endregion

        #region Entry-Points

        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            SystemHelper.WriteLine("OnLaunched");
#endif
            base.OnLaunched(e);
            if (Settings.LoadCharOnStart)
            {
                Model.MainObject = await CharHolderIO.Load(Settings.LastSaveInfo, eUD: UserDecision.ThrowError);
                Settings.CountLoadings++;
            }
#if DEBUG
            SystemHelper.WriteLine("OnLaunchedComplete");
#endif
        }

        protected async override void OnFileActivated(FileActivatedEventArgs args)
        {
            CharHolder NewHolder;
            try
            {
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace(SharedConstants.ACCESSTOKEN_FILEACTIVATED, args.Files[0]);

                NewHolder = await CharHolderIO.Load(
                    new FileInfoClass()
                    {
                        Fileplace = Place.Extern,
                        Filename = args.Files[0].Name,
                        Filepath = args.Files[0].Path.Substring(0, args.Files[0].Path.Length - args.Files[0].Name.Length),
                        FolderToken = SharedConstants.ACCESSTOKEN_FILEACTIVATED
                    }
                    , null
                    , UserDecision.ThrowError);
                Settings.CountLoadings++;
            }
            catch (Exception ex)
            {
                Model.NewNotification(StringHelper.GetString("Notification_Error_FileActivation"), ex);
                return;
            }
            if (Model.MainObject != null) // Save CurrentChar //todo for later: open  new window if user whish this so
            {
                try
                {
                    await SharedIO.SaveAtOriginPlace(Model.MainObject, SaveType.Auto, UserDecision.ThrowError);
                    Settings.CountSavings++;
                }
                catch (Exception)
                {
                    return;
                }
            }
            Model.MainObject = NewHolder;

        }
        #endregion

        private async void App_EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
#if DEBUG
            SystemHelper.WriteLine("App_EnteredBackground");
#endif
            var def = e.GetDeferral();
            try
            {
                if (Settings.AutoSave)
                {
                    await SharedIO.SaveAtOriginPlace(Model.MainObject, SaveType.Auto, UserDecision.ThrowError);
                }
                else
                {
                    await SharedIO.SaveAtTempPlace(Model.MainObject);
                }
                Settings.CharInTempStore = true;
                Settings.LastSaveInfo = Model.MainObject.FileInfo;
                Settings.CountSavings++;
            } catch (Exception) { }
            def.Complete();
#if DEBUG
            SystemHelper.WriteLine("App_EnteredBackgroundComplete");
#endif
        }

        async void App_LeavingBackground(object sender, LeavingBackgroundEventArgs e)
        {
#if DEBUG
            SystemHelper.WriteLine("App_LeavingBackground");
#endif
            var def = e.GetDeferral();
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            try
            {
                if (Settings.CharInTempStore)
                {
                    if (Model.MainObject == null)
                    {
                        Model.MainObject = await CharHolderIO.Load(
                            new FileInfoClass() { Fileplace = Place.Temp, Filename = Settings.LastSaveInfo.Filename }
                            , null
                            , UserDecision.ThrowError);
                    }
                    Settings.CharInTempStore = false;
                    Settings.LastSaveInfo = null;
                }
            }
            catch (Exception) { }

            Frame rootFrame = Window.Current.Content as Frame;
            // App-Initialisierung nicht wiederholen, wenn das Fenster bereits Inhalte enthaelt.
            // Nur sicherstellen, dass das Fenster aktiv ist.
            if (rootFrame == null)
            {
                // Frame erstellen, der als Navigationskontext fungiert und zum Parameter der ersten Seite navigieren
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                // Den Frame im aktuellen Fenster platzieren
                Window.Current.Content = rootFrame;
            }
            if (rootFrame.Content == null)
            {
                // Wenn der Navigationsstapel nicht wiederhergestellt wird, zur ersten Seite navigieren
                // und die neue Seite konfigurieren, indem die erforderlichen Informationen als Navigationsparameter
                // uebergeben werden
                rootFrame.Navigate(typeof(MainPage));
            }
            else
            {
                // Seite ist aktiv, wir versuchen, den Char anzuzeigen
                Model.RequestNavigation(this, ProjectPages.Char);
            }
            // Sicherstellen, dass das aktuelle Fenster aktiv ist
            Window.Current.Activate();

            def.Complete();
#if DEBUG
            SystemHelper.WriteLine("App_LeavingBackgroundComplete");
#endif
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
#if DEBUG
            SystemHelper.WriteLine("OnActivated");
#endif
#if DEBUG
            SystemHelper.WriteLine("OnActivatedComplete");
#endif
            base.OnActivated(args);
        }
        protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
#if DEBUG
            SystemHelper.WriteLine("OnBackgroundActivated");
#endif
#if DEBUG
            SystemHelper.WriteLine("OnBackgroundActivatedComplete");
#endif
            base.OnBackgroundActivated(args);
        }
        void App_Suspending(object sender, SuspendingEventArgs e)
        {
#if DEBUG
            SystemHelper.WriteLine("App_Suspending, time: " + (e.SuspendingOperation.Deadline - DateTimeOffset.Now));
#endif
#if DEBUG
            SystemHelper.WriteLine("App_SuspendingComplete");
#endif
        }
        private void App_Resuming(object sender, object e)
        {
#if DEBUG
            SystemHelper.WriteLine("App_Resuming");
#endif
#if DEBUG
            SystemHelper.WriteLine("App_ResumingComplete");
#endif
        }
        #region Exception Handling

        /// <summary>
        /// Wird aufgerufen, wenn die Navigation auf eine bestimmte Seite fehlschlaegt
        /// </summary>
        /// <param name="sender">Der Rahmen, bei dem die Navigation fehlgeschlagen ist</param>
        /// <param name="e">Details ueber den Navigationsfehler</param>
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
        async Task App_UnhandledExceptionAsync(object sender, UnhandledExceptionEventArgs e)
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
            Analytics.TrackEvent("App_UnhandledExceptionAsync");
            if (!e.Message.Contains(Constants.TESTEXCEPTIONTEXT))
            {
                e.Handled = true;
            }
        }
        #endregion
    }
}
