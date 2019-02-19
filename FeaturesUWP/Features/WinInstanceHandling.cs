using System;
using Windows.ApplicationModel;

namespace ShadowRunHelper
{
    class WinInstanceHandling : IInstanceHandling
    {
        Windows.ApplicationModel.
        AppInstance Instance;
        string instanceKey = "";
        public string InstanceKey
        {
            get
            {
                return Instance == null ? instanceKey : Instance.Key;
            }
            set => instanceKey = value;
        }

        public void CreateInstance()
        {
            if (!Windows.Foundation.Metadata.ApiInformation.IsMethodPresent("AppInstance", "FindOrRegisterInstanceForKey") && Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 5))
            {
                string key = Guid.NewGuid().ToString();
                try
                {
                    Instance = AppInstance.FindOrRegisterInstanceForKey(key);
                }
                catch (Exception)
                {
                    InstanceKey = key;
                }
            }
        }

    }
}
