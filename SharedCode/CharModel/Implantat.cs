//Author: Tobi van Helsinki

///Author: Tobi van Helsinki

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowRunHelper.CharModel
{
    public class Implantat : Item
    {
        private double essenz = 0;
        [Used_UserAttribute]
        public double Essenz
        {
            get { return essenz; }
            set
            {
                if (value != this.essenz)
                {
                    this.essenz = value;
                    NotifyPropertyChanged();
                }
            }
        }

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

        private string _Auswirkung = "";
        [Obsolete(Constants.ObsoleteCalcProperty)]
        public string Auswirkung
        {
            get { return _Auswirkung; }
            set
            {
                if (value != this._Auswirkung)
                {
                    this._Auswirkung = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        public override IEnumerable<ThingDefs> Filter => StaticFilter;

        private static readonly IEnumerable<ThingDefs> StaticFilter = TypeHelper.ThingTypeProperties.Where(x =>
         x.ThingType != ThingDefs.Item &&
         x.ThingType != ThingDefs.Vorteil &&
         x.ThingType != ThingDefs.Nachteil
        ).Select(x => x.ThingType);

        public Implantat()
        {
            Aktiv = true;
        }
    }
}