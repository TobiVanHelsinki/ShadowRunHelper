using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using ShadowRunHelper.Model;
using System.Collections.ObjectModel;

namespace ShadowRunHelper.IO
{
    class CharIO
    {
        private static string Char_to_JSON(Controller.CharHolder SaveChar)
        {

            JsonSerializerSettings test = new JsonSerializerSettings();
            test.PreserveReferencesHandling = PreserveReferencesHandling.All; //war vorher objects
            test.Error = ErrorHandler;

            //return JsonConvert.SerializeObject(SaveChar);
            return JsonConvert.SerializeObject(SaveChar, test);
        }

        private static void ErrorHandler(object o, Newtonsoft.Json.Serialization.ErrorEventArgs a)
        {
            //((Newtonsoft.Json.JsonSerializer)o).
            a.ErrorContext.Handled = true;
            //a.ErrorContext.Error.Data;
        }

        private static Controller.CharHolder JSON_to_Char(string fileContent)
        {
            Controller.CharHolder tempChar = new Controller.CharHolder();
            try
            {
                JsonSerializerSettings test = new JsonSerializerSettings();
                test.Error = ErrorHandler;
                test.PreserveReferencesHandling = PreserveReferencesHandling.All;
                tempChar = JsonConvert.DeserializeObject<Controller.CharHolder>(fileContent, test);
            }
            catch (Exception)
            {
                tempChar = new Controller.CharHolder();
                //TODO Message system
            }
            //tempChar.CTRLCyberDeck.Data[0].Angriff++;

            return tempChar;
            //Controller.CharHolder newChar = new Controller.CharHolder();
            ////    newChar.Char_ID = tempChar.Char_ID;

            //int maxID = 0;
            //maxID = 0;

            //newChar.HD.toggleHDEdit(false);

            //foreach (var item in tempChar.FertigkeitController)
            //{
            //    newChar.Add("Fertigkeit", item.HD_ID);
            //    newChar.FertigkeitController[maxID].Data.Ordnung = item.Data.Ordnung;
            //    newChar.FertigkeitController[maxID].Data.Bezeichner = item.Data.Bezeichner;
            //    newChar.FertigkeitController[maxID].Data.Wert = item.Data.Wert;
            //    newChar.FertigkeitController[maxID].Data.Typ = item.Data.Typ;
            //    newChar.FertigkeitController[maxID].Data.Zusatz = item.Data.Zusatz;
            //    newChar.FertigkeitController[maxID].Data.Notiz = item.Data.Notiz;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.AttributController)
            //{
            //    newChar.Add("Attribut", item.HD_ID);
            //    newChar.AttributController[maxID].Data.Ordnung = item.Data.Ordnung;
            //    newChar.AttributController[maxID].Data.Bezeichner = item.Data.Bezeichner;
            //    newChar.AttributController[maxID].Data.Wert = item.Data.Wert;
            //    newChar.AttributController[maxID].Data.Typ = item.Data.Typ;
            //    newChar.AttributController[maxID].Data.Zusatz = item.Data.Zusatz;
            //    newChar.AttributController[maxID].Data.Notiz = item.Data.Notiz;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.ItemController)
            //{
            //    newChar.Add("Item", item.HD_ID);
            //    newChar.ItemController[maxID].Data.Ordnung = item.Data.Ordnung;
            //    newChar.ItemController[maxID].Data.Bezeichner = item.Data.Bezeichner;
            //    newChar.ItemController[maxID].Data.Wert = item.Data.Wert;
            //    newChar.ItemController[maxID].Data.Typ = item.Data.Typ;
            //    newChar.ItemController[maxID].Data.Zusatz = item.Data.Zusatz;
            //    newChar.ItemController[maxID].Data.Notiz = item.Data.Notiz;
            //    newChar.ItemController[maxID].Data.Besitz = item.Data.Besitz;
            //    newChar.ItemController[maxID].Data.Aktiv = item.Data.Aktiv;
            //    newChar.ItemController[maxID].Data.Anzahl = item.Data.Anzahl;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.ProgrammController)
            //{
            //    newChar.Add("Programm", item.HD_ID);
            //    newChar.ProgrammController[maxID].Data.Ordnung = item.Data.Ordnung;
            //    newChar.ProgrammController[maxID].Data.Bezeichner = item.Data.Bezeichner;
            //    newChar.ProgrammController[maxID].Data.Wert = item.Data.Wert;
            //    newChar.ProgrammController[maxID].Data.Typ = item.Data.Typ;
            //    newChar.ProgrammController[maxID].Data.Zusatz = item.Data.Zusatz;
            //    newChar.ProgrammController[maxID].Data.Notiz = item.Data.Notiz;
            //    newChar.ProgrammController[maxID].Data.Besitz = item.Data.Besitz;
            //    newChar.ProgrammController[maxID].Data.Aktiv = item.Data.Aktiv;
            //    newChar.ProgrammController[maxID].Data.Anzahl = item.Data.Anzahl;
            //    newChar.ProgrammController[maxID].Data.Optionen = item.Data.Optionen;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.MunitionController)
            //{
            //    newChar.Add("Munition", item.HD_ID);
            //    newChar.MunitionController[maxID].Data.Ordnung = item.Data.Ordnung;
            //    newChar.MunitionController[maxID].Data.Bezeichner = item.Data.Bezeichner;
            //    newChar.MunitionController[maxID].Data.Wert = item.Data.Wert;
            //    newChar.MunitionController[maxID].Data.Typ = item.Data.Typ;
            //    newChar.MunitionController[maxID].Data.Zusatz = item.Data.Zusatz;
            //    newChar.MunitionController[maxID].Data.Notiz = item.Data.Notiz;
            //    newChar.MunitionController[maxID].Data.Besitz = item.Data.Besitz;
            //    newChar.MunitionController[maxID].Data.Aktiv = item.Data.Aktiv;
            //    newChar.MunitionController[maxID].Data.Anzahl = item.Data.Anzahl;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.ImplantatController)
            //{
            //    newChar.Add("Implantat", item.HD_ID);
            //    newChar.ImplantatController[maxID].Data.Ordnung = item.Data.Ordnung;
            //    newChar.ImplantatController[maxID].Data.Bezeichner = item.Data.Bezeichner;
            //    newChar.ImplantatController[maxID].Data.Wert = item.Data.Wert;
            //    newChar.ImplantatController[maxID].Data.Typ = item.Data.Typ;
            //    newChar.ImplantatController[maxID].Data.Zusatz = item.Data.Zusatz;
            //    newChar.ImplantatController[maxID].Data.Notiz = item.Data.Notiz;
            //    newChar.ImplantatController[maxID].Data.Besitz = item.Data.Besitz;
            //    newChar.ImplantatController[maxID].Data.Aktiv = item.Data.Aktiv;
            //    newChar.ImplantatController[maxID].Data.Anzahl = item.Data.Anzahl;
            //    newChar.ImplantatController[maxID].Data.Essenz = item.Data.Essenz;
            //    newChar.ImplantatController[maxID].Data.Kapazität = item.Data.Kapazität;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.VorteilController)
            //{
            //    newChar.Add("Vorteil", item.HD_ID);
            //    newChar.VorteilController[maxID].Data.Ordnung = item.Data.Ordnung;
            //    newChar.VorteilController[maxID].Data.Bezeichner = item.Data.Bezeichner;
            //    newChar.VorteilController[maxID].Data.Wert = item.Data.Wert;
            //    newChar.VorteilController[maxID].Data.Typ = item.Data.Typ;
            //    newChar.VorteilController[maxID].Data.Zusatz = item.Data.Zusatz;
            //    newChar.VorteilController[maxID].Data.Notiz = item.Data.Notiz;
            //    newChar.VorteilController[maxID].Data.Auswirkungen = item.Data.Auswirkungen;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.NachteilController)
            //{
            //    newChar.Add("Nachteil", item.HD_ID);
            //    newChar.NachteilController[maxID].Data.Ordnung = item.Data.Ordnung;
            //    newChar.NachteilController[maxID].Data.Bezeichner = item.Data.Bezeichner;
            //    newChar.NachteilController[maxID].Data.Wert = item.Data.Wert;
            //    newChar.NachteilController[maxID].Data.Typ = item.Data.Typ;
            //    newChar.NachteilController[maxID].Data.Zusatz = item.Data.Zusatz;
            //    newChar.NachteilController[maxID].Data.Notiz = item.Data.Notiz;
            //    newChar.NachteilController[maxID].Data.Auswirkungen = item.Data.Auswirkungen;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.ConnectionController)
            //{
            //    newChar.Add("Connection", 0);
            //    newChar.ConnectionController[maxID].Data.Wert = item.Data.Wert;
            //    newChar.ConnectionController[maxID].Data.Loyal = item.Data.Loyal;
            //    newChar.ConnectionController[maxID].Data.Alias = item.Data.Alias;
            //    newChar.ConnectionController[maxID].Data.Notizen = item.Data.Notizen;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.SinController)
            //{
            //    newChar.Add("Sin", 0);
            //    newChar.SinController[maxID].Data.Ordnung = item.Data.Ordnung;
            //    newChar.SinController[maxID].Data.Bezeichner = item.Data.Bezeichner;
            //    newChar.SinController[maxID].Data.Wert = item.Data.Wert;
            //    newChar.SinController[maxID].Data.Typ = item.Data.Typ;
            //    newChar.SinController[maxID].Data.Zusatz = item.Data.Zusatz;
            //    newChar.SinController[maxID].Data.Notiz = item.Data.Notiz;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.NahkampfwaffeController.DataList)
            //{
            //    newChar.Add("Nahkampfwaffe", tempChar.NahkampfwaffeController.HD_ID);
            //    newChar.NahkampfwaffeController.DataList[maxID].Ordnung = item.Ordnung;
            //    newChar.NahkampfwaffeController.DataList[maxID].Bezeichner = item.Bezeichner;
            //    newChar.NahkampfwaffeController.DataList[maxID].Wert = item.Wert;
            //    newChar.NahkampfwaffeController.DataList[maxID].Typ = item.Typ;
            //    newChar.NahkampfwaffeController.DataList[maxID].Zusatz = item.Zusatz;
            //    newChar.NahkampfwaffeController.DataList[maxID].Notiz = item.Notiz;
            //    newChar.NahkampfwaffeController.DataList[maxID].Aktiv = item.Aktiv;
            //    newChar.NahkampfwaffeController.DataList[maxID].Anzahl = item.Anzahl;
            //    newChar.NahkampfwaffeController.DataList[maxID].Besitz = item.Besitz;
            //    newChar.NahkampfwaffeController.DataList[maxID].PB = item.PB;
            //    newChar.NahkampfwaffeController.DataList[maxID].Pool = item.Pool;
            //    newChar.NahkampfwaffeController.DataList[maxID].Reichweite = item.Reichweite;
            //    newChar.NahkampfwaffeController.DataList[maxID].SchadenTyp = item.SchadenTyp;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.FernkampfwaffeController.DataList)
            //{
            //    newChar.Add("Fernkampfwaffe", tempChar.FernkampfwaffeController.HD_ID);
            //    newChar.FernkampfwaffeController.DataList[maxID].Ordnung = item.Ordnung;
            //    newChar.FernkampfwaffeController.DataList[maxID].Bezeichner = item.Bezeichner;
            //    newChar.FernkampfwaffeController.DataList[maxID].Wert = item.Wert;
            //    newChar.FernkampfwaffeController.DataList[maxID].Typ = item.Typ;
            //    newChar.FernkampfwaffeController.DataList[maxID].Zusatz = item.Zusatz;
            //    newChar.FernkampfwaffeController.DataList[maxID].Notiz = item.Notiz;
            //    newChar.FernkampfwaffeController.DataList[maxID].Aktiv = item.Aktiv;
            //    newChar.FernkampfwaffeController.DataList[maxID].Anzahl = item.Anzahl;
            //    newChar.FernkampfwaffeController.DataList[maxID].Besitz = item.Besitz;
            //    newChar.FernkampfwaffeController.DataList[maxID].PB = item.PB;
            //    newChar.FernkampfwaffeController.DataList[maxID].Pool = item.Pool;
            //    newChar.FernkampfwaffeController.DataList[maxID].Modi = item.Modi;
            //    newChar.FernkampfwaffeController.DataList[maxID].Rückstoß = item.Rückstoß;
            //    newChar.FernkampfwaffeController.DataList[maxID].SchadenTyp = item.SchadenTyp;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.KommlinkController.DataList)
            //{
            //    newChar.Add("Kommlink", tempChar.KommlinkController.HD_ID);
            //    newChar.KommlinkController.DataList[maxID].Ordnung = item.Ordnung;
            //    newChar.KommlinkController.DataList[maxID].Bezeichner = item.Bezeichner;
            //    newChar.KommlinkController.DataList[maxID].Wert = item.Wert;
            //    newChar.KommlinkController.DataList[maxID].Typ = item.Typ;
            //    newChar.KommlinkController.DataList[maxID].Zusatz = item.Zusatz;
            //    newChar.KommlinkController.DataList[maxID].Notiz = item.Notiz;
            //    newChar.KommlinkController.DataList[maxID].Aktiv = item.Aktiv;
            //    newChar.KommlinkController.DataList[maxID].Anzahl = item.Anzahl;
            //    newChar.KommlinkController.DataList[maxID].Besitz = item.Besitz;
            //    newChar.KommlinkController.DataList[maxID].Datenverarbeitung = item.Datenverarbeitung;
            //    newChar.KommlinkController.DataList[maxID].Firewall = item.Firewall;
            //    newChar.KommlinkController.DataList[maxID].Programmanzahl = item.Programmanzahl;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.CyberDeckController.DataList)
            //{
            //    newChar.Add("CyberDeck", tempChar.CyberDeckController.HD_ID);
            //    newChar.CyberDeckController.DataList[maxID].Ordnung = item.Ordnung;
            //    newChar.CyberDeckController.DataList[maxID].Bezeichner = item.Bezeichner;
            //    newChar.CyberDeckController.DataList[maxID].Wert = item.Wert;
            //    newChar.CyberDeckController.DataList[maxID].Typ = item.Typ;
            //    newChar.CyberDeckController.DataList[maxID].Zusatz = item.Zusatz;
            //    newChar.CyberDeckController.DataList[maxID].Notiz = item.Notiz;
            //    newChar.CyberDeckController.DataList[maxID].Aktiv = item.Aktiv;
            //    newChar.CyberDeckController.DataList[maxID].Anzahl = item.Anzahl;
            //    newChar.CyberDeckController.DataList[maxID].Besitz = item.Besitz;
            //    newChar.CyberDeckController.DataList[maxID].Programmanzahl = item.Programmanzahl;
            //    newChar.CyberDeckController.DataList[maxID].Datenverarbeitung = item.Datenverarbeitung;
            //    newChar.CyberDeckController.DataList[maxID].Firewall = item.Firewall;
            //    newChar.CyberDeckController.DataList[maxID].Angriff = item.Angriff;
            //    newChar.CyberDeckController.DataList[maxID].Schleicher = item.Schleicher;
            //    newChar.CyberDeckController.DataList[maxID].Datenverarbeitung_o = item.Datenverarbeitung_o;
            //    newChar.CyberDeckController.DataList[maxID].Firewall_o = item.Firewall_o;
            //    newChar.CyberDeckController.DataList[maxID].Angriff_o = item.Angriff_o;
            //    newChar.CyberDeckController.DataList[maxID].Schleicher_o = item.Schleicher_o;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.VehikelController.DataList)
            //{
            //    newChar.Add("Vehikel", tempChar.VehikelController.HD_ID);
            //    newChar.VehikelController.DataList[maxID].Ordnung = item.Ordnung;
            //    newChar.VehikelController.DataList[maxID].Bezeichner = item.Bezeichner;
            //    newChar.VehikelController.DataList[maxID].Wert = item.Wert;
            //    newChar.VehikelController.DataList[maxID].Typ = item.Typ;
            //    newChar.VehikelController.DataList[maxID].Zusatz = item.Zusatz;
            //    newChar.VehikelController.DataList[maxID].Notiz = item.Notiz;
            //    newChar.VehikelController.DataList[maxID].Aktiv = item.Aktiv;
            //    newChar.VehikelController.DataList[maxID].Anzahl = item.Anzahl;
            //    newChar.VehikelController.DataList[maxID].Besitz = item.Besitz;
            //    newChar.VehikelController.DataList[maxID].Beschleunigung = item.Beschleunigung;
            //    newChar.VehikelController.DataList[maxID].Geschwindigkeit = item.Geschwindigkeit;
            //    newChar.VehikelController.DataList[maxID].Gewicht = item.Gewicht;
            //    newChar.VehikelController.DataList[maxID].Handling = item.Handling;
            //    newChar.VehikelController.DataList[maxID].Panzerung = item.Panzerung;
            //    newChar.VehikelController.DataList[maxID].Pilot = item.Pilot;
            //    newChar.VehikelController.DataList[maxID].Rumpf = item.Rumpf;
            //    newChar.VehikelController.DataList[maxID].Sensor = item.Sensor;
            //    newChar.VehikelController.DataList[maxID].Sitze = item.Sitze;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.PanzerungController.DataList)
            //{
            //    newChar.Add("Panzerung", tempChar.PanzerungController.HD_ID);
            //    newChar.PanzerungController.DataList[maxID].Ordnung = item.Ordnung;
            //    newChar.PanzerungController.DataList[maxID].Bezeichner = item.Bezeichner;
            //    newChar.PanzerungController.DataList[maxID].Wert = item.Wert;
            //    newChar.PanzerungController.DataList[maxID].Typ = item.Typ;
            //    newChar.PanzerungController.DataList[maxID].Zusatz = item.Zusatz;
            //    newChar.PanzerungController.DataList[maxID].Notiz = item.Notiz;
            //    newChar.PanzerungController.DataList[maxID].Aktiv = item.Aktiv;
            //    newChar.PanzerungController.DataList[maxID].Anzahl = item.Anzahl;
            //    newChar.PanzerungController.DataList[maxID].Besitz = item.Besitz;
            //    newChar.PanzerungController.DataList[maxID].Stoß = item.Stoß;
            //    maxID++;
            //}

            //newChar.Person = tempChar.Person;
            //if (newChar.Person == null)
            //{
            //    newChar.Person = new CharModel.Person();
            //}
            //// handlung muss als letztes in dieser liste erscheinen, da es abhängigkeiten besitzt
            //maxID = 0;
            //foreach (var item in tempChar.HandlungController)
            //{
            //    newChar.Add("Handlung", item.HD_ID);
            //    newChar.HandlungController[maxID].Data.Ordnung = item.Data.Ordnung;
            //    newChar.HandlungController[maxID].Data.Bezeichner = item.Data.Bezeichner;
            //    newChar.HandlungController[maxID].Data.Wert = item.Data.Wert;
            //    newChar.HandlungController[maxID].Data.Typ = item.Data.Typ;
            //    newChar.HandlungController[maxID].Data.Zusatz = item.Data.Zusatz;
            //    newChar.HandlungController[maxID].Data.Notiz = item.Data.Notiz;
            //    newChar.HandlungController[maxID].Data.Zusammensetzung = item.Data.Zusammensetzung;
            //    newChar.HandlungController[maxID].Data.GrenzeZusammensetzung = item.Data.GrenzeZusammensetzung;
            //    newChar.HandlungController[maxID].Data.GegenZusammensetzung = item.Data.GegenZusammensetzung;
            //    maxID++;
            //}
            //newChar.HD.toggleHDEdit(true);
            //return newChar;
        }


