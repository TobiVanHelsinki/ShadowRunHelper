using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRunHelper.Model
{
    public class ThingListEntry
    {

        public Thing Object;
        public ThingListEntry This;
        public string strProperty;
        public double nValue;

        public ThingListEntry(Thing o, string strPropName = "", double value = 0)
        {
            Object = o;
            strProperty = strPropName;
            nValue = value;
            This = this;
        }
    }
}
