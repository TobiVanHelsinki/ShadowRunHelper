using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Model : INotifyPropertyChanged
    {
        public int dicCD_ID;
        public int DicCD_ID
        {
            get { return dicCD_ID; }
            set
            {
                if (value != this.dicCD_ID)
                {
                    this.dicCD_ID = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int ordnung;
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
        public String bezeichner;
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
        public String typ { get; set; }
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
        public double wert { get; set; }
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
        public String zusatz { get; set; }
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
        public String notiz { get; set; }
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

        public Model()
        {

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
