///Author: Tobi van Helsinki

using Newtonsoft.Json;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Handlung : Thing
    {
        [Obsolete(Constants.ObsoleteCalcProperty)]
        public LinkList GrenzeZusammensetzung { get; set; }

        [Obsolete(Constants.ObsoleteCalcProperty)]
        public LinkList GegenZusammensetzung { get; set; }

        [Obsolete(Constants.ObsoleteCalcProperty)]
        double grenze = 0;
        [Obsolete(Constants.ObsoleteCalcProperty)]
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

        [Obsolete(Constants.ObsoleteCalcProperty)]
        double grenzeCalced = 0;
        [JsonIgnore]
        [Obsolete(Constants.ObsoleteCalcProperty)]
        public double GrenzeCalced
        {
            get { return grenzeCalced; }
            set
            {
                if (value != grenzeCalced)
                {
                    grenzeCalced = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Obsolete(Constants.ObsoleteCalcProperty)]
        double gegen = 0;
        [Obsolete(Constants.ObsoleteCalcProperty)]
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

        [Obsolete(Constants.ObsoleteCalcProperty)]
        double gegenCalced = 0;
        [JsonIgnore]
        [Obsolete(Constants.ObsoleteCalcProperty)]
        public double GegenCalced
        {
            get { return gegenCalced; }
            set
            {
                if (value != gegenCalced)
                {
                    gegenCalced = value;
                    NotifyPropertyChanged();
                }
            }
        }

        CharCalcProperty _Against;
        [Used_UserAttribute]
        public CharCalcProperty Against
        {
            get { return _Against; }
            set { if (_Against != value) { _Against = value; NotifyPropertyChanged(); } }
        }

        CharCalcProperty _Limit;
        [Used_UserAttribute]
        public CharCalcProperty Limit
        {
            get { return _Limit; }
            set { if (_Limit != value) { _Limit = value; NotifyPropertyChanged(); } }
        }

        public static IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Connection
            };

        public Handlung() : base()
        {
            LinkedThings.FilterOut = Filter;
            GrenzeZusammensetzung = new LinkList(this)
            {
                FilterOut = Filter
            };
            GegenZusammensetzung = new LinkList(this)
            {
                FilterOut = Filter
            };

            PropertyChanged += This_PropertyChanged;
            GrenzeZusammensetzung.OnCollectionChangedCall(UpdateGrenze);
            GegenZusammensetzung.OnCollectionChangedCall(UpdateGegen);
        }

        [Obsolete(Constants.ObsoleteCalcProperty)]
        private void This_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Grenze))
            {
                UpdateGrenze();
            }
            if (e.PropertyName == nameof(Gegen))
            {
                UpdateGegen();
            }
        }

        [Obsolete(Constants.ObsoleteCalcProperty)]
        private void UpdateGrenze()
        {
            GrenzeCalced = Grenze + GrenzeZusammensetzung.Recalculate();
        }

        [Obsolete(Constants.ObsoleteCalcProperty)]
        private void UpdateGegen()
        {
            GegenCalced = Gegen + GegenZusammensetzung.Recalculate();
        }
    }
}