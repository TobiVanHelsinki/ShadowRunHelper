using System.Collections.Generic;
using TLIB_UWPFRAME;

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
                if (value != firewall_o)
                {
                    firewall_o = value;
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
                if (value != datenverarbeitung_o)
                {
                    datenverarbeitung_o = value;
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
                if (value != angriff)
                {
                    angriff = value;
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
                if (value != angriff_o)
                {
                    angriff_o = value;
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
                if (value != schleicher)
                {
                    schleicher = value;
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
                if (value != schleicher_o)
                {
                    schleicher_o = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public CyberDeck()
        {
            ThingType = ThingDefs.CyberDeck;
        }


        public override Thing Copy(Thing target = null)
        {
            if (target == null)
            {
                target = new CyberDeck();
            }
            base.Copy(target);
            CyberDeck target2 = target as CyberDeck;
            target2.Angriff = Angriff;
            target2.Angriff_o = Angriff_o;
            target2.Schleicher = Schleicher;
            target2.Schleicher_o = Schleicher_o;
            target2.Firewall_o = Firewall_o;
            target2.Datenverarbeitung_o = Datenverarbeitung_o;
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
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlatformHelper.GetString("Model_CyberDeck_Angriff/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_CyberDeck_Angriff_o/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_CyberDeck_Schleicher/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_CyberDeck_Schleicher_o/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_CyberDeck_Firewall_o/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_CyberDeck_Datenverarbeitung_o/Text");
            strReturn += Delimiter;
            return strReturn;
        }

        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlatformHelper.GetString("Model_CyberDeck_Angriff/Text"))
                {
                    Angriff = int.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_CyberDeck_Angriff_o/Text"))
                {
                    Angriff_o = int.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_CyberDeck_Schleicher/Text"))
                {
                    Schleicher = int.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_CyberDeck_Schleicher_o/Text"))
                {
                    Schleicher_o = int.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_CyberDeck_Firewall_o/Text"))
                {
                    Firewall_o = int.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_CyberDeck_Datenverarbeitung_o/Text"))
                {
                    Datenverarbeitung_o = int.Parse(item.Value);
                    continue;
                }
            }
        }
    }

}
