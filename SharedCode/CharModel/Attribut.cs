///Author: Tobi van Helsinki

using System.Collections.Generic;
using System.Linq;

namespace ShadowRunHelper.CharModel
{
    public class Attribut : Thing
    {
        public override IEnumerable<ThingDefs> Filter => StaticFilter;

        private static readonly IEnumerable<ThingDefs> StaticFilter = TypeHelper.ThingTypeProperties.Where(x =>
         x.ThingType != ThingDefs.Implantat &&
         x.ThingType != ThingDefs.Vorteil &&
         x.ThingType != ThingDefs.Nachteil
        ).Select(x => x.ThingType);
    }
}