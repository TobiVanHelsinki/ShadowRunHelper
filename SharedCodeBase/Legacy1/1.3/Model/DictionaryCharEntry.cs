using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper1_3.Model
{

    public class DictionaryCharEntry : INotifyPropertyChanged
    {
        private String bezeichner;
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
        private double wert { get; set; }
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
        private String zusatz { get; set; }
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
        private String notiz { get; set; }
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

        public DictionaryCharEntry(String bezeichner, String typ, double wert, String zusatz,  String notiz)
        {
            this.Bezeichner = bezeichner;
            this.Typ = typ;
            this.Wert = wert;
            this.Zusatz = zusatz;
            this.Notiz = notiz;
        }

        public DictionaryCharEntry(String bezeichner, int wert)
        {
            this.Bezeichner = bezeichner;
            this.Wert = wert;
        }

        public DictionaryCharEntry()
        {
            this.Bezeichner = "File Load";
            this.Wert = 0;
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
