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

        public static IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Fertigkeit, ThingDefs.Connection, ThingDefs.Sin
            };

        public Panzerung() : base()
        {
            LinkedThings.FilterOut = (Filter);
        }
    }
}
