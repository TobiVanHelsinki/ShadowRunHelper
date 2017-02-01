using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.CharController;
using System.Collections.ObjectModel;
using System.Linq;

namespace ShadowRunHelper.Model
{
    /// <summary>
    /// Hält einen Char mit samst Controlern und Daten
    /// </summary>
    public class CharHolder
    {
        public string APP_VERSION_NUMBER = Konstanten.APP_VERSION_NUMBER_1_5;
        public string FILE_VERSION_NUMBER = Konstanten.CHARFILE_VERSION_1_5;
        public CharController.cController<Fertigkeit> CTRLFertigkeit { get; set; }
        public CharController.cController<Item> CTRLItem { get; set; }
        public CharController.cController<Programm> CTRLProgramm { get; set; }
        public CharController.cController<Munition> CTRLMunition { get; set; }
        public CharController.cController<Implantat> CTRLImplantat { get; set; }
        public CharController.cController<Vorteil> CTRLVorteil { get; set; }
        public CharController.cController<Nachteil> CTRLNachteil { get; set; }
        public CharController.cController<Connection> CTRLConnection { get; set; }
        public CharController.cController<Sin> CTRLSin { get; set; }

        public CharController.cAttributController CTRLAttribut { get; set; }
        public CharController.cNahkampfwaffeController CTRLNahkampfwaffe { get; set; }
        public CharController.cFernkampfwaffeController CTRLFernkampfwaffe { get; set; }
        public CharController.cKommlinkController CTRLKommlink { get; set; }
        public CharController.cCyberDeckController CTRLCyberDeck { get; set; }
        public CharController.cVehikelController CTRLVehikel { get; set; }
        public CharController.cPanzerungController CTRLPanzerung { get; set; }
        public CharController.cController<Handlung> CTRLHandlung { get; set; }

        [System.Runtime.Serialization.IgnoreDataMember]
        public List<KeyValuePair<Thing, string>> lstThings;

        public Person Person { get; set; }

        /// <summary>
        /// Konstruktor nutzen, um neue Controller und Objekte zu erhalten
        /// </summary>
        public CharHolder()
        {
            CTRLFertigkeit = new CharController.cController<Fertigkeit>();
            CTRLItem= new CharController.cController<Item>();
            CTRLProgramm = new CharController.cController<Programm>();
            CTRLMunition = new CharController.cController<Munition>();
            CTRLImplantat = new CharController.cController<Implantat>();
            CTRLVorteil = new CharController.cController<Vorteil>();
            CTRLNachteil = new CharController.cController<Nachteil>();
            CTRLConnection = new CharController.cController<Connection>();
            CTRLSin = new CharController.cController<Sin>();
            CTRLAttribut = new CharController.cAttributController();
            CTRLNahkampfwaffe = new CharController.cNahkampfwaffeController();
            CTRLFernkampfwaffe = new CharController.cFernkampfwaffeController();
            CTRLKommlink = new CharController.cKommlinkController();
            CTRLCyberDeck = new CharController.cCyberDeckController();
            CTRLVehikel = new CharController.cVehikelController();
            CTRLPanzerung = new CharController.cPanzerungController();
            CTRLHandlung = new CharController.cController<Handlung>();
            Person = new Person();
            CTRLAttribut.SetDependencies(Person, CTRLImplantat.Data);

            lstThings = new List<KeyValuePair<Thing, string>>();
        }


        internal void Add(ThingDefs thingDefs)
        {
            switch (thingDefs)
            {
                case ThingDefs.Handlung:
                    CTRLHandlung.AddNewThing();
                    break;
                case ThingDefs.Fertigkeit:
                    CTRLFertigkeit.AddNewThing();
                    break;
                case ThingDefs.Item:
                    CTRLItem.AddNewThing();
                    break;
                case ThingDefs.Programm:
                    CTRLProgramm.AddNewThing();
                    break;
                case ThingDefs.Munition:
                    CTRLMunition.AddNewThing();
                    break;
                case ThingDefs.Implantat:
                    CTRLImplantat.AddNewThing();
                    break;
                case ThingDefs.Vorteil:
                    CTRLVorteil.AddNewThing();
                    break;
                case ThingDefs.Nachteil:
                    CTRLNachteil.AddNewThing();
                    break;
                case ThingDefs.Connection:
                    CTRLConnection.AddNewThing();
                    break;
                case ThingDefs.Sin:
                    CTRLSin.AddNewThing();
                    break;
                case ThingDefs.Attribut:
                    CTRLAttribut.AddNewThing();
                    break;
                case ThingDefs.Nahkampfwaffe:
                    CTRLNahkampfwaffe.AddNewThing();
                    break;
                case ThingDefs.Fernkampfwaffe:
                    CTRLFernkampfwaffe.AddNewThing();
                    break;
                case ThingDefs.Kommlink:
                    CTRLKommlink.AddNewThing();
                    break;
                case ThingDefs.CyberDeck:
                    CTRLCyberDeck.AddNewThing();
                    break;
                case ThingDefs.Vehikel:
                    CTRLVehikel.AddNewThing();
                    break;
                case ThingDefs.Panzerung:
                    CTRLPanzerung.AddNewThing();
                    break;
                default:
                    break;
            }
            RefreshThingList();
        }

