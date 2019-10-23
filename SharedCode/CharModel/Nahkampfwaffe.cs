///Author: Tobi van Helsinki

using System.Collections.Generic;
using System.Linq;

namespace ShadowRunHelper.CharModel
{
    public class Nahkampfwaffe : Waffe
    {
        private double reichweite = 0;
        [Used_UserAttribute]
        public double Reichweite
        {
            get { return reichweite; }
            set
            {
                if (value != this.reichweite)
                {
                    this.reichweite = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public static IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Fertigkeit, ThingDefs.Connection, ThingDefs.Sin
            };

        public Nahkampfwaffe()
        {
            //LinkedThings.FilterOut = (Filter);
        }
    }
}