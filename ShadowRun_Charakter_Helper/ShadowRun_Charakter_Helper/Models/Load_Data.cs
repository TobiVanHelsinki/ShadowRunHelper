//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ShadowRun_Charakter_Helper.Models
//{
//    class Load_Data
//    {

//        public static bool Load_Char(Char LoadChar)
//        {

//            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
//            //Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

//            //Char LoadChar = new Char();

//            // Container mit Current_Char_ID
//            String Current_Char_Container_Name = "Char_ID_" + LoadChar.ID_Char;
//            Windows.Storage.ApplicationDataContainer container = localSettings.CreateContainer(Current_Char_Container_Name, Windows.Storage.ApplicationDataCreateDisposition.Always);

//            LoadChar.Alias = (string)localSettings.Containers[Current_Char_Container_Name].Values["Alias"];
//            LoadChar.Char_Typ = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Typ"];
//            LoadChar.Kontostand = (double)localSettings.Containers[Current_Char_Container_Name].Values["Kontostand"];
//            LoadChar.Karma_Gesamt = (double)localSettings.Containers[Current_Char_Container_Name].Values["Karma_Gesamt"];
//            LoadChar.Karma_Aktuell = (double)localSettings.Containers[Current_Char_Container_Name].Values["Karma_Aktuell"];
//            LoadChar.Edgne_Aktuell = (double)localSettings.Containers[Current_Char_Container_Name].Values["Edgne_Aktuell"];
//            LoadChar.Edge_Gesamt = (double)localSettings.Containers[Current_Char_Container_Name].Values["Edge_Gesamt"];
//            LoadChar.Essenz = (double)localSettings.Containers[Current_Char_Container_Name].Values["Essenz"];
//            LoadChar.Schaden_K = (double)localSettings.Containers[Current_Char_Container_Name].Values["Schaden_K"];
//            LoadChar.Schaden_G = (double)localSettings.Containers[Current_Char_Container_Name].Values["Schaden_G"];
//            LoadChar.Schaden_M = (double)localSettings.Containers[Current_Char_Container_Name].Values["Schaden_M"];
//            LoadChar.Schaden_K_max = (double)localSettings.Containers[Current_Char_Container_Name].Values["Schaden_K_max"];
//            LoadChar.Schaden_G_max = (double)localSettings.Containers[Current_Char_Container_Name].Values["Schaden_G_max"];
//            LoadChar.Schaden_M_max = (double)localSettings.Containers[Current_Char_Container_Name].Values["Schaden_M_max"];
//            LoadChar.Notizen = (string)localSettings.Containers[Current_Char_Container_Name].Values["Notizen"];
//            LoadChar.MetaTyp = (string)localSettings.Containers[Current_Char_Container_Name].Values["MetaTyp"];
//            LoadChar.Lebesstil = (string)localSettings.Containers[Current_Char_Container_Name].Values["Lebesstil"];
            

//            try
//            {
//                LoadChar.Geburtsdatum2 = new DateTime(
//                    (int)localSettings.Containers[Current_Char_Container_Name].Values["Geburtsdatum_Jahr"],
//                    (int)localSettings.Containers[Current_Char_Container_Name].Values["Geburtsdatum_Monat"],
//                    (int)localSettings.Containers[Current_Char_Container_Name].Values["Geburtsdatum_Tag"]);
//            }
//            catch (NullReferenceException)
//            {

//            }
//            LoadChar.Geburtsdatum = (string)localSettings.Containers[Current_Char_Container_Name].Values["Geburtsdatum"];
//            LoadChar.Geschlecht = (string)localSettings.Containers[Current_Char_Container_Name].Values["Geschlecht"];
//            LoadChar.Größe = (double)localSettings.Containers[Current_Char_Container_Name].Values["Größe"];
//            LoadChar.Gewicht = (double)localSettings.Containers[Current_Char_Container_Name].Values["Gewicht"];
//            LoadChar.Augenfarbe = (string)localSettings.Containers[Current_Char_Container_Name].Values["Augenfarbe"];
//            LoadChar.Haarfarbe = (string)localSettings.Containers[Current_Char_Container_Name].Values["Haarfarbe"];
//            LoadChar.Hautfarbe = (string)localSettings.Containers[Current_Char_Container_Name].Values["Hautfarbe"];
//            LoadChar.Bild = (string)localSettings.Containers[Current_Char_Container_Name].Values["Bild"];
//            LoadChar.Zusammenfassung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Zusammenfassung"];
//            try
//            {
//                LoadChar.Runs = (double)localSettings.Containers[Current_Char_Container_Name].Values["Runs"];
//            }
//            catch (Exception)
//            {

         
//            }
            
//            int i;

//            //Char_Fertigkeiten
//            i = 0;
//            LoadChar.Char_Fertigkeiten = null;
//            LoadChar.Char_Fertigkeiten = new System.Collections.ObjectModel.ObservableCollection<Char_Fertigkeit>();
//            for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fertigkeiten_Count"]; i++)
//            {
//                Char_Fertigkeit Char_Fertigkeiten_Temp = new Char_Fertigkeit();
//                Char_Fertigkeiten_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fertigkeit_" + i + "_ID_Char_Fertigkeit"];
//                Char_Fertigkeiten_Temp.Bezeichnung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fertigkeit_" + i + "_Bezeichnung"];
//                Char_Fertigkeiten_Temp.Stufe = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fertigkeit_" + i + "_Stufe"];
//                Char_Fertigkeiten_Temp.Anmerkung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fertigkeit_" + i + "_Anmerkung"];
//                Char_Fertigkeiten_Temp.Art = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fertigkeit_" + i + "_Art"];

