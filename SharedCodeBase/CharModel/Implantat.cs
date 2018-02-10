using System.Collections.Generic;
using TLIB_UWPFRAME;

namespace ShadowRunHelper.CharModel
{
    public class Implantat : Item
    {
        private double essenz = 0;
        [Used]
        public double Essenz
        {
            get { return essenz; }
            set
            {
                if (value != this.essenz)
                {
                    this.essenz = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double kapazitaet = 0;
        [Used]
        public double Kapazitaet
        {
            get { return kapazitaet; }
            set
            {
                if (value != this.kapazitaet)
                {
                    this.kapazitaet = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _Auswirkung = "";
        [Used]
        public string Auswirkung
        {
            get { return _Auswirkung; }
            set
            {
                if (value != this._Auswirkung)
                {
                    this._Auswirkung = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Implantat()
        {
            this.ThingType = ThingDefs.Implantat;
        }

    
        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Essenz;
            strReturn += Delimiter;
            strReturn += Kapazitaet;
            strReturn += Delimiter;
            strReturn += Auswirkung;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlatformHelper.GetString("Model_Implantat_Essenz/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Implantat_Kapazitaet/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Implantat_Auswirkung/Text");
            strReturn += Delimiter;
            return strReturn;
        }

        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlatformHelper.GetString("Model_Implantat_Essenz/Text"))
                {
                    Essenz = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Implantat_Kapazitaet/Text"))
                {
                    Kapazitaet = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Implantat_Auswirkung/Text"))
                {
                    Auswirkung = (item.Value);
                    continue;
                }
            }
        }
    }
}
