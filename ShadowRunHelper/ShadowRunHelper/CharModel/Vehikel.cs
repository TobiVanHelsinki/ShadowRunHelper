﻿using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharModel
{
    public class Vehikel : Item
    {
        private double handling = 0;
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
        private double geschwindigkeit = 0;
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
        private double beschleunigung = 0;
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
        private double rumpf = 0;
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
        private double panzerung = 0;
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
        private double pilot = 0;
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
        private double sensor = 0;
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
        private double sitze = 0;
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
        private double gewicht = 0;
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
            this.ThingType = ThingDefs.Vehikel;

        }

        public Vehikel Copy(ref Vehikel target)
        {
            if (target == null)
            {
                target = new Vehikel();
            }
            base.Copy((Thing)target);
            target.Beschleunigung = Beschleunigung;
            target.Geschwindigkeit = Geschwindigkeit;
            target.Gewicht = Gewicht;
            target.Handling = Handling;
            target.Panzerung = Panzerung;
            target.Rumpf = Rumpf;
            target.Pilot = Pilot;
            target.Sensor = Sensor;
            target.Sitze = Sitze;
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
            var res = ResourceLoader.GetForCurrentView();
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += res.GetString("Model_Vehikel_Beschleunigung/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Vehikel_Geschwindigkeit/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Vehikel_Gewicht/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Vehikel_Handling/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Vehikel_Panzerung/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Vehikel_Rumpf/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Vehikel_Pilot/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Vehikel_Sensor/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Vehikel_Sitze/Text");
            strReturn += Delimiter;
            return strReturn;
        }
    }
}