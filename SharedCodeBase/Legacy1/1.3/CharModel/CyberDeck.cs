namespace ShadowRunHelper1_3.CharModel
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

        }
    }

}
