namespace ShadowRunHelper
{
    public class Init
    {
        public static void Do()
        {
            Features.Activities = new DroidActivities();
            Features.Analytics = new DroidAnalytics();
            Features.IAP = new DroidIAP();
            Features.InstanceHandling = new DroidInstanceHandling();
            Features.AppInformation = new DroidAppInformation();
            Features.AppDataPorter = new DroidAppDataPorter();
        }
    }
}
