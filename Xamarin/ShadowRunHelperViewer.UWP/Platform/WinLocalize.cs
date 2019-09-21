using ShadowRunHelperViewer;
using ShadowRunHelperViewer.Platform;
using Xamarin.Forms;

[assembly: Dependency(typeof(WinLocalize))]
namespace ShadowRunHelperViewer
{
    public class WinLocalize : ILocalize
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