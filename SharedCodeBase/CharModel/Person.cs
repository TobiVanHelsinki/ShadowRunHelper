using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TLIB_UWPFRAME.Model;

namespace ShadowRunHelper.CharModel
{
    public class Person : INotifyPropertyChanged
    {
        string alias = TLIB_UWPFRAME.CrossPlatformHelper.GetString("Model_Person_Alias_STD/Text");
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
        double schaden_M;
        [Used_UserAttribute]
        public double Schaden_M
        {
            get { return schaden_M; }
            set
            {
                if (value != schaden_M)
                {
                    schaden_M = value;
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
        //double schaden_M_max;
        //        [Used] public double Schaden_M_max
        //{
        //    get { return schaden_M_max; }
        //    set
        //    {
        //        if (value != schaden_M_max)
        //        {
        //            schaden_M_max = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}
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
        //string geburtsdatum;
        //        [Used] public string Geburtsdatum
        //{
        //    get { return geburtsdatum; }
        //    set
        //    {
        //        if (value != geburtsdatum)
        //        {
        //            geburtsdatum = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}
        //DateTime geburtsdatum2 = new DateTime();
        //        [Used] public DateTime Geburtsdatum2
        //{
        //    get { return geburtsdatum2; }
        //    set
        //    {
        //        if (value != geburtsdatum2)
        //        {
        //            geburtsdatum2 = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}
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
        string bild;
        [Used_UserAttribute]
        public string Bild
        {
            get { return bild; }
            set
            {
                if (value != bild)
                {
                    bild = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ModelHelper.CallPropertyChangedAtDispatcher(PropertyChanged, this, propertyName);
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
            //ReturnPerson.Geburtsdatum = Source.Geburtsdatum;
            //ReturnPerson.Geburtsdatum2 = Source.Geburtsdatum2;
            ReturnPerson.BirthDate = Source.BirthDate;
            ReturnPerson.Geschlecht = Source.Geschlecht;
            ReturnPerson.Gewicht = Source.Gewicht;
            ReturnPerson.Groesse = Source.Groesse;
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
            //ReturnPerson.Schaden_G_max = Source.Schaden_G_max;
            ReturnPerson.Schaden_K = Source.Schaden_K;
            //ReturnPerson.Schaden_K_max = Source.Schaden_K_max;
            ReturnPerson.Schaden_M = Source.Schaden_M;
            //ReturnPerson.Schaden_M_max = Source.Schaden_M_max;
            ReturnPerson.Zusammenfassung = Source.Zusammenfassung;
            return ReturnPerson;
        }

        public override string ToString()
        {
            return Alias + " " + base.ToString();
        }
        public string ToCSV(string Delimiter)
        {
            string strReturn = "";
            strReturn += Alias;
            strReturn += Delimiter;
            strReturn += Augenfarbe;
            strReturn += Delimiter;
            //strReturn += Bild;
            strReturn += Delimiter;
            strReturn += Char_Typ;
            strReturn += Delimiter;
            strReturn += Edge_Aktuell;
            strReturn += Delimiter;
            strReturn += Edge_Gesamt;
            strReturn += Delimiter;
            strReturn += Essenz;
            strReturn += Delimiter;
            //strReturn += Geburtsdatum;
            strReturn += Delimiter;
            //strReturn += Geburtsdatum2;
            strReturn += Delimiter;
            //strReturn += GeburtsdatumDateTimeOffset;
            strReturn += Delimiter;
            strReturn += Geschlecht;
            strReturn += Delimiter;
            strReturn += Gewicht;
            strReturn += Delimiter;
            strReturn += Groesse;
            strReturn += Delimiter;
            strReturn += Haarfarbe;
            strReturn += Delimiter;
            strReturn += Hautfarbe;
            strReturn += Delimiter;
            strReturn += Initiative;
            strReturn += Delimiter;
            strReturn += Karma_Aktuell;
            strReturn += Delimiter;
            strReturn += Karma_Gesamt;
            strReturn += Delimiter;
            strReturn += Kontostand;
            strReturn += Delimiter;
            strReturn += Lebesstil;
            strReturn += Delimiter;
            strReturn += MetaTyp;
            strReturn += Delimiter;
            strReturn += MetaTyp_sub;
            strReturn += Delimiter;
            strReturn += Notizen;
            strReturn += Delimiter;
            strReturn += Runs;
            strReturn += Delimiter;
            strReturn += Schaden_G;
            strReturn += Delimiter;
            strReturn += Schaden_G_max;
            strReturn += Delimiter;
            strReturn += Schaden_K;
            strReturn += Delimiter;
            strReturn += Schaden_K_max;
            strReturn += Delimiter;
            strReturn += Schaden_M;
            strReturn += Delimiter;
            strReturn += Delimiter;
            //strReturn += Schaden_M_max;
            strReturn += Delimiter;
            strReturn += Zusammenfassung;
            strReturn += Delimiter;
            return strReturn;
        }

    }
}