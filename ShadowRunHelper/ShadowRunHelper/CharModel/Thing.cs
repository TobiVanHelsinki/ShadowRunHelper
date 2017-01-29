using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper.CharModel
{
    public class Thing : INotifyPropertyChanged
    {
        private ThingDefs thingType = 0;
        public ThingDefs ThingType
        {
            get { return thingType; }
            protected set
            {
                if (value != this.thingType)
                {
                    this.thingType = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int ordnung = 0;
        public int Ordnung
        {
            get { return ordnung; }
            set
            {
                if (value != this.ordnung)
                {
                    this.ordnung = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string bezeichner ="";
        public string Bezeichner
        {
            get { return bezeichner; }
            set
            {
                if (value != this.bezeichner)
                {
                    this.bezeichner = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string typ = "";
        public string Typ
        {
            get { return typ; }
            set
            {
                if (value != this.typ)
                {
                    this.typ = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double wert = 0;
        public double Wert
        {
            get { return wert; }
            set
            {
                if (value != this.wert)
                {
                    this.wert = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private string zusatz = "";
        public string Zusatz
        {
            get { return zusatz; }
            set
            {
                if (value != this.zusatz)
                {
                    this.zusatz = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string notiz = "";
        public string Notiz
        {
            get { return notiz; }
            set
            {
                if (value != this.notiz)
                {
                    this.notiz = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public List<KeyValuePair<string, double>> Value
        {
            get { return this.GetValueList(); }
            private set {            }
        }

        public Thing()
        {

        }

        public virtual double GetValue([CallerMemberNameAttribute] string ID = "")
        {
            return Wert;
        }
        public virtual List<KeyValuePair<string, double>> GetValueList([CallerMemberNameAttribute] string ID = "")
        {
            List<KeyValuePair<string, double>> lst = new List<KeyValuePair<string, double>>();
            lst.Add(new KeyValuePair<string, double>(Bezeichner,Wert));
            return lst;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Thing Copy(Thing target)
        {
            if (target == null)
            {
                target = new Item();
            }
            target.Bezeichner = Bezeichner;
            target.Notiz = Notiz;
            target.Ordnung = Ordnung;
            target.ThingType= ThingType;
            target.Typ= Typ;
            target.Wert = Wert;
            target.Zusatz = Zusatz;
            return target;
        }
        public void Reset()
        {
            Bezeichner = "";
            Notiz = "";
            Ordnung = 0;
            //ThingType = 0;
            Typ = "";
            Wert = 0;
            Zusatz = "";
        }

    }
}
