using ShadowRunHelper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ShadowRunHelperViewer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //TAPPLICATION_Xamarin.Init.Do();
            //TLIB_Xamarin.Init.Do();

            AppHolder.InitModel();
            if (SettingsModel.I.FIRST_START)
            {
                SettingsModel.I.InitSettings();
            }

            MainPage = new MainPage();
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
