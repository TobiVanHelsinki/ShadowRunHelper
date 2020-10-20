//Author: Tobi van Helsinki

using ShadowRunHelperViewer.Platform.UWP;
using ShadowRunHelperViewer.Platform.Xam;
using Xamarin.Forms;

[assembly: Dependency(typeof(WinLocalize))]

namespace ShadowRunHelperViewer.Platform.UWP
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