//                LoadChar.Char_Fertigkeiten.Add(Char_Fertigkeiten_Temp);
//            }
//            //Char_Fähigkeiten
//            i = 0;
//            LoadChar.Char_Fähigkeiten = null;
//            LoadChar.Char_Fähigkeiten = new System.Collections.ObjectModel.ObservableCollection<Char_Fähigkeit>();
//            for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeiten_Count"]; i++)
//            {
//                Char_Fähigkeit Char_Fähigkeiten_Temp = new Char_Fähigkeit(LoadChar.Char_Fähigkeiten);
//                Char_Fähigkeiten_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "_ID_Char_Fähigkeit"];
//                Char_Fähigkeiten_Temp.Bezeichnung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "_Bezeichnung"];
//                Char_Fähigkeiten_Temp.Zusammensetzung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "_Zusammensetzung"];
//                Char_Fähigkeiten_Temp.Anmerkung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "_Anmerkung"];
//                Char_Fähigkeiten_Temp.Pool_Calc = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "_Pool_Calc"];
//                Char_Fähigkeiten_Temp.Pool_Modifier = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "_Pool_Modifier"];
//                Char_Fähigkeiten_Temp.Pool_User = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "_Pool_User"];

//            //    k = 0;
//            //    Char_Fähigkeiten_Temp.Zusammensetzung_F = new List<int>();
//            //    try { 
//            //        for (k = 0; k < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "Zusammensetzung_F_count"]; k++)
//            //        {
//            //            Char_Fähigkeiten_Temp.Zusammensetzung_F.Add((int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "Zusammensetzung_F" + k]);
//            //        }
//            //        k = 0;
//            //        Char_Fähigkeiten_Temp.Zusammensetzung_A = new List<int>();
//            //        for (k = 0; k < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "Zusammensetzung_A_count"]; k++)
//            //        {
//            //            Char_Fähigkeiten_Temp.Zusammensetzung_A.Add((int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fähigkeit_" + i + "Zusammensetzung_A" + k]);
//            //        }
//            //    }
//            //    catch (Exception) { }
//                LoadChar.Char_Fähigkeiten.Add(Char_Fähigkeiten_Temp);
//            }
//            //Char_Connections
//            i = 0;
//            LoadChar.Char_Connections = null;
//            LoadChar.Char_Connections = new System.Collections.ObjectModel.ObservableCollection<Char_Connection>();
//            for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Connections_Count"]; i++)
//            {
//                Char_Connection Char_Connections_Temp = new Char_Connection();
//                Char_Connections_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Connection_" + i + "_ID_Char_Connection"];
//                Char_Connections_Temp.Name = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Connection_" + i + "_Name"];
//                Char_Connections_Temp.Loyal = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Connection_" + i + "_Loyal"];
//                Char_Connections_Temp.Connection = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Connection_" + i + "_Connection"];
//                Char_Connections_Temp.Anmerkung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Connection_" + i + "_Anmerkung"];
//                LoadChar.Char_Connections.Add(Char_Connections_Temp);
//            }
//            //Char_Dronen_Fahrzeuge
//            i = 0;
//            LoadChar.Char_Dronen_Fahrzeuge = null;
//            LoadChar.Char_Dronen_Fahrzeuge = new System.Collections.ObjectModel.ObservableCollection<Char_Drone_Fahrzeug>();
//            for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Dronen_Fahrzeuge_Count"]; i++)
//            {
//                Char_Drone_Fahrzeug Char_Dronen_Fahrzeuge_Temp = new Char_Drone_Fahrzeug();
//                Char_Dronen_Fahrzeuge_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_ID_Char_Drone_Fahrzeug"];
//                Char_Dronen_Fahrzeuge_Temp.Name = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Name"];
//                Char_Dronen_Fahrzeuge_Temp.Typ = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Typ"];
//                Char_Dronen_Fahrzeuge_Temp.Größe = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Größe"];
//                Char_Dronen_Fahrzeuge_Temp.Handling = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Handling"];
//                Char_Dronen_Fahrzeuge_Temp.Beschleunigung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Beschleunigung"];
//                Char_Dronen_Fahrzeuge_Temp.Gewicht = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Gewicht"];
//                Char_Dronen_Fahrzeuge_Temp.Pilot = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Pilot"];
//                Char_Dronen_Fahrzeuge_Temp.Rumpf = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Rumpf"];
//                Char_Dronen_Fahrzeuge_Temp.Panzer = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Panzer"];
//                Char_Dronen_Fahrzeuge_Temp.Anmerkung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Anmerkung"];
//                Char_Dronen_Fahrzeuge_Temp.Schaden = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Schaden"];
//                Char_Dronen_Fahrzeuge_Temp.Geschwindigkeit = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Drone_Fahrzeug_" + i + "_Geschwindigkeit"];
//                LoadChar.Char_Dronen_Fahrzeuge.Add(Char_Dronen_Fahrzeuge_Temp);
//            }
//            //Char_Fernkampfwaffen
//            i = 0;
//            LoadChar.Char_Fernkampfwaffen = null;
//            LoadChar.Char_Fernkampfwaffen = new System.Collections.ObjectModel.ObservableCollection<Char_Fernkampfwaffe>();
//            for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffen_Count"]; i++)
//            {
//                Char_Fernkampfwaffe Char_Fernkampfwaffen_Temp = new Char_Fernkampfwaffe();
//                Char_Fernkampfwaffen_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_ID_Char_Fernkampfwaffe"];
//                Char_Fernkampfwaffen_Temp.Bezeichnung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_Bezeichnung"];
//                Char_Fernkampfwaffen_Temp.Schaden = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_Schaden"];
//                Char_Fernkampfwaffen_Temp.Schaden_Typ = (char)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_Schaden_Typ"];
//                Char_Fernkampfwaffen_Temp.Modus = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_Modus"];
//                Char_Fernkampfwaffen_Temp.Rückstoß = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_Rückstoß"];
//                Char_Fernkampfwaffen_Temp.Munition = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_Munition"];
//                Char_Fernkampfwaffen_Temp.PB = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Fernkampfwaffe_" + i + "_PB"];
//                LoadChar.Char_Fernkampfwaffen.Add(Char_Fernkampfwaffen_Temp);
//            }
//            //Char_Implantate
//            i = 0;
//            LoadChar.Char_Implantate = null;
//            LoadChar.Char_Implantate = new System.Collections.ObjectModel.ObservableCollection<Char_Implantat>();
//            for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Implantate_Count"]; i++)
//            {
//                Char_Implantat Char_Implantate_Temp = new Char_Implantat();
//                Char_Implantate_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Implantat_" + i + "_ID_Char_Implantat"];
//                Char_Implantate_Temp.Bezeichnung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Implantat_" + i + "_Bezeichnung"];
//                Char_Implantate_Temp.Stufe = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Implantat_" + i + "_Stufe"];
//                Char_Implantate_Temp.Essenz = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Implantat_" + i + "_Essenz"];
//                Char_Implantate_Temp.Anmerkung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Implantat_" + i + "_Anmerkung"];
//                LoadChar.Char_Implantate.Add(Char_Implantate_Temp);
//            }
//            //Char_Items
//            i = 0;
//            LoadChar.Char_Items = null;
//            LoadChar.Char_Items = new System.Collections.ObjectModel.ObservableCollection<Char_Item>();
//            for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Items_Count"]; i++)
//            {
//                Char_Item Char_Items_Temp = new Char_Item();
//                Char_Items_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Item_" + i + "_ID_Char_Item"];
//                Char_Items_Temp.Bezeichnung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Item_" + i + "_Bezeichnung"];
//                Char_Items_Temp.Stufe = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Item_" + i + "_Stufe"];
//                Char_Items_Temp.Anmerkung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Item_" + i + "_Anmerkung"];
//                LoadChar.Char_Items.Add(Char_Items_Temp);
//            }
//            //Char_Kommlinks
//            i = 0;
//            LoadChar.Char_Kommlinks = null;
//            LoadChar.Char_Kommlinks = new System.Collections.ObjectModel.ObservableCollection<Char_Kommlink>();
//            for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlinks_Count"]; i++)
//            {
//                Char_Kommlink Char_Kommlinks_Temp = new Char_Kommlink();
//                Char_Kommlinks_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_ID_Char_Kommlink"];
//                Char_Kommlinks_Temp.Bezeichnung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_Bezeichnung"];
//                Char_Kommlinks_Temp.Stufe = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_Stufe"];
//                Char_Kommlinks_Temp.AnzahlProgramme = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_AnzahlProgramme"];
//                Char_Kommlinks_Temp.GrundKonfiguration = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_GrundKonfiguration"];
//                Char_Kommlinks_Temp.Angriff = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_Angriff"];
//                Char_Kommlinks_Temp.Schleicher = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_Schleicher"];
//                Char_Kommlinks_Temp.Firewall = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_Firewall"];
//                Char_Kommlinks_Temp.Datenverarbeitung = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Kommlink_" + i + "_Datenverarbeitung"];
//                LoadChar.Char_Kommlinks.Add(Char_Kommlinks_Temp);
//            }
//            //Char_Nachteile
//            i = 0;
//            LoadChar.Char_Nachteile = null;
//            LoadChar.Char_Nachteile = new System.Collections.ObjectModel.ObservableCollection<Char_Nachteil>();
//            for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Nachteile_Count"]; i++)
//            {
//                Char_Nachteil Char_Nachteile_Temp = new Char_Nachteil();
//                Char_Nachteile_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Nachteil_" + i + "_ID_Char_Nachteil"];
//                Char_Nachteile_Temp.Bezeichnung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Nachteil_" + i + "_Bezeichnung"];
//                Char_Nachteile_Temp.Stufe = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Nachteil_" + i + "_Stufe"];
//                Char_Nachteile_Temp.Zusammensetzung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Nachteil_" + i + "_Zusammensetzung"];
//                Char_Nachteile_Temp.Auswirkungen = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Nachteil_" + i + "_Auswirkung"];
//                Char_Nachteile_Temp.Anmerkungen = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Nachteil_" + i + "_Anmerkung"];
//                LoadChar.Char_Nachteile.Add(Char_Nachteile_Temp);
//            }
//            //Char_Nahkampfwaffen
//            i = 0;
//            LoadChar.Char_Nahkampfwaffen = null;
//            LoadChar.Char_Nahkampfwaffen = new System.Collections.ObjectModel.ObservableCollection<Char_Nahkampfwaffe>();
//            for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Nahkampfwaffen_Count"]; i++)
//            {
//                Char_Nahkampfwaffe Char_Nahkampfwaffen_Temp = new Char_Nahkampfwaffe();
//                Char_Nahkampfwaffen_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Nahkampfwaffe_" + i + "_ID_Char_Nahkampfwaffe"];
//                Char_Nahkampfwaffen_Temp.Bezeichnung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Nahkampfwaffe_" + i + "_Bezeichnung"];
//                Char_Nahkampfwaffen_Temp.Schaden = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Nahkampfwaffe_" + i + "_Schaden"];
//                Char_Nahkampfwaffen_Temp.Schaden_Typ = (char)localSettings.Containers[Current_Char_Container_Name].Values["Char_Nahkampfwaffe_" + i + "_Schaden_Typ"];
//                Char_Nahkampfwaffen_Temp.Reichweite = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Nahkampfwaffe_" + i + "_Reichweite"];
//                Char_Nahkampfwaffen_Temp.PB = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Nahkampfwaffe_" + i + "_PB"];
//                LoadChar.Char_Nahkampfwaffen.Add(Char_Nahkampfwaffen_Temp);
//            }
//            //Char_Panzerungen
//            i = 0;
//            LoadChar.Char_Panzerungen = null;
//            LoadChar.Char_Panzerungen = new System.Collections.ObjectModel.ObservableCollection<Char_Panzerung>();
//            for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Panzerungen_Count"]; i++)
//            {
//                Char_Panzerung Char_Panzerungen_Temp = new Char_Panzerung();
//                Char_Panzerungen_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Panzerung_" + i + "_ID_Char_Panzerung"];
//                Char_Panzerungen_Temp.Bezeichnung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Panzerung_" + i + "_Bezeichnung"];
//                Char_Panzerungen_Temp.Ballistik = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Panzerung_" + i + "_Ballistik"];
//                Char_Panzerungen_Temp.Stoß = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Panzerung_" + i + "_Stoß"];
//                Char_Panzerungen_Temp.Anmerkung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Panzerung_" + i + "_Anmerkung"];
//                LoadChar.Char_Panzerungen.Add(Char_Panzerungen_Temp);
//            }
//            //Char_Programme
//            i = 0;
//            LoadChar.Char_Programme = null;
//            LoadChar.Char_Programme = new System.Collections.ObjectModel.ObservableCollection<Char_Programm>();
//            for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Programme_Count"]; i++)
//            {
//                Char_Programm Char_Programme_Temp = new Char_Programm();
//                Char_Programme_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Programm_" + i + "_ID_Char_Programm"];
//                Char_Programme_Temp.Bezeichnung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Programm_" + i + "_Bezeichnung"];
//                Char_Programme_Temp.Optionen = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Programm_" + i + "_Option"];
//                Char_Programme_Temp.Notizen = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Programm_" + i + "_Notizen"];
//                LoadChar.Char_Programme.Add(Char_Programme_Temp);
//            }
//            //Char_Sins
//            i = 0;
//            LoadChar.Char_Sins = null;
//            LoadChar.Char_Sins = new System.Collections.ObjectModel.ObservableCollection<Char_Sin>();
//            for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Sins_Count"]; i++)
//            {
//                Char_Sin Char_Sins_Temp = new Char_Sin();
//                Char_Sins_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_ID_Char_Sin"];
//                Char_Sins_Temp.Name = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_Name"];
//                Char_Sins_Temp.Stufe = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_Stufe"];
//                Char_Sins_Temp.Verwendung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_Verwendung"];
//                Char_Sins_Temp.Geburtstag = new DateTime(
//                    (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_Geburtstag_Jahr"],
//                    (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_Geburtstag_Monat"],
//                    (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_Geburtstag_Tag"]);
//                Char_Sins_Temp.Zusätze = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Sin_" + i + "_Zusätze"];
//                LoadChar.Char_Sins.Add(Char_Sins_Temp);
//            }
//            //Char_Vorteile
//            i = 0;
//            LoadChar.Char_Vorteile = null;
//            LoadChar.Char_Vorteile = new System.Collections.ObjectModel.ObservableCollection<Char_Vorteil>();
//            for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Vorteile_Count"]; i++)
//            {
//                Char_Vorteil Char_Vorteile_Temp = new Char_Vorteil();
//                Char_Vorteile_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Vorteil_" + i + "_ID_Char_Vorteil"];
//                Char_Vorteile_Temp.Bezeichnung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Vorteil_" + i + "_Bezeichnung"];
//                Char_Vorteile_Temp.Stufe = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Vorteil_" + i + "_Stufe"];
//                Char_Vorteile_Temp.Zusammensetzung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Vorteil_" + i + "_Zusammensetzung"];
//                Char_Vorteile_Temp.Auswirkungen = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Vorteil_" + i + "_Auswirkung"];
//                Char_Vorteile_Temp.Anmerkungen = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Vorteil_" + i + "_Anmerkungen"];
//                LoadChar.Char_Vorteile.Add(Char_Vorteile_Temp);
//            }
//            //Char_Attribute
//            try { 
//                i = 0;
//                LoadChar.Char_Attribute = null;
//                LoadChar.Char_Attribute = new System.Collections.ObjectModel.ObservableCollection<Char_Attribut>();
//                for (i = 0; i < (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Attribute_Count"]; i++)
//                {
//                    Char_Attribut Char_Attribute_Temp = new Char_Attribut();
//                    Char_Attribute_Temp.ID = (int)localSettings.Containers[Current_Char_Container_Name].Values["Char_Attribut_" + i + "_ID_Char_Vorteil"];
//                    Char_Attribute_Temp.Bezeichnung = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Attribut_" + i + "_Bezeichnung"];
//                    Char_Attribute_Temp.Stufe = (double)localSettings.Containers[Current_Char_Container_Name].Values["Char_Attribut_" + i + "_Stufe"];
//                    Char_Attribute_Temp.Stufe_Modifier = (string)localSettings.Containers[Current_Char_Container_Name].Values["Char_Attribut_" + i + "_Stufe_Modifier"];
               
