using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Person : INotifyPropertyChanged
    {
        private string alias;
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
        private string char_Typ;
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
        private double kontostand;
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
        private double karma_Gesamt;
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
        private double karma_Aktuell;
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
        private double edge_Aktuell;
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
        private double edge_Gesamt;
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
        private double essenz;
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
        private double schaden_K;
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
        private double schaden_G;
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
        private double schaden_M;
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
        private double schaden_K_max;
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
        private double schaden_G_max;
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
        private double schaden_M_max;
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
        private string notizen;
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
        private string metaTyp;
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
        private string metaTyp_sub;
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
        private string lebesstil;
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
        private string geburtsdatum;
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
        private DateTime geburtsdatum2;
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
        private string geschlecht;
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
        private double größe;
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
        private double gewicht;
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
        private string augenfarbe;
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
        private string haarfarbe;
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
        private string hautfarbe;
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
        private string bild;
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

        private string zusammenfassung;
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

        private double initiative;
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

        private double runs;
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

    }


}
