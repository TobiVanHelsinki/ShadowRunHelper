//Author: Tobi van Helsinki

///Author: Tobi van Helsinki

using Newtonsoft.Json;
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

        [JsonIgnore]
        public override IEnumerable<ThingDefs> Filter => StaticFilter;

        private static readonly IEnumerable<ThingDefs> StaticFilter = new[]
            {
                ThingDefs.Handlung, ThingDefs.Fertigkeit
            };
    }
}