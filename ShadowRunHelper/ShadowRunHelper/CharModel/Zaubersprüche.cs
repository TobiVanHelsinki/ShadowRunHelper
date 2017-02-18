using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharModel
{
    public class Zaubersprüche : Item
    {
        double reichweite = 0;
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

        public Zaubersprüche()
        {
            this.ThingType = ThingDefs.Zaubersprüche;
        }

        public override double GetValue([CallerMemberName] string ID = "")
        {
            return base.GetValue(ID);
        }
        public override Thing Copy(ref Thing target)
        {
            if (target == null)
            {
                target = new Item();
            }
            base.Copy(ref target);
            Zaubersprüche TargetS = (Zaubersprüche)target;
            TargetS.Reichweite = Reichweite;
            TargetS.Dauer = Dauer;
            TargetS.Entzug = Entzug;
            return target;
        }

        public override void Reset()
        {
            base.Reset();
            Reichweite = 0;
            Dauer = 0;
            Entzug = 0;
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
            var res = ResourceLoader.GetForCurrentView();
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += res.GetString("Model_Zaubersprüche_Reichweite/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Zaubersprüche_Dauer/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Zaubersprüche_Entzug/Text");
            strReturn += Delimiter;
            return strReturn;
        }


        public override void FromCSV(Dictionary<string, string> dic)
        {
            var res = ResourceLoader.GetForCurrentView();
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == res.GetString("Model_Zaubersprüche_Reichweite/Text"))
                {
                    this.Reichweite = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Zaubersprüche_Dauer/Text"))
                {
                    this.Dauer = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Zaubersprüche_Entzug/Text"))
                {
                    this.Entzug = double.Parse(item.Value);
                    continue;
                }
            }
        }

    }
}
