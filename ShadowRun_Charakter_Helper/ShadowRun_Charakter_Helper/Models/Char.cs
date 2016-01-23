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

        public ObservableCollection<Char_Attribut> Char_Attribute { get; set; }
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
            
            this.Char_Fertigkeiten = new ObservableCollection<Char_Fertigkeit>();
            this.Char_Fähigkeiten = new ObservableCollection<Char_Fähigkeit>();
            this.Char_Attribute = new ObservableCollection<Char_Attribut>();
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

        public static implicit operator char(Char v)
        {
            throw new NotImplementedException();
        }


        internal void Säubern()
        {
            ID_Char = 0;
            Alias = null;
            Char_Typ = null;
            Kontostand = 0;
            Karma_Gesamt = 0;
            Karma_Aktuell = 0;
            Edgne_Aktuell = 0;
            Edge_Gesamt = 0;
            Essenz = 0;
            Schaden_K = 0;
            Schaden_G = 0;
            Schaden_M = 0;
            Schaden_K_max = 0;
            Schaden_G_max = 0;
            Schaden_M_max = 0;
            Notizen = null;
            MetaTyp = null;
            Lebesstil = null;
            try
            {
                Geburtsdatum2 = new DateTime();
            }
            catch (NullReferenceException)
            {

            }
            Geburtsdatum = null;
            Geschlecht = null;
            Größe = 0;
            Gewicht = 0;
            Augenfarbe = null;
            Haarfarbe = null;
            Hautfarbe = null;
            Bild = null;
            //Geschicklichkeit = 0;
            //Konstitution = 0;
            //Reaktion = 0;
            //Stärke = 0;
            //Charisma = 0;
            //Intuition = 0;
            //Logik = 0;
            //Willenskraft = 0;
            Zusammenfassung = null;

            Runs = 0;

            //Char_Fertigkeiten

            Char_Fertigkeiten = null;
            Char_Fertigkeiten = new System.Collections.ObjectModel.ObservableCollection<Char_Fertigkeit>();

            //Char_Fähigkeiten

            Char_Fähigkeiten = null;
            Char_Fähigkeiten = new System.Collections.ObjectModel.ObservableCollection<Char_Fähigkeit>();

            //Char_Connections

            Char_Connections = null;
            Char_Connections = new System.Collections.ObjectModel.ObservableCollection<Char_Connection>();

            //Char_Dronen_Fahrzeuge

            Char_Dronen_Fahrzeuge = null;
            Char_Dronen_Fahrzeuge = new System.Collections.ObjectModel.ObservableCollection<Char_Drone_Fahrzeug>();

            //Char_Fernkampfwaffen

            Char_Fernkampfwaffen = null;
            Char_Fernkampfwaffen = new System.Collections.ObjectModel.ObservableCollection<Char_Fernkampfwaffe>();

            //Char_Implantate

            Char_Implantate = null;
            Char_Implantate = new System.Collections.ObjectModel.ObservableCollection<Char_Implantat>();

            //Char_Items

            Char_Items = null;
            Char_Items = new System.Collections.ObjectModel.ObservableCollection<Char_Item>();

            //Char_Kommlinks

            Char_Kommlinks = null;
            Char_Kommlinks = new System.Collections.ObjectModel.ObservableCollection<Char_Kommlink>();

            //Char_Nachteile

            Char_Nachteile = null;
            Char_Nachteile = new System.Collections.ObjectModel.ObservableCollection<Char_Nachteil>();

            //Char_Nahkampfwaffen

            Char_Nahkampfwaffen = null;
            Char_Nahkampfwaffen = new System.Collections.ObjectModel.ObservableCollection<Char_Nahkampfwaffe>();

            //Char_Panzerungen

            Char_Panzerungen = null;
            Char_Panzerungen = new System.Collections.ObjectModel.ObservableCollection<Char_Panzerung>();

            //Char_Programme

            Char_Programme = null;
            Char_Programme = new System.Collections.ObjectModel.ObservableCollection<Char_Programm>();

            //Char_Sins

            Char_Sins = null;
            Char_Sins = new System.Collections.ObjectModel.ObservableCollection<Char_Sin>();

            //Char_Vorteile

            Char_Vorteile = null;
            Char_Vorteile = new System.Collections.ObjectModel.ObservableCollection<Char_Vorteil>();

            //Char_Attribute

            Char_Attribute = null;
            Char_Attribute = new System.Collections.ObjectModel.ObservableCollection<Char_Attribut>();

        }

    }

    public class CharViewModel : INotifyPropertyChanged
    {
        private Char defaultChar = new Char();
        public Char DefaultChar { get { return this.defaultChar; }
                                                                    set
                                                                    {
                                                                        if (value != this.defaultChar)
                                                                        {
                                                                            this.defaultChar = value;
                                                                            NotifyPropertyChanged();
                }
                                                                   }
                                }

        public CharViewModel()
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




}