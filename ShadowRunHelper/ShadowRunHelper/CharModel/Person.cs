using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharModel
{
    public class Person : INotifyPropertyChanged
    {
        private string alias = "";
        public string Alias
        {
            get { return alias; }
            set
            {
                if (value != this.alias)
                {
                    this.alias = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string strMakeName
        {
            get { return MakeName(); }
            private set{}
        }

        public string MakeName(bool WithDate = false)
        {
            var res = ResourceLoader.GetForCurrentView();
            string strSaveName = "";
            strSaveName += Alias == string.Empty ? "$$" : Alias;
            strSaveName += ",";
            strSaveName += Char_Typ == string.Empty ? "$$" : Char_Typ;
            strSaveName += ",";
            strSaveName += Runs.ToString();
            strSaveName += res.GetString("Model_Person_Runs/Text") + ",";
            strSaveName += Karma_Gesamt.ToString();
            strSaveName += res.GetString("Model_Person_Karma/Text");
            strSaveName += WithDate ? DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "_" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second : "";
            strSaveName += Konstanten.DATEIENDUNG_CHAR;
            return strSaveName;
        }

        private string char_Typ = "";
        public string Char_Typ
        {
            get { return char_Typ; }
            set
            {
                if (value != this.char_Typ)
                {
                    this.char_Typ = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double kontostand = 0;
        public double Kontostand
        {
            get { return kontostand; }
            set
            {
                if (value != this.kontostand)
                {
                    this.kontostand = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double karma_Gesamt = 0;
        public double Karma_Gesamt
        {
            get { return karma_Gesamt; }
            set
            {
                if (value != this.karma_Gesamt)
                {
                    this.karma_Gesamt = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double karma_Aktuell = 0;
        public double Karma_Aktuell
        {
            get { return karma_Aktuell; }
            set
            {
                if (value != this.karma_Aktuell)
                {
                    this.karma_Aktuell = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double edge_Aktuell = 0;
        public double Edge_Aktuell
        {
            get { return edge_Aktuell; }
            set
            {
                if (value != this.edge_Aktuell)
                {
                    this.edge_Aktuell = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double edge_Gesamt = 0;
        public double Edge_Gesamt
        {
            get { return edge_Gesamt; }
            set
            {
                if (value != this.edge_Gesamt)
                {
                    this.edge_Gesamt = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double essenz = 0;
        public double Essenz
        {
            get { return essenz; }
            set
            {
                if (value != this.essenz)
                {
                    this.essenz = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double schaden_K = 0;
        public double Schaden_K
        {
            get { return schaden_K; }
            set
            {
                if (value != this.schaden_K)
                {
                    this.schaden_K = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double schaden_G = 0;
        public double Schaden_G
        {
            get { return schaden_G; }
            set
            {
                if (value != this.schaden_G)
                {
                    this.schaden_G = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double schaden_M = 0;
        public double Schaden_M
        {
            get { return schaden_M; }
            set
            {
                if (value != this.schaden_M)
                {
                    this.schaden_M = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double schaden_K_max = 0;
        public double Schaden_K_max
        {
            get { return schaden_K_max; }
            set
            {
                if (value != this.schaden_K_max)
                {
                    this.schaden_K_max = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double schaden_G_max = 0;
        public double Schaden_G_max
        {
            get { return schaden_G_max; }
            set
            {
                if (value != this.schaden_G_max)
                {
                    this.schaden_G_max = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double schaden_M_max = 0;
        public double Schaden_M_max
        {
            get { return schaden_M_max; }
            set
            {
                if (value != this.schaden_M_max)
                {
                    this.schaden_M_max = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string notizen = "";
        public string Notizen
        {
            get { return notizen; }
            set
            {
                if (value != this.notizen)
                {
                    this.notizen = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string metaTyp = "";
        public string MetaTyp
        {
            get { return metaTyp; }
            set
            {
                if (value != this.metaTyp)
                {
                    this.metaTyp = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string metaTyp_sub = "";
        public string MetaTyp_sub
        {
            get { return metaTyp_sub; }
            set
            {
                if (value != this.metaTyp_sub)
                {
                    this.metaTyp_sub = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string lebesstil = "";
        public string Lebesstil
        {
            get { return lebesstil; }
            set
            {
                if (value != this.lebesstil)
                {
                    this.lebesstil = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string geburtsdatum = "";
        public string Geburtsdatum
        {
            get { return geburtsdatum; }
            set
            {
                if (value != this.geburtsdatum)
                {
                    this.geburtsdatum = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DateTime geburtsdatum2 = new DateTime();
        public DateTime Geburtsdatum2
        {
            get { return geburtsdatum2; }
            set
            {
                if (value != this.geburtsdatum2)
                {
                    this.geburtsdatum2 = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DateTimeOffset geburtsdatum3 = new DateTimeOffset(2060, 1, 1, 0, 0, 0, new System.TimeSpan(0));
        public DateTimeOffset GeburtsdatumDateTimeOffset
        {
            get { return geburtsdatum3; }
            set
            {
                if (value != this.geburtsdatum3)
                {
                    this.geburtsdatum3 = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string geschlecht = "";
        public string Geschlecht
        {
            get { return geschlecht; }
            set
            {
                if (value != this.geschlecht)
                {
                    this.geschlecht = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double größe = 0;
        public double Größe
        {
            get { return größe; }
            set
            {
                if (value != this.größe)
                {
                    this.größe = value;
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
        private string augenfarbe = "";
        public string Augenfarbe
        {
            get { return augenfarbe; }
            set
            {
                if (value != this.augenfarbe)
                {
                    this.augenfarbe = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string haarfarbe = "";
        public string Haarfarbe
        {
            get { return haarfarbe; }
            set
            {
                if (value != this.haarfarbe)
                {
                    this.haarfarbe = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string hautfarbe = "";
        public string Hautfarbe
        {
            get { return hautfarbe; }
            set
            {
                if (value != this.hautfarbe)
                {
                    this.hautfarbe = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string bild = "";
        public string Bild
        {
            get { return bild; }
            set
            {
                if (value != this.bild)
                {
                    this.bild = value;
                    NotifyPropertyChanged();
                }
            }
        }

        double _Strassenruf = 0;
        public double Strassenruf
        {
            get { return _Strassenruf; }
            private set
            {
                if (value != this._Strassenruf)
                {
                    this._Strassenruf = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double _StrassenrufMod = 0;
        public double StrassenrufMod
        {
            get { return _StrassenrufMod; }
            set
            {
                if (value != this._StrassenrufMod)
                {
                    this._StrassenrufMod = value;
                    this.Strassenruf = Math.Floor(this.karma_Gesamt / 10) + _StrassenrufMod;
                    NotifyPropertyChanged();
                }
            }
        }
        double _SchlechterRuf = 0;
        public double SchlechterRuf
        {
            get { return _SchlechterRuf; }
            set
            {
                if (value != this._SchlechterRuf)
                {
                    this._SchlechterRuf = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double _Prominenz = 0;
        public double Prominenz
        {
            get { return _Prominenz; }
            set
            {
                if (value != this._Prominenz)
                {
                    this._Prominenz = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private string zusammenfassung = "";
        public string Zusammenfassung
        {
            get { return zusammenfassung; }
            set
            {
                if (value != this.zusammenfassung)
                {
                    this.zusammenfassung = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double initiative = 0;
        public double Initiative
        {
            get { return initiative; }
            set
            {
                if (value != this.initiative)
                {
                    this.initiative = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double runs = 0;
        public double Runs
        {
            get { return runs; }
            set
            {
                if (value != this.runs)
                {
                    this.runs = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Person()
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

        public Person Copy(Person Source = null)
        {
            if (Source == null)
            {
                Source = this;
            }
            Person ReturnPerson = new Person();
            ReturnPerson.Alias = Source.Alias;
            ReturnPerson.Augenfarbe = Source.Augenfarbe;
            ReturnPerson.Bild = Source.Bild;
            ReturnPerson.Char_Typ = Source.Char_Typ;
            ReturnPerson.Edge_Aktuell = Source.Edge_Aktuell;
            ReturnPerson.Edge_Gesamt = Source.Edge_Gesamt;
            ReturnPerson.Essenz = Source.Essenz;
            ReturnPerson.Geburtsdatum = Source.Geburtsdatum;
            ReturnPerson.Geburtsdatum2 = Source.Geburtsdatum2;
            ReturnPerson.GeburtsdatumDateTimeOffset = Source.GeburtsdatumDateTimeOffset;
            ReturnPerson.Geschlecht = Source.Geschlecht;
            ReturnPerson.Gewicht = Source.Gewicht;
            ReturnPerson.Größe = Source.Größe;
            ReturnPerson.Haarfarbe = Source.Haarfarbe;
            ReturnPerson.Hautfarbe = Source.Hautfarbe;
            ReturnPerson.Initiative = Source.Initiative;
            ReturnPerson.Karma_Aktuell = Source.Karma_Aktuell;
            ReturnPerson.Karma_Gesamt = Source.Karma_Gesamt;
            ReturnPerson.Kontostand = Source.Kontostand;
            ReturnPerson.Lebesstil = Source.Lebesstil;
            ReturnPerson.MetaTyp = Source.MetaTyp;
            ReturnPerson.MetaTyp_sub = Source.MetaTyp_sub;
            ReturnPerson.Notizen = Source.Notizen;
            ReturnPerson.Runs = Source.Runs;
            ReturnPerson.Schaden_G = Source.Schaden_G;
            ReturnPerson.Schaden_G_max = Source.Schaden_G_max;
            ReturnPerson.Schaden_K = Source.Schaden_K;
            ReturnPerson.Schaden_K_max = Source.Schaden_K_max;
            ReturnPerson.Schaden_M = Source.Schaden_M;
            ReturnPerson.Schaden_M_max = Source.Schaden_M_max;
            ReturnPerson.Zusammenfassung = Source.Zusammenfassung;
            return ReturnPerson;
        }

        //public string ToCSV(string Delimiter)
        //{
        //    string strReturn = "";
        //    strReturn += Alias;
        //    strReturn += Delimiter;
        //    strReturn += Augenfarbe;
        //    strReturn += Delimiter;
        //    //strReturn += Bild;
        //    strReturn += Delimiter;
        //    strReturn += Char_Typ;
        //    strReturn += Delimiter;
        //    strReturn += Edge_Aktuell;
        //    strReturn += Delimiter;
        //    strReturn += Edge_Gesamt;
        //    strReturn += Delimiter;
        //    strReturn += Essenz;
        //    strReturn += Delimiter;
        //    strReturn += Geburtsdatum;
        //    strReturn += Delimiter;
        //    strReturn += Geburtsdatum2;
        //    strReturn += Delimiter;
        //    strReturn += GeburtsdatumDateTimeOffset;
        //    strReturn += Delimiter;
        //    strReturn += Geschlecht;
        //    strReturn += Delimiter;
        //    strReturn += Gewicht;
        //    strReturn += Delimiter;
        //    strReturn += Größe;
        //    strReturn += Delimiter;
        //    strReturn += Haarfarbe;
        //    strReturn += Delimiter;
        //    strReturn += Hautfarbe;
        //    strReturn += Delimiter;
        //    strReturn += Initiative;
        //    strReturn += Delimiter;
        //    strReturn += Karma_Aktuell;
        //    strReturn += Delimiter;
        //    strReturn += Karma_Gesamt;
        //    strReturn += Delimiter;
        //    strReturn += Kontostand;
        //    strReturn += Delimiter;
        //    strReturn += Lebesstil;
        //    strReturn += Delimiter;
        //    strReturn += MetaTyp;
        //    strReturn += Delimiter;
        //    strReturn += MetaTyp_sub;
        //    strReturn += Delimiter;
        //    strReturn += Notizen;
        //    strReturn += Delimiter;
        //    strReturn += Runs;
        //    strReturn += Delimiter;
        //    strReturn += Schaden_G;
        //    strReturn += Delimiter;
        //    strReturn += Schaden_G_max;
        //    strReturn += Delimiter;
        //    strReturn += Schaden_K;
        //    strReturn += Delimiter;
        //    strReturn += Schaden_K_max;
        //    strReturn += Delimiter;
        //    strReturn += Schaden_M;
        //    strReturn += Delimiter;
        //    strReturn += Delimiter;
        //    strReturn += Schaden_M_max;
        //    strReturn += Delimiter;
        //    strReturn += Zusammenfassung;
        //    strReturn += Delimiter;
        //    return strReturn;
        //}

    }


}