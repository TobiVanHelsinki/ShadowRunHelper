///Author: Tobi van Helsinki

using System;
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public abstract class Eigenschaft : Thing
    {
        private string auswirkungen = "";
        [Obsolete(Constants.ObsoleteCalcProperty)]
        public string Auswirkungen
        {
            get { return auswirkungen; }
            set
            {
                if (value != auswirkungen)
                {
                    auswirkungen = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public override IEnumerable<ThingDefs> Filter => StaticFilter;

        static readonly IEnumerable<ThingDefs> StaticFilter = new[]
            {
                ThingDefs.Handlung, ThingDefs.Fertigkeit
            };
    }
}