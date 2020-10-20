//Author: Tobi van Helsinki

using ShadowRunHelper;

namespace ShadowRunHelperViewer.Platform.Xam
{
    public class Init
    {
        public static void Do()
        {
            ShadowRunHelper.IO.SharedIO.CurrentIO = new ShadowRunHelperViewer.Platform.Xam.IO();
            ShadowRunHelper.Model.SharedSettingsModel.PlatformSettings = new ShadowRunHelperViewer.Platform.Xam.Settings();
            ShadowRunHelper.Helper.PlatformHelper.Platform = new ShadowRunHelperViewer.Platform.Xam.PlatformHelper();
        }
    }
}