using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Fertigkeit : Thing
    {
        public static IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Connection, ThingDefs.Sin
            };

        public Fertigkeit() : base()
        {
            LinkedThings.SetFilter(Filter);
        }

        protected override double InternValueOf(string ID)
        {
            if (ID == null || ID == "" || ID == "Wert")
            {
                return Wert;
            }
            else
            {
                return base.InternValueOf(ID);
            }
        }
    }
}
