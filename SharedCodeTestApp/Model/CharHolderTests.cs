using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedCodeTestApp.Model
{
    public static class CharHolderTests
    {
        public static void Delete()
        {
            SettingsModel.Initialize();
            AppModel.Initialize();
            CharHolder c = new CharHolder();
            Thing t = new Fertigkeit();
            t.Bezeichner = "Test";
            c.Add(t);
            c.Add(t);
            c.Add(t);
            if (c.ThingList.Count != 3)
            {

            }
            c.Remove(t);
            if (c.ThingList.Count != 0)
            {

            }
        }
    }
}
