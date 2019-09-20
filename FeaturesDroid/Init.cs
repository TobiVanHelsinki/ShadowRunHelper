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
            Features.Ui = new DroidUi();
        }
        public class DroidUi : IUi
        {
            public bool IsTopUiSizeEnabled { get ; set ; }

            public event TopUiSizeChangedEventHandler TopUiSizeChanged;

            public void DisplayCurrentCharName()
            {
            }

            public void GetTopUiSizeChanged()
            {
            }

            public void RegisterTopUiSizeChanged(object VisualElement)
            {
            }
        }
    }
}
