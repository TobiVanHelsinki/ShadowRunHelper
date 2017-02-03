using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharModel
{
    public class Kommlink : Item
    {
        private double programmanzahl = 0;
        public double Programmanzahl
        {
            get { return programmanzahl; }
            set
            {
                if (value != this.programmanzahl)
                {
                    this.programmanzahl = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double firewall = 0;
        public double Firewall
        {
            get { return firewall; }
            set
            {
                if (value != this.firewall)
                {
                    this.firewall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double datenverarbeitung = 0;
        public double Datenverarbeitung
        {
            get { return datenverarbeitung; }
            set
            {
                if (value != this.datenverarbeitung)
                {
                    this.datenverarbeitung = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Kommlink()
        {
            this.ThingType = ThingDefs.Kommlink;

        }

        public Kommlink Copy(Kommlink target = null)
        {
            if (target == null)
            {
                target = new Kommlink();
            }
            base.Copy(target);
            target.Programmanzahl = Programmanzahl;
            target.Firewall = Firewall;
            target.Datenverarbeitung = Datenverarbeitung;
            return target;
        }

        public override void Reset()
        {
            base.Reset();
            Programmanzahl = 0;
            Firewall = 0;
            Datenverarbeitung = 0;
        }
        public override List<KeyValuePair<string, double>> GetValueList([CallerMemberName] string ID = "")
        {
            var res = ResourceLoader.GetForCurrentView();

            List<KeyValuePair<string, double>> lst = new List<KeyValuePair<string, double>>();
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Thing_Wert/Text"), Wert));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Kommlink_Firewall/Text"), Firewall));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Kommlink_Datenverarbeitung/Text"), Datenverarbeitung));
            return lst;
        }


        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Programmanzahl;
            strReturn += Delimiter;
            strReturn += Firewall;
            strReturn += Delimiter;
            strReturn += Datenverarbeitung;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            var res = ResourceLoader.GetForCurrentView();
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += res.GetString("Model_Kommlink_Programmanzahl/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Kommlink_Firewall/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Kommlink_Datenverarbeitung/Text");
            strReturn += Delimiter;
            return strReturn;
        }

        public override void FromCSV(Dictionary<string, string> dic)
        {
            var res = ResourceLoader.GetForCurrentView();
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == res.GetString("Model_Kommlink_Programmanzahl/Text"))
                {
                    this.Programmanzahl = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Kommlink_Firewall/Text"))
                {
                    this.Firewall = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Kommlink_Datenverarbeitung/Text"))
                {
                    this.Datenverarbeitung = double.Parse(item.Value);
                    continue;
                }
            }
        }
    }
}