using System.Collections.Generic;
using System.Linq;

namespace ShadowRunHelper.CharModel
{
    public class Fernkampfwaffe : Waffe
    {
        private double _RK = 0;
        [Used_User]
        public double RK //RK
        {
            get { return _RK; }
            set
            {
                if (value != _RK)
                {
                    _RK = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string modi = "";
        [Used_User]
        public string Modi
        {
            get { return modi; }
            set
            {
                if (value != modi)
                {
                    modi = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public static IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Fertigkeit, ThingDefs.Connection, ThingDefs.Sin
            };

        public Fernkampfwaffe() : base()
        {
            LinkedThings.FilterOut = (Filter);
        }


    }
}
