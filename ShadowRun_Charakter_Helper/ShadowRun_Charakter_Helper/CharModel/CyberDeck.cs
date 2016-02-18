using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.CharModel
{
    public class CyberDeck : CharModel.Kommlink
    {


        public double firewall_o;
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
        public double datenverarbeitung_o;
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
        public double angriff;
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
        public double angriff_o;
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
        public double schleicher;
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
        public double schleicher_o;
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

        }
    }

}
