using ShadowRunHelper.CharModel;

namespace ShadowRunHelper.Model
{
    public class ThingListEntry
    {
        Thing o;
        public Thing Object { get { return o; } set {
                if (value != null)
                {
                    o = value;
                }
                else
                {

                }
            } }
        [Newtonsoft.Json.JsonIgnore]
        public readonly ThingListEntry This;
        public string strProperty;
        public string strPropertyName;

        public ThingListEntry(Thing o, string strPropID = "", string strPropName = "")
        {
            if (o == null)
            {
                Object = new Thing();
            }
            else
            {
                Object = o;
            }
            if (strPropID == null)
            {
                strProperty = "";
            }
            else
            {
                strProperty = strPropName;
            }
            if (strPropName == null)
            {
                strProperty = "";
            }
            else
            {
                strPropertyName = strPropName;
            }
            This = this;
        }
        public ThingListEntry()
        {
            Object = new Thing();
            strProperty = "";
            strPropertyName = "";
            This = this;
        }
    }
}
