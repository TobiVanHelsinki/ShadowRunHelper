using ShadowRunHelper.Model;
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Fertigkeit : Thing
    {
        [Used_List]
        public ObservableThingListEntryCollection PoolZusammensetzung { get; set; }

        private double _Pool = 0;
        [Used_UserAttribute]
        public double Pool
        {
            get { return _Pool; }
            set
            {
                if (value != _Pool)
                {
                    _Pool = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public static IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Connection, ThingDefs.Fertigkeit
            };

        public Fertigkeit() : base()
        {
            PoolZusammensetzung = new ObservableThingListEntryCollection(Filter);
            PoolZusammensetzung.OnCollectionChangedCall(() => 
            { Pool = Wert + PoolZusammensetzung.Recalculate(); });

            PropertyChanged += (s, e)=> { if (e.PropertyName == "Wert") Pool = Wert + PoolZusammensetzung.Recalculate(); };
        }
    }
}