//                    LoadChar.Char_Attribute.Add(Char_Attribute_Temp);
//                }
//            }
//            catch (NullReferenceException)
//            {

//            }

//            //Char_Fähigkeit.Pool_Berechnen(LoadChar);
//            return true;
//        }

//        public static void Load_Char_from_String(string fileContent, Char LoadChar)
//        {
//            string[] stringSeparators = new string[] { "};;{" };
//            string[] result = fileContent.Split(stringSeparators, StringSplitOptions.None);
//            int i = 0;
//            int k = 0;

//            //LoadChar.ID_Char = Int32.Parse(result[k]);
//            k++;
//            LoadChar.Alias = result[k];
//            k++;
//            LoadChar.Char_Typ = result[k];
//            k++;
//            LoadChar.Kontostand = Int32.Parse(result[k]);
//            k++;
//            LoadChar.Karma_Gesamt = Int32.Parse(result[k]);
//            k++;
//            LoadChar.Karma_Aktuell = Int32.Parse(result[k]);
//            k++;
//            LoadChar.Edgne_Aktuell = Int32.Parse(result[k]);
//            k++;
//            LoadChar.Edge_Gesamt = Int32.Parse(result[k]);
//            k++;
//            LoadChar.Essenz = Int32.Parse(result[k]);
//            k++;
//            LoadChar.Schaden_K = Int32.Parse(result[k]);
//            k++;
//            LoadChar.Schaden_G = Int32.Parse(result[k]);
//            k++;
//            LoadChar.Schaden_M = Int32.Parse(result[k]);
//            k++;
//            LoadChar.Schaden_K_max = Int32.Parse(result[k]);
//            k++;
//            LoadChar.Schaden_G_max = Int32.Parse(result[k]);
//            k++;
//            LoadChar.Schaden_M_max = Int32.Parse(result[k]);
//            k++;
//            LoadChar.Notizen = result[k];
//            k++;
//            LoadChar.MetaTyp = result[k];
//            k++;
//            LoadChar.Lebesstil = result[k];
//            k++;
//            LoadChar.Geburtsdatum = result[k];
//            k++;
//            LoadChar.Geburtsdatum2 = new DateTime(Int32.Parse(result[k]), Int32.Parse(result[k + 1]), Int32.Parse(result[k + 2]));
//            k++;
//            k++;
//            k++;
//            LoadChar.Geburtsdatum = (result[k]);
//            k++;
//            LoadChar.Geschlecht = (result[k]);
//            k++;
//            LoadChar.Größe = Int32.Parse(result[k]);
//            k++;
//            LoadChar.Gewicht = Int32.Parse(result[k]);
//            k++;
//            LoadChar.Augenfarbe = (result[k]);
//            k++;
//            LoadChar.Haarfarbe = (result[k]);
//            k++;
//            LoadChar.Hautfarbe = (result[k]);
//            k++;
//            LoadChar.Bild = (result[k]);
//            k++;
//            LoadChar.Zusammenfassung = (result[k]);
//            k++;
//            //LoadChar.Geschicklichkeit = Int32.Parse(result[k]);
//            k++;
//            //LoadChar.Reaktion = Int32.Parse(result[k]);
//            k++;
//            //LoadChar.Stärke = Int32.Parse(result[k]);
//            k++;
//            //LoadChar.Charisma = Int32.Parse(result[k]);
//            k++;
//            //LoadChar.Intuition = Int32.Parse(result[k]);
//            k++;
//            //LoadChar.Konstitution = Int32.Parse(result[k]);
//            k++;
//            //LoadChar.Logik = Int32.Parse(result[k]);
//            k++;
//            //LoadChar.Willenskraft = Int32.Parse(result[k]);
//            k++;
//            int iteration_temp = 0;

