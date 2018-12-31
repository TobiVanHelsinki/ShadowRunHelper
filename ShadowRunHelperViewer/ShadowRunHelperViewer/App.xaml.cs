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
            //ci = new System.Globalization.CultureInfo("de");
            L10n.SetLocale(ci);
            Strings.Culture = ci;
            var assembly = typeof(App).Assembly; // "EmbeddedImages" should be a class in your app
            var assembly2 = typeof(CharHolder).Assembly; // "EmbeddedImages" should be a class in your app
            foreach (var res in assembly.GetManifestResourceNames().Concat(assembly2.GetManifestResourceNames()))
            {
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            }
            var texttest = Strings.Model_SIN__Text;
            AppHolder.InitModel();
            if (SettingsModel.I.FIRST_START)
            {
#if DEBUG
                //SettingsModel.I.InitSettings(); //TODO incomment when not testing
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
