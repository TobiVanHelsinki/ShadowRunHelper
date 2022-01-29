using ShadowRunHelper.Model;
using ShadowRunHelper.UI;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper
{
    sealed partial class App : Application
    {
        public App()
        {
            UnhandledException += (x, y) => { AppHolder.App_UnhandledException(y.Message,y.Exception); };

            EnteredBackground += App_EnteredBackground;
            LeavingBackground += App_LeavingBackground;
            InitializeComponent();

            Init.Do();
            TAPPLICATION_UWP.Init.Do();
            AppHolder.InitModel();
        }

        #region Entry-Points
        protected override void OnActivated(IActivatedEventArgs args)
        {
            if (args.Kind == ActivationKind.Protocol && args is ProtocolActivatedEventArgs uriArgs)
            {
                var name = uriArgs.Uri.Segments[uriArgs.Uri.Segments.Length - 1];
                name = name.Remove(name.Length - 1);
                AppHolder.FileActivated(name, 
                    uriArgs.Uri.LocalPath.Remove(uriArgs.Uri.LocalPath.Length - name.Length));
            }
        }
        protected override void OnFileActivated(FileActivatedEventArgs args)
        {
            if (args.Files[0].Name.EndsWith(Constants.DATEIENDUNG_CHAR))
            {
                try
                {
                    Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace(Constants.ACCESSTOKEN_FILEACTIVATED, args.Files[0]);
                }finally{}
                AppHolder.FileActivated(args.Files[0].Name, args.Files[0].Path.Substring(0, args.Files[0].Path.Length - args.Files[0].Name.Length));
            }
            else if (args.Files[0].Name.EndsWith(".SRHApp1"))
            {
                Features.AppDataPorter.Loading = Features.AppDataPorter.LoadAppPacket(args.Files[0]);
            }
        }
        #endregion

        void App_LeavingBackground(object sender, LeavingBackgroundEventArgs e)
        {
            Stopwatch w = new Stopwatch();
            //var def = e.GetDeferral();
            w.Start();
            if (!(Window.Current.Content is Frame rootFrame))
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += (s, ee)=>AppHolder.OnNavigationFailed(ee.SourcePageType.FullName);
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
            w.Stop();
            //def.Complete();
        }


        void App_EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
            //Stopwatch w = new Stopwatch();
            var def = e.GetDeferral();
            //w.Start();
            AppHolder.EnteredBackground(); // 100 ms
            //w.Stop();
            //Debug.WriteLine(w.Elapsed);
            def.Complete();
        }
    }
}
