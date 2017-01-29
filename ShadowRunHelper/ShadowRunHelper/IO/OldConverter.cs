using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.IO
{
    internal static class OldConverter
    {
        private static void CopyAttribute(CharModel.Attribut Target, ShadowRunHelper1_3.CharModel.Attribut Source)
        {
            Target.Ordnung = Source.Ordnung;
            Target.Bezeichner = Source.Bezeichner;
            Target.Wert = Source.Wert;
            Target.Typ = Source.Typ;
            Target.Zusatz = Source.Zusatz;
            Target.Notiz = Source.Notiz;
        }

        internal static CharHolder ConvertVersion1_3to1_5(ShadowRunHelper1_3.Controller.CharHolder cH1_3)
        {
            CharHolder ReturnCharHolder = new CharHolder();

            foreach (var item in cH1_3.FertigkeitController)
            {
                var temp = ReturnCharHolder.CTRLFertigkeit.AddNewThing();
                temp.Ordnung = item.Data.Ordnung;
                temp.Bezeichner = item.Data.Bezeichner;
                temp.Wert = item.Data.Wert;
                temp.Typ = item.Data.Typ;
                temp.Zusatz = item.Data.Zusatz;
                temp.Notiz = item.Data.Notiz;
            }
            var res = ResourceLoader.GetForCurrentView();
            foreach (var item in cH1_3.AttributController)
            {
                if (item.Data.Bezeichner == res.GetString("Model_Attribut_Konsti/Text"))
                {
                    CopyAttribute(ReturnCharHolder.CTRLAttribut.Konsti, item.Data);
                }
                else if (item.Data.Bezeichner == res.GetString("Model_Attribut_Geschick/Text"))
                {
                    CopyAttribute(ReturnCharHolder.CTRLAttribut.Geschick, item.Data);
                }
                else if (item.Data.Bezeichner == res.GetString("Model_Attribut_Reaktion/Text"))
                {
                    CopyAttribute(ReturnCharHolder.CTRLAttribut.Reaktion, item.Data);
                }
                else if (item.Data.Bezeichner == res.GetString("Model_Attribut_Staerke/Text"))
                {
                    CopyAttribute(ReturnCharHolder.CTRLAttribut.Staerke, item.Data);
                }
                else if (item.Data.Bezeichner == res.GetString("Model_Attribut_Charisma/Text"))
                {
                    CopyAttribute(ReturnCharHolder.CTRLAttribut.Charisma, item.Data);
                }
                else if (item.Data.Bezeichner == res.GetString("Model_Attribut_Logik/Text"))
                {
                    CopyAttribute(ReturnCharHolder.CTRLAttribut.Logik, item.Data);
                }
                else if (item.Data.Bezeichner == res.GetString("Model_Attribut_Intuition/Text"))
                {
                    CopyAttribute(ReturnCharHolder.CTRLAttribut.Intuition, item.Data);
                }
                else if (item.Data.Bezeichner == res.GetString("Model_Attribut_Willen/Text"))
                {
                    CopyAttribute(ReturnCharHolder.CTRLAttribut.Willen, item.Data);
                }
            }
            foreach (var item in cH1_3.ItemController)
            {
                var temp = ReturnCharHolder.CTRLItem.AddNewThing();
                temp.Ordnung = item.Data.Ordnung;
                temp.Bezeichner = item.Data.Bezeichner;
                temp.Wert = item.Data.Wert;
                temp.Typ = item.Data.Typ;
                temp.Zusatz = item.Data.Zusatz;
                temp.Notiz = item.Data.Notiz;
                temp.Besitz = item.Data.Besitz;
                temp.Aktiv = item.Data.Aktiv;
                temp.Anzahl = item.Data.Anzahl;

            }

            foreach (var item in cH1_3.ProgrammController)
            {
                var temp = ReturnCharHolder.CTRLProgramm.AddNewThing();
                temp.Ordnung = item.Data.Ordnung;
                temp.Bezeichner = item.Data.Bezeichner;
                temp.Wert = item.Data.Wert;
                temp.Typ = item.Data.Typ;
                temp.Zusatz = item.Data.Zusatz;
                temp.Notiz = item.Data.Notiz;
                temp.Besitz = item.Data.Besitz;
                temp.Aktiv = item.Data.Aktiv;
                temp.Anzahl = item.Data.Anzahl;
                temp.Optionen = item.Data.Optionen;

            }

            foreach (var item in cH1_3.MunitionController)
            {
                var temp = ReturnCharHolder.CTRLMunition.AddNewThing();
                temp.Ordnung = item.Data.Ordnung;
                temp.Bezeichner = item.Data.Bezeichner;
                temp.Wert = item.Data.Wert;
                temp.Typ = item.Data.Typ;
                temp.Zusatz = item.Data.Zusatz;
                temp.Notiz = item.Data.Notiz;
                temp.Besitz = item.Data.Besitz;
                temp.Aktiv = item.Data.Aktiv;
                temp.Anzahl = item.Data.Anzahl;

            }

            foreach (var item in cH1_3.ImplantatController)
            {
                var temp = ReturnCharHolder.CTRLImplantat.AddNewThing();
                temp.Ordnung = item.Data.Ordnung;
                temp.Bezeichner = item.Data.Bezeichner;
                temp.Wert = item.Data.Wert;
                temp.Typ = item.Data.Typ;
                temp.Zusatz = item.Data.Zusatz;
                temp.Notiz = item.Data.Notiz;
                temp.Besitz = item.Data.Besitz;
                temp.Aktiv = item.Data.Aktiv;
                temp.Anzahl = item.Data.Anzahl;
                temp.Essenz = item.Data.Essenz;
                temp.Kapazität = item.Data.Kapazität;

            }

            foreach (var item in cH1_3.VorteilController)
            {
                var temp = ReturnCharHolder.CTRLVorteil.AddNewThing();
                temp.Ordnung = item.Data.Ordnung;
                temp.Bezeichner = item.Data.Bezeichner;
                temp.Wert = item.Data.Wert;
                temp.Typ = item.Data.Typ;
                temp.Zusatz = item.Data.Zusatz;
                temp.Notiz = item.Data.Notiz;
                temp.Auswirkungen = item.Data.Auswirkungen;

            }

            foreach (var item in cH1_3.NachteilController)
            {
                var temp = ReturnCharHolder.CTRLNachteil.AddNewThing();
                temp.Ordnung = item.Data.Ordnung;
                temp.Bezeichner = item.Data.Bezeichner;
                temp.Wert = item.Data.Wert;
                temp.Typ = item.Data.Typ;
                temp.Zusatz = item.Data.Zusatz;
                temp.Notiz = item.Data.Notiz;
                temp.Auswirkungen = item.Data.Auswirkungen;

            }

            foreach (var item in cH1_3.ConnectionController)
            {
                var temp = ReturnCharHolder.CTRLConnection.AddNewThing();
                temp.Wert = item.Data.Wert;
                temp.Loyal = item.Data.Loyal;
                temp.Bezeichner = item.Data.Alias;
                temp.Notiz = item.Data.Notizen;
            }

            foreach (var item in cH1_3.SinController)
            {
                var temp = ReturnCharHolder.CTRLSin.AddNewThing();
                temp.Ordnung = item.Data.Ordnung;
                temp.Bezeichner = item.Data.Bezeichner;
                temp.Wert = item.Data.Wert;
                temp.Typ = item.Data.Typ;
                temp.Zusatz = item.Data.Zusatz;
                temp.Notiz = item.Data.Notiz;

            }

            foreach (var item in cH1_3.NahkampfwaffeController.DataList)
            {
                var temp = ReturnCharHolder.CTRLNahkampfwaffe.AddNewThing();
                temp.Ordnung = item.Ordnung;
                temp.Bezeichner = item.Bezeichner;
                temp.Wert = item.Wert;
                temp.Typ = item.Typ;
                temp.Zusatz = item.Zusatz;
                temp.Notiz = item.Notiz;
                temp.Aktiv = item.Aktiv;
                temp.Anzahl = item.Anzahl;
                temp.Besitz = item.Besitz;
                temp.PB = item.PB;
                temp.Pool = item.Pool;
                temp.Reichweite = item.Reichweite;
                temp.SchadenTyp = item.SchadenTyp;

            }

            foreach (var item in cH1_3.FernkampfwaffeController.DataList)
            {
                var temp = ReturnCharHolder.CTRLFernkampfwaffe.AddNewThing();
                temp.Ordnung = item.Ordnung;
                temp.Bezeichner = item.Bezeichner;
                temp.Wert = item.Wert;
                temp.Typ = item.Typ;
                temp.Zusatz = item.Zusatz;
                temp.Notiz = item.Notiz;
                temp.Aktiv = item.Aktiv;
                temp.Anzahl = item.Anzahl;
                temp.Besitz = item.Besitz;
                temp.PB = item.PB;
                temp.Pool = item.Pool;
                temp.Modi = item.Modi;
                temp.Rückstoß = item.Rückstoß;
                temp.SchadenTyp = item.SchadenTyp;

            }

            foreach (var item in cH1_3.KommlinkController.DataList)
            {
                var temp = ReturnCharHolder.CTRLKommlink.AddNewThing();
                temp.Ordnung = item.Ordnung;
                temp.Bezeichner = item.Bezeichner;
                temp.Wert = item.Wert;
                temp.Typ = item.Typ;
                temp.Zusatz = item.Zusatz;
                temp.Notiz = item.Notiz;
                temp.Aktiv = item.Aktiv;
                temp.Anzahl = item.Anzahl;
                temp.Besitz = item.Besitz;
                temp.Datenverarbeitung = item.Datenverarbeitung;
                temp.Firewall = item.Firewall;
                temp.Programmanzahl = item.Programmanzahl;

            }

            foreach (var item in cH1_3.CyberDeckController.DataList)
            {
                var temp = ReturnCharHolder.CTRLCyberDeck.AddNewThing();
                temp.Ordnung = item.Ordnung;
                temp.Bezeichner = item.Bezeichner;
                temp.Wert = item.Wert;
                temp.Typ = item.Typ;
                temp.Zusatz = item.Zusatz;
                temp.Notiz = item.Notiz;
                temp.Aktiv = item.Aktiv;
                temp.Anzahl = item.Anzahl;
                temp.Besitz = item.Besitz;
                temp.Programmanzahl = item.Programmanzahl;
                temp.Datenverarbeitung = item.Datenverarbeitung;
                temp.Firewall = item.Firewall;
                temp.Angriff = item.Angriff;
                temp.Schleicher = item.Schleicher;
                temp.Datenverarbeitung_o = item.Datenverarbeitung_o;
                temp.Firewall_o = item.Firewall_o;
                temp.Angriff_o = item.Angriff_o;
                temp.Schleicher_o = item.Schleicher_o;

            }

            foreach (var item in cH1_3.VehikelController.DataList)
            {
                var temp = ReturnCharHolder.CTRLVehikel.AddNewThing();
                temp.Ordnung = item.Ordnung;
                temp.Bezeichner = item.Bezeichner;
                temp.Wert = item.Wert;
                temp.Typ = item.Typ;
                temp.Zusatz = item.Zusatz;
                temp.Notiz = item.Notiz;
                temp.Aktiv = item.Aktiv;
                temp.Anzahl = item.Anzahl;
                temp.Besitz = item.Besitz;
                temp.Beschleunigung = item.Beschleunigung;
                temp.Geschwindigkeit = item.Geschwindigkeit;
                temp.Gewicht = item.Gewicht;
                temp.Handling = item.Handling;
                temp.Panzerung = item.Panzerung;
                temp.Pilot = item.Pilot;
                temp.Rumpf = item.Rumpf;
                temp.Sensor = item.Sensor;
                temp.Sitze = item.Sitze;
            }

            foreach (var item in cH1_3.PanzerungController.DataList)
            {
                var temp = ReturnCharHolder.CTRLPanzerung.AddNewThing();
                temp.Ordnung = item.Ordnung;
                temp.Bezeichner = item.Bezeichner;
                temp.Wert = item.Wert;
                temp.Typ = item.Typ;
                temp.Zusatz = item.Zusatz;
                temp.Notiz = item.Notiz;
                temp.Aktiv = item.Aktiv;
                temp.Anzahl = item.Anzahl;
                temp.Besitz = item.Besitz;
                temp.Stoß = item.Stoß;
            }
            ReturnCharHolder.Person.Alias = cH1_3.Person.Alias;
            ReturnCharHolder.Person.Augenfarbe = cH1_3.Person.Augenfarbe;
            ReturnCharHolder.Person.Bild = cH1_3.Person.Bild;
            ReturnCharHolder.Person.Char_Typ = cH1_3.Person.Char_Typ;
            ReturnCharHolder.Person.Edge_Aktuell = cH1_3.Person.Edge_Aktuell;
            ReturnCharHolder.Person.Edge_Gesamt = cH1_3.Person.Edge_Gesamt;
            ReturnCharHolder.Person.Essenz = cH1_3.Person.Essenz;
            ReturnCharHolder.Person.Geburtsdatum = cH1_3.Person.Geburtsdatum;
            ReturnCharHolder.Person.Geburtsdatum2 = cH1_3.Person.Geburtsdatum2;
            ReturnCharHolder.Person.GeburtsdatumDateTimeOffset = cH1_3.Person.GeburtsdatumDateTimeOffset;
            ReturnCharHolder.Person.Geschlecht = cH1_3.Person.Geschlecht;
            ReturnCharHolder.Person.Gewicht = cH1_3.Person.Gewicht;
            ReturnCharHolder.Person.Größe = cH1_3.Person.Größe;
            ReturnCharHolder.Person.Haarfarbe = cH1_3.Person.Haarfarbe;
            ReturnCharHolder.Person.Hautfarbe = cH1_3.Person.Hautfarbe;
            ReturnCharHolder.Person.Initiative = cH1_3.Person.Initiative;
            ReturnCharHolder.Person.Karma_Aktuell = cH1_3.Person.Karma_Aktuell;
            ReturnCharHolder.Person.Karma_Gesamt = cH1_3.Person.Karma_Gesamt;
            ReturnCharHolder.Person.Kontostand = cH1_3.Person.Kontostand;
            ReturnCharHolder.Person.Lebesstil = cH1_3.Person.Lebesstil;
            ReturnCharHolder.Person.MetaTyp = cH1_3.Person.MetaTyp;
            ReturnCharHolder.Person.MetaTyp_sub = cH1_3.Person.MetaTyp_sub;
            ReturnCharHolder.Person.Notizen = cH1_3.Person.Notizen;
            ReturnCharHolder.Person.Runs = cH1_3.Person.Runs;
            ReturnCharHolder.Person.Schaden_G = cH1_3.Person.Schaden_G;
            ReturnCharHolder.Person.Schaden_G_max = cH1_3.Person.Schaden_G_max;
            ReturnCharHolder.Person.Schaden_K = cH1_3.Person.Schaden_K;
            ReturnCharHolder.Person.Schaden_K_max = cH1_3.Person.Schaden_K_max;
            ReturnCharHolder.Person.Schaden_M = cH1_3.Person.Schaden_M;
            ReturnCharHolder.Person.Schaden_M_max = cH1_3.Person.Schaden_M_max;
            ReturnCharHolder.Person.Zusammenfassung = cH1_3.Person.Zusammenfassung;
            ReturnCharHolder.RefreshThingList();

            foreach (var item in cH1_3.HandlungController)
            {
                var temp = ReturnCharHolder.CTRLHandlung.AddNewThing();
                temp.Bezeichner = item.Data.Bezeichner;
                temp.Ordnung = item.Data.Ordnung;
                temp.Typ = item.Data.Typ;
                temp.Zusatz = item.Data.Zusatz;
                temp.Notiz = item.Data.Notiz;
                //temp.Wert = item.Data.Wert;
                //temp.Gegen = item.Data.Gegen;
                //temp.Grenze = item.Data.Grenze;
                //temp.GegenZusammensetzung = ;
                foreach (var zusitem in item.Data.Zusammensetzung)
                {
                    var t = ReturnCharHolder.lstThings.Find(x => x.Key.Bezeichner == zusitem.Value.Bezeichner);
                    if (t.Key != null)
                    {
                        temp.WertZusammensetzung.Add(t);
                    }
                }
                foreach (var zusitem in item.Data.GegenZusammensetzung)
                {
                    var t = ReturnCharHolder.lstThings.Find(x => x.Key.Bezeichner == zusitem.Value.Bezeichner);
                    if (t.Key != null)
                    {
                        temp.GegenZusammensetzung.Add(t);
                    }
                }
                foreach (var zusitem in item.Data.GrenzeZusammensetzung)
                {
                    var t = ReturnCharHolder.lstThings.Find(x => x.Key.Bezeichner == zusitem.Value.Bezeichner);
                    if (t.Key != null)
                    {
                        temp.GrenzeZusammensetzung.Add(t);
                    }
                }
            }

            return ReturnCharHolder;
        }

    }
}
