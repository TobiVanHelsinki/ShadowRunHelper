using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static ShadowRunHelper.Ressourcen.TypNamen;

namespace ShadowRunHelper.CharModel
{
    public class Handlung : CharModel.Thing, INotifyPropertyChanged
    {
        //public override double GetValue(string ID = "")
        //{
        //    return Wert;
        //}

        //private Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry> zusammensetzung = new Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry>();
        //public Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry> Zusammensetzung
        //{
        //    get { return zusammensetzung;
        //    }
        //    set
        //    {
        //        if (value != this.zusammensetzung)
        //        {
        //                this.zusammensetzung = value;
        //                NotifyPropertyChanged();
        //        }
        //    }
        //}

        //private Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry> grenzeZusammensetzung = new Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry>();
        //public Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry> GrenzeZusammensetzung
        //{
        //    get
        //    {
        //        return grenzeZusammensetzung;
        //    }
        //    set
        //    {
        //        if (value != this.grenzeZusammensetzung)
        //        {
        //            this.grenzeZusammensetzung = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        //private Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry> gegenZusammensetzung = new Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry>();
        //public Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry> GegenZusammensetzung
        //{
        //    get
        //    {
        //        return gegenZusammensetzung;
        //    }
        //    set
        //    {
        //        if (value != this.gegenZusammensetzung)
        //        {
        //            this.gegenZusammensetzung = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        public ObservableCollection<KeyValuePair<Thing, string>> Zusammensetzung;
        public ObservableCollection<KeyValuePair<Thing, string>> GrenzeZusammensetzung;
        public ObservableCollection<KeyValuePair<Thing, string>> GegenZusammensetzung;

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

            Zusammensetzung = new ObservableCollection<KeyValuePair<Thing, string>>();
            GrenzeZusammensetzung = new ObservableCollection<KeyValuePair<Thing, string>>();
            GegenZusammensetzung = new ObservableCollection<KeyValuePair<Thing, string>>();
            Zusammensetzung.CollectionChanged += (u, c) => { CollectionChanged(Zusammensetzung); };
            GrenzeZusammensetzung.CollectionChanged += (u, c) => { CollectionChanged(GrenzeZusammensetzung); };
            GegenZusammensetzung.CollectionChanged += (u, c) => { CollectionChanged(GegenZusammensetzung); };
            CollectionChanged(Zusammensetzung);
            CollectionChanged(GrenzeZusammensetzung);
            CollectionChanged(GegenZusammensetzung);

        }

        private void CollectionChanged(ObservableCollection<KeyValuePair<Thing, string>> lst)
        {
            double temp;
            Recalculate(lst, out temp);
            Wert = temp;

            foreach (var item in Zusammensetzung)
            {
                temp = 0;
                item.Key.PropertyChanged -= (u, c) => { Recalculate(lst, out temp); Wert = temp; };
                temp = 0;
                item.Key.PropertyChanged += (u, c) => { Recalculate(lst, out temp); Wert = temp; };
            }
        }

        private void Recalculate(ObservableCollection<KeyValuePair<Thing, string>> List, out double o_Value) {
            double temp = 0;
            foreach (KeyValuePair<Thing, string> item in List)
            {
                temp += item.Key.GetValue(item.Value);
            }
            o_Value = temp;
        }
    }
}