//            //Char_Fertigkeiten

//            if (result[k] != null || result[k] != "")
//            {
//                iteration_temp = Int32.Parse(result[k]);
//                k++;
//                for (i = 0; i < iteration_temp; i++) //ToDo: Surrender with try catch
//                {
//                    Char_Fertigkeit Char_Fertigkeiten_Temp = new Char_Fertigkeit();
//                    Char_Fertigkeiten_Temp.ID = Int32.Parse(result[k]);
//                    k++;
//                    Char_Fertigkeiten_Temp.Bezeichnung = (result[k]);
//                    k++;
//                    Char_Fertigkeiten_Temp.Stufe = Int32.Parse(result[k]);
//                    k++;
//                    Char_Fertigkeiten_Temp.Anmerkung = (result[k]);
//                    k++;
//                    Char_Fertigkeiten_Temp.Art = (result[k]);
//                    k++;
//                    LoadChar.Char_Fertigkeiten.Add(Char_Fertigkeiten_Temp);
//                }

//            }
//            //Char_Fähigkeiten

//            if (result[k] != null || result[k] != "")
//            {
//                iteration_temp = Int32.Parse(result[k]);
//                k++;
//                for (i = 0; i < iteration_temp; i++) //ToDo: Surrender with try catch
//                {
//                    Char_Fähigkeit Char_Fähigkeiten_Temp = new Char_Fähigkeit(LoadChar.Char_Fähigkeiten);
//                    Char_Fähigkeiten_Temp.ID = Int32.Parse(result[k]);
//                    k++;
//                    Char_Fähigkeiten_Temp.Bezeichnung = (result[k]);
//                    k++;
//                    Char_Fähigkeiten_Temp.Zusammensetzung = (result[k]);
//                    k++;
//                    Char_Fähigkeiten_Temp.Anmerkung = (result[k]);
//                    k++;
//                    Char_Fähigkeiten_Temp.Pool_Calc = Int32.Parse(result[k]);
//                    k++;
//                    Char_Fähigkeiten_Temp.Pool_Modifier = (result[k]);
//                    k++;
//                    Char_Fähigkeiten_Temp.Pool_User = Int32.Parse(result[k]);
//                    k++;
//                    LoadChar.Char_Fähigkeiten.Add(Char_Fähigkeiten_Temp);
//                }

