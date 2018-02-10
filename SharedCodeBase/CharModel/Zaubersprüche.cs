﻿using System.Collections.Generic;
using TLIB_UWPFRAME;

namespace ShadowRunHelper.CharModel
{
    public class Zaubersprueche : Item
    {
        double reichweite = 0;
        [Used]
        public double Reichweite
        {
            get { return reichweite; }
            set
            {
                if (value != this.reichweite)
                {
                    this.reichweite = value;
                    NotifyPropertyChanged();
                }
            }
        }

        double _Dauer = 0;
        [Used]
        public double Dauer
        {
            get { return _Dauer; }
            set
            {
                if (value != this._Dauer)
                {
                    this._Dauer = value;
                    NotifyPropertyChanged();
                }
            }
        }

        double _Entzug = 0;
        [Used]
        public double Entzug
        {
            get { return _Entzug; }
            set
            {
                if (value != this._Entzug)
                {
                    this._Entzug = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Zaubersprueche()
        {
            this.ThingType = ThingDefs.Zaubersprueche;
        }


        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Reichweite;
            strReturn += Delimiter;
            strReturn += Dauer;
            strReturn += Delimiter;
            strReturn += Entzug;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlatformHelper.GetString("Model_Zaubersprueche_Reichweite/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Zaubersprueche_Dauer/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Zaubersprueche_Entzug/Text");
            strReturn += Delimiter;
            return strReturn;
        }


        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlatformHelper.GetString("Model_Zaubersprueche_Reichweite/Text"))
                {
                    this.Reichweite = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Zaubersprueche_Dauer/Text"))
                {
                    this.Dauer = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Zaubersprueche_Entzug/Text"))
                {
                    this.Entzug = double.Parse(item.Value);
                    continue;
                }
            }
        }

    }
}
