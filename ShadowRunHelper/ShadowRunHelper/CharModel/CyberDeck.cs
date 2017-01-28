using System;

namespace ShadowRunHelper.CharModel
{
    public class CyberDeck : CharModel.Kommlink
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
            this.ThingType = Ressourcen.TypNamen.ThingDefs.CyberDeck;
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

        public void Reset()
        {
            base.Reset();
            Angriff = 0;
            Angriff_o = 0;
            Schleicher = 0;
            Schleicher_o = 0;
            Firewall_o = 0;
            Datenverarbeitung_o = 0;
        }

    }

}
