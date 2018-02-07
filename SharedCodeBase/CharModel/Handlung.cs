using ShadowRunHelper.Model;
using SharedCodeBase.Model;
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
        public ObservableThingListEntryCollection WertZusammensetzung;
        public ObservableThingListEntryCollection GrenzeZusammensetzung;
        public ObservableThingListEntryCollection GegenZusammensetzung;

        double grenze = 0;
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

        public Handlung()
        {
            ThingType = ThingDefs.Handlung;
            WertZusammensetzung = new ObservableThingListEntryCollection(lstForbidden);
            GrenzeZusammensetzung = new ObservableThingListEntryCollection(lstForbidden);
            GegenZusammensetzung = new ObservableThingListEntryCollection(lstForbidden);
            WertZusammensetzung.CollectionChanged += (u, c) => { CollectionChanged(Mode.Wert); };
            GrenzeZusammensetzung.CollectionChanged += (u, c) => { CollectionChanged(Mode.Grenze); };
            GegenZusammensetzung.CollectionChanged += (u, c) => { CollectionChanged(Mode.Gegen); };
            CollectionChanged(Mode.Wert);
            CollectionChanged(Mode.Grenze);
            CollectionChanged(Mode.Gegen);
        }

        public override Thing Copy(Thing target = null)
        {
            if (target == null)
            {
                target = new Handlung();
            }
            base.Copy(target);
            Handlung target2 = target as Handlung;

            foreach (AllListEntry item in WertZusammensetzung)
            {
                target2.WertZusammensetzung.Add(new AllListEntry() { Object = item.Object.Copy(), strProperty = item.strProperty, strPropertyName = item.strPropertyName });
            }
            foreach (AllListEntry item in GegenZusammensetzung)
            {
                target2.GegenZusammensetzung.Add(new AllListEntry() { Object = item.Object.Copy(), strProperty = item.strProperty, strPropertyName = item.strPropertyName });
            }
            foreach (AllListEntry item in GrenzeZusammensetzung)
            {
                target2.GrenzeZusammensetzung.Add(new AllListEntry() { Object = item.Object.Copy(), strProperty = item.strProperty, strPropertyName = item.strPropertyName });
            }
            return target;
        }

        void CollectionChanged(Mode mode)
        {
            NotifyPropertyChanged();
            switch (mode)
            {
                case Mode.Wert:
                    Wert = Recalculate(WertZusammensetzung);
                    foreach (var item in WertZusammensetzung)
                    {
                        item.Object.PropertyChanged -= (u, c) => { Wert = Recalculate(WertZusammensetzung); };
                        item.Object.PropertyChanged += (u, c) => { Wert = Recalculate(WertZusammensetzung); };
                    }
                    break;
                case Mode.Grenze:
                    Grenze = Recalculate(GrenzeZusammensetzung);
                    foreach (var item in GrenzeZusammensetzung)
                    {
                        item.Object.PropertyChanged -= (u, c) => { Grenze = Recalculate(GrenzeZusammensetzung); };
                        item.Object.PropertyChanged += (u, c) => { Grenze = Recalculate(GrenzeZusammensetzung); };
                    }
                    break;
                case Mode.Gegen:
                    Gegen = Recalculate(GegenZusammensetzung);
                    foreach (var item in GegenZusammensetzung)
                    {
                        item.Object.PropertyChanged -= (u, c) => { Gegen = Recalculate(GegenZusammensetzung); };
                        item.Object.PropertyChanged += (u, c) => { Gegen = Recalculate(GegenZusammensetzung); };
                    }
                    break;
                default:
                    break;
            }
        }

        static double Recalculate(ObservableCollection<AllListEntry> List) {
            return List.Aggregate<AllListEntry, double>(0, (accvalue, next) => accvalue + next.Object.GetValue(next.strProperty));
        }
    }
}
