
using Newtonsoft.Json;
using ShadowRunHelper.Model;
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class KomplexeForm : Thing
    {
        string _Option = "";
        [Used_UserAttribute]
        public string Option
        {
            get { return _Option; }
            set
            {
                if (value != this._Option)
                {
                    this._Option = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string _Target;
        [Used_UserAttribute]
        public string Target
        {
            get { return _Target; }
            set { if (_Target != value) { _Target = value; NotifyPropertyChanged(); } }
        }
        string _Duration;
        [Used_UserAttribute]
        public string Duration
        {
            get { return _Duration; }
            set { if (_Duration != value) { _Duration = value; NotifyPropertyChanged(); } }
        }
        string _Fading;
        [Used_UserAttribute]
        public string Fading
        {
            get { return _Fading; }
            set { if (_Fading != value) { _Fading = value; NotifyPropertyChanged(); } }
        }

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

        public static IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung
            };

        public KomplexeForm() : base()
        {
            LinkedThings.FilterOut = Filter;
            GrenzeZusammensetzung = new LinkList(this);
            GrenzeZusammensetzung.FilterOut = Filter;
            GegenZusammensetzung = new LinkList(this);
            GegenZusammensetzung.FilterOut = Filter;

            PropertyChanged += This_PropertyChanged;
            GrenzeZusammensetzung.OnCollectionChangedCall(UpdateGrenze);
            GegenZusammensetzung.OnCollectionChangedCall(UpdateGegen);
        }

        void This_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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

        void UpdateGrenze()
        {
            GrenzeCalced = Grenze + GrenzeZusammensetzung.Recalculate();
        }
        void UpdateGegen()
        {
            GegenCalced = Gegen + GegenZusammensetzung.Recalculate();
        }


    }
}
