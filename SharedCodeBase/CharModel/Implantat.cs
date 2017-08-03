using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLIB;

namespace ShadowRunHelper.CharModel
{
    public class Implantat : Item
    {
        private double essenz = 0;
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
        private string _Auswirkung = "";
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

        public override Thing Copy( Thing target)
        {
            if (target == null)
            {
                target = new Implantat();
            }
            base.Copy( target);
            ((Implantat)target).Essenz = Essenz;
            ((Implantat)target).Kapazität = Kapazität;
            ((Implantat)target).Auswirkung = Auswirkung;
            return target;
        }

        public override void Reset()
        {
            base.Reset();
            Kapazität = 0;
            Essenz = 0;
        }

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Essenz;
            strReturn += Delimiter;
            strReturn += Kapazität;
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
            strReturn += CrossPlatformHelper.GetString("Model_Implantat_Kapazität/Text");
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
                if (item.Key == CrossPlatformHelper.GetString("Model_Implantat_Kapazität/Text"))
                {
                    Kapazität = double.Parse(item.Value);
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
