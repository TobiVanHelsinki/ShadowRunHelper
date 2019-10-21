///Author: Tobi van Helsinki

using Newtonsoft.Json;
using ShadowRunHelper.Model;
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Handlung : Thing
    {
        [Used_List]
        public LinkList GrenzeZusammensetzung { get; set; }

        [Used_List]
        public LinkList GegenZusammensetzung { get; set; }

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

        double grenzeCalced = 0;
        [JsonIgnore]
        [Used_UserAttribute]
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

        double gegenCalced = 0;
        [JsonIgnore]
        [Used_UserAttribute]
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
            _Limit = new CharCalcProperty(nameof(Limit), this);
            _Against = new CharCalcProperty(nameof(Against), this);
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

        private void UpdateGrenze()
        {
            GrenzeCalced = Grenze + GrenzeZusammensetzung.Recalculate();
        }

        private void UpdateGegen()
        {
            GegenCalced = Gegen + GegenZusammensetzung.Recalculate();
        }
    }
}