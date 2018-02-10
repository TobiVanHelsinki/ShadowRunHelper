using System.Collections.Generic;
using TLIB_UWPFRAME;

namespace ShadowRunHelper.CharModel
{
    public class Fernkampfwaffe : Waffe
    {
        private double rueckstoss = 0;
        [Used]
        public double Rueckstoss
        {
            get { return rueckstoss; }
            set
            {
                if (value != rueckstoss)
                {
                    rueckstoss = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string modi = "";
        [Used]
        public string Modi
        {
            get { return modi; }
            set
            {
                if (value != modi)
                {
                    modi = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Fernkampfwaffe()
        {

            ThingType = ThingDefs.Fernkampfwaffe;
        }

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Rueckstoss;
            strReturn += Delimiter;
            strReturn += Modi;
            strReturn += Delimiter;
            return strReturn;
        }


        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlatformHelper.GetString("Model_Fernkampfwaffe_Rueckstoss/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Fernkampfwaffe_Modi/Text");
            strReturn += Delimiter;
            return strReturn;
        }
        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlatformHelper.GetString("Model_Fernkampfwaffe_Rueckstoss/Text"))
                {
                    Rueckstoss = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Fernkampfwaffe_Modi/Text"))
                {
                    Modi= (item.Value);
                    continue;
                }
            }
        }
    }
}