//            }

//            //Char_Connections

//            if (result[k] != null || result[k] != "")
//            {
//                iteration_temp = Int32.Parse(result[k]);
//                k++;
//                for (i = 0; i < iteration_temp; i++) //ToDo: Surrender with try catch
//                {

//                    Char_Connection Char_Connections_Temp = new Char_Connection();
//                    Char_Connections_Temp.ID = Int32.Parse(result[k]);
//                    k++;
//                    Char_Connections_Temp.Name = (result[k]);
//                    k++;
//                    Char_Connections_Temp.Loyal = Int32.Parse(result[k]);
//                    k++;
//                    Char_Connections_Temp.Connection = Int32.Parse(result[k]);
//                    k++;
//                    Char_Connections_Temp.Anmerkung = (result[k]);
//                    k++;
//                    LoadChar.Char_Connections.Add(Char_Connections_Temp);
//                }

//            }
//            //Char_Dronen_Fahrzeuge

//            if (result[k] != null || result[k] != "")
//            {
//                iteration_temp = Int32.Parse(result[k]);
//                k++;
//                for (i = 0; i < iteration_temp; i++) //ToDo: Surrender with try catch
//                {

//                    Char_Drone_Fahrzeug Char_Dronen_Fahrzeuge_Temp = new Char_Drone_Fahrzeug();
//                    Char_Dronen_Fahrzeuge_Temp.ID = Int32.Parse(result[k]);
//                    k++;
//                    Char_Dronen_Fahrzeuge_Temp.Name = (result[k]);
//                    k++;
//                    Char_Dronen_Fahrzeuge_Temp.Typ = (result[k]);
//                    k++;
//                    Char_Dronen_Fahrzeuge_Temp.Größe = (result[k]);
//                    k++;
//                    Char_Dronen_Fahrzeuge_Temp.Handling = (result[k]);
//                    k++;
//                    Char_Dronen_Fahrzeuge_Temp.Beschleunigung = (result[k]);
//                    k++;
//                    Char_Dronen_Fahrzeuge_Temp.Gewicht = (result[k]);
//                    k++;
//                    Char_Dronen_Fahrzeuge_Temp.Pilot = (result[k]);
//                    k++;
//                    Char_Dronen_Fahrzeuge_Temp.Rumpf = (result[k]);
//                    k++;
//                    Char_Dronen_Fahrzeuge_Temp.Panzer = (result[k]);
//                    k++;
//                    Char_Dronen_Fahrzeuge_Temp.Anmerkung = (result[k]);
//                    k++;
//                    Char_Dronen_Fahrzeuge_Temp.Schaden = (result[k]);
//                    k++;
//                    Char_Dronen_Fahrzeuge_Temp.Geschwindigkeit = (result[k]);
//                    k++;
//                    LoadChar.Char_Dronen_Fahrzeuge.Add(Char_Dronen_Fahrzeuge_Temp);
//                }
//            }


