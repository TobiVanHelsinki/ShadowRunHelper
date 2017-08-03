using System.Collections.Generic;
using TLIB;

namespace ShadowRunHelper.CharModel
{
    public class Tradition_Initiation : Thing
    {
        //Bezeichner ist Tradition
        //initiationsgrad ist Wert
        string _Schutzpatron = "";
        public string Schutzpatron
        {
            get { return _Schutzpatron; }
            set
            {
                if (value != this._Schutzpatron)
                {
                    this._Schutzpatron = value;
                    NotifyPropertyChanged();
                }
            }
        }
   
        string _Metamagie = "";
        public string Metamagie
        {
            get { return _Metamagie; }
            set
            {
                if (value != this._Metamagie)
                {
                    this._Metamagie = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Tradition_Initiation()
        {
            this.ThingType = ThingDefs.Tradition_Initiation;
        }

        public override Thing Copy( Thing target = null)
        {
            if (target == null)
            {
                target = new Tradition_Initiation();
            }
            base.Copy( target);
            Tradition_Initiation TargetS = (Tradition_Initiation)target;
            TargetS.Schutzpatron = Schutzpatron;
            TargetS.Metamagie = Metamagie;
            return target;
        }

        public override void Reset()
        {
            base.Reset();
            Schutzpatron = "";
            Metamagie = "";
        }


        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Schutzpatron;
            strReturn += Delimiter;
            strReturn += Metamagie;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlatformHelper.GetString("Model_Tradition_Initiation_Schutzpatron/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Tradition_Initiation_Metamagie/Text");
            strReturn += Delimiter;
            return strReturn;
        }


        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlatformHelper.GetString("Model_Tradition_Initiation_Schutzpatron/Text"))
                {
                    this.Schutzpatron = (item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Tradition_Initiation_Metamagie/Text"))
                {
                    this.Metamagie = (item.Value);
                    continue;
                }
            }
        }
    }
}
