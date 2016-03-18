using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static ShadowRunHelper.Controller.TApp;


namespace ShadowRunHelper
{
    /// <summary>
    /// Stellt das anwendungsspezifische Verhalten bereit, um die Standardanwendungsklasse zu ergänzen.
    /// </summary>
    sealed partial class App : Application
    {
        public CharViewModel ViewModel { get; set; }


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
        }

        /// <summary>
        /// Wird aufgerufen, wenn die Anwendung durch den Endbenutzer normal gestartet wird. Weitere Einstiegspunkte
        /// werden z. B. verwendet, wenn die Anwendung gestartet wird, um eine bestimmte Datei zu öffnen.
        /// </summary>
        /// <param name="e">Details über Startanforderung und -prozess.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
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

                if (/*e.PreviousExecutionState == ApplicationExecutionState.Terminated || */Optionen.LOAD_CHAR_ON_START || Optionen.IS_FILE_IN_PROGRESS)
                {
                    Optionen.IS_FILE_IN_PROGRESS = false;
                    IO.CharVerwaltung VerwaltungTemp = new IO.CharVerwaltung();
                    try
                    {
                        ViewModel = new CharViewModel(await VerwaltungTemp.LadenIntern(Optionen.LAST_CHAR_IS));
                        ViewModel.currentState = TCharState.LOAD_CHAR;
                    }
                    catch (Exception)
                    {
                        ViewModel = new CharViewModel();
                    }
                    VerwaltungTemp = null;
                }
                // Den Frame im aktuellen Fenster platzieren
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // Wenn der Navigationsstapel nicht wiederhergestellt wird, zur ersten Seite navigieren
                // und die neue Seite konfigurieren, indem die erforderlichen Informationen als Navigationsparameter
                // übergeben werden

                if (ViewModel == null)
                {
                    ViewModel = new CharViewModel();
                }
                rootFrame.Navigate(typeof(MainPage), ViewModel);
               
               
            }
            // Sicherstellen, dass das aktuelle Fenster aktiv ist
            Window.Current.Activate();
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
            var deferral = e.SuspendingOperation.GetDeferral();
            if (Optionen.SAVE_CHAR_ON_EXIT)
            {
                try
                {
                    IO.CharVerwaltung VerwaltungTemp = new IO.CharVerwaltung();
                    string savename = await VerwaltungTemp.SpeichernIntern(ViewModel.Current);

                    Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    Optionen.LAST_CHAR_IS = savename;
                    

                    VerwaltungTemp = null;
                }
                catch (Exception)
                {
                }
            }
            deferral.Complete();
        }

    //    protected override async void OnFileActivated(FileActivatedEventArgs args)
    //    {
    //        if (args.Files.Count != 1)
    //        {
    //            System.Diagnostics.Debug.WriteLine("Falscha anzahl an Datein");
    //        }
    //        else
    //        {
    //            IO.CharVerwaltung VerwaltungTemp = new IO.CharVerwaltung();
    //            try
    //            {
    //                VerwaltungTemp.LadenExtern((Windows.Storage.StorageFile)(args.Files[0]));
    //                Variablen.LAST_CHAR = args.Files[0].Name;
    //                Optionen.IS_FILE_IN_PROGRESS(true);
    //            }
    //            catch (Exception)
    //            {

    //                ViewModel.Current = new Controller.CharHolder();
    //            }

    //            var messageDialog = new MessageDialog("Der Char wurde in den Internen Speicher kopiert.");
    //            messageDialog.Commands.Add(new UICommand("Close"));
    //            await messageDialog.ShowAsync();

    //            //
    //            Frame rootFrame = Window.Current.Content as Frame;
    //            this.ViewModel = new CharViewModel();

    //            // App-Initialisierung nicht wiederholen, wenn das Fenster bereits Inhalte enthält.
    //            // Nur sicherstellen, dass das Fenster aktiv ist.
    //            if (rootFrame == null)
    //            {
    //                // Frame erstellen, der als Navigationskontext fungiert und zum Parameter der ersten Seite navigieren
    //                rootFrame = new Frame();
    //                rootFrame.NavigationFailed += OnNavigationFailed;

    //                if (Optionen.LOAD_CHAR_ON_START() || Optionen.IS_FILE_IN_PROGRESS())
    //                {
                        
    //                    try
    //                    {
    //                        ViewModel.Current = await VerwaltungTemp.LadenIntern(Variablen.LAST_CHAR);
    //                    }
    //                    catch (Exception)
    //                    {

    //                        ViewModel.Current = new Controller.CharHolder();
    //                    }

    //                    VerwaltungTemp = null;
    //                }

    //                // Den Frame im aktuellen Fenster platzieren
    //                Window.Current.Content = rootFrame;
    //            }

    //            if (rootFrame.Content == null)
    //            {
    //                // Wenn der Navigationsstapel nicht wiederhergestellt wird, zur ersten Seite navigieren
    //                // und die neue Seite konfigurieren, indem die erforderlichen Informationen als Navigationsparameter
    //                // übergeben werden

    //                rootFrame.Navigate(typeof(MainPage), ViewModel);
    //            }
    //            // Sicherstellen, dass das aktuelle Fenster aktiv ist
    //            Window.Current.Activate();
    //            //
    //        }


    //}

    }

}
