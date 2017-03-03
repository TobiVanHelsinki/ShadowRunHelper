using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ShadowRunHelper.CharModel
{
    public class Handlung : Thing
    {
        public enum Mode
        {
            Wert = 1,
            Grenze = 2,
            Gegen = 3
        }
        public ObservableCollection<ThingListEntry> WertZusammensetzung;
        public ObservableCollection<ThingListEntry> GrenzeZusammensetzung;
        public ObservableCollection<ThingListEntry> GegenZusammensetzung;

        private double grenze = 0;
        public double Grenze
        {
            get { return grenze; }
            set
            {
                if (value != this.grenze)
                {
                    this.grenze = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double gegen = 0;
        public double Gegen
        {
            get { return gegen; }
            set
            {
                if (value != this.gegen)
                {
                    this.gegen = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public Handlung()
        {
            ThingType = ThingDefs.Handlung;

            WertZusammensetzung = new ObservableCollection<ThingListEntry>();
            GrenzeZusammensetzung = new ObservableCollection<ThingListEntry>();
            GegenZusammensetzung = new ObservableCollection<ThingListEntry>();
            WertZusammensetzung.CollectionChanged += (u, c) => { CollectionChanged(Mode.Wert); };
            GrenzeZusammensetzung.CollectionChanged += (u, c) => { CollectionChanged(Mode.Grenze); };
            GegenZusammensetzung.CollectionChanged += (u, c) => { CollectionChanged(Mode.Gegen); };
            CollectionChanged(Mode.Wert);
            CollectionChanged(Mode.Grenze);
            CollectionChanged(Mode.Gegen);

        }

        private void CollectionChanged(Mode mode)
        {
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

        private static double Recalculate(ObservableCollection<ThingListEntry> List) {
            double temp = 0;
            foreach (ThingListEntry item in List)
            {
                try
                {
                    temp += item.Object.GetValue(item.strProperty);
                }
                catch (Exception)
                {
                }
            }
            return temp;
        }
    }
}