        public static async Task<ObservableCollection<CharSummory>> getListofChars(StorageFolder CharFolder)
        {
            ObservableCollection<CharSummory> templist = new ObservableCollection<CharSummory>();
            try
            {
                IReadOnlyList<StorageFile> Liste = await CharFolder.GetFilesAsync();
                foreach (var item in Liste)
                {
                    if (item.FileType == Variablen.DATEIENDUNG_CHAR)
                    {
                        Windows.Storage.FileProperties.BasicProperties basicProperties = await item.GetBasicPropertiesAsync();
                        templist.Add(new CharSummory(item.Name, "", basicProperties.DateModified));
                    }
                }
            }
            catch (Exception)
            {
            }
            return templist;
        }

        public static async void Speichern(Controller.CharHolder SaveChar, StorageFile file)
        {
            //Ausgewählten Char auf Plattensubsystem schreiben
            await FileIO.WriteTextAsync(file, CharIO.Char_to_JSON(SaveChar));
        }

        public static async Task<Controller.CharHolder> Laden(StorageFile file)
        {
            String inputString = "";
            try
            {
                inputString = await FileIO.ReadTextAsync(file);
                return JSON_to_Char(inputString);
            }
            catch (Exception)
            {
                throw new Exception("Konnte nicht aus Datei lesen und oder laden");
            }
        }

        public static async void Löschen(StorageFile toDelFile)
        {
            try
            {
                await toDelFile.DeleteAsync();
            }
            catch (Exception)
            {
                
            }
           
        }
    }
}
