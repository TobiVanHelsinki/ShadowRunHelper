
using System;

namespace ShadowRun_Charakter_Helper.Models
{
    class Store_Data
    {
        public static Boolean Store_Char(Models.Char SaveChar)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            // Container mit Current_Char_ID
            String Current_Char_Container_Name = "Char_ID_" + SaveChar.ID_Char.ToString();
            Windows.Storage.ApplicationDataContainer container = localSettings.CreateContainer(Current_Char_Container_Name, Windows.Storage.ApplicationDataCreateDisposition.Always);

            localSettings.Containers[Current_Char_Container_Name].Values["ID_Char"] = SaveChar.ID_Char;
            localSettings.Containers[Current_Char_Container_Name].Values["Alias"] = SaveChar.Alias;
            localSettings.Containers[Current_Char_Container_Name].Values["Char_Typ"] = SaveChar.Char_Typ;
            localSettings.Containers[Current_Char_Container_Name].Values["Kontostand"] = SaveChar.Kontostand;
            localSettings.Containers[Current_Char_Container_Name].Values["Karma_Gesamt"] = SaveChar.Karma_Gesamt;
            localSettings.Containers[Current_Char_Container_Name].Values["Karma_Aktuell"] = SaveChar.Karma_Aktuell;
            localSettings.Containers[Current_Char_Container_Name].Values["Edgne_Aktuell"] = SaveChar.Edgne_Aktuell;
            localSettings.Containers[Current_Char_Container_Name].Values["Edge_Gesamt"] = SaveChar.Edge_Gesamt;
            localSettings.Containers[Current_Char_Container_Name].Values["Essenz"] = SaveChar.Essenz;
            localSettings.Containers[Current_Char_Container_Name].Values["Schaden_K"] = SaveChar.Schaden_K;
            localSettings.Containers[Current_Char_Container_Name].Values["Schaden_G"] = SaveChar.Schaden_G;
            localSettings.Containers[Current_Char_Container_Name].Values["Schaden_M"] = SaveChar.Schaden_M;
            localSettings.Containers[Current_Char_Container_Name].Values["Schaden_K_max"] = SaveChar.Schaden_K_max;
            localSettings.Containers[Current_Char_Container_Name].Values["Schaden_G_max"] = SaveChar.Schaden_G_max;
            localSettings.Containers[Current_Char_Container_Name].Values["Schaden_M_max"] = SaveChar.Schaden_M_max;
            localSettings.Containers[Current_Char_Container_Name].Values["Notizen"] = SaveChar.Notizen;
            localSettings.Containers[Current_Char_Container_Name].Values["MetaTyp"] = SaveChar.MetaTyp;
            localSettings.Containers[Current_Char_Container_Name].Values["Lebesstil"] = SaveChar.Lebesstil;
            localSettings.Containers[Current_Char_Container_Name].Values["Geburtsdatum"] = SaveChar.Geburtsdatum;
            localSettings.Containers[Current_Char_Container_Name].Values["Geburtsdatum_Jahr"] = SaveChar.Geburtsdatum2.Year;
            localSettings.Containers[Current_Char_Container_Name].Values["Geburtsdatum_Monat"] = SaveChar.Geburtsdatum2.Month;
            localSettings.Containers[Current_Char_Container_Name].Values["Geburtsdatum_Tag"] = SaveChar.Geburtsdatum2.Day;
            localSettings.Containers[Current_Char_Container_Name].Values["Geburtsdatum"] = SaveChar.Geburtsdatum;
            localSettings.Containers[Current_Char_Container_Name].Values["Geschlecht"] = SaveChar.Geschlecht;
            localSettings.Containers[Current_Char_Container_Name].Values["Größe"] = SaveChar.Größe;
            localSettings.Containers[Current_Char_Container_Name].Values["Gewicht"] = SaveChar.Gewicht;
            localSettings.Containers[Current_Char_Container_Name].Values["Augenfarbe"] = SaveChar.Augenfarbe;
            localSettings.Containers[Current_Char_Container_Name].Values["Haarfarbe"] = SaveChar.Haarfarbe;
            localSettings.Containers[Current_Char_Container_Name].Values["Hautfarbe"] = SaveChar.Hautfarbe;
            localSettings.Containers[Current_Char_Container_Name].Values["Bild"] = SaveChar.Bild;
            localSettings.Containers[Current_Char_Container_Name].Values["Zusammenfassung"] = SaveChar.Zusammenfassung;
            localSettings.Containers[Current_Char_Container_Name].Values["Geschicklichkeit"] = (int)SaveChar.Geschicklichkeit;
            localSettings.Containers[Current_Char_Container_Name].Values["Reaktion"] = (int)SaveChar.Reaktion;
            localSettings.Containers[Current_Char_Container_Name].Values["Stärke"] = SaveChar.Stärke;
            localSettings.Containers[Current_Char_Container_Name].Values["Charisma"] = (int)SaveChar.Charisma;
            localSettings.Containers[Current_Char_Container_Name].Values["Intuition"] = (int)SaveChar.Intuition;
            localSettings.Containers[Current_Char_Container_Name].Values["Konstitution"] = (int)SaveChar.Konstitution;
            localSettings.Containers[Current_Char_Container_Name].Values["Logik"] = (int)SaveChar.Logik;
            localSettings.Containers[Current_Char_Container_Name].Values["Willenskraft"] = (int)SaveChar.Willenskraft;

