using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Threading.Tasks;
using TLIB;
using TAPPLICATION;
using TAPPLICATION.IO;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Services.Store;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using TAMARIN.IO;
using ShadowRunHelper.UI;
using System.Linq;

namespace ShadowRunHelper
{
    /// <summary>
    /// Stellt das anwendungsspezifische Verhalten bereit, um die Standardanwendungsklasse zu ergaenzen.
    /// </summary>
    sealed partial class App : Application
    {
        bool FirstStart = true;
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
            
            AppCenter.Start(Constants.AppCenterID, typeof(Crashes), typeof(Analytics));
        }

        public async void SetConstantStuff()
        {
            SharedConstants.APP_VERSION_BUILD_DELIM = String.Format("{0}.{1}.{2}.{3}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor, Package.Current.Id.Version.Build, Package.Current.Id.Version.Revision);

            var SPR = await StoreContext.GetDefault().GetStoreProductForCurrentAppAsync();
            SharedConstants.APP_STORE_ID = SPR?.Product?.StoreId;
        }

        #endregion

        #region Entry-Points

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            SystemHelper.WriteLine("OnLaunched");
#endif
            base.OnLaunched(e);
#if DEBUG
            SystemHelper.WriteLine("OnLaunchedComplete");
#endif
        }

        protected override void OnFileActivated(FileActivatedEventArgs args)
        {
            CharHolder NewHolder;
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
#pragma warning disable CS4014
                CharLoadingHandling();
#pragma warning restore CS4014
            }
        }
        #endregion

        async void App_EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
#if DEBUG
            SystemHelper.WriteLine("App_EnteredBackground");
#endif
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
            } catch (Exception) { }
            def.Complete();
#if DEBUG
            SystemHelper.WriteLine("App_EnteredBackgroundComplete");
#endif
        }

        async void App_LeavingBackground(object sender, LeavingBackgroundEventArgs e)
        {
            var def = e.GetDeferral();
#if DEBUG
            SystemHelper.WriteLine("App_LeavingBackground");
            if (System.Diagnostics.Debugger.IsAttached)
            {
                DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            await CharLoadingHandling();

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
                Model.RequestNavigation(ProjectPages.Char);
            }
            // Sicherstellen, dass das aktuelle Fenster aktiv ist
            Window.Current.Activate();

            FirstStart = false;
            def.Complete();
#if DEBUG
            SystemHelper.WriteLine("App_LeavingBackgroundComplete");
#endif
        }

        private async Task CharLoadingHandling()
        {
            try
            {
                if ((Settings.CharInTempStore && !FirstStart || Settings.LoadCharOnStart && FirstStart) && Model.MainObject == null || Settings.ForceLoadCharOnStart)
                {
                    var info = Settings.LastSaveInfo;
                    var TMPChar = await CharHolderIO.Load(info, eUD: UserDecision.ThrowError);
                    if (TMPChar.FileInfo.Fileplace == Place.Temp)
                    {
#pragma warning disable CS4014
                        CharHolderIO.SaveAtCurrentPlace(TMPChar, SaveType.Auto, UserDecision.ThrowError);
#pragma warning restore CS4014
                    }
                    if (Model.MainObject != null)
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
                    Model.MainObject = TMPChar;
                    Settings.CountLoadings++;
                }
            }
            catch (Exception) { }
            finally
            {
                Settings.LastSaveInfo = null;
                Settings.CharInTempStore = false;
            }
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
            if (!e.Message.Contains(Constants.TESTEXCEPTIONTEXT))
            {
                Analytics.TrackEvent("App_UnhandledExceptionAsync");
                e.Handled = true;
            }
        }
        #endregion
    }
}
