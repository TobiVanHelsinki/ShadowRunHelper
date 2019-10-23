///Author: Tobi van Helsinki

using System;
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public abstract class Eigenschaft : Thing
    {
        private string auswirkungen = "";
        //[Used_UserAttribute]
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

        public static IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Fertigkeit
            };

        protected Eigenschaft() : base()
        {
            //LinkedThings.FilterOut = Filter;
        }
    }
}