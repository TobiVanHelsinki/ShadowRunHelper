using System;
using System.Collections.Generic;
using TLIB_UWPFRAME;

namespace ShadowRunHelper.CharModel
{
    public abstract class Waffe : Item
    {
        /// <summary>
        /// Praezision
        /// </summary>
        private double pool = 0;
        [Used]
        public double Praezision
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
        [Used]
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
        [Used]
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
            strReturn += Praezision;
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
            strReturn += CrossPlatformHelper.GetString("Model_Waffe_Praezision/Text");
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
                if (item.Key == CrossPlatformHelper.GetString("Model_Waffe_Praezision/Text"))
                {
                    Praezision = double.Parse(item.Value);
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
