using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Char : INotifyPropertyChanged
    {
        private int iD_Char;
        public int ID_Char
        {
            get { return iD_Char; }
            set
            {
                if (value != this.iD_Char)
                {
                    this.iD_Char = value;
                    NotifyPropertyChanged();
                }
            }
        }
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
        private double edgne_Aktuell;
        public double Edgne_Aktuell
        {
            get { return edgne_Aktuell; }
            set
            {
                if (value != this.edgne_Aktuell)
                {
                    this.edgne_Aktuell = value;
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

        private int konstitution;
        public int Konstitution
        {
            get { return konstitution; }
            set
            {
                if (value != this.konstitution)
                {
                    this.konstitution = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int geschicklichkeit;
        public int Geschicklichkeit
        {
            get { return geschicklichkeit; }
            set
            {
                if (value != this.geschicklichkeit)
                {
                    this.geschicklichkeit = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int reaktion;
        public int Reaktion
        {
            get { return reaktion; }
            set
            {
                if (value != this.reaktion)
                {
                    this.reaktion = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int stärke;
        public int Stärke
        {
            get { return stärke; }
            set
            {
                if (value != this.stärke)
                {
                    this.stärke = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int charisma;
        public int Charisma
        {
            get { return charisma; }
            set
            {
                if (value != this.charisma)
                {
                    this.charisma = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int intuition;
        public int Intuition
        {
            get { return intuition; }
            set
            {
                if (value != this.intuition)
                {
                    this.intuition = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int logik;
        public int Logik
        {
            get { return logik; }
            set
            {
                if (value != this.logik)
                {
                    this.logik = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int willenskraft;
        public int Willenskraft
        {
            get { return willenskraft; }
            set
            {
                if (value != this.willenskraft)
                {
                    this.willenskraft = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int initiative;
        public int Initiative
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

        
        public ObservableCollection<Char_Fertigkeit> Char_Fertigkeiten { get; set; }
        public ObservableCollection<Char_Fähigkeit> Char_Fähigkeiten { get; set; }
        public ObservableCollection<Char_Connection> Char_Connections { get; set; }
        public ObservableCollection<Char_Drone_Fahrzeug> Char_Dronen_Fahrzeuge { get; set; }
        public ObservableCollection<Char_Fernkampfwaffe> Char_Fernkampfwaffen { get; set; }
        public ObservableCollection<Char_Implantat> Char_Implantate { get; set; }
        public ObservableCollection<Char_Item> Char_Items { get; set; }
        public ObservableCollection<Char_Kommlink> Char_Kommlinks { get; set; }
        public ObservableCollection<Char_Nachteil> Char_Nachteile { get; set; }
        public ObservableCollection<Char_Nahkampfwaffe> Char_Nahkampfwaffen { get; set; }
        public ObservableCollection<Char_Panzerung> Char_Panzerungen { get; set; }
        public ObservableCollection<Char_Programm> Char_Programme { get; set; }
        public ObservableCollection<Char_Sin> Char_Sins { get; set; }
        public ObservableCollection<Char_Vorteil> Char_Vorteile { get; set; }

        public Char()
        {
            //this.Alias = "";
            //this.MetaTyp = "";
            this.Char_Fertigkeiten = new ObservableCollection<Char_Fertigkeit>();
            this.Char_Fähigkeiten = new ObservableCollection<Char_Fähigkeit>();
            //this.Char_Attribute = new ObservableCollection<Char_Attribut>();
            this.Char_Items = new ObservableCollection<Char_Item>();
            this.Char_Connections = new ObservableCollection<Char_Connection> ();
            Char_Dronen_Fahrzeuge = new ObservableCollection<Char_Drone_Fahrzeug>();
            Char_Fernkampfwaffen = new ObservableCollection<Char_Fernkampfwaffe>();
            Char_Implantate = new ObservableCollection<Char_Implantat>();
            Char_Items = new ObservableCollection<Char_Item>();
            Char_Kommlinks = new ObservableCollection<Char_Kommlink>();
            Char_Nachteile = new ObservableCollection<Char_Nachteil>();
            Char_Nahkampfwaffen = new ObservableCollection<Char_Nahkampfwaffe>();
            Char_Panzerungen = new ObservableCollection<Char_Panzerung>();
            Char_Programme = new ObservableCollection<Char_Programm>();
            Char_Sins = new ObservableCollection<Char_Sin>();
            Char_Vorteile = new ObservableCollection<Char_Vorteil>();
        }

             public void Char_add_Aktion()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



    }

    public class CharViewModel
    {
        private Char defaultChar = new Char();
        public Char DefaultChar { get { return this.defaultChar; } }

        public CharViewModel()
        {

        }

    }




}