//using Microsoft.AppCenter;
//using Microsoft.AppCenter.Analytics;
//using Microsoft.AppCenter.Crashes;
using System.Collections.Generic;

namespace ShadowRunHelper
{
    class WinAnalytics : IAnalytics
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
