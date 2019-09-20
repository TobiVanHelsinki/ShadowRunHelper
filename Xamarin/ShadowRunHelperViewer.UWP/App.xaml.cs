using ShadowRunHelper;
using ShadowRunHelper.Model;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ShadowRunHelperViewer.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            UnhandledException += (x, y) => { AppHolder.App_UnhandledException(y.Message, y.Exception); };

            EnteredBackground += App_EnteredBackground;
            LeavingBackground += App_LeavingBackground;
            Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            InitializeComponent();

            Init.Do();
            TAPPLICATION.IO.SharedIO.CurrentIO = new TAPPLICATION_UWP.IO();
            TAPPLICATION.Model.SharedSettingsModel.PlatformSettings = new TAPPLICATION_UWP.Settings();
            TAPPLICATION.PlatformHelper.Platform = new TAPPLICATION_Xamarin.PlatformHelper();
            Rg.Plugins.Popup.Popup.Init();
            AppHolder.InitModel();
        }

        #region Entry-Points
        /// <summary>
        /// normal App launc
        /// </summary>
        /// <param name="args"></param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Xamarin.Forms.Forms.Init(args);
            base.OnLaunched(args);
        }
        /// <summary>
        /// launch via a protocol
        /// </summary>
        /// <param name="args"></param>
        protected override void OnActivated(IActivatedEventArgs args)
        {
            Xamarin.Forms.Forms.Init(args);
            if (args.Kind == ActivationKind.Protocol && args is ProtocolActivatedEventArgs uriArgs)
            {
                var name = uriArgs.Uri.Segments[uriArgs.Uri.Segments.Length - 1];
                name = name.Remove(name.Length - 1);
                AppHolder.FileActivated(name, uriArgs.Uri.LocalPath.Remove(uriArgs.Uri.LocalPath.Length - name.Length));
            }
        }
        /// <summary>
        /// launch vie open file 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnFileActivated(FileActivatedEventArgs args)
        {
            Xamarin.Forms.Forms.Init(args);
            if (args.Files[0].Name.EndsWith(Constants.DATEIENDUNG_CHAR))
            {
                try
                {
                    Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace(Constants.ACCESSTOKEN_FILEACTIVATED, args.Files[0]);
                }
                finally { }
                AppHolder.FileActivated(args.Files[0].Name, args.Files[0].Path.Substring(0, args.Files[0].Path.Length - args.Files[0].Name.Length));
            }
            else if (args.Files[0].Name.EndsWith(".SRHApp1"))
            {
                Features.AppDataPorter.Loading = Features.AppDataPorter.LoadAppPacket(args.Files[0]);
            }
        }
        #endregion

        /// <summary>
        /// Creating and activating ui
        /// get's called after evry entry point method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void App_LeavingBackground(object sender, LeavingBackgroundEventArgs e)
        {
            if (!(Window.Current.Content is Frame rootFrame))
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += (s, ee) => AppHolder.OnNavigationFailed(ee.SourcePageType.FullName);
                Window.Current.Content = rootFrame;
            }
            Window.Current.Activate();
            if (rootFrame.Content == null)
            {
                // Wenn der Navigationsstapel nicht wiederhergestellt wird, zur ersten Seite navigieren
                rootFrame.Navigate(typeof(MainPage));
            }
            else
            {
                // Seite ist aktiv, wir versuchen, den Char anzuzeigen
                AppModel.Instance?.RequestNavigation(SettingsModel.I.LAST_PAGE);
            }
        }

        /// <summary>
        /// gets called if the app is minimized or closed or suspended (by system)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void App_EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
            var def = e.GetDeferral();
            AppHolder.EnteredBackground();
            def.Complete();
        }
    }
}
