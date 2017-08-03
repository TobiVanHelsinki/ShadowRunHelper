using System.Collections.Generic;
using TLIB;

namespace ShadowRunHelper.CharModel
{
    public class Strömung_Wandlung : Thing
    {
        // Bezeichner ist Strömung
        // Wert ist Wandlungsgrad
       
        string _Paragon = "";
        public string Paragon
        {
            get { return _Paragon; }
            set
            {
                if (value != this._Paragon)
                {
                    this._Paragon = value;
                    NotifyPropertyChanged();
                }
            }
        }
       
        string _Echos = "";
        public string Echos
        {
            get { return _Echos; }
            set
            {
                if (value != this._Echos)
                {
                    this._Echos = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Strömung_Wandlung()
        {
            this.ThingType = ThingDefs.Strömung_Wandlung;
        }

        public override Thing Copy( Thing target = null)
        {
            if (target == null)
            {
                target = new Strömung_Wandlung();
            }
            base.Copy( target);
            Strömung_Wandlung TargetS = (Strömung_Wandlung)target;
            TargetS.Paragon = Paragon;
            TargetS.Echos = Echos;
            return target;
        }

        public override void Reset()
        {
            base.Reset();
            Paragon = "";
            Echos = "";
        }

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Paragon;
            strReturn += Delimiter;
            strReturn += Echos;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlatformHelper.GetString("Model_Strömung_Wandlung_Paragon/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Strömung_Wandlung_Echos/Text");
            strReturn += Delimiter;
            return strReturn;
        }

        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlatformHelper.GetString("Model_Strömung_Wandlung_Paragon/Text"))
                {
                    this.Paragon = (item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Strömung_Wandlung_Echos/Text"))
                {
                    this.Echos = (item.Value);
                    continue;
                }
            }
        }
    }
}
