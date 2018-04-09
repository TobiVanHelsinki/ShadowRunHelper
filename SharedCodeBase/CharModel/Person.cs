using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TLIB_UWPFRAME.Model;

namespace ShadowRunHelper.CharModel
{
    public class Person : INotifyPropertyChanged
    {
        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ModelHelper.CallPropertyChangedAtDispatcher(PropertyChanged, this, propertyName);
        }

        #endregion

        #region Schaden
        double schaden_K;
        [Used_UserAttribute]
        public double Schaden_K
        {
            get { return schaden_K; }
            set
            {
                if (value != schaden_K)
                {
                    schaden_K = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double schaden_G;
        [Used_UserAttribute]
        public double Schaden_G
        {
            get { return schaden_G; }
            set
            {
                if (value != schaden_G)
                {
                    schaden_G = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double schaden_K_max;
        [Used_UserAttribute]
        public double Schaden_K_max
        {
            get { return schaden_K_max; }
            set
            {
                if (value != schaden_K_max)
                {
                    schaden_K_max = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double schaden_G_max;
        [Used_UserAttribute]
        public double Schaden_G_max
        {
            get { return schaden_G_max; }
            set
            {
                if (value != schaden_G_max)
                {
                    schaden_G_max = value;
                    NotifyPropertyChanged();
                }
            }
        }

        double schaden_K_max_mod;
        [Used_UserAttribute]
        public double Schaden_K_max_mod
        {
            get { return schaden_K_max_mod; }
            set
            {
                if (value != schaden_K_max_mod)
                {
                    schaden_K_max_mod = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double schaden_G_max_mod;
        [Used_UserAttribute]
        public double Schaden_G_max_mod
        {
            get { return schaden_G_max_mod; }
            set
            {
                if (value != schaden_G_max_mod)
                {
                    schaden_G_max_mod = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region ZahlenWerte
        double kontostand;
        [Used_UserAttribute]
        public double Kontostand
        {
            get { return kontostand; }
            set
            {
                if (value != kontostand)
                {
                    kontostand = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double karma_Gesamt;
        [Used_UserAttribute]
        public double Karma_Gesamt
        {
            get { return karma_Gesamt; }
            set
            {
                if (value != karma_Gesamt)
                {
                    karma_Gesamt = value;
                    Strassenruf = 0;
                    NotifyPropertyChanged();
                }
            }
        }
        double karma_Aktuell;
        [Used_UserAttribute]
        public double Karma_Aktuell
        {
            get { return karma_Aktuell; }
            set
            {
                if (value != karma_Aktuell)
                {
                    karma_Aktuell = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double edge_Aktuell;
        [Used_UserAttribute]
        public double Edge_Aktuell
        {
            get { return edge_Aktuell; }
            set
            {
                if (value != edge_Aktuell)
                {
                    edge_Aktuell = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double edge_Gesamt;
        [Used_UserAttribute]
        public double Edge_Gesamt
        {
            get { return edge_Gesamt; }
            set
            {
                if (value != edge_Gesamt)
                {
                    edge_Gesamt = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double essenz;
        [Used_UserAttribute]
        public double Essenz
        {
            get { return essenz; }
            set
            {
                if (value != essenz)
                {
                    essenz = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double _StrassenrufMod;
        [Used_UserAttribute]
        public double StrassenrufMod
        {
            get { return _StrassenrufMod; }
            set
            {
                if (value != _StrassenrufMod)
                {
                    _StrassenrufMod = value;
                    Strassenruf = 0;
                    NotifyPropertyChanged();
                }
            }
        }
        [Used_UserAttribute]
        public double Strassenruf
        {
            get { return Math.Floor(karma_Gesamt / 10) + _StrassenrufMod; }
            set
            {
                if (value != Strassenruf)
                {
                    NotifyPropertyChanged();
                }
            }
        }
        double _SchlechterRuf;
        [Used_UserAttribute]
        public double SchlechterRuf
        {
            get { return _SchlechterRuf; }
            set
            {
                if (value != _SchlechterRuf)
                {
                    _SchlechterRuf = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double _Prominenz;
        [Used_UserAttribute]
        public double Prominenz
        {
            get { return _Prominenz; }
            set
            {
                if (value != _Prominenz)
                {
                    _Prominenz = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double initiative;
        [Used_UserAttribute]
        public double Initiative
        {
            get { return initiative; }
            set
            {
                if (value != initiative)
                {
                    initiative = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double runs;
        [Used_UserAttribute]
        public double Runs
        {
            get { return runs; }
            set
            {
                if (value != runs)
                {
                    runs = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Details
        string alias = TLIB.StringHelper.GetString("Model_Person_Alias_STD/Text");
        [Used_UserAttribute]
        public string Alias
        {
            get { return alias; }
            set
            {
                if (value != alias)
                {
                    alias = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string char_Typ;
        [Used_UserAttribute]
        public string Char_Typ
        {
            get { return char_Typ; }
            set
            {
                if (value != char_Typ)
                {
                    char_Typ = value;
                    NotifyPropertyChanged();
                }
            }
        }

        string notizen;
        [Used_UserAttribute]
        public string Notizen
        {
            get { return notizen; }
            set
            {
                if (value != notizen)
                {
                    notizen = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string metaTyp;
        [Used_UserAttribute]
        public string MetaTyp
        {
            get { return metaTyp; }
            set
            {
                if (value != metaTyp)
                {
                    metaTyp = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string metaTyp_sub;
        [Used_UserAttribute]
        public string MetaTyp_sub
        {
            get { return metaTyp_sub; }
            set
            {
                if (value != metaTyp_sub)
                {
                    metaTyp_sub = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string lebesstil;
        [Used_UserAttribute]
        public string Lebesstil
        {
            get { return lebesstil; }
            set
            {
                if (value != lebesstil)
                {
                    lebesstil = value;
                    NotifyPropertyChanged();
                }
            }
        }

        DateTimeOffset _BirthDate = new DateTimeOffset(2060, 1, 1, 0, 0, 0, new System.TimeSpan(0));
        [Used_UserAttribute]
        public DateTimeOffset BirthDate
        {
            get { return _BirthDate; }
            set
            {
                if (value != _BirthDate)
                {
                    _BirthDate = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string geschlecht;
        [Used_UserAttribute]
        public string Geschlecht
        {
            get { return geschlecht; }
            set
            {
                if (value != geschlecht)
                {
                    geschlecht = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double groesse;
        [Used_UserAttribute]
        public double Groesse
        {
            get { return groesse; }
            set
            {
                if (value != groesse)
                {
                    groesse = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double gewicht;
        [Used_UserAttribute]
        public double Gewicht
        {
            get { return gewicht; }
            set
            {
                if (value != gewicht)
                {
                    gewicht = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string augenfarbe;
        [Used_UserAttribute]
        public string Augenfarbe
        {
            get { return augenfarbe; }
            set
            {
                if (value != augenfarbe)
                {
                    augenfarbe = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string haarfarbe;
        [Used_UserAttribute]
        public string Haarfarbe
        {
            get { return haarfarbe; }
            set
            {
                if (value != haarfarbe)
                {
                    haarfarbe = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string hautfarbe;
        [Used_UserAttribute]
        public string Hautfarbe
        {
            get { return hautfarbe; }
            set
            {
                if (value != hautfarbe)
                {
                    hautfarbe = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string zusammenfassung;
        [Used_UserAttribute]
        public string Zusammenfassung
        {
            get { return zusammenfassung; }
            set
            {
                if (value != zusammenfassung)
                {
                    zusammenfassung = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        public override string ToString()
        {
            return Alias + " " + base.ToString();
        }

    }
}