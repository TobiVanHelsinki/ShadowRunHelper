﻿//Author: Tobi van Helsinki

using ShadowRunHelper;

namespace ShadowRunHelperViewer.Platform.Droid
{
    public class Init
    {
        public static void Do()
        {
            ShadowRunHelper.IO.SharedIO.CurrentIO = new IO();
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
            public bool IsCustomTitleBarEnabled { get; set; }

            public event CustomTitleBarChangesEventHandler CustomTitleBarChanges;

            public void DisplayCurrentCharName()
            {
            }

            public void TriggerCustomTitleBarChanges()
            {
            }

            public void SetCustomTitleBar(object VisualElement)
            {
            }
        }
    }
}