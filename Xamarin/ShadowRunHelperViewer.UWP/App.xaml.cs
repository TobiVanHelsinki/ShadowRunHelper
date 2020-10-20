//Author: Tobi van Helsinki

using ShadowRunHelper;
using Syncfusion.ListView.XForms.UWP;
using Syncfusion.SfBusyIndicator.XForms.UWP;
using Syncfusion.SfNavigationDrawer.XForms.UWP;
using Syncfusion.XForms.UWP.Border;
using Syncfusion.XForms.UWP.Buttons;
using Syncfusion.XForms.UWP.PopupLayout;
using System.Collections.Generic;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ShadowRunHelperViewer.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// Additional assemblies to include
        /// </summary>
        private readonly List<Assembly> assembliesToInclude = new List<Assembly>();

        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            UnhandledException += (x, y) => { AppHolder.App_UnhandledException(y.Message, y.Exception); };

            EnteredBackground += App_EnteredBackground;
            LeavingBackground += App_LeavingBackground;

            InitializeComponent();

            #region Init Libs
            assembliesToInclude.Add(typeof(dotMorten.Xamarin.Forms.AutoSuggestBox).GetTypeInfo().Assembly);
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(LocalConstants.SyncFusion_LICENSEKEY);
            assembliesToInclude.Add(typeof(SfBusyIndicatorRenderer).GetTypeInfo().Assembly);
            assembliesToInclude.Add(typeof(SfListViewRenderer).GetTypeInfo().Assembly);
            assembliesToInclude.Add(typeof(SfBorderRenderer).GetTypeInfo().Assembly);
            assembliesToInclude.Add(typeof(SfButtonRenderer).GetTypeInfo().Assembly);
            assembliesToInclude.Add(typeof(SfPopupLayoutRenderer).GetTypeInfo().Assembly);
            assembliesToInclude.Add(typeof(SfNavigationDrawerRenderer).GetTypeInfo().Assembly);
            assembliesToInclude.Add(typeof(SfPopupLayoutRenderer).GetTypeInfo().Assembly);
            assembliesToInclude.Add(typeof(Rg.Plugins.Popup.Popup).GetTypeInfo().Assembly);
            SfListViewRenderer.Init();
            SfPopupLayoutRenderer.Init();

            ShadowRunHelperViewer.Platform.Xam.Init.Do();
            ShadowRunHelperViewer.Platform.UWP.Init.Do();

            Rg.Plugins.Popup.Popup.Init();
            #endregion Init Libs
        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            ShadowRunHelperViewer.UI.Pages.MainPage.Instance.SendBackButtonPressed();
        }

        #region Entry-Points

        /// <summary>
        /// normal App launc
        /// </summary>
        /// <param name="args"></param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Xamarin.Forms.Forms.Init(args, assembliesToInclude);
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
            base.OnLaunched(args);
        }

        /// <summary>
        /// launch via a protocol
        /// </summary>
        /// <param name="args"></param>
        protected override void OnActivated(IActivatedEventArgs args)
        {
            Xamarin.Forms.Forms.Init(args, assembliesToInclude);
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
            if (args.Kind == ActivationKind.Protocol && args is ProtocolActivatedEventArgs uriArgs)
            {
                AppHolder.FileActivated(uriArgs.Uri.LocalPath);
            }
        }

        /// <summary>
        /// launch via open file
        /// </summary>
        /// <param name="args"></param>
        protected override void OnFileActivated(FileActivatedEventArgs args)
        {
            Xamarin.Forms.Forms.Init(args, assembliesToInclude);
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
            if (args.Files[0].Name.EndsWith(Constants.DATEIENDUNG_CHAR))
            {
                try
                {
                    Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace(Constants.ACCESSTOKEN_FILEACTIVATED, args.Files[0]);
                }
                finally { }
                AppHolder.FileActivated(args.Files[0].Path);
            }
            else if (args.Files[0].Name.EndsWith(".SRHApp1"))
            {
                Features.AppDataPorter.Loading = Features.AppDataPorter.LoadAppPacket(args.Files[0]);
            }
        }
        #endregion Entry-Points

        /// <summary>
        /// Creating and activating ui get's called after evry entry point method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_LeavingBackground(object sender, LeavingBackgroundEventArgs e)
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
                ShadowRunHelper.Model.AppModel.Instance?.RequestNavigation(SettingsModel.I.LAST_PAGE);
            }
        }

        /// <summary>
        /// gets called if the app is minimized or closed or suspended (by system)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
            Windows.Foundation.Deferral def = e.GetDeferral();
            AppHolder.EnteredBackground();
            def.Complete();
        }
    }
}