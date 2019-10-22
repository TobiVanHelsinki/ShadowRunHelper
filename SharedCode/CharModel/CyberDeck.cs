///Author: Tobi van Helsinki

namespace ShadowRunHelper.CharModel
{
    public class CyberDeck : Kommlink
    {
        private double firewall_o;
        [Used_UserAttribute]
        public double Firewall_o
        {
            get => firewall_o;
            set
            {
                if (value != firewall_o)
                {
                    firewall_o = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double datenverarbeitung_o;
        [Used_UserAttribute]
        public double Datenverarbeitung_o
        {
            get => datenverarbeitung_o;
            set
            {
                if (value != datenverarbeitung_o)
                {
                    datenverarbeitung_o = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private CharCalcProperty angriff;
        [Used_UserAttribute]
        public CharCalcProperty Angriff
        {
            get => angriff;
            set
            {
                if (value != angriff)
                {
                    angriff = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double angriff_o;
        public double Angriff_o
        {
            get => angriff_o;
            set
            {
                if (value != angriff_o)
                {
                    angriff_o = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private CharCalcProperty schleicher;
        [Used_UserAttribute]
        public CharCalcProperty Schleicher
        {
            get => schleicher;
            set
            {
                if (value != schleicher)
                {
                    schleicher = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double schleicher_o;
        [Used_UserAttribute]
        public double Schleicher_o
        {
            get => schleicher_o;
            set
            {
                if (value != schleicher_o)
                {
                    schleicher_o = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}