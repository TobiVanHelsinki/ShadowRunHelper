using System.Collections.Generic;
using System.Linq;

namespace ShadowRunHelper.CharModel
{
    public class Attribut : Thing
    {
        public static IEnumerable<ThingDefs> Filter = TypeHelper.ThingTypeProperties.Where(x => 
        x.ThingType != ThingDefs.Implantat &&
        x.ThingType != ThingDefs.Vorteil &&
        x.ThingType != ThingDefs.Nachteil
        ).Select(x => x.ThingType);

        public Attribut() : base()
        {
            LinkedThings.SetFilter(Filter);
        }
    }
}
