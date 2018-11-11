namespace ShadowRunHelper.CharModel
{
    public class CyberDeck : Kommlink
    {
        private double firewall_o =0;
        [Used_UserAttribute]
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
        [Used_UserAttribute]
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
        [Used_UserAttribute]
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
        [Used_UserAttribute]
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
        [Used_UserAttribute]
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
    }

}
