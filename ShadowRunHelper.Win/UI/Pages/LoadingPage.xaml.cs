using Windows.UI.Xaml.Controls;
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

namespace ShadowRunHelper.UI
{
    public sealed partial class LoadingPage : Page
    {
        public static AppInstance Instance;
        private static string instanceKey = "";
        public static string InstanceKey { get { return Instance == null ? instanceKey : Instance.Key; } set => instanceKey = value; }
        bool FirstStart = true;
        readonly AppModel Model;
        readonly SettingsModel Settings;
        Task CheckLicence;
        #region App Startup and Init
        public LoadingPage()
        {
            InitializeComponent();

            //Debug_TimeAnalyser.Start("Overall");
            //Debug_TimeAnalyser.Start("App()");
            Settings = SettingsModel.Initialize();
            CheckLicence = Task.Run(() => IAP.CheckLicence());
            SetConstantStuff();
            Model = AppModel.Initialize();
            if (Settings.START_COUNT < 1)
            {
                Settings.ResetAllSettings();
            }


            Settings.START_COUNT++;
            Task.Run(AppCenterConfiguration);
            Task.Run(RegisterAppInstance);
            //Debug_TimeAnalyser.Stop("App()");
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
                AppCenter.Start(Constants.AppCenterID, typeof(Crashes), typeof(Analytics)); // zu lange, nach mainwindow creation
            }
            catch (Exception)
            {
            }
        }

        public void SetConstantStuff()
        {
            SharedConstants.APP_VERSION_BUILD_DELIM = String.Format("{0}.{1}.{2}.{3}", Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor, Package.Current.Id.Version.Build, Package.Current.Id.Version.Revision);
            SharedConstants.APP_PUBLISHER_MAIL = Constants.APP_PUBLISHER_MAIL_TvH;
            SharedConstants.APP_PUBLISHER = Constants.APP_PUBLISHER_TvH;
            SharedConstants.APP_STORE_ID = Constants.APP_STORE_ID_SRE;
        }

        #endregion
        enum NavType
        {

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);
        }
    }
}
