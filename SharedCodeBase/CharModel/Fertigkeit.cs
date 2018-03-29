using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Fertigkeit : Thing
    {
        public static IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Connection, ThingDefs.Fertigkeit
            };

        public Fertigkeit() : base()
        {
            LinkedThings.SetFilter(Filter);
        }
    }
}
