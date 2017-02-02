using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharModel
{
    public class CyberDeck : Kommlink
    {
        private double firewall_o =0;
        public double Firewall_o
        {
            get { return firewall_o; }
            set
            {
                if (value != this.firewall_o)
                {
                    this.firewall_o = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double datenverarbeitung_o=0;
        public double Datenverarbeitung_o
        {
            get { return datenverarbeitung_o; }
            set
            {
                if (value != this.datenverarbeitung_o)
                {
                    this.datenverarbeitung_o = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double angriff = 0;
        public double Angriff
        {
            get { return angriff; }
            set
            {
                if (value != this.angriff)
                {
                    this.angriff = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double angriff_o=0;
        public double Angriff_o
        {
            get { return angriff_o; }
            set
            {
                if (value != this.angriff_o)
                {
                    this.angriff_o = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double schleicher = 0;
        public double Schleicher
        {
            get { return schleicher; }
            set
            {
                if (value != this.schleicher)
                {
                    this.schleicher = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double schleicher_o=0;
        public double Schleicher_o
        {
            get { return schleicher_o; }
            set
            {
                if (value != this.schleicher_o)
                {
                    this.schleicher_o = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public CyberDeck()
        {
            this.ThingType = ThingDefs.CyberDeck;
        }


        public override double GetValue(string ID = "")
        {
            switch (ID)
            {
                case "Angriff":
                    return this.Angriff;
                case "Schleicher":
                    return this.Schleicher;
                case "Datenverarbeitung":
                    return this.Datenverarbeitung;
                case "Firewall":
                    return this.Firewall;
                default:
                    break;
            }
            return Wert;
        }

        public override List<KeyValuePair<string, double>> GetValueList([CallerMemberNameAttribute] string ID = "")
        {
            var res = ResourceLoader.GetForCurrentView();

            List<KeyValuePair<string, double>> lst = new List<KeyValuePair<string, double>>();
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Thing_Wert/Text"), Wert));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_CyberDeck_Angriff/Text"), Angriff));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_CyberDeck_Schleicher/Text"), Schleicher));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_CyberDeck_Firewall/Text"), Firewall));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_CyberDeck_Datenverarbeitung/Text"), Datenverarbeitung));
            return lst;
        }

        public CyberDeck Copy(CyberDeck target = null)
        {
            if (target == null)
            {
                target = new CyberDeck();
            }
            base.Copy(target);
            target.Angriff = Angriff;
            target.Angriff_o = Angriff_o;
            target.Schleicher = Schleicher;
            target.Schleicher_o = Schleicher_o;
            target.Firewall_o = Firewall_o;
            target.Datenverarbeitung_o = Datenverarbeitung_o;
            return target;
        }

        public override void Reset()
        {
            base.Reset();
            Angriff = 0;
            Angriff_o = 0;
            Schleicher = 0;
            Schleicher_o = 0;
            Firewall_o = 0;
            Datenverarbeitung_o = 0;
        }


        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Angriff;
            strReturn += Delimiter;
            strReturn += Angriff_o;
            strReturn += Delimiter;
            strReturn += Schleicher;
            strReturn += Delimiter;
            strReturn += Schleicher_o;
            strReturn += Delimiter;
            strReturn += Firewall_o;
            strReturn += Delimiter;
            strReturn += Datenverarbeitung_o;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            var res = ResourceLoader.GetForCurrentView();
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += res.GetString("Model_CyberDeck_Angriff/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_CyberDeck_Angriff_o/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_CyberDeck_Schleicher/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_CyberDeck_Schleicher_o/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_CyberDeck_Firewall_o/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_CyberDeck_Datenverarbeitung_o/Text");
            strReturn += Delimiter;
            return strReturn;
        }

        public override void FromCSV(Dictionary<string, string> dic)
        {
            var res = ResourceLoader.GetForCurrentView();
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == res.GetString("Model_CyberDeck_Angriff/Text"))
                {
                    this.Angriff = Int64.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_CyberDeck_Angriff_o/Text"))
                {
                    this.Angriff_o = Int64.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_CyberDeck_Schleicher/Text"))
                {
                    this.Schleicher = Int64.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_CyberDeck_Schleicher_o/Text"))
                {
                    this.Schleicher_o = Int64.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_CyberDeck_Firewall_o/Text"))
                {
                    this.Firewall_o = Int64.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_CyberDeck_Datenverarbeitung_o/Text"))
                {
                    this.Datenverarbeitung_o = Int64.Parse(item.Value);
                    continue;
                }
            }
        }
    }

}
