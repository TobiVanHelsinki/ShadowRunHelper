//Author: Tobi van Helsinki

///Author: Tobi van Helsinki

using ShadowRunHelper;
using ShadowRunHelperViewer.UI.Pages;
using System;
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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(LocalConstants.SyncFusion_LICENSEKEY);
            Log.IsConsoleLogEnabled = true;
            Log.IsInMemoryLogEnabled = true;
            Log.Mode = LogMode.Verbose;
            Log.DisplayChoiceRequested += TLIB.Choice.Xamarin.TLIBChoice_Xamarin.Log_DisplayQuestionRequested;
            CreateLogFile();
            InitializeComponent();
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
            MainPage = /*new NavigationPage*/(new MainPage());
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