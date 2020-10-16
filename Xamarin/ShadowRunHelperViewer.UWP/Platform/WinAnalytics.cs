//Author: Tobi van Helsinki

//using Microsoft.AppCenter;
//using Microsoft.AppCenter.Analytics;
//using Microsoft.AppCenter.Crashes;
using ShadowRunHelper;
using System.Collections.Generic;

namespace ShadowRunHelperViewer.Platform.UWP
{
    internal class WinAnalytics : IAnalytics
    {
        public void Init()
        {
            //AppCenter.Start(Constants.AppCenterID, typeof(Crashes), typeof(Analytics)); // zu lange, nach mainwindow creation
        }

        public void TrackEvent(string name, IDictionary<string, string> properties = null)
        {
            //Microsoft.AppCenter.Analytics.Analytics.TrackEvent(name, properties);
        }
    }
}