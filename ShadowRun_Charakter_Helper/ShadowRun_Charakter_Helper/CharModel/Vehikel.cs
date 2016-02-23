namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Vehikel : CharModel.Item
    {
        private double handling;
        public double Handling
        {
            get { return handling; }
            set
            {
                if (value != this.handling)
                {
                    this.handling = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double geschwindigkeit;
        public double Geschwindigkeit
        {
            get { return geschwindigkeit; }
            set
            {
                if (value != this.geschwindigkeit)
                {
                    this.geschwindigkeit = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double beschleunigung;
        public double Beschleunigung
        {
            get { return beschleunigung; }
            set
            {
                if (value != this.beschleunigung)
                {
                    this.beschleunigung = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double rumpf;
        public double Rumpf
        {
            get { return rumpf; }
            set
            {
                if (value != this.rumpf)
                {
                    this.rumpf = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double panzerung;
        public double Panzerung
        {
            get { return panzerung; }
            set
            {
                if (value != this.panzerung)
                {
                    this.panzerung = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double pilot;
        public double Pilot
        {
            get { return pilot; }
            set
            {
                if (value != this.pilot)
                {
                    this.pilot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double sensor;
        public double Sensor
        {
            get { return sensor; }
            set
            {
                if (value != this.sensor)
                {
                    this.sensor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double sitze;
        public double Sitze
        {
            get { return sitze; }
            set
            {
                if (value != this.sitze)
                {
                    this.sitze = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double gewicht;
        public double Gewicht
        {
            get { return gewicht; }
            set
            {
                if (value != this.gewicht)
                {
                    this.gewicht = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Vehikel()
        {

        }
    }
}