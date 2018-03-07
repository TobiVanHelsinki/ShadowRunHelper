
using ShadowRunHelper.Model;
using System.Collections.Generic;
using System.Linq;

namespace ShadowRunHelper.CharModel
{
    public class Attribut : Thing
    {
        [Used_List]
        public ObservableThingListEntryCollection Addidtions { get; set; }

        public static IEnumerable<ThingDefs> Filter = TypeHelper.ThingTypeProperties.Where(x => 
        x.ThingType != ThingDefs.Implantat ||
        x.ThingType != ThingDefs.Vorteil ||
        x.ThingType != ThingDefs.Nachteil
        ).Select(x => x.ThingType);

    }
}
