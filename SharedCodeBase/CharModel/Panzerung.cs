using System.Collections.Generic;
using System.Linq;

namespace ShadowRunHelper.CharModel
{
    public class Panzerung : Item
    {
        private double kapazitaet = 0;
        [Used_UserAttribute]
        public double Kapazitaet
        {
            get { return kapazitaet; }
            set
            {
                if (value != this.kapazitaet)
                {
                    this.kapazitaet = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public static IEnumerable<ThingDefs> Filter = TypeHelper.ThingTypeProperties.Where(x =>
            x.ThingType != ThingDefs.Item &&
            x.ThingType != ThingDefs.Fernkampfwaffe &&
            x.ThingType != ThingDefs.Nahkampfwaffe &&
            x.ThingType != ThingDefs.Panzerung &&
            x.ThingType != ThingDefs.Implantat &&
            x.ThingType != ThingDefs.Vorteil &&
            x.ThingType != ThingDefs.Nachteil
        ).Select(x => x.ThingType);

        public Panzerung() : base()
        {
            LinkedThings.SetFilter(Filter);
        }
    }
}
