namespace ShadowRunHelper
{
    public class Init
    {
        public static void Do()
        {
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
