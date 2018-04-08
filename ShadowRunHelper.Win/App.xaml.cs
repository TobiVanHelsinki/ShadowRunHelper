using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Threading.Tasks;
using TLIB;
using TLIB_UWPFRAME;
using TLIB_UWPFRAME.IO;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.ViewManagement;
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

        #region App Startup

        /// <summary>
        /// Initialisiert das Singletonanwendungsobjekt.  Dies ist die erste Zeile von erstelltem Code
        /// und daher das logische aequivalent von main() bzw. WinMain().
        /// </summary>
        public App()
        {
            UnhandledException += async (x, y) => { await App_UnhandledExceptionAsync(x, y); };
            CreateDataStructure();
            Model = AppModel.Initialize();
            SettingsModel.Initialize();
            if (SettingsModel.I.StartCount < 1)
            {
                SettingsModel.I.ResetAllSettings();
            }

            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);

            //EnteredBackground += App_EnteredBackground;
            //LeavingBackground += App_LeavingBackground;
            Suspending += App_Suspending;
            Resuming += App_Resuming;

            InitializeComponent();
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
                await ApplicationData.Current.RoamingFolder.CreateFolderAsync(SharedConstants.INTERN_SAVE_CONTAINER, CreationCollisionOption.FailIfExists);
            }
            catch
            {
            }

            try
            {
                await ApplicationData.Current.LocalFolder.CreateFolderAsync(SharedConstants.INTERN_SAVE_CONTAINER, CreationCollisionOption.FailIfExists);
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

        #endregion

        /// <summary>
        /// Wird aufgerufen, wenn die Anwendung durch den Endbenutzer normal gestartet wird. Weitere Einstiegspunkte
        /// werden z. B. verwendet, wenn die Anwendung gestartet wird, um eine bestimmte Datei zu oeffnen.
        /// </summary>
        /// <param name="e">Details ueber Startanforderung und -prozess.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            //base.OnLaunched(e);
            SystemHelper.WriteLine("OnLaunched");
            if (SettingsModel.I.LoadCharOnStart && e.PreviousExecutionState != ApplicationExecutionState.Running && e.PreviousExecutionState != ApplicationExecutionState.Suspended)
            {
                try
                {
                    Model.MainObject = await CharHolderIO.Load(SettingsModel.I.LastSaveInfo, null, UserDecision.ThrowError);
                    SettingsModel.I.CountLoadings++;
                }
                catch (Exception) { }
            }
            Launch();

            ExtendAcrylicIntoTitleBar();
            SystemHelper.WriteLine("OnLaunchedComplete");

        }

        void ExtendAcrylicIntoTitleBar()
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = false;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = null;
            titleBar.ButtonBackgroundColor = null;
            titleBar.ButtonInactiveBackgroundColor = null;
        }
        /// <summary>
        /// Wird aufgerufen, wenn eine Datei gestartet wird. 
        /// Speichert den jetzigen Zustand und oeffnet die neue Datei
        /// </summary>
        protected async override void OnFileActivated(FileActivatedEventArgs args)
        {
            if (Model.MainObject != null) // Save CurrentChar //todo for later: open  new window if user whish this so
            {
                try
                {
                    await CharHolderIO.SaveAtOriginPlace(Model.MainObject, SaveType.Manually, UserDecision.ThrowError);
                    SettingsModel.I.CountSavings++;
                }
                catch (Exception)
                {
                    return;
                }
            }
            try
            {
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace(SharedConstants.ACCESSTOKEN_FILEACTIVATED, args.Files[0]);

                Model.MainObject = await CharHolderIO.Load(
                    new FileInfoClass()
                    {
                        Fileplace = Place.Extern,
                        Filename = args.Files[0].Name,
                        Filepath = args.Files[0].Path.Substring(0, args.Files[0].Path.Length - args.Files[0].Name.Length),
                        FolderToken = Constants.ACCESSTOKEN_FILEACTIVATED
                    }
                    , null
                    , UserDecision.ThrowError);

                SettingsModel.I.CountLoadings++;
            }
            catch (Exception ex)
            {
                Model.NewNotification(StringHelper.GetString("Notification_Error_FileActivation"), ex);
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
            // Sicherstellen, dass das aktuelle Fenster aktiv ist
            Window.Current.Activate();
        }

        void App_Suspending(object sender, SuspendingEventArgs e)
        {
            SystemHelper.WriteLine("App_Suspending");
            var def = e.SuspendingOperation.GetDeferral();
            SettingsModel.I.LastSaveInfo = Model?.MainObject?.FileInfo;
            Model?.MainObject?.SetSaveTimerTo();
            def.Complete();
            SystemHelper.WriteLine("App_SuspendingComplete");
        }
        private void App_Resuming(object sender, object e)
        {
            SystemHelper.WriteLine("App_Resuming");
            SystemHelper.WriteLine("App_ResumingComplete");
        }

        //private void App_EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        //{
        //    SystemHelper.WriteLine("App_EnteredBackground");
        //    SettingsModel.I.LastSaveInfo = Model?.MainObject?.FileInfo;
        //    Model?.MainObject?.SetSaveTimerTo();
        //    e.GetDeferral().Complete();
        //    SystemHelper.WriteLine("App_EnteredBackgroundComplete");
        //}

        //private void App_LeavingBackground(object sender, LeavingBackgroundEventArgs e)
        //{
        //    SystemHelper.WriteLine("App_LeavingBackground");
        //    SystemHelper.WriteLine("App_LeavingBackgroundComplete");
        //}

        //protected override void OnActivated(IActivatedEventArgs args)
        //{
        //    SystemHelper.WriteLine("OnActivated");
        //    SystemHelper.WriteLine("OnActivatedComplete");
        //    base.OnActivated(args);
        //}
        //protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        //{
        //    SystemHelper.WriteLine("OnBackgroundActivated");
        //    SystemHelper.WriteLine("OnBackgroundActivatedComplete");
        //    base.OnBackgroundActivated(args);
        //}
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
        async Task App_UnhandledExceptionAsync(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            SettingsModel.I.LastSaveInfo = null;
            try
            {
                await CharHolderIO.SaveAtOriginPlace(Model.MainObject, TLIB_UWPFRAME.IO.SaveType.Emergency);
                var res = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
                Model.NewNotification(res.GetString("Notification_Error_Unknown"), e.Exception);
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