            int i;
            //Char_Fertigkeiten
            i = 0;
            if (SaveChar.Char_Fertigkeiten != null)
            {
                for (i = 0; i < SaveChar.Char_Fertigkeiten.Count; i++) //ToDo: Surrender with try catch
                {
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fertigkeit_" + i + "_ID_Char_Fertigkeit"] = SaveChar.Char_Fertigkeiten[i].ID;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fertigkeit_" + i + "_Bezeichnung"] = SaveChar.Char_Fertigkeiten[i].Bezeichnung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fertigkeit_" + i + "_Stufe"] = SaveChar.Char_Fertigkeiten[i].Stufe;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fertigkeit_" + i + "_Anmerkung"] = SaveChar.Char_Fertigkeiten[i].Anmerkung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fertigkeit_" + i + "_Art"] = SaveChar.Char_Fertigkeiten[i].Art;
                }
                localSettings.Containers[Current_Char_Container_Name].Values["Char_Fertigkeiten_Count"] = i;
            }
            //Char_Fähigkeiten
            i = 0;
            if (SaveChar.Char_Fähigkeiten != null)
            {
                for (i = 0; i < SaveChar.Char_Fähigkeiten.Count; i++) //ToDo: Surrender with try catch
                {
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "_ID_Char_Fähigkeit"] = SaveChar.Char_Fähigkeiten[i].ID;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "_Bezeichnung"] = SaveChar.Char_Fähigkeiten[i].Bezeichnung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "_Zusammensetzung"] = SaveChar.Char_Fähigkeiten[i].Zusammensetzung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "_Anmerkung"] = SaveChar.Char_Fähigkeiten[i].Anmerkung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "_Pool_Calc"] = SaveChar.Char_Fähigkeiten[i].Pool_Calc;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "_Pool_Modifier"] = SaveChar.Char_Fähigkeiten[i].Pool_Modifier;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "_Pool_User"] = SaveChar.Char_Fähigkeiten[i].Pool_User;
                }
                localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeiten_Count"] = i;
            }

            //Char_Connections
            i = 0;
            if (SaveChar.Char_Connections != null)
            {

                for (i = 0; i < SaveChar.Char_Connections.Count; i++)
                {

                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Connection_" + i + "_ID_Char_Connection"] = SaveChar.Char_Connections[i].ID;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Connection_" + i + "_Name"] = SaveChar.Char_Connections[i].Name;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Connection_" + i + "_Loyal"] = SaveChar.Char_Connections[i].Loyal;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Connection_" + i + "_Connection"] = SaveChar.Char_Connections[i].Connection;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Connection_" + i + "_Anmerkung"] = SaveChar.Char_Connections[i].Anmerkung;
                }
                localSettings.Containers[Current_Char_Container_Name].Values["Char_Connections_Count"] = i;
            }
            //Char_Dronen_Fahrzeuge
            i = 0;
            if (SaveChar.Char_Dronen_Fahrzeuge != null)
            {

                for (i = 0; i < SaveChar.Char_Dronen_Fahrzeuge.Count; i++)
                {

                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_ID_Char_Drone_Fahrzeug"] = SaveChar.Char_Dronen_Fahrzeuge[i].ID;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Name"] = SaveChar.Char_Dronen_Fahrzeuge[i].Name;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Typ"] = SaveChar.Char_Dronen_Fahrzeuge[i].Typ;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Größe"] = SaveChar.Char_Dronen_Fahrzeuge[i].Größe;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Handling"] = SaveChar.Char_Dronen_Fahrzeuge[i].Handling;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Beschleunigung"] = SaveChar.Char_Dronen_Fahrzeuge[i].Beschleunigung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Gewicht"] = SaveChar.Char_Dronen_Fahrzeuge[i].Gewicht;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Pilot"] = SaveChar.Char_Dronen_Fahrzeuge[i].Pilot;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Rumpf"] = SaveChar.Char_Dronen_Fahrzeuge[i].Rumpf;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Panzer"] = SaveChar.Char_Dronen_Fahrzeuge[i].Panzer;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Anmerkung"] = SaveChar.Char_Dronen_Fahrzeuge[i].Anmerkung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Schaden"] = SaveChar.Char_Dronen_Fahrzeuge[i].Schaden;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Geschwindigkeit"] = SaveChar.Char_Dronen_Fahrzeuge[i].Geschwindigkeit;
                }
            }
            localSettings.Containers[Current_Char_Container_Name].Values["Char_Dronen_Fahrzeuge_Count"] = i;

            //Char_Fernkampfwaffen
            i = 0;
            if (SaveChar.Char_Fernkampfwaffen != null)
            {

                for (i = 0; i < SaveChar.Char_Fernkampfwaffen.Count; i++)
                {
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_ID_Char_Fernkampfwaffe"] = SaveChar.Char_Fernkampfwaffen[i].ID;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_Bezeichnung"] = SaveChar.Char_Fernkampfwaffen[i].Bezeichnung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_Schaden"] = SaveChar.Char_Fernkampfwaffen[i].Schaden;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_Schaden_Typ"] = SaveChar.Char_Fernkampfwaffen[i].Schaden_Typ;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_Modus"] = SaveChar.Char_Fernkampfwaffen[i].Modus;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_Rückstoß"] = SaveChar.Char_Fernkampfwaffen[i].Rückstoß;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_Munition"] = SaveChar.Char_Fernkampfwaffen[i].Munition;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_PB"] = SaveChar.Char_Fernkampfwaffen[i].PB;
                }
            }
            localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffen_Count"] = i;


            //Char_Implantate
            i = 0;
            if (SaveChar.Char_Implantate != null)
            {

                for (i = 0; i < SaveChar.Char_Implantate.Count; i++)
                {

                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Implantat_" + i + "_ID_Char_Implantat"] = SaveChar.Char_Implantate[i].ID;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Implantat_" + i + "_Bezeichnung"] = SaveChar.Char_Implantate[i].Bezeichnung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Implantat_" + i + "_Stufe"] = SaveChar.Char_Implantate[i].Stufe;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Implantat_" + i + "_Essenz"] = SaveChar.Char_Implantate[i].Essenz;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Implantat_" + i + "_Anmerkung"] = SaveChar.Char_Implantate[i].Anmerkung;
                }
            }
            localSettings.Containers[Current_Char_Container_Name].Values["Char_Implantate_Count"] = i;

            //Char_Items
            i = 0;
            if (SaveChar.Char_Items != null)
            {

                for (i = 0; i < SaveChar.Char_Items.Count; i++)
                {

                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Item_" + i + "_ID_Char_Item"] = SaveChar.Char_Items[i].ID;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Item_" + i + "_Bezeichnung"] = SaveChar.Char_Items[i].Bezeichnung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Item_" + i + "_Stufe"] = SaveChar.Char_Items[i].Stufe;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Item_" + i + "_Anmerkung"] = SaveChar.Char_Items[i].Anmerkung;
                }
            }
            localSettings.Containers[Current_Char_Container_Name].Values["Char_Items_Count"] = i;

            //Char_Kommlinks
            i = 0;
            if (SaveChar.Char_Kommlinks != null)
            {

                for (i = 0; i < SaveChar.Char_Kommlinks.Count; i++)
                {

                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_ID_Char_Kommlink"] = SaveChar.Char_Kommlinks[i].ID;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_Bezeichnung"] = SaveChar.Char_Kommlinks[i].Bezeichnung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_Stufe"] = SaveChar.Char_Kommlinks[i].Stufe;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_AnzahlProgramme"] = SaveChar.Char_Kommlinks[i].AnzahlProgramme;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_GrundKonfiguration"] = SaveChar.Char_Kommlinks[i].GrundKonfiguration;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_Angriff"] = SaveChar.Char_Kommlinks[i].Angriff;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_Schleicher"] = SaveChar.Char_Kommlinks[i].Schleicher;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_Firewall"] = SaveChar.Char_Kommlinks[i].Firewall;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_Datenverarbeitung"] = SaveChar.Char_Kommlinks[i].Datenverarbeitung;
                }
            }
            localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlinks_Count"] = i;

            //Char_Nachteile
            i = 0;
            if (SaveChar.Char_Nachteile != null)
            {

                for (i = 0; i < SaveChar.Char_Nachteile.Count; i++)
                {

                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Nachteil_" + i + "_ID_Char_Nachteil"] = SaveChar.Char_Nachteile[i].ID;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Nachteil_" + i + "_Bezeichnung"] = SaveChar.Char_Nachteile[i].Bezeichnung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Nachteil_" + i + "_Stufe"] = SaveChar.Char_Nachteile[i].Stufe;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Nachteil_" + i + "_Zusammensetzung"] = SaveChar.Char_Nachteile[i].Zusammensetzung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Nachteil_" + i + "_Auswirkungen"] = SaveChar.Char_Nachteile[i].Auswirkungen;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Nachteil_" + i + "_Anmerkungen"] = SaveChar.Char_Nachteile[i].Anmerkungen;
                }
            }
            localSettings.Containers[Current_Char_Container_Name].Values["Char_Nachteile_Count"] = i;

            //Char_Nahkampfwaffen
            i = 0;
            if (SaveChar.Char_Nahkampfwaffen != null)
            {

                for (i = 0; i < SaveChar.Char_Nahkampfwaffen.Count; i++)
                {

                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Nahkampfwaffe_" + i + "_ID_Char_Nahkampfwaffe"] = SaveChar.Char_Nahkampfwaffen[i].ID;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Nahkampfwaffe_" + i + "_Bezeichnung"] = SaveChar.Char_Nahkampfwaffen[i].Bezeichnung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Nahkampfwaffe_" + i + "_Schaden"] = SaveChar.Char_Nahkampfwaffen[i].Schaden;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Nahkampfwaffe_" + i + "_Schaden_Typ"] = SaveChar.Char_Nahkampfwaffen[i].Schaden_Typ;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Nahkampfwaffe_" + i + "_Reichweite"] = SaveChar.Char_Nahkampfwaffen[i].Reichweite;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Nahkampfwaffe_" + i + "_PB"] = SaveChar.Char_Nahkampfwaffen[i].PB;
                }
            }
            localSettings.Containers[Current_Char_Container_Name].Values["Char_Nahkampfwaffen_Count"] = i;

            //Char_Panzerungen
            i = 0;
            if (SaveChar.Char_Panzerungen != null)
            {

                for (i = 0; i < SaveChar.Char_Panzerungen.Count; i++)
                {

                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Panzerung_" + i + "_ID_Char_Panzerung"] = SaveChar.Char_Panzerungen[i].ID;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Panzerung_" + i + "_Bezeichnung"] = SaveChar.Char_Panzerungen[i].Bezeichnung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Panzerung_" + i + "_Ballistik"] = SaveChar.Char_Panzerungen[i].Ballistik;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Panzerung_" + i + "_Stoß"] = SaveChar.Char_Panzerungen[i].Stoß;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Panzerung_" + i + "_Anmerkung"] = SaveChar.Char_Panzerungen[i].Anmerkung;
                }
            }
            localSettings.Containers[Current_Char_Container_Name].Values["Char_Panzerungen_Count"] = i;

            //Char_Programme
            i = 0;
            if (SaveChar.Char_Programme != null)
            {

                for (i = 0; i < SaveChar.Char_Programme.Count; i++)
                {

                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Programm_" + i + "_ID_Char_Programm"] = SaveChar.Char_Programme[i].ID;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Programm_" + i + "_Bezeichnung"] = SaveChar.Char_Programme[i].Bezeichnung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Programm_" + i + "_Optionen"] = SaveChar.Char_Programme[i].Optionen;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Programm_" + i + "_Notizen"] = SaveChar.Char_Programme[i].Notizen;
                }
            }
            localSettings.Containers[Current_Char_Container_Name].Values["Char_Programme_Count"] = i;

            //Char_Sins
            i = 0;
            if (SaveChar.Char_Sins != null)
            {

                for (i = 0; i < SaveChar.Char_Sins.Count; i++)
                {
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_ID_Char_Sin"] = SaveChar.Char_Sins[i].ID;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_Name"] = SaveChar.Char_Sins[i].Name;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_Stufe"] = SaveChar.Char_Sins[i].Stufe;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_Verwendung"] = SaveChar.Char_Sins[i].Verwendung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_Geburtstag_Jahr"] = SaveChar.Char_Sins[i].Geburtstag.Year;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_Geburtstag_Monat"] = SaveChar.Char_Sins[i].Geburtstag.Month;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_Geburtstag_Tag"] = SaveChar.Char_Sins[i].Geburtstag.Day;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_Zusätze"] = SaveChar.Char_Sins[i].Zusätze;
                }
            }
            localSettings.Containers[Current_Char_Container_Name].Values["Char_Sins_Count"] = i;

            //Char_Vorteile
            i = 0;
            if (SaveChar.Char_Vorteile != null)
            {
                for (i = 0; i < SaveChar.Char_Vorteile.Count; i++)
                {

                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Vorteil_" + i + "_ID_Char_Vorteil"] = SaveChar.Char_Vorteile[i].ID;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Vorteil_" + i + "_Bezeichnung"] = SaveChar.Char_Vorteile[i].Bezeichnung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Vorteil_" + i + "_Stufe"] = SaveChar.Char_Vorteile[i].Stufe;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Vorteil_" + i + "_Zusammensetzung"] = SaveChar.Char_Vorteile[i].Zusammensetzung;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Vorteil_" + i + "_Auswirkungen"] = SaveChar.Char_Vorteile[i].Auswirkungen;
                    localSettings.Containers[Current_Char_Container_Name].Values["Char_Vorteil_" + i + "_Anmerkungen"] = SaveChar.Char_Vorteile[i].Anmerkungen;
                }
            }
            localSettings.Containers[Current_Char_Container_Name].Values["Char_Vorteile_Count"] = i;

            return true;
        }
        public static string Store_Char_to_String(Models.Char SaveChar)
        {
            String FileString = "";
            String Separator = "};;{";
            FileString += SaveChar.ID_Char;
            FileString += Separator;
            FileString += SaveChar.Alias;
            FileString += Separator;
            FileString += SaveChar.Char_Typ;
            FileString += Separator;
            FileString += SaveChar.Kontostand;
            FileString += Separator;
            FileString += SaveChar.Karma_Gesamt;
            FileString += Separator;
            FileString += SaveChar.Karma_Aktuell;
            FileString += Separator;
            FileString += SaveChar.Edgne_Aktuell;
            FileString += Separator;
            FileString += SaveChar.Edge_Gesamt;
            FileString += Separator;
            FileString += SaveChar.Essenz;
            FileString += Separator;
            FileString += SaveChar.Schaden_K;
            FileString += Separator;
            FileString += SaveChar.Schaden_G;
            FileString += Separator;
            FileString += SaveChar.Schaden_M;
            FileString += Separator;
            FileString += SaveChar.Schaden_K_max;
            FileString += Separator;
            FileString += SaveChar.Schaden_G_max;
            FileString += Separator;
            FileString += SaveChar.Schaden_M_max;
            FileString += Separator;
            FileString += SaveChar.Notizen;
            FileString += Separator;
            FileString += SaveChar.MetaTyp;
            FileString += Separator;
            FileString += SaveChar.Lebesstil;
            FileString += Separator;
            FileString += SaveChar.Geburtsdatum;
            FileString += Separator;
            FileString += SaveChar.Geburtsdatum2.Year;
            FileString += Separator;
            FileString += SaveChar.Geburtsdatum2.Month;
            FileString += Separator;
            FileString += SaveChar.Geburtsdatum2.Day;
            FileString += Separator;
            FileString += SaveChar.Geburtsdatum;
            FileString += Separator;
            FileString += SaveChar.Geschlecht;
            FileString += Separator;
            FileString += SaveChar.Größe;
            FileString += Separator;
            FileString += SaveChar.Gewicht;
            FileString += Separator;
            FileString += SaveChar.Augenfarbe;
            FileString += Separator;
            FileString += SaveChar.Haarfarbe;
            FileString += Separator;
            FileString += SaveChar.Hautfarbe;
            FileString += Separator;
            FileString += SaveChar.Bild;
            FileString += Separator;
            FileString += SaveChar.Zusammenfassung;
            FileString += Separator;
            FileString += (int)SaveChar.Geschicklichkeit;
            FileString += Separator;
            FileString += (int)SaveChar.Reaktion;
            FileString += Separator;
            FileString += SaveChar.Stärke;
            FileString += Separator;
            FileString += (int)SaveChar.Charisma;
            FileString += Separator;
            FileString += (int)SaveChar.Intuition;
            FileString += Separator;
            FileString += SaveChar.Konstitution;
            FileString += Separator;
            FileString += (int)SaveChar.Logik;
            FileString += Separator;
            FileString += (int)SaveChar.Willenskraft;

            int i;
            //Char_Fertigkeiten
            i = 0;
            if (SaveChar.Char_Fertigkeiten != null)
            {
                FileString += Separator;
                FileString += SaveChar.Char_Fertigkeiten.Count;
                for (i = 0; i < SaveChar.Char_Fertigkeiten.Count; i++) //ToDo: Surrender with try catch
                {
                    FileString += Separator;
                    FileString += SaveChar.Char_Fertigkeiten[i].ID;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fertigkeiten[i].Bezeichnung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fertigkeiten[i].Stufe;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fertigkeiten[i].Anmerkung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fertigkeiten[i].Art;
                }

            }
            //Char_Fähigkeiten
            i = 0;
            if (SaveChar.Char_Fähigkeiten != null)
            {
                FileString += Separator;
                FileString += SaveChar.Char_Fähigkeiten.Count;
                for (i = 0; i < SaveChar.Char_Fähigkeiten.Count; i++) //ToDo: Surrender with try catch
                {
                    FileString += Separator;
                    FileString += SaveChar.Char_Fähigkeiten[i].ID;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fähigkeiten[i].Bezeichnung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fähigkeiten[i].Zusammensetzung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fähigkeiten[i].Anmerkung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fähigkeiten[i].Pool_Calc;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fähigkeiten[i].Pool_Modifier;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fähigkeiten[i].Pool_User;
                }

            }

            //Char_Connections
            i = 0;
            if (SaveChar.Char_Connections != null)
            {
                FileString += Separator;
                FileString += SaveChar.Char_Connections.Count;
                for (i = 0; i < SaveChar.Char_Connections.Count; i++)
                {

                    FileString += Separator;
                    FileString += SaveChar.Char_Connections[i].ID;
                    FileString += Separator;
                    FileString += SaveChar.Char_Connections[i].Name;
                    FileString += Separator;
                    FileString += SaveChar.Char_Connections[i].Loyal;
                    FileString += Separator;
                    FileString += SaveChar.Char_Connections[i].Connection;
                    FileString += Separator;
                    FileString += SaveChar.Char_Connections[i].Anmerkung;
                }

            }
            //Char_Dronen_Fahrzeuge
            i = 0;
            if (SaveChar.Char_Dronen_Fahrzeuge != null)
            {
                FileString += Separator;
                FileString += SaveChar.Char_Dronen_Fahrzeuge.Count;
                for (i = 0; i < SaveChar.Char_Dronen_Fahrzeuge.Count; i++)
                {

                    FileString += Separator;
                    FileString += SaveChar.Char_Dronen_Fahrzeuge[i].ID;
                    FileString += Separator;
                    FileString += SaveChar.Char_Dronen_Fahrzeuge[i].Name;
                    FileString += Separator;
                    FileString += SaveChar.Char_Dronen_Fahrzeuge[i].Typ;
                    FileString += Separator;
                    FileString += SaveChar.Char_Dronen_Fahrzeuge[i].Größe;
                    FileString += Separator;
                    FileString += SaveChar.Char_Dronen_Fahrzeuge[i].Handling;
                    FileString += Separator;
                    FileString += SaveChar.Char_Dronen_Fahrzeuge[i].Beschleunigung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Dronen_Fahrzeuge[i].Gewicht;
                    FileString += Separator;
                    FileString += SaveChar.Char_Dronen_Fahrzeuge[i].Pilot;
                    FileString += Separator;
                    FileString += SaveChar.Char_Dronen_Fahrzeuge[i].Rumpf;
                    FileString += Separator;
                    FileString += SaveChar.Char_Dronen_Fahrzeuge[i].Panzer;
                    FileString += Separator;
                    FileString += SaveChar.Char_Dronen_Fahrzeuge[i].Anmerkung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Dronen_Fahrzeuge[i].Schaden;
                    FileString += Separator;
                    FileString += SaveChar.Char_Dronen_Fahrzeuge[i].Geschwindigkeit;
                }
            }
 

            //Char_Fernkampfwaffen
            i = 0;
            if (SaveChar.Char_Fernkampfwaffen != null)
            {
                FileString += Separator;
                FileString += SaveChar.Char_Fernkampfwaffen.Count;
                for (i = 0; i < SaveChar.Char_Fernkampfwaffen.Count; i++)
                {
                    FileString += Separator;
                    FileString += SaveChar.Char_Fernkampfwaffen[i].ID;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fernkampfwaffen[i].Bezeichnung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fernkampfwaffen[i].Schaden;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fernkampfwaffen[i].Schaden_Typ;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fernkampfwaffen[i].Modus;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fernkampfwaffen[i].Rückstoß;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fernkampfwaffen[i].Munition;
                    FileString += Separator;
                    FileString += SaveChar.Char_Fernkampfwaffen[i].PB;
                }
            }



            //Char_Implantate
            i = 0;
            if (SaveChar.Char_Implantate != null)
            {
                FileString += Separator;
                FileString += SaveChar.Char_Implantate.Count;
                for (i = 0; i < SaveChar.Char_Implantate.Count; i++)
                {
                    
                    FileString += Separator;
                    FileString += SaveChar.Char_Implantate[i].ID;
                    FileString += Separator;
                    FileString += SaveChar.Char_Implantate[i].Bezeichnung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Implantate[i].Stufe;
                    FileString += Separator;
                    FileString += SaveChar.Char_Implantate[i].Essenz;
                    FileString += Separator;
                    FileString += SaveChar.Char_Implantate[i].Anmerkung;
                }
            }

            //Char_Items
            i = 0;
            if (SaveChar.Char_Items != null)
            {
                FileString += Separator;
                FileString += SaveChar.Char_Items.Count;
                for (i = 0; i < SaveChar.Char_Items.Count; i++)
                {

                    FileString += Separator;
                    FileString += SaveChar.Char_Items[i].ID;
                    FileString += Separator;
                    FileString += SaveChar.Char_Items[i].Bezeichnung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Items[i].Stufe;
                    FileString += Separator;
                    FileString += SaveChar.Char_Items[i].Anmerkung;
                }
            }


            //Char_Kommlinks
            i = 0;
            if (SaveChar.Char_Kommlinks != null)
            {
                FileString += Separator;
                FileString += SaveChar.Char_Kommlinks.Count;
                for (i = 0; i < SaveChar.Char_Kommlinks.Count; i++)
                {

                    FileString += Separator;
                    FileString += SaveChar.Char_Kommlinks[i].ID;
                    FileString += Separator;
                    FileString += SaveChar.Char_Kommlinks[i].Bezeichnung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Kommlinks[i].Stufe;
                    FileString += Separator;
                    FileString += SaveChar.Char_Kommlinks[i].AnzahlProgramme;
                    FileString += Separator;
                    FileString += SaveChar.Char_Kommlinks[i].GrundKonfiguration;
                    FileString += Separator;
                    FileString += SaveChar.Char_Kommlinks[i].Angriff;
                    FileString += Separator;
                    FileString += SaveChar.Char_Kommlinks[i].Schleicher;
                    FileString += Separator;
                    FileString += SaveChar.Char_Kommlinks[i].Firewall;
                    FileString += Separator;
                    FileString += SaveChar.Char_Kommlinks[i].Datenverarbeitung;
                }
            }


            //Char_Nachteile
            i = 0;
            if (SaveChar.Char_Nachteile != null)
            {
                FileString += Separator;
                FileString += SaveChar.Char_Nachteile.Count;
                for (i = 0; i < SaveChar.Char_Nachteile.Count; i++)
                {

                    FileString += Separator;
                    FileString += SaveChar.Char_Nachteile[i].ID;
                    FileString += Separator;
                    FileString += SaveChar.Char_Nachteile[i].Bezeichnung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Nachteile[i].Stufe;
                    FileString += Separator;
                    FileString += SaveChar.Char_Nachteile[i].Zusammensetzung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Nachteile[i].Auswirkungen;
                    FileString += Separator;
                    FileString += SaveChar.Char_Nachteile[i].Anmerkungen;
                }
            }
           

            //Char_Nahkampfwaffen
            i = 0;
            if (SaveChar.Char_Nahkampfwaffen != null)
            {
                FileString += Separator;
                FileString += SaveChar.Char_Nahkampfwaffen.Count;
                for (i = 0; i < SaveChar.Char_Nahkampfwaffen.Count; i++)
                {

                    FileString += Separator;
                    FileString += SaveChar.Char_Nahkampfwaffen[i].ID;
                    FileString += Separator;
                    FileString += SaveChar.Char_Nahkampfwaffen[i].Bezeichnung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Nahkampfwaffen[i].Schaden;
                    FileString += Separator;
                    FileString += SaveChar.Char_Nahkampfwaffen[i].Schaden_Typ;
                    FileString += Separator;
                    FileString += SaveChar.Char_Nahkampfwaffen[i].Reichweite;
                    FileString += Separator;
                    FileString += SaveChar.Char_Nahkampfwaffen[i].PB;
                }
            }


            //Char_Panzerungen
            i = 0;
            if (SaveChar.Char_Panzerungen != null)
            {
                FileString += Separator;
                FileString += SaveChar.Char_Panzerungen.Count;
                for (i = 0; i < SaveChar.Char_Panzerungen.Count; i++)
                {

                    FileString += Separator;
                    FileString += SaveChar.Char_Panzerungen[i].ID;
                    FileString += Separator;
                    FileString += SaveChar.Char_Panzerungen[i].Bezeichnung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Panzerungen[i].Ballistik;
                    FileString += Separator;
                    FileString += SaveChar.Char_Panzerungen[i].Stoß;
                    FileString += Separator;
                    FileString += SaveChar.Char_Panzerungen[i].Anmerkung;
                }
            }


            //Char_Programme
            i = 0;
            if (SaveChar.Char_Programme != null)
            {
                FileString += Separator;
                FileString += SaveChar.Char_Programme.Count;
                for (i = 0; i < SaveChar.Char_Programme.Count; i++)
                {

                    FileString += Separator;
                    FileString += SaveChar.Char_Programme[i].ID;
                    FileString += Separator;
                    FileString += SaveChar.Char_Programme[i].Bezeichnung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Programme[i].Optionen;
                    FileString += Separator;
                    FileString += SaveChar.Char_Programme[i].Notizen;
                }
            }
  

            //Char_Sins
            i = 0;
            if (SaveChar.Char_Sins != null)
            {
                FileString += Separator;
                FileString += SaveChar.Char_Sins.Count;
                for (i = 0; i < SaveChar.Char_Sins.Count; i++)
                {
                    FileString += Separator;
                    FileString += SaveChar.Char_Sins[i].ID;
                    FileString += Separator;
                    FileString += SaveChar.Char_Sins[i].Name;
                    FileString += Separator;
                    FileString += SaveChar.Char_Sins[i].Stufe;
                    FileString += Separator;
                    FileString += SaveChar.Char_Sins[i].Verwendung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Sins[i].Geburtstag.Year;
                    FileString += Separator;
                    FileString += SaveChar.Char_Sins[i].Geburtstag.Month;
                    FileString += Separator;
                    FileString += SaveChar.Char_Sins[i].Geburtstag.Day;
                    FileString += Separator;
                    FileString += SaveChar.Char_Sins[i].Zusätze;
                }
            }


            //Char_Vorteile
            i = 0;
            if (SaveChar.Char_Vorteile != null)
            {
                FileString += Separator;
                FileString += SaveChar.Char_Vorteile.Count;
                for (i = 0; i < SaveChar.Char_Vorteile.Count; i++)
                {

                    FileString += Separator;
                    FileString += SaveChar.Char_Vorteile[i].ID;
                    FileString += Separator;
                    FileString += SaveChar.Char_Vorteile[i].Bezeichnung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Vorteile[i].Stufe;
                    FileString += Separator;
                    FileString += SaveChar.Char_Vorteile[i].Zusammensetzung;
                    FileString += Separator;
                    FileString += SaveChar.Char_Vorteile[i].Auswirkungen;
                    FileString += Separator;
                    FileString += SaveChar.Char_Vorteile[i].Anmerkungen;
                }
            }


            return FileString;
        }


    }
}

