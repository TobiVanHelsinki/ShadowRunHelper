using System;
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Handlung : CharModel.Thing
    {
        private Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry> zusammensetzung = new Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry>();
        public Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry> Zusammensetzung
        {
            get { return zusammensetzung;
            }
            set
            {
                if (value != this.zusammensetzung)
                {
                        this.zusammensetzung = value;
                        NotifyPropertyChanged();
                }
            }
        }

        private Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry> grenzeZusammensetzung = new Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry>();
        public Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry> GrenzeZusammensetzung
        {
            get
            {
                return grenzeZusammensetzung;
            }
            set
            {
                if (value != this.grenzeZusammensetzung)
                {
                    this.grenzeZusammensetzung = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry> gegenZusammensetzung = new Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry>();
        public Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry> GegenZusammensetzung
        {
            get
            {
                return gegenZusammensetzung;
            }
            set
            {
                if (value != this.gegenZusammensetzung)
                {
                    this.gegenZusammensetzung = value;
                    NotifyPropertyChanged();
                }
            }
        }


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

        public Handlung(int dicCD_ID)
        {

        }

        public Handlung()
        {
            Zusammensetzung = new Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry>();
            GrenzeZusammensetzung = new Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry>();
            GegenZusammensetzung = new Dictionary<int, ShadowRunHelper.Model.DictionaryCharEntry>();
        }
    }
}