//            //Char_Fernkampfwaffen

//            if (result[k] != null || result[k] != "")
//            {
//                iteration_temp = Int32.Parse(result[k]);
//                k++;
//                for (i = 0; i < iteration_temp; i++) //ToDo: Surrender with try catch
//                {
//                    Char_Fernkampfwaffe Char_Fernkampfwaffen_Temp = new Char_Fernkampfwaffe();
//                    Char_Fernkampfwaffen_Temp.ID = Int32.Parse(result[k]);
//                    k++;
//                    Char_Fernkampfwaffen_Temp.Bezeichnung = (result[k]);
//                    k++;
//                    Char_Fernkampfwaffen_Temp.Schaden = Int32.Parse(result[k]);
//                    k++;
//                    Char_Fernkampfwaffen_Temp.Schaden_Typ = char.Parse(result[k]);
//                    k++;
//                    Char_Fernkampfwaffen_Temp.Modus = (result[k]);
//                    k++;
//                    Char_Fernkampfwaffen_Temp.Rückstoß = Double.Parse(result[k]);
//                    k++;
//                    Char_Fernkampfwaffen_Temp.Munition = (result[k]);
//                    k++;
//                    Char_Fernkampfwaffen_Temp.PB = (result[k]);
//                    k++;
//                    LoadChar.Char_Fernkampfwaffen.Add(Char_Fernkampfwaffen_Temp);
//                }
//            }



//            //Char_Implantate

//            if (result[k] != null || result[k] != "")
//            {
//                iteration_temp = Int32.Parse(result[k]);
//                k++;
//                for (i = 0; i < iteration_temp; i++) //ToDo: Surrender with try catch
//                {

//                    Char_Implantat Char_Implantate_Temp = new Char_Implantat();
//                    Char_Implantate_Temp.ID = Int32.Parse(result[k]);
//                    k++;
//                    Char_Implantate_Temp.Bezeichnung = (result[k]);
//                    k++;
//                    Char_Implantate_Temp.Stufe = Int32.Parse(result[k]);
//                    k++;
//                    Char_Implantate_Temp.Essenz = Int32.Parse(result[k]);
//                    k++;
//                    Char_Implantate_Temp.Anmerkung = (result[k]);
//                    k++;
//                    LoadChar.Char_Implantate.Add(Char_Implantate_Temp);
//                }
//            }

//            //Char_Items

//            if (result[k] != null || result[k] != "")
//            {
//                iteration_temp = Int32.Parse(result[k]);
//                k++;
//                for (i = 0; i < iteration_temp; i++) //ToDo: Surrender with try catch
//                {

//                    Char_Item Char_Items_Temp = new Char_Item();
//                    Char_Items_Temp.ID = Int32.Parse(result[k]);
//                    k++;
//                    Char_Items_Temp.Bezeichnung = (result[k]);
//                    k++;
//                    Char_Items_Temp.Stufe = Int32.Parse(result[k]);
//                    k++;
//                    Char_Items_Temp.Anmerkung = (result[k]);
//                    k++;
//                    LoadChar.Char_Items.Add(Char_Items_Temp);
//                }
//            }


//            //Char_Kommlinks

//            if (result[k] != null || result[k] != "")
//            {
//                iteration_temp = Int32.Parse(result[k]);
//                k++;
//                for (i = 0; i < iteration_temp; i++) //ToDo: Surrender with try catch
//                {

//                    Char_Kommlink Char_Kommlinks_Temp = new Char_Kommlink();
//                    Char_Kommlinks_Temp.ID = Int32.Parse(result[k]);
//                    k++;
//                    Char_Kommlinks_Temp.Bezeichnung = (result[k]);
//                    k++;
//                    Char_Kommlinks_Temp.Stufe = Int32.Parse(result[k]);
//                    k++;
//                    Char_Kommlinks_Temp.AnzahlProgramme = Int32.Parse(result[k]);
//                    k++;
//                    Char_Kommlinks_Temp.GrundKonfiguration = (result[k]);
//                    k++;
//                    Char_Kommlinks_Temp.Angriff = Int32.Parse(result[k]);
//                    k++;
//                    Char_Kommlinks_Temp.Schleicher = Int32.Parse(result[k]);
//                    k++;
//                    Char_Kommlinks_Temp.Firewall = Int32.Parse(result[k]);
//                    k++;
//                    Char_Kommlinks_Temp.Datenverarbeitung = Int32.Parse(result[k]);
//                    k++;
//                    LoadChar.Char_Kommlinks.Add(Char_Kommlinks_Temp);
//                }
//            }


//            //Char_Nachteile

//            if (result[k] != null || result[k] != "")
//            {
//                iteration_temp = Int32.Parse(result[k]);
//                k++;
//                for (i = 0; i < iteration_temp; i++) //ToDo: Surrender with try catch
//                {

