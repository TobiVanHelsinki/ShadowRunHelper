//Author: Tobi van Helsinki

using ShadowRunHelper;

namespace ShadowRunHelperViewer.Platform.UWP
{
    public class Init
    {
        public static void Do()
        {
            ShadowRunHelper.IO.SharedIO.CurrentIO = new ShadowRunHelperViewer.Platform.UWP.IO();
            ShadowRunHelper.Model.SharedSettingsModel.PlatformSettings = new ShadowRunHelperViewer.Platform.UWP.Settings();
            ShadowRunHelper.Helper.PlatformHelper.Platform = new ShadowRunHelperViewer.Platform.UWP.PlatformHelper();

            Features.Activities = new WinActivities();
            Features.Analytics = new WinAnalytics();
            Features.IAP = new WinIAP();
            Features.InstanceHandling = new WinInstanceHandling();
            Features.AppInformation = new WinAppInformation();
            Features.AppDataPorter = new WinAppDataPorter();
            Features.Ui = new WinUi();
        }
    }
}