using System.Collections.Generic;
using TLIB_UWPFRAME;

namespace ShadowRunHelper.CharModel
{
    public class Vehikel : Item
    {
        double handling = 0;
        [Used]
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
        [Used]
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
        [Used]
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
        [Used]
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
        [Used]
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
        [Used]
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
        [Used]
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
        [Used] public double Sitze
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
        [Used] public double Gewicht
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

        public Vehikel()
        {
            ThingType = ThingDefs.Vehikel;
        }

        public override Thing Copy( Thing target = null)
        {
            if (target == null)
            {
                target = new Vehikel();
            }
            base.Copy( target);
            Vehikel targetS = (Vehikel)target;
            targetS.Beschleunigung = Beschleunigung;
            targetS.Geschwindigkeit = Geschwindigkeit;
            targetS.Gewicht = Gewicht;
            targetS.Handling = Handling;
            targetS.Panzerung = Panzerung;
            targetS.Rumpf = Rumpf;
            targetS.Pilot = Pilot;
            targetS.Sensor = Sensor;
            targetS.Sitze = Sitze;
            return target;
        }

        public override void Reset()
        {
            base.Reset();
            Beschleunigung = 0;
            Geschwindigkeit = 0;
            Gewicht = 0;
            Handling = 0;
            Panzerung = 0;
            Rumpf = 0;
            Pilot = 0;
            Sensor = 0;
            Sitze = 0;
        }

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Beschleunigung;
            strReturn += Delimiter;
            strReturn += Geschwindigkeit;
            strReturn += Delimiter;
            strReturn += Gewicht;
            strReturn += Delimiter;
            strReturn += Handling;
            strReturn += Delimiter;
            strReturn += Panzerung;
            strReturn += Delimiter;
            strReturn += Rumpf;
            strReturn += Delimiter;
            strReturn += Pilot;
            strReturn += Delimiter;
            strReturn += Sensor;
            strReturn += Delimiter;
            strReturn += Sitze;
            strReturn += Delimiter;
            return strReturn;
        }


        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlatformHelper.GetString("Model_Vehikel_Beschleunigung/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Vehikel_Geschwindigkeit/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Vehikel_Gewicht/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Vehikel_Handling/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Vehikel_Panzerung/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Vehikel_Rumpf/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Vehikel_Pilot/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Vehikel_Sensor/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Vehikel_Sitze/Text");
            strReturn += Delimiter;
            return strReturn;
        }

        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlatformHelper.GetString("Model_Vehikel_Beschleunigung/Text"))
                {
                    Beschleunigung = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Vehikel_Geschwindigkeit/Text"))
                {
                    Geschwindigkeit = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Vehikel_Gewicht/Text"))
                {
                    Gewicht = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Vehikel_Handling/Text"))
                {
                    Handling = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Vehikel_Panzerung/Text"))
                {
                    Panzerung = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Vehikel_Rumpf/Text"))
                {
                    Rumpf = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Vehikel_Pilot/Text"))
                {
                    Pilot = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Vehikel_Sensor/Text"))
                {
                    Sensor = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Vehikel_Sitze/Text"))
                {
                    Sitze = double.Parse(item.Value);
                    continue;
                }
            }
        }
    }
}