using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper.CharModel
{
    public class Thing : INotifyPropertyChanged
    {
        private Model.ThingDefs thingType = 0;
        public Model.ThingDefs ThingType
        {
            get { return thingType; }
            set
            {
                if (value != this.ThingType)
                {
                    this.ThingType = value;
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
        private String bezeichner ="";
        public String Bezeichner
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
        private String typ { get; set; }
        public String Typ
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
        private String zusatz = "";
        public String Zusatz
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
        private String notiz = "";
        public String Notiz
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

        public Thing()
        {
            Ordnung = 0;
            Bezeichner = "";
            Typ = "";
            Wert = 0;
            Zusatz = "";
            Notiz = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
