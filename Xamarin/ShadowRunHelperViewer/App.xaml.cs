using ShadowRunHelper;
using ShadowRunHelperViewer.Platform;
using ShadowRunHelperViewer.UI.Pages;
using SharedCode.Ressourcen;
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
            Log.IsConsoleLogEnabled = true;
            Log.IsInMemoryLogEnabled = true;
            Log.Mode = LogMode.Verbose;
            CreateLogFile();
            InitializeComponent();
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            L10n.SetLocale(ci);
            Strings.Culture = ci;
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
