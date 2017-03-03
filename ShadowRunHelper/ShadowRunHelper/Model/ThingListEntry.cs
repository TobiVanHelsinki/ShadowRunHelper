using ShadowRunHelper.CharModel;

namespace ShadowRunHelper.Model
{
    public class ThingListEntry
    {
        Thing _object;
        public readonly Thing Object;// {
        //    get
        //    { return _object; }
        //    set
        //    {
        //        //if (value == null)
        //        //{
        //            //_object = new Thing();

        //        //}
        //        //else
        //        {
        //            _object = value;
        //        }
        //         }
        //}
        public readonly ThingListEntry This;
        public readonly string strProperty;

        public ThingListEntry(Thing o, string strPropName = "")
        {
            if (o == null)
            {
                Object = new Thing();
            }
            else
            {
                Object = o;
            }
            if (strPropName == null)
            {
                strProperty = "";
            }
            else
            {
                strProperty = strPropName;
            }
            This = this;
        }
        public ThingListEntry()
        {
            Object = new Thing();
            strProperty = "";
            This = this;
        }
    }
}
