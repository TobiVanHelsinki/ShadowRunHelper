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

        ConnectProperty _Against;
        [Used_UserAttribute]
        public ConnectProperty Against
        {
            get { return _Against; }
            set { if (_Against != value) { _Against = value; NotifyPropertyChanged(); } }
        }

        ConnectProperty _Limit;
        [Used_UserAttribute]
        public ConnectProperty Limit
        {
            get { return _Limit; }
            set { if (_Limit != value) { _Limit = value; NotifyPropertyChanged(); } }
        }

        public override IEnumerable<ThingDefs> Filter => StaticFilter;

        private static readonly IEnumerable<ThingDefs> StaticFilter = new[]
            {
                ThingDefs.Handlung, ThingDefs.Connection
            };

        public Handlung() : base()
        {
            //LinkedThings.FilterOut = Filter;
            GrenzeZusammensetzung = new LinkList(this)
            {
                FilterOut = Filter
            };
            GegenZusammensetzung = new LinkList(this)
            {
                FilterOut = Filter
            };
        }
    }
}