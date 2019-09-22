using ShadowRunHelper;
using ShadowRunHelperViewer.Platform;
using ShadowRunHelperViewer.Strings;
using ShadowRunHelperViewer.UI.Pages;
using System;
using System.Globalization;
using System.Threading.Tasks;
using TAPPLICATION.IO;
using TAPPLICATION.Model;
using TLIB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ShadowRunHelperViewer
{
    public partial class App : Application
    {
        public App()
        {
            Log.IsConsoleLogEnabled = true;
            Log.IsInMemoryLogEnabled = true;
            Log.Mode = LogMode.Verbose;
            CreateLogFile();
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                AppResources.Culture = ci; // set the RESX for resource localization
                CultureInfo.CurrentUICulture = ci;
                DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods

                AppResources.Culture = new CultureInfo("fr-FR"); //TestPurpose
            }

            AppHolder.InitModel();
            try
            {
                SharedSettingsModel.PlatformSettings.PrepareSettingsSavePlace();
            }
            catch (Exception ex)
            {
                Log.Write("Error creating char store directory", ex);
            }
            try
            {
                AppHolder.Init();
            }
            catch (Exception ex)
            {
                Log.Write("Error initing app", ex);
            }
            MainPage = new MainPage();
        }

        private static async Task CreateLogFile()
        {
            Log.LogFile = await SharedIO.CurrentIO.GetCompleteInternPath(Place.Local) + "SRLogFile.txt";
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
