//Author: Tobi van Helsinki

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Munition : Waffe
    {
        [Obsolete]
        public double Praezision
        {
            get { return Precision.TrueValue; }
            set
            {
                if (value != Precision.BaseValue)
                {
                    Precision.BaseValue = value;
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