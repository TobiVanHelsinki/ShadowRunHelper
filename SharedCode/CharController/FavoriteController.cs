///Author: Tobi van Helsinki

using ShadowRunHelper.CharModel;
using System;

namespace ShadowRunHelper.CharController
{
    [ShadowRunHelperController(SupportsEdit = false)]
    public class FavoriteController : Controller<Thing>
    {
        public FavoriteController() : base(ThingDefs.Favorite)
        {
        }

        //Override cController ############################
        public override void RegisterEventAtData(Action Method)
        {
            //do not call base to prevent dead loops
        }
    }
}