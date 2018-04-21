﻿using Newtonsoft.Json;
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

        public static IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Connection
            };

        public Handlung() : base()
        {
            LinkedThings.FilterOut = Filter;
            GrenzeZusammensetzung = new LinkList(this);
            GrenzeZusammensetzung.FilterOut = Filter;
            GegenZusammensetzung = new LinkList(this);
            GegenZusammensetzung.FilterOut = Filter;

            GrenzeZusammensetzung.OnCollectionChangedCall(() => { GrenzeCalced = Grenze + GrenzeZusammensetzung.Recalculate(); });
            GegenZusammensetzung.OnCollectionChangedCall(()=> { GegenCalced = Gegen + GegenZusammensetzung.Recalculate(); });
        }
    }
}
