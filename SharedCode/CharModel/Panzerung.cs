///Author: Tobi van Helsinki

using System.Collections.Generic;
using System.Linq;

namespace ShadowRunHelper.CharModel
{
    public class Panzerung : Item
    {
        private ConnectProperty _Capacity;
        [Used_UserAttribute]
        public ConnectProperty Capacity
        {
            get { return _Capacity; }
            set
            {
                if (value != this._Capacity)
                {
                    this._Capacity = value;
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
            //LinkedThings.FilterOut = (Filter);
        }
    }
}