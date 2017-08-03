﻿using System.Collections.Generic;
using TLIB;

namespace ShadowRunHelper.CharModel
{
    public class Panzerung : Item
    {
        /// <summary>
        /// für SR4 - ballisitisch wäre dann thing.wert
        /// </summary>
        private double stoß = 0;
        public double Stoß
        {
            get { return stoß; }
            set
            {
                if (value != this.stoß)
                {
                    this.stoß = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double kapazität = 0;
        public double Kapazität
        {
            get { return kapazität; }
            set
            {
                if (value != this.kapazität)
                {
                    this.kapazität = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Panzerung()
        {
            ThingType = ThingDefs.Panzerung;
        }

        public override Thing Copy(Thing target = null)
        {
            if (target == null)
            {
                target = new Panzerung();
            }
            return base.Copy(target);
        }

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Stoß;
            strReturn += Delimiter;
            strReturn += Kapazität;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlatformHelper.GetString("Model_Panzerung_Stoß/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Panzerung_Kapazität/Text");
            strReturn += Delimiter;
            return strReturn;
        }
        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlatformHelper.GetString("Model_Panzerung_Stoß/Text"))
                {
                    Stoß = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Panzerung_Kapazität/Text"))
                {
                    Kapazität = double.Parse(item.Value);
                    continue;
                }
            }
        }
    }
}
