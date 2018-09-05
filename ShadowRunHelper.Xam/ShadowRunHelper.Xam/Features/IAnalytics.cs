using System.Collections.Generic;

namespace ShadowRunHelper
{
    public interface IAnalytics
    {
        void TrackEvent(string name, IDictionary<string, string> properties = null);
        void Init();
    }

}
