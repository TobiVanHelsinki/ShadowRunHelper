using ShadowRunHelperViewer;
using Xamarin.Forms;

[assembly: Dependency(typeof(Localize))]
namespace ShadowRunHelperViewer
{
    public class Localize : ILocale
    {
        public System.Globalization.CultureInfo GetCurrentCultureInfo()
        {
            return new System.Globalization.CultureInfo(
                Windows.System.UserProfile.GlobalizationPreferences.Languages[0].ToString());
        }

        public void SetLocale(System.Globalization.CultureInfo ci)
        {
            // Do nothing
        }
    }
}