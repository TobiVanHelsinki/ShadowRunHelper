using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ShadowRunHelper.CharModel
{
    public class Handlung : Thing
    {
        [Used_List]
        public ObservableThingListEntryCollection WertZusammensetzung { get; set; }
        [Used_List]
        public ObservableThingListEntryCollection GrenzeZusammensetzung { get; set; }
        [Used_List]
        public ObservableThingListEntryCollection GegenZusammensetzung { get; set; }

        double grenze = 0;
        [Used_UserAttribute]
        public double Grenze
        {
            get { return grenze; }
            set
            {
                if (value != grenze)
                {
                    grenze = value;
                    NotifyPropertyChanged();
                }
            }
        }

        double gegen = 0;
        [Used_UserAttribute]
        public double Gegen
        {
            get { return gegen; }
            set
            {
                if (value != gegen)
                {
                    gegen = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public static IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Connection
            };

        public Handlung() : base()
        {
            WertZusammensetzung = new ObservableThingListEntryCollection(Filter);
            GrenzeZusammensetzung = new ObservableThingListEntryCollection(Filter);
            GegenZusammensetzung = new ObservableThingListEntryCollection(Filter);
            
            WertZusammensetzung.OnCollectionChangedCall(() => { Wert = WertZusammensetzung.Recalculate(); });
            GrenzeZusammensetzung.OnCollectionChangedCall(() => { Grenze = GrenzeZusammensetzung.Recalculate(); });
            GegenZusammensetzung.OnCollectionChangedCall(()=> { Gegen = GegenZusammensetzung.Recalculate(); });
        }
    }
}
