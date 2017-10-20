using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Threading.Tasks;
using TLIB;
using TLIB.IO;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper
{
    /// <summary>
    /// Stellt das anwendungsspezifische Verhalten bereit, um die Standardanwendungsklasse zu ergänzen.
    /// </summary>
    sealed partial class App : Application
    {
        readonly AppModel Model;

        /// <summary>
        /// Initialisiert das Singletonanwendungsobjekt.  Dies ist die erste Zeile von erstelltem Code
        /// und daher das logische Äquivalent von main() bzw. WinMain().
        /// </summary>
        public App()
        {
            Model = AppModel.Initialize();
            SettingsModel.Initialize();
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            InitializeComponent();
            Suspending += (x,y)=>App_OnSuspending(x,y);
            UnhandledException += async (x, y) => { await App_UnhandledExceptionAsync(x, y); };
            CreateDataStructure();
            if (SettingsModel.I.StartCount < 1)
            {
                SettingsModel.I.ResetAllSettings();
            }
            try
            {
                EnteredBackground += (x, y) => App_EnteredBackground(x, y);
            }
            catch
            {
                Suspending += (x, y) => App_OnSuspending(x, y);
            }
            SettingsModel.I.StartCount++;
        }

        /// <summary>
        /// Method to create the intern Folders and Settingplaces
        /// Do not create them, if they allready exists
        /// </summary>
        async void CreateDataStructure()
        {
            try
            {
                await ApplicationData.Current.RoamingFolder.CreateFolderAsync(Constants.INTERN_SAVE_CONTAINER, CreationCollisionOption.FailIfExists);
            }
            catch
            {
            }

            try
            {
                await ApplicationData.Current.LocalFolder.CreateFolderAsync(Constants.INTERN_SAVE_CONTAINER, CreationCollisionOption.FailIfExists);
            }
            catch
            {
            }

            try
            {
                ApplicationData.Current.LocalSettings.CreateContainer(Constants.CONTAINER_SETTINGS, ApplicationDataCreateDisposition.Always);
            }
            catch
            {
            }
        }

        // Startup ############################################################

        /// <summary>
        /// Wird aufgerufen, wenn die Anwendung durch den Endbenutzer normal gestartet wird. Weitere Einstiegspunkte
        /// werden z. B. verwendet, wenn die Anwendung gestartet wird, um eine bestimmte Datei zu öffnen.
        /// </summary>
        /// <param name="e">Details über Startanforderung und -prozess.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (SettingsModel.I.LoadCharOnStart && e.PreviousExecutionState != ApplicationExecutionState.Running && e.PreviousExecutionState != ApplicationExecutionState.Suspended)
            {
                try
                {
                    Model.CurrentChar = await CharHolderIO.Load(SettingsModel.I.LastSaveInfo, null, UserDecision.ThrowError);
                    SettingsModel.I.CountLoadings++;
                }
                catch (Exception) { }
            }
            Launch();
        }
        /// <summary>
        /// Wird aufgerufen, wenn eine Datei gestartet wird. 
        /// Speichert den jetzigen Zustand und öffnet die neue Datei
        /// </summary>
        protected async override void OnFileActivated(FileActivatedEventArgs args)
        {
            if (Model.CurrentChar != null) // Save CurrentChar //todo for later: open  new window if user whish this so
            {
                try
                {
                    await CharHolderIO.SaveAtOriginPlace(Model.CurrentChar, SaveType.Manually, UserDecision.ThrowError);
                }
                catch (Exception)
                {
                    return;
                }
            }
            try
            {
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace(Constants.ACCESSTOKEN_FILEACTIVATED, args.Files[0]);
                Model.MainObject = await CharHolderIO.Load(new FileInfoClass() { Fileplace = Place.Extern, Filename = args.Files[0].Name, Filepath = args.Files[0].Path.Substring(0, args.Files[0].Path.Length - args.Files[0].Name.Length), FolderToken = Constants.ACCESSTOKEN_FILEACTIVATED }, null, UserDecision.ThrowError);
                SettingsModel.I.CountLoadings++;
            }
            catch (Exception ex)
            {
                Model.NewNotification(CrossPlatformHelper.GetString("Notification_Error_FileActivation"), ex);
            }
            Launch();
        }
        /// <summary>
        /// generelle Aktivierungssequenz der App, wird nach den spezifischen Starts aufgerufen
        /// </summary>
        void Launch()
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;
            // App-Initialisierung nicht wiederholen, wenn das Fenster bereits Inhalte enthält.
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
                // übergeben werden
                rootFrame.Navigate(typeof(MainPage));
            }
            // Sicherstellen, dass das aktuelle Fenster aktiv ist
            Window.Current.Activate();
        }
        // ShutDown ###########################################################

        void App_OnSuspending(object sender, SuspendingEventArgs e)
        {
            SettingsModel.I.LastSaveInfo = Model?.CurrentChar?.FileInfo;
            Model?.CurrentChar?.SetSaveTimerTo();
            e.SuspendingOperation.GetDeferral().Complete();
        }

        private void App_EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
            SettingsModel.I.LastSaveInfo = Model?.CurrentChar?.FileInfo;
            Model?.CurrentChar?.SetSaveTimerTo();
            e.GetDeferral().Complete();
        }

        // Exception Handling #################################################
        /// <summary>
        /// Wird aufgerufen, wenn die Navigation auf eine bestimmte Seite fehlschlägt
        /// </summary>
        /// <param name="sender">Der Rahmen, bei dem die Navigation fehlgeschlagen ist</param>
        /// <param name="e">Details über den Navigationsfehler</param>
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
            e.Handled = true;
            SettingsModel.I.LastSaveInfo = null;
            try
            {
                await CharHolderIO.SaveAtOriginPlace(Model.CurrentChar, TLIB.IO.SaveType.Emergency);
                var res = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
                Model.NewNotification(res.GetString("Notification_Error_Unknown"), e.Exception);
            }
            catch (Exception)
            {
            }
        }
    }
}
