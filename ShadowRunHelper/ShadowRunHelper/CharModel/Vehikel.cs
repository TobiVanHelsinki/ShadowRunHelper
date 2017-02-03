using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources;

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

        public override double GetValue(string ID = "")
        {
            return GetValueList(ID).Find((x) => x.Key == ID).Value;
        }
        public override List<KeyValuePair<string, double>> GetValueList([CallerMemberName] string ID = "")
        {
            var res = ResourceLoader.GetForCurrentView();

            List<KeyValuePair<string, double>> lst = new List<KeyValuePair<string, double>>();
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Thing_Wert/Text"), Wert));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Vehikel_Sitze/Text"), Sitze));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Vehikel_Sensor/Text"), Sensor));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Vehikel_Rumpf/Text"), Rumpf));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Vehikel_Pilot/Text"), Pilot));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Vehikel_Panzerung/Text"), Panzerung));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Vehikel_Handling/Text"), Handling));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Vehikel_Gewicht/Text"), Gewicht));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Vehikel_Geschwindigkeit/Text"), Geschwindigkeit));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Vehikel_Beschleunigung/Text"), Beschleunigung));
            return lst;
        }
        public override Thing Copy(ref Thing target)
        {
            if (target == null)
            {
                target = new Vehikel();
            }
            base.Copy(ref target);
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

        public override void FromCSV(Dictionary<string, string> dic)
        {
            var res = ResourceLoader.GetForCurrentView();
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == res.GetString("Model_Vehikel_Beschleunigung/Text"))
                {
                    this.Beschleunigung = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Vehikel_Geschwindigkeit/Text"))
                {
                    this.Geschwindigkeit = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Vehikel_Gewicht/Text"))
                {
                    this.Gewicht = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Vehikel_Handling/Text"))
                {
                    this.Handling = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Vehikel_Panzerung/Text"))
                {
                    this.Panzerung = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Vehikel_Rumpf/Text"))
                {
                    this.Rumpf = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Vehikel_Pilot/Text"))
                {
                    this.Pilot = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Vehikel_Sensor/Text"))
                {
                    this.Sensor = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Vehikel_Sitze/Text"))
                {
                    this.Sitze = double.Parse(item.Value);
                    continue;
                }
            }
        }
    }
}