
namespace ShadowRunHelper.CharModel
{
    public class Vehikel : Item
    {
        double handling = 0;
        [Used_UserAttribute]
        public double Handling
        {
            get { return handling; }
            set
            {
                if (value != handling)
                {
                    handling = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double geschwindigkeit = 0;
        [Used_UserAttribute]
        public double Geschwindigkeit
        {
            get { return geschwindigkeit; }
            set
            {
                if (value != geschwindigkeit)
                {
                    geschwindigkeit = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double beschleunigung = 0;
        [Used_UserAttribute]
        public double Beschleunigung
        {
            get { return beschleunigung; }
            set
            {
                if (value != beschleunigung)
                {
                    beschleunigung = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double rumpf = 0;
        [Used_UserAttribute]
        public double Rumpf
        {
            get { return rumpf; }
            set
            {
                if (value != rumpf)
                {
                    rumpf = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double panzerung = 0;
        [Used_UserAttribute]
        public double Panzerung
        {
            get { return panzerung; }
            set
            {
                if (value != panzerung)
                {
                    panzerung = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double pilot = 0;
        [Used_UserAttribute]
        public double Pilot
        {
            get { return pilot; }
            set
            {
                if (value != pilot)
                {
                    pilot = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double sensor = 0;
        [Used_UserAttribute]
        public double Sensor
        {
            get { return sensor; }
            set
            {
                if (value != sensor)
                {
                    sensor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double sitze = 0;
        [Used_UserAttribute] public double Sitze
        {
            get { return sitze; }
            set
            {
                if (value != sitze)
                {
                    sitze = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double gewicht = 0;
        [Used_UserAttribute] public double Gewicht
        {
            get { return gewicht; }
            set
            {
                if (value != gewicht)
                {
                    gewicht = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}