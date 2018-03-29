using ShadowRunHelper.Model;
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Handlung : Thing
    {
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
            LinkedThings.SetFilter(Filter);
            GrenzeZusammensetzung = new ObservableThingListEntryCollection();
            GrenzeZusammensetzung.SetFilter(Filter);
            GegenZusammensetzung = new ObservableThingListEntryCollection();
            GegenZusammensetzung.SetFilter(Filter);

            GrenzeZusammensetzung.OnCollectionChangedCall(() => { Grenze = GrenzeZusammensetzung.Recalculate(); });
            GegenZusammensetzung.OnCollectionChangedCall(()=> { Gegen = GegenZusammensetzung.Recalculate(); });
        }
    }
}
