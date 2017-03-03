using ShadowRunHelper.Model;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
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

        readonly ViewModel ViewModel = ViewModel.Instance;
        /// <summary>
        /// Initialisiert das Singletonanwendungsobjekt.  Dies ist die erste Zeile von erstelltem Code
        /// und daher das logische Äquivalent von main() bzw. WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.UnhandledException += App_UnhandledExceptionAsync;
            CreateDataStructure();
        }

        async void CreateDataStructure()
        {
            await ApplicationData.Current.RoamingFolder.CreateFolderAsync(Konstanten.CONTAINER_CHAR, CreationCollisionOption.OpenIfExists);
            ApplicationData.Current.LocalSettings.CreateContainer(Konstanten.CONTAINER_SETTINGS, ApplicationDataCreateDisposition.Always);
        }

        async void App_UnhandledExceptionAsync(object sender, UnhandledExceptionEventArgs e)
        {
            this.UnhandledException -= App_UnhandledExceptionAsync;
            e.Handled = true;
            await IO.CharIO.SaveCharAtCurrentPlace(ViewModel.CurrentChar, IO.SaveType.Emergency);
            Optionen.strLastChar = "";
            var res = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            ViewModel.lstNotifications.Add(new Notification(res.GetString("Notification_Error_Unknown"), e.Exception));
            Current.Exit();
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Anwendung durch den Endbenutzer normal gestartet wird. Weitere Einstiegspunkte
        /// werden z. B. verwendet, wenn die Anwendung gestartet wird, um eine bestimmte Datei zu öffnen.
        /// </summary>
        /// <param name="e">Details über Startanforderung und -prozess.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (Optionen.bLoadCharOnStart || Optionen.bIsFIleInProgress)
            {
                Optionen.bIsFIleInProgress = false;
                try
                {
                    ViewModel.CurrentChar = await IO.CharIO.TryLoadCharAtCurrentPlace(Optionen.strLastChar);
                }
                catch (Exception) { }
            }
            Launch();
        }

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
        /// Wird aufgerufen, wenn die Ausführung der Anwendung angehalten wird.  Der Anwendungszustand wird gespeichert,
        /// ohne zu wissen, ob die Anwendung beendet oder fortgesetzt wird und die Speicherinhalte dabei
        /// unbeschädigt bleiben.
        /// </summary>
        /// <param name="sender">Die Quelle der Anhalteanforderung.</param>
        /// <param name="e">Details zur Anhalteanforderung.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            if (Optionen.bSaveCharOnExit && ViewModel.CurrentChar != null)
            {
                try
                {
                    await IO.CharIO.SaveCharAtCurrentPlace(ViewModel.CurrentChar);
                }
                catch (Exception)
                {
                }
            }
            try
            {
                Optionen.strLastChar = ViewModel.CurrentChar.MakeName();
            }
            catch (Exception)
            {
                Optionen.strLastChar = "";
            }
            e.SuspendingOperation.GetDeferral().Complete();
        }

        protected async override void OnFileActivated(FileActivatedEventArgs args)
        {
            if (ViewModel.CurrentChar != null) // Save CurrentChar
            {
                try
                {
                    await IO.CharIO.SaveCharAtCurrentPlace(ViewModel.CurrentChar);
                }
                catch (Exception)
                {
                }
            }
            try
            {
                StorageFile File = await IO.GeneralIO.GetFile(IO.Place.Extern, args.Files[0].Name, args.Files[0].Path.Substring(0, args.Files[0].Path.Length - args.Files[0].Name.Length));
                ViewModel.CurrentChar = await IO.CharIO.LoadCharFromFile(File);
                //OnLaunched(null);
                ViewModel.RequestedNavigation(ProjectPages.Char);
            }
            catch (Exception)
            {
            }
            Launch();
        }
        void Launch()
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
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

    }
}
