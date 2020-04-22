
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

        private ConnectProperty angriff;
        [Used_UserAttribute]
        public ConnectProperty Angriff
        {
            get => angriff;
            set
            {
                if (value != angriff)
                {
                    RefreshInnerPropertyChangedListener(ref angriff, value, this);
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

        private ConnectProperty schleicher;
        [Used_UserAttribute]
        public ConnectProperty Schleicher
        {
            get => schleicher;
            set
            {
                if (value != schleicher)
                {
                    RefreshInnerPropertyChangedListener(ref schleicher, value, this);
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