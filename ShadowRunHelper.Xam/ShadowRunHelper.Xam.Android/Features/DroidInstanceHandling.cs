namespace ShadowRunHelper
{
    class DroidInstanceHandling : IInstanceHandling
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
