using System;
using System.Collections.Generic;
using TLIB;

namespace ShadowRunHelper.CharModel
{
    public abstract class Waffe : Item
    {
        /// <summary>
        /// Präzision
        /// </summary>
        private double pool = 0;
        public double Präzision
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
            strReturn += Präzision;
            strReturn += Delimiter;
            strReturn += SchadenTyp;
            strReturn += Delimiter;
            strReturn += PB;
            strReturn += Delimiter;
            return strReturn;
        }


        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlatformHelper.GetString("Model_Waffe_Präzision/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Waffe_SchadenTyp/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Waffe_PB/Text");
            strReturn += Delimiter;
            return strReturn;
        }
   
        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlatformHelper.GetString("Model_Waffe_Präzision/Text"))
                {
                    Präzision = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Waffe_SchadenTyp/Text"))
                {
                    SchadenTyp = item.Value;
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Waffe_PB/Text"))
                {
                    PB = double.Parse(item.Value);
                    continue;
                }
            }
        }
    }
}
