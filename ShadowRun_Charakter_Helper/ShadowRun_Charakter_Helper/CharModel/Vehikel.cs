namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Vehikel : CharModel.Item
    {
        public double handling;
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
        public double geschwindigkeit;
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
        public double beschleunigung;
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
        public double rumpf;
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
        public double panzerung;
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
        public double pilot;
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
        public double sensor;
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
        public double sitze;
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
        public double gewicht;
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
        public double schaden;
        public double Schaden
        {
            get { return schaden; }
            set
            {
                if (value != this.schaden)
                {
                    this.schaden = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Vehikel()
        {

        }
    }
}