//Author: Tobi van Helsinki

using ShadowRunHelper;

namespace ShadowRunHelperViewer.Platform.Droid
{
    internal class DroidInstanceHandling : IInstanceHandling
    {
        string instanceKey = "";
        public string InstanceKey
        {
            get
            {
                return instanceKey;
            }
            set => instanceKey = value;
        }

        public void CreateInstance()
        {
        }
    }
}