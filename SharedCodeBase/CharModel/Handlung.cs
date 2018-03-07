using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ShadowRunHelper.CharModel
{
    public class Handlung : Thing
    {
        enum Mode
        {
            Wert = 1,
            Grenze = 2,
            Gegen = 3
        }
        List<ThingDefs> lstForbidden = new List<ThingDefs>() {ThingDefs.Handlung};
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

        public Handlung() : base()
        {
            WertZusammensetzung = new ObservableThingListEntryCollection(lstForbidden);
            GrenzeZusammensetzung = new ObservableThingListEntryCollection(lstForbidden);
            GegenZusammensetzung = new ObservableThingListEntryCollection(lstForbidden);
            
            WertZusammensetzung.OnCollectionChangedAndNow(() => { Wert = WertZusammensetzung.Recalculate(); });
            GrenzeZusammensetzung.OnCollectionChangedAndNow(() => { Grenze = GrenzeZusammensetzung.Recalculate(); });
            GegenZusammensetzung.OnCollectionChangedAndNow(()=> { Gegen = GegenZusammensetzung.Recalculate(); });

            //WertZusammensetzung.CollectionChanged += (u, c) => { CollectionChanged(Mode.Wert); };
            //GrenzeZusammensetzung.CollectionChanged += (u, c) => { CollectionChanged(Mode.Grenze); };
            //GegenZusammensetzung.CollectionChanged += (u, c) => { CollectionChanged(Mode.Gegen); };
            //CollectionChanged(Mode.Wert);
            //CollectionChanged(Mode.Grenze);
            //CollectionChanged(Mode.Gegen);
        }
        public static IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Connection
            };

        //void CollectionChanged(Mode mode)
        //{
        //    NotifyPropertyChanged();
        //    Action TODO;
        //    switch (mode)
        //    {
        //        case Mode.Grenze:
        //            TODO = ()=>{ Grenze = Recalculate(GrenzeZusammensetzung); };
        //            break;
        //        case Mode.Gegen:
        //            TODO = ()=>{ Gegen = Recalculate(GegenZusammensetzung); };
        //            break;
        //        case Mode.Wert:
        //        default:
        //            TODO = ()=>{ Wert = Recalculate(WertZusammensetzung); };
        //            break;
        //    }
        //    TODO();
        //    foreach (var item in WertZusammensetzung)
        //    {
        //        item.Object.PropertyChanged -= (u, c) => TODO();
        //        item.Object.PropertyChanged += (u, c) => TODO();
        //    }

        //    switch (mode)
        //    {
        //        case Mode.Wert:
        //            Wert = Recalculate(WertZusammensetzung);
        //            foreach (var item in WertZusammensetzung)
        //            {
        //                item.Object.PropertyChanged -= (u, c) => { Wert = Recalculate(WertZusammensetzung); };
        //                item.Object.PropertyChanged += (u, c) => { Wert = Recalculate(WertZusammensetzung); };
        //            }
        //            break;
        //        case Mode.Grenze:
        //            Grenze = Recalculate(GrenzeZusammensetzung);
        //            foreach (var item in GrenzeZusammensetzung)
        //            {
        //                item.Object.PropertyChanged -= (u, c) => { Grenze = Recalculate(GrenzeZusammensetzung); };
        //                item.Object.PropertyChanged += (u, c) => { Grenze = Recalculate(GrenzeZusammensetzung); };
        //            }
        //            break;
        //        case Mode.Gegen:
        //            Gegen = Recalculate(GegenZusammensetzung);
        //            foreach (var item in GegenZusammensetzung)
        //            {
        //                item.Object.PropertyChanged -= (u, c) => { Gegen = GegenZusammensetzung.Recalculate(); };
        //                item.Object.PropertyChanged += (u, c) => { Gegen = GegenZusammensetzung.Recalculate(); };
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}

        //static double Recalculate(ObservableCollection<AllListEntry> List) {
        //    return List.Aggregate<AllListEntry, double>(0, (accvalue, next) => accvalue + next.Object.GetPropertyValueOrDefault(next.PropertyID));
        //}
    }
}
