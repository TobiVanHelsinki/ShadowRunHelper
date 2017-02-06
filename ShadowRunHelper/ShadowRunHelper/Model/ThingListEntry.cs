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
        public ThingListEntry This { get; private set; }
        public string strProperty;

        public ThingListEntry(Thing o, string strPropName = "")
        {
            Object = o;
            strProperty = strPropName;
            This = this;
        }
    }
}
