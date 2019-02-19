using ShadowRunHelper;
using ShadowRunHelper.Model;
using SharedCode.Ressourcen;
using System.Linq;
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
            var ci = DependencyService.Get<ILocale>().GetCurrentCultureInfo();
            L10n.SetLocale(ci);
            Strings.Culture = ci;

            AppHolder.InitModel();
            if (SettingsModel.I.FIRST_START)
            {
#if DEBUG
                SettingsModel.I.InitSettings(); //TODO incomment when not testing
#else
                SettingsModel.I.InitSettings();
#endif
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
