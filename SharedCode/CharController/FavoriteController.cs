using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;

namespace ShadowRunHelper.CharController
{
    [ShadowRunHelperController(SupportsEdit=false)]
    public class FavoriteController : Controller<Thing>
    {
        public FavoriteController() : base(ThingDefs.Favorite)
        {
        }

        // Implement IController ##########################
        public override IEnumerable<AllListEntry> GetElementsForThingList()
        {
            return Array.Empty<AllListEntry>();
        }

        //Override cController ############################
        public override void RegisterEventAtData(Action Method)
        {
            //do not call base to prevent dead loops
        }
    }
}