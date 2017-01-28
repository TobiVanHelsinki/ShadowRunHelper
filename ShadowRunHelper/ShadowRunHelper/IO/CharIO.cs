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
        private static string Char_to_JSON(CharHolder SaveChar)
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

        private static CharHolder JSON_to_Char(string fileContent)
        {
            CharHolder tempChar = new CharHolder();
            try
            {
                JsonSerializerSettings test = new JsonSerializerSettings();
                test.Error = ErrorHandler;
                test.PreserveReferencesHandling = PreserveReferencesHandling.All;
                tempChar = JsonConvert.DeserializeObject<CharHolder>(fileContent, test);
            }
            catch (Exception)
            {
                tempChar = new CharHolder();
                //TODO Message system
            }
            //tempChar.CTRLCyberDeck.Data[0].Angriff++;

            return tempChar;
            //CharHolder newChar = new CharHolder();
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
            //foreach (var item in tempChar.NahkampfwaffeDataList)
            //{
            //    newChar.Add("Nahkampfwaffe", tempChar.NahkampfwaffeHD_ID);
            //    newChar.NahkampfwaffeDataList[maxID].Ordnung = item.Ordnung;
            //    newChar.NahkampfwaffeDataList[maxID].Bezeichner = item.Bezeichner;
            //    newChar.NahkampfwaffeDataList[maxID].Wert = item.Wert;
            //    newChar.NahkampfwaffeDataList[maxID].Typ = item.Typ;
            //    newChar.NahkampfwaffeDataList[maxID].Zusatz = item.Zusatz;
            //    newChar.NahkampfwaffeDataList[maxID].Notiz = item.Notiz;
            //    newChar.NahkampfwaffeDataList[maxID].Aktiv = item.Aktiv;
            //    newChar.NahkampfwaffeDataList[maxID].Anzahl = item.Anzahl;
            //    newChar.NahkampfwaffeDataList[maxID].Besitz = item.Besitz;
            //    newChar.NahkampfwaffeDataList[maxID].PB = item.PB;
            //    newChar.NahkampfwaffeDataList[maxID].Pool = item.Pool;
            //    newChar.NahkampfwaffeDataList[maxID].Reichweite = item.Reichweite;
            //    newChar.NahkampfwaffeDataList[maxID].SchadenTyp = item.SchadenTyp;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.FernkampfwaffeDataList)
            //{
            //    newChar.Add("Fernkampfwaffe", tempChar.FernkampfwaffeHD_ID);
            //    newChar.FernkampfwaffeDataList[maxID].Ordnung = item.Ordnung;
            //    newChar.FernkampfwaffeDataList[maxID].Bezeichner = item.Bezeichner;
            //    newChar.FernkampfwaffeDataList[maxID].Wert = item.Wert;
            //    newChar.FernkampfwaffeDataList[maxID].Typ = item.Typ;
            //    newChar.FernkampfwaffeDataList[maxID].Zusatz = item.Zusatz;
            //    newChar.FernkampfwaffeDataList[maxID].Notiz = item.Notiz;
            //    newChar.FernkampfwaffeDataList[maxID].Aktiv = item.Aktiv;
            //    newChar.FernkampfwaffeDataList[maxID].Anzahl = item.Anzahl;
            //    newChar.FernkampfwaffeDataList[maxID].Besitz = item.Besitz;
            //    newChar.FernkampfwaffeDataList[maxID].PB = item.PB;
            //    newChar.FernkampfwaffeDataList[maxID].Pool = item.Pool;
            //    newChar.FernkampfwaffeDataList[maxID].Modi = item.Modi;
            //    newChar.FernkampfwaffeDataList[maxID].Rückstoß = item.Rückstoß;
            //    newChar.FernkampfwaffeDataList[maxID].SchadenTyp = item.SchadenTyp;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.KommlinkDataList)
            //{
            //    newChar.Add("Kommlink", tempChar.KommlinkHD_ID);
            //    newChar.KommlinkDataList[maxID].Ordnung = item.Ordnung;
            //    newChar.KommlinkDataList[maxID].Bezeichner = item.Bezeichner;
            //    newChar.KommlinkDataList[maxID].Wert = item.Wert;
            //    newChar.KommlinkDataList[maxID].Typ = item.Typ;
            //    newChar.KommlinkDataList[maxID].Zusatz = item.Zusatz;
            //    newChar.KommlinkDataList[maxID].Notiz = item.Notiz;
            //    newChar.KommlinkDataList[maxID].Aktiv = item.Aktiv;
            //    newChar.KommlinkDataList[maxID].Anzahl = item.Anzahl;
            //    newChar.KommlinkDataList[maxID].Besitz = item.Besitz;
            //    newChar.KommlinkDataList[maxID].Datenverarbeitung = item.Datenverarbeitung;
            //    newChar.KommlinkDataList[maxID].Firewall = item.Firewall;
            //    newChar.KommlinkDataList[maxID].Programmanzahl = item.Programmanzahl;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.CyberDeckDataList)
            //{
            //    newChar.Add("CyberDeck", tempChar.CyberDeckHD_ID);
            //    newChar.CyberDeckDataList[maxID].Ordnung = item.Ordnung;
            //    newChar.CyberDeckDataList[maxID].Bezeichner = item.Bezeichner;
            //    newChar.CyberDeckDataList[maxID].Wert = item.Wert;
            //    newChar.CyberDeckDataList[maxID].Typ = item.Typ;
            //    newChar.CyberDeckDataList[maxID].Zusatz = item.Zusatz;
            //    newChar.CyberDeckDataList[maxID].Notiz = item.Notiz;
            //    newChar.CyberDeckDataList[maxID].Aktiv = item.Aktiv;
            //    newChar.CyberDeckDataList[maxID].Anzahl = item.Anzahl;
            //    newChar.CyberDeckDataList[maxID].Besitz = item.Besitz;
            //    newChar.CyberDeckDataList[maxID].Programmanzahl = item.Programmanzahl;
            //    newChar.CyberDeckDataList[maxID].Datenverarbeitung = item.Datenverarbeitung;
            //    newChar.CyberDeckDataList[maxID].Firewall = item.Firewall;
            //    newChar.CyberDeckDataList[maxID].Angriff = item.Angriff;
            //    newChar.CyberDeckDataList[maxID].Schleicher = item.Schleicher;
            //    newChar.CyberDeckDataList[maxID].Datenverarbeitung_o = item.Datenverarbeitung_o;
            //    newChar.CyberDeckDataList[maxID].Firewall_o = item.Firewall_o;
            //    newChar.CyberDeckDataList[maxID].Angriff_o = item.Angriff_o;
            //    newChar.CyberDeckDataList[maxID].Schleicher_o = item.Schleicher_o;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.VehikelDataList)
            //{
            //    newChar.Add("Vehikel", tempChar.VehikelHD_ID);
            //    newChar.VehikelDataList[maxID].Ordnung = item.Ordnung;
            //    newChar.VehikelDataList[maxID].Bezeichner = item.Bezeichner;
            //    newChar.VehikelDataList[maxID].Wert = item.Wert;
            //    newChar.VehikelDataList[maxID].Typ = item.Typ;
            //    newChar.VehikelDataList[maxID].Zusatz = item.Zusatz;
            //    newChar.VehikelDataList[maxID].Notiz = item.Notiz;
            //    newChar.VehikelDataList[maxID].Aktiv = item.Aktiv;
            //    newChar.VehikelDataList[maxID].Anzahl = item.Anzahl;
            //    newChar.VehikelDataList[maxID].Besitz = item.Besitz;
            //    newChar.VehikelDataList[maxID].Beschleunigung = item.Beschleunigung;
            //    newChar.VehikelDataList[maxID].Geschwindigkeit = item.Geschwindigkeit;
            //    newChar.VehikelDataList[maxID].Gewicht = item.Gewicht;
            //    newChar.VehikelDataList[maxID].Handling = item.Handling;
            //    newChar.VehikelDataList[maxID].Panzerung = item.Panzerung;
            //    newChar.VehikelDataList[maxID].Pilot = item.Pilot;
            //    newChar.VehikelDataList[maxID].Rumpf = item.Rumpf;
            //    newChar.VehikelDataList[maxID].Sensor = item.Sensor;
            //    newChar.VehikelDataList[maxID].Sitze = item.Sitze;
            //    maxID++;
            //}
            //maxID = 0;
            //foreach (var item in tempChar.PanzerungDataList)
            //{
            //    newChar.Add("Panzerung", tempChar.PanzerungHD_ID);
            //    newChar.PanzerungDataList[maxID].Ordnung = item.Ordnung;
            //    newChar.PanzerungDataList[maxID].Bezeichner = item.Bezeichner;
            //    newChar.PanzerungDataList[maxID].Wert = item.Wert;
            //    newChar.PanzerungDataList[maxID].Typ = item.Typ;
            //    newChar.PanzerungDataList[maxID].Zusatz = item.Zusatz;
            //    newChar.PanzerungDataList[maxID].Notiz = item.Notiz;
            //    newChar.PanzerungDataList[maxID].Aktiv = item.Aktiv;
            //    newChar.PanzerungDataList[maxID].Anzahl = item.Anzahl;
            //    newChar.PanzerungDataList[maxID].Besitz = item.Besitz;
            //    newChar.PanzerungDataList[maxID].Stoß = item.Stoß;
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

        public static async void Speichern(CharHolder SaveChar, StorageFile file)
        {
            //Ausgewählten Char auf Plattensubsystem schreiben
            await FileIO.WriteTextAsync(file, CharIO.Char_to_JSON(SaveChar));
        }

        public static async Task<CharHolder> Laden(StorageFile file)
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
