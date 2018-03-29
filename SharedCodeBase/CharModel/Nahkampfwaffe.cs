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
        public static IEnumerable<ThingDefs> Filter = TypeHelper.ThingTypeProperties.Where(x =>
            x.ThingType != ThingDefs.Handlung &&
            x.ThingType != ThingDefs.Fernkampfwaffe &&
            x.ThingType != ThingDefs.Nahkampfwaffe &&
            x.ThingType != ThingDefs.Munition &&
            x.ThingType != ThingDefs.Implantat
        ).Select(x => x.ThingType);

        public Nahkampfwaffe()
        {
            LinkedThings.SetFilter(Filter);
        }
    }
}
