//Author: Tobi van Helsinki

using ShadowRunHelper;

namespace ShadowRunHelperViewer.Platform.Xamarin
{
    public class Init
    {
        public static void Do()
        {
            ShadowRunHelper.IO.SharedIO.CurrentIO = new ShadowRunHelperViewer.Platform.Xamarin.IO();
            ShadowRunHelper.Model.SharedSettingsModel.PlatformSettings = new ShadowRunHelperViewer.Platform.Xamarin.Settings();
            ShadowRunHelper.Helper.PlatformHelper.Platform = new ShadowRunHelperViewer.Platform.Xamarin.PlatformHelper();
        }
    }
}