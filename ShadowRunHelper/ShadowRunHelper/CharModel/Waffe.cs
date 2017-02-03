using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharModel
{
    public abstract class Waffe : Item
    {
        /// <summary>
        /// Präzision
        /// </summary>
        private double pool = 0;
        public double Pool
        {
            get { return pool; }
            set
            {
                if (value != pool)
                {
                    pool = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string schadenTyp = "";
        public string SchadenTyp
        {
            get { return schadenTyp; }
            set
            {
                if (value != schadenTyp)
                {
                    schadenTyp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// PB could be english, means DK "Durschlagskompensation"
        /// </summary>
        private double pB = 0; // DK
        public double PB
        {
            get { return pB; }
            set
            {
                if (value != pB)
                {
                    pB = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Pool;
            strReturn += Delimiter;
            strReturn += SchadenTyp;
            strReturn += Delimiter;
            strReturn += PB;
            strReturn += Delimiter;
            return strReturn;
        }


        public override string HeaderToCSV(string Delimiter)
        {
            var res = ResourceLoader.GetForCurrentView();
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += res.GetString("Model_Waffe_Pool/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Waffe_SchadenTyp/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Waffe_PB/Text");
            strReturn += Delimiter;
            return strReturn;
        }
   
        public override void FromCSV(Dictionary<string, string> dic)
        {
            var res = ResourceLoader.GetForCurrentView();
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == res.GetString("Model_Waffe_Pool/Text"))
                {
                    Pool = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Waffe_SchadenTyp/Text"))
                {
                    SchadenTyp = item.Value;
                    continue;
                }
                if (item.Key == res.GetString("Model_Waffe_PB/Text"))
                {
                    PB = double.Parse(item.Value);
                    continue;
                }
            }
        }
    }
}
