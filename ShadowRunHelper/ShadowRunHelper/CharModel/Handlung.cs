using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static ShadowRunHelper.Ressourcen.TypNamen;

namespace ShadowRunHelper.CharModel
{
    public class Handlung : CharModel.Thing, INotifyPropertyChanged
    {
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

        public ObservableCollection<CharModel.Thing> Zusammensetzung;
        public ObservableCollection<CharModel.Thing> GrenzeZusammensetzung;
        public ObservableCollection<CharModel.Thing> GegenZusammensetzung;

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

            Zusammensetzung = new ObservableCollection<CharModel.Thing>();
            GrenzeZusammensetzung = new ObservableCollection<CharModel.Thing>();
            GegenZusammensetzung = new ObservableCollection<CharModel.Thing>();
            GrenzeZusammensetzung.CollectionChanged += (u, c) => { double temp; Recalculate(GrenzeZusammensetzung, out temp); Wert = temp; };
            GegenZusammensetzung.CollectionChanged += (u, c) => { double temp; Recalculate(GegenZusammensetzung, out temp); Wert = temp; };
            Zusammensetzung.CollectionChanged += (u, c) => { double temp; Recalculate(Zusammensetzung, out temp); Wert = temp; };
        }

        private void Recalculate(ObservableCollection<Thing> List, out double o_Value) {
            double temp = 0;
            foreach (Thing item in List)
            {
                temp += item.GetValue();
            }
            o_Value = temp;
        }
    }
}