//                    Char_Nachteil Char_Nachteile_Temp = new Char_Nachteil();
//                    Char_Nachteile_Temp.ID = Int32.Parse(result[k]);
//                    k++;
//                    Char_Nachteile_Temp.Bezeichnung = (result[k]);
//                    k++;
//                    Char_Nachteile_Temp.Stufe = Int32.Parse(result[k]);
//                    k++;
//                    Char_Nachteile_Temp.Zusammensetzung = (result[k]);
//                    k++;
//                    Char_Nachteile_Temp.Auswirkungen = (result[k]);
//                    k++;
//                    Char_Nachteile_Temp.Anmerkungen = (result[k]);
//                    k++;
//                    LoadChar.Char_Nachteile.Add(Char_Nachteile_Temp);
//                }
//            }


//            //Char_Nahkampfwaffen
//            if (result[k] != null || result[k] != "")
//            {
//                iteration_temp = Int32.Parse(result[k]);
//                k++;
//                for (i = 0; i < iteration_temp; i++) //ToDo: Surrender with try catch
//                {

//                    Char_Nahkampfwaffe Char_Nahkampfwaffen_Temp = new Char_Nahkampfwaffe();
//                    Char_Nahkampfwaffen_Temp.ID = Int32.Parse(result[k]);
//                    k++;
//                    Char_Nahkampfwaffen_Temp.Bezeichnung = (result[k]);
//                    k++;
//                    Char_Nahkampfwaffen_Temp.Schaden = Int32.Parse(result[k]);
//                    k++;
//                    Char_Nahkampfwaffen_Temp.Schaden_Typ = char.Parse(result[k]);
//                    k++;
//                    Char_Nahkampfwaffen_Temp.Reichweite = Int32.Parse(result[k]);
//                    k++;
//                    Char_Nahkampfwaffen_Temp.PB = (result[k]);
//                    k++;
//                    LoadChar.Char_Nahkampfwaffen.Add(Char_Nahkampfwaffen_Temp);
//                }
//            }


//            //Char_Panzerungen
//            if (result[k] != null || result[k] != "")
//            {
//                iteration_temp = Int32.Parse(result[k]);
//                k++;
//                for (i = 0; i < iteration_temp; i++) //ToDo: Surrender with try catch
//                {

//                    Char_Panzerung Char_Panzerungen_Temp = new Char_Panzerung();
//                    Char_Panzerungen_Temp.ID = Int32.Parse(result[k]);
//                    k++;
//                    Char_Panzerungen_Temp.Bezeichnung = (result[k]);
//                    k++;
//                    Char_Panzerungen_Temp.Ballistik = Int32.Parse(result[k]);
//                    k++;
//                    Char_Panzerungen_Temp.Stoß = Int32.Parse(result[k]);
//                    k++;
//                    Char_Panzerungen_Temp.Anmerkung = (result[k]);
//                    k++;
//                    LoadChar.Char_Panzerungen.Add(Char_Panzerungen_Temp);
//                }
//            }


//            //Char_Programme

//            if (result[k] != null || result[k] != "")
//            {
//                iteration_temp = Int32.Parse(result[k]);
//                k++;
//                for (i = 0; i < iteration_temp; i++) //ToDo: Surrender with try catch
//                {

//                    Char_Programm Char_Programme_Temp = new Char_Programm();
//                    Char_Programme_Temp.ID = Int32.Parse(result[k]);
//                    k++;
//                    Char_Programme_Temp.Bezeichnung = (result[k]);
//                    k++;
//                    Char_Programme_Temp.Optionen = (result[k]);
//                    k++;
//                    Char_Programme_Temp.Notizen = (result[k]);
//                    k++;
//                    LoadChar.Char_Programme.Add(Char_Programme_Temp);
//                }
//            }


//            //Char_Sins
//            if (result[k] != null || result[k] != "")
//            {
//                iteration_temp = Int32.Parse(result[k]);
//                k++;
//                for (i = 0; i < iteration_temp; i++) //ToDo: Surrender with try catch
//                {
//                    Char_Sin Char_Sins_Temp = new Char_Sin();
//                    Char_Sins_Temp.ID = Int32.Parse(result[k]);
//                    k++;
//                    Char_Sins_Temp.Name = (result[k]);
//                    k++;
//                    Char_Sins_Temp.Stufe = Int32.Parse(result[k]);
//                    k++;
//                    Char_Sins_Temp.Verwendung = (result[k]);
//                    k++;
//                    Char_Sins_Temp.Geburtstag = new DateTime(Int32.Parse(result[k]), Int32.Parse(result[k + 1]), Int32.Parse(result[k + 2]));
//                    k++;
//                    k++;
//                    k++;
//                    Char_Sins_Temp.Zusätze = (result[k]);
//                    k++;
//                    LoadChar.Char_Sins.Add(Char_Sins_Temp);
//                }
//            }


//            //Char_Vorteile

//            if (result[k] != null || result[k] != "")
//            {
//                iteration_temp = Int32.Parse(result[k]);
//                k++;
//                for (i = 0; i < iteration_temp; i++) //ToDo: Surrender with try catch
//                {

//                    Char_Vorteil Char_Vorteile_Temp = new Char_Vorteil();
//                    Char_Vorteile_Temp.ID = Int32.Parse(result[k]);
//                    k++;
//                    Char_Vorteile_Temp.Bezeichnung = (result[k]);
//                    k++;
//                    Char_Vorteile_Temp.Stufe = Int32.Parse(result[k]);
//                    k++;
//                    Char_Vorteile_Temp.Zusammensetzung = (result[k]);
//                    k++;
//                    Char_Vorteile_Temp.Auswirkungen = (result[k]);
//                    k++;
//                    Char_Vorteile_Temp.Anmerkungen = (result[k]);
//                    k++;
//                    LoadChar.Char_Vorteile.Add(Char_Vorteile_Temp);
//                }
//            }

//            //Char_Attribute
//            try
//            {
//                if (result[k] != null || result[k] != "")
//                {
//                    iteration_temp = Int32.Parse(result[k]);
//                    k++;
//                    for (i = 0; i < iteration_temp; i++) //ToDo: Surrender with try catch
//                    {

