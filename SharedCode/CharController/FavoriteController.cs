using Newtonsoft.Json;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ShadowRunHelper.CharController
{
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

    }
}