        public void RefreshThingList()
        {
            lstThings.Clear();
            lstThings.AddRange(CTRLAttribut.GetElementsForThingList()); //liefert immer das selbe
            lstThings.AddRange(CTRLFertigkeit.GetElementsForThingList()); //liefert immer das selbe
            lstThings.AddRange(CTRLFernkampfwaffe.GetElementsForThingList()); //liefert immer das selbe
            lstThings.AddRange(CTRLNahkampfwaffe.GetElementsForThingList()); //liefert immer das selbe
            lstThings.AddRange(CTRLPanzerung.GetElementsForThingList()); //liefert immer das selbe

            lstThings.AddRange(CTRLItem.GetElementsForThingList());

            lstThings.AddRange(CTRLCyberDeck.GetElementsForThingList()); //liefert immer das selbe
            lstThings.AddRange(CTRLKommlink.GetElementsForThingList()); //liefert immer das selbe
            lstThings.AddRange(CTRLVehikel.GetElementsForThingList()); //liefert immer das selbe

            lstThings.AddRange(CTRLProgramm.GetElementsForThingList());
            lstThings.AddRange(CTRLMunition.GetElementsForThingList());
            lstThings.AddRange(CTRLImplantat.GetElementsForThingList());
            lstThings.AddRange(CTRLVorteil.GetElementsForThingList());
            lstThings.AddRange(CTRLNachteil.GetElementsForThingList());
            lstThings.AddRange(CTRLConnection.GetElementsForThingList());
            lstThings.AddRange(CTRLSin.GetElementsForThingList());
            
            //lstThings.AddRange(CTRLHandlung.GetElementsForThingList()); // nötig?
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void Remove(Thing tToRemove)
        {
            switch (tToRemove.ThingType)
            {
                case ThingDefs.Handlung:
                    CTRLHandlung.RemoveThing((Handlung)tToRemove);
                    break;
                case ThingDefs.Fertigkeit:
                    CTRLFertigkeit.RemoveThing((Fertigkeit)tToRemove);
                    break;
                case ThingDefs.Item:
                    CTRLItem.RemoveThing((Item)tToRemove);
                    break;
                case ThingDefs.Programm:
                    CTRLProgramm.RemoveThing((Programm)tToRemove);
                    break;
                case ThingDefs.Munition:
                    CTRLMunition.RemoveThing((Munition)tToRemove);
                    break;
                case ThingDefs.Implantat:
                    CTRLImplantat.RemoveThing((Implantat)tToRemove);
                    break;
                case ThingDefs.Vorteil:
                    CTRLVorteil.RemoveThing((Vorteil)tToRemove);
                    break;
                case ThingDefs.Nachteil:
                    CTRLNachteil.RemoveThing((Nachteil)tToRemove);
                    break;
                case ThingDefs.Connection:
                    CTRLConnection.RemoveThing((Connection)tToRemove);
                    break;
                case ThingDefs.Sin:
                    CTRLSin.RemoveThing((Sin)tToRemove);
                    break;
                case ThingDefs.Attribut:
                    CTRLAttribut.RemoveThing((Attribut)tToRemove);
                    break;
                case ThingDefs.Nahkampfwaffe:
                    CTRLNahkampfwaffe.RemoveThing((Nahkampfwaffe)tToRemove);
                    break;
                case ThingDefs.Fernkampfwaffe:
                    CTRLFernkampfwaffe.RemoveThing((Fernkampfwaffe)tToRemove);
                    break;
                case ThingDefs.Kommlink:
                    CTRLKommlink.RemoveThing((Kommlink)tToRemove);
                    break;
                case ThingDefs.CyberDeck:
                    CTRLCyberDeck.RemoveThing((CyberDeck)tToRemove);
                    break;
                case ThingDefs.Vehikel:
                    CTRLVehikel.RemoveThing((Vehikel)tToRemove);
                    break;
                case ThingDefs.Panzerung:
                    CTRLPanzerung.RemoveThing((Panzerung)tToRemove);
                    break;
                default:
                    break;
            }
            lstThings.Remove(lstThings.Find((x) => x.Key == tToRemove));
        }

        public string MakeName(bool WithDate = true)
        {
            String temp_Alias = "";
            String temp_Char_Typ = "";
            String temp_Karma = "";
            String temp_Runs = "";
            try
            {
                temp_Alias = Person.Alias;
                if (temp_Alias == "")
                {
                    throw new NullReferenceException();
                }
            }
            catch (NullReferenceException) { temp_Alias = "$ohne Namen$"; }
            try
            {
                temp_Char_Typ = Person.Char_Typ;
                if (temp_Char_Typ == "")
                {
                    throw new NullReferenceException();
                }
            }
            catch (NullReferenceException) { temp_Char_Typ = "$ohne Beruf$"; }
            try
            {
                temp_Karma = Person.Karma_Gesamt.ToString();
                if (temp_Karma == "")
                {
                    throw new NullReferenceException();
                }
            }
            catch (NullReferenceException) { temp_Karma = "$ohne Erfolg$"; }
            try
            {
                temp_Runs = Person.Runs.ToString();
                if (temp_Runs == "")
                {
                    throw new NullReferenceException();
                }
            }
            catch (NullReferenceException) { temp_Runs = "$ohne Erfolg$"; }

            if (WithDate)
            {
                return temp_Alias + "_" + temp_Char_Typ + "_Karma_" + temp_Karma + "_Runs_" + temp_Runs + Konstanten.DATEIENDUNG_CHAR;
            }
            else
            {
                return temp_Alias + "_" + temp_Char_Typ + "_Karma_" + temp_Karma + "_Runs_" + temp_Runs + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + Konstanten.DATEIENDUNG_CHAR;
            }
        }

        /// <summary>
        /// retval: List of Pairs: Key is Name for waht is at the string (attribut or cyberdeck orso)
        /// </summary>
        /// <param name="strDelimiter"></param>
        /// <returns></returns>
        /// 
        public List<KeyValuePair<string, string>> TOCSV(string strDelimiter)
        {
            var lstReturn = new List<KeyValuePair<string, string>>();
            const string strNewLine = "\n";
            string strNew = "sep="+strDelimiter+strNewLine;
        
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Attribut, CTRLAttribut));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Connection, CTRLConnection));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.CyberDeck, CTRLCyberDeck));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Fernkampfwaffe, CTRLFernkampfwaffe));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Fertigkeit, CTRLFertigkeit));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Handlung, CTRLHandlung));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Implantat, CTRLImplantat));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Item, CTRLItem));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Kommlink, CTRLKommlink));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Munition, CTRLMunition));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Nachteil, CTRLNachteil));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Nahkampfwaffe, CTRLNahkampfwaffe));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Panzerung, CTRLPanzerung));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Programm, CTRLProgramm));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Sin, CTRLSin));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Vehikel, CTRLVehikel));
            lstReturn.Add(PartExport(strDelimiter, strNewLine, strNew, ThingDefs.Vorteil, CTRLVorteil));
            return lstReturn;
        }

        static KeyValuePair<string, string> PartExport<T>(string strDelimiter, string strNewLine, string strNew, ThingDefs eDef, cController<T> CTRL) where T: Thing, new()
        {
            string strTemp = strNew;
            if (CTRL.Data.Count >=1)
            {
                strTemp += CTRL.Data[0].HeaderToCSV(strDelimiter);
            }
            strTemp += strNewLine;
            foreach (T item in CTRL.Data)
            {
                strTemp += item.ToCSV(strDelimiter);
                strTemp += strNewLine;
            }
            return new KeyValuePair<string, string>(strTemp, TypenHelper.ThingDefToString(eDef, true));
        }

        public void FromCSV(char strDelimiter, string strImport)
        {
            const char strNewLine = '\n';
            PartImport(strDelimiter, strNewLine, strImport, CTRLAttribut);
            PartImport(strDelimiter, strNewLine, strImport, CTRLConnection);
            PartImport(strDelimiter, strNewLine, strImport, CTRLCyberDeck);
            PartImport(strDelimiter, strNewLine, strImport, CTRLFernkampfwaffe);
            PartImport(strDelimiter, strNewLine, strImport, CTRLFertigkeit);
            PartImport(strDelimiter, strNewLine, strImport, CTRLHandlung);
            PartImport(strDelimiter, strNewLine, strImport, CTRLImplantat);
            PartImport(strDelimiter, strNewLine, strImport, CTRLItem);
            PartImport(strDelimiter, strNewLine, strImport, CTRLKommlink);
            PartImport(strDelimiter, strNewLine, strImport, CTRLMunition);
            PartImport(strDelimiter, strNewLine, strImport, CTRLNachteil);
            PartImport(strDelimiter, strNewLine, strImport, CTRLNahkampfwaffe);
            PartImport(strDelimiter, strNewLine, strImport, CTRLPanzerung);
            PartImport(strDelimiter, strNewLine, strImport, CTRLProgramm);
            PartImport(strDelimiter, strNewLine, strImport, CTRLSin);
            PartImport(strDelimiter, strNewLine, strImport, CTRLVehikel);
            PartImport(strDelimiter, strNewLine, strImport, CTRLVorteil);
        }

        static void PartImport<T>(char strDelimiter, char strNewLine, string strReadFile, cController<T> CTRL) where T : Thing, new()
        {
            string[] Lines = strReadFile.Split(strNewLine);
            string[] Headar = Lines[0].Split(strDelimiter);
            for (int i = 1; i < Lines.Length; i++) //start at 1 to overjump first line
            {
                // key = propertie name, value = value
                Dictionary<string, string> Dic = new Dictionary<string, string>();
                int j = 0;
                foreach (var itemstring in Lines[i].Split(strDelimiter))
                {
                    Dic.Add(Headar[j], itemstring);
                    j++;
                }
                (CTRL.AddNewThing()).FromCSV(Dic);
            }
        }
    }
}