//                        Char_Attribut Char_Attribute_Temp = new Char_Attribut();
//                        Char_Attribute_Temp.ID = Int32.Parse(result[k]);
//                        k++;
//                        Char_Attribute_Temp.Bezeichnung = (result[k]);
//                        k++;
//                        Char_Attribute_Temp.Stufe = Int32.Parse(result[k]);
//                        k++;
//                        Char_Attribute_Temp.Stufe_Modifier = (result[k]);
//                        k++;
//                        LoadChar.Char_Attribute.Add(Char_Attribute_Temp);
//                    }
//                }
//            }
//            catch (Exception) { }
//            k++;
//            LoadChar.Runs = Int32.Parse(result[k]);

//        }



//        public static bool Clear_Char(Char ClearChar)
//        {

//            ClearChar.ID_Char = 0;
//            ClearChar.Alias = null;
//            ClearChar.Char_Typ = null;
//            ClearChar.Kontostand = 0;
//            ClearChar.Karma_Gesamt = 0;
//            ClearChar.Karma_Aktuell = 0;
//            ClearChar.Edgne_Aktuell = 0;
//            ClearChar.Edge_Gesamt = 0;
//            ClearChar.Essenz = 0;
//            ClearChar.Schaden_K = 0;
//            ClearChar.Schaden_G = 0;
//            ClearChar.Schaden_M = 0;
//            ClearChar.Schaden_K_max = 0;
//            ClearChar.Schaden_G_max = 0;
//            ClearChar.Schaden_M_max = 0;
//            ClearChar.Notizen = null;
//            ClearChar.MetaTyp = null;
//            ClearChar.Lebesstil = null;
//            try
//            {
//                ClearChar.Geburtsdatum2 = new DateTime();
//            }
//            catch (NullReferenceException)
//            {

//            }
//            ClearChar.Geburtsdatum = null;
//            ClearChar.Geschlecht = null;
//            ClearChar.Größe = 0;
//            ClearChar.Gewicht = 0;
//            ClearChar.Augenfarbe = null;
//            ClearChar.Haarfarbe = null;
//            ClearChar.Hautfarbe = null;
//            ClearChar.Bild = null;
//            //ClearChar.Geschicklichkeit = 0;
//            //ClearChar.Konstitution = 0;
//            //ClearChar.Reaktion = 0;
//            //ClearChar.Stärke = 0;
//            //ClearChar.Charisma = 0;
//            //ClearChar.Intuition = 0;
//            //ClearChar.Logik = 0;
//            //ClearChar.Willenskraft = 0;
//            ClearChar.Zusammenfassung = null;

//            ClearChar.Runs = 0;

//            //Char_Fertigkeiten

//            ClearChar.Char_Fertigkeiten = null;
//            ClearChar.Char_Fertigkeiten = new System.Collections.ObjectModel.ObservableCollection<Char_Fertigkeit>();

//            //Char_Fähigkeiten

//            ClearChar.Char_Fähigkeiten = null;
//            ClearChar.Char_Fähigkeiten = new System.Collections.ObjectModel.ObservableCollection<Char_Fähigkeit>();
           
//            //Char_Connections

//            ClearChar.Char_Connections = null;
//            ClearChar.Char_Connections = new System.Collections.ObjectModel.ObservableCollection<Char_Connection>();
           
//            //Char_Dronen_Fahrzeuge

//            ClearChar.Char_Dronen_Fahrzeuge = null;
//            ClearChar.Char_Dronen_Fahrzeuge = new System.Collections.ObjectModel.ObservableCollection<Char_Drone_Fahrzeug>();
            
//            //Char_Fernkampfwaffen

//            ClearChar.Char_Fernkampfwaffen = null;
//            ClearChar.Char_Fernkampfwaffen = new System.Collections.ObjectModel.ObservableCollection<Char_Fernkampfwaffe>();
          
//            //Char_Implantate

//            ClearChar.Char_Implantate = null;
//            ClearChar.Char_Implantate = new System.Collections.ObjectModel.ObservableCollection<Char_Implantat>();
            
//            //Char_Items

//            ClearChar.Char_Items = null;
//            ClearChar.Char_Items = new System.Collections.ObjectModel.ObservableCollection<Char_Item>();
            
//            //Char_Kommlinks

//            ClearChar.Char_Kommlinks = null;
//            ClearChar.Char_Kommlinks = new System.Collections.ObjectModel.ObservableCollection<Char_Kommlink>();
           
//            //Char_Nachteile

//            ClearChar.Char_Nachteile = null;
//            ClearChar.Char_Nachteile = new System.Collections.ObjectModel.ObservableCollection<Char_Nachteil>();
           
//            //Char_Nahkampfwaffen

//            ClearChar.Char_Nahkampfwaffen = null;
//            ClearChar.Char_Nahkampfwaffen = new System.Collections.ObjectModel.ObservableCollection<Char_Nahkampfwaffe>();
           
//            //Char_Panzerungen

//            ClearChar.Char_Panzerungen = null;
//            ClearChar.Char_Panzerungen = new System.Collections.ObjectModel.ObservableCollection<Char_Panzerung>();
            
//            //Char_Programme

//            ClearChar.Char_Programme = null;
//            ClearChar.Char_Programme = new System.Collections.ObjectModel.ObservableCollection<Char_Programm>();
            
//            //Char_Sins

//            ClearChar.Char_Sins = null;
//            ClearChar.Char_Sins = new System.Collections.ObjectModel.ObservableCollection<Char_Sin>();

//            //Char_Vorteile

//            ClearChar.Char_Vorteile = null;
//            ClearChar.Char_Vorteile = new System.Collections.ObjectModel.ObservableCollection<Char_Vorteil>();

//            //Char_Attribute

//            ClearChar.Char_Attribute = null;
//            ClearChar.Char_Attribute = new System.Collections.ObjectModel.ObservableCollection<Char_Attribut>();


//            return true;
//        }
//    }
//}

