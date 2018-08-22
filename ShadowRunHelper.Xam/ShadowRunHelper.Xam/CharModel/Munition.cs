
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Munition : Item
    { 
        private string schadenTyp = "";
        [Used_UserAttribute]
        public string SchadenTyp
        {
            get { return schadenTyp; }
            set
            {
                if (value != schadenTyp)
                {
                    schadenTyp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double pool = 0;
        [Used_UserAttribute]
        public double Praezision
        {
            get { return pool; }
            set
            {
                if (value != pool)
                {
                    pool = value;
                    NotifyPropertyChanged();
                }
            }
        }

        double _DK = 0;
        [Used_UserAttribute]
        public double DK
        {
            get { return _DK; }
            set
            {
                if (value != _DK)
                {
                    _DK = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public static IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Fertigkeit
            };
    }
}
