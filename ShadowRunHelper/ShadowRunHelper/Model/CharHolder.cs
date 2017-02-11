using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.CharController;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.ApplicationModel.Resources;

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
        public List<ThingListEntry> lstThings;

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

            lstThings = new List<ThingListEntry>();
        }


        internal Thing Add(ThingDefs thingDefs)
        {
            Thing returnThing = null;
            switch (thingDefs)
            {
                case ThingDefs.Handlung:
                    returnThing = CTRLHandlung.AddNewThing();
                    break;
                case ThingDefs.Fertigkeit:
                    returnThing = CTRLFertigkeit.AddNewThing();
                    break;
                case ThingDefs.Item:
                    returnThing = CTRLItem.AddNewThing();
                    break;
                case ThingDefs.Programm:
                    returnThing = CTRLProgramm.AddNewThing();
                    break;
                case ThingDefs.Munition:
                    returnThing = CTRLMunition.AddNewThing();
                    break;
                case ThingDefs.Implantat:
                    returnThing = CTRLImplantat.AddNewThing();
                    break;
                case ThingDefs.Vorteil:
                    returnThing = CTRLVorteil.AddNewThing();
                    break;
                case ThingDefs.Nachteil:
                    returnThing = CTRLNachteil.AddNewThing();
                    break;
                case ThingDefs.Connection:
                    returnThing = CTRLConnection.AddNewThing();
                    break;
                case ThingDefs.Sin:
                    returnThing = CTRLSin.AddNewThing();
                    break;
                case ThingDefs.Attribut:
                    returnThing = CTRLAttribut.AddNewThing();
                    break;
                case ThingDefs.Nahkampfwaffe:
                    returnThing = CTRLNahkampfwaffe.AddNewThing();
                    break;
                case ThingDefs.Fernkampfwaffe:
                    returnThing = CTRLFernkampfwaffe.AddNewThing();
                    break;
                case ThingDefs.Kommlink:
                    returnThing = CTRLKommlink.AddNewThing();
                    break;
                case ThingDefs.CyberDeck:
                    returnThing = CTRLCyberDeck.AddNewThing();
                    break;
                case ThingDefs.Vehikel:
                    returnThing = CTRLVehikel.AddNewThing();
                    break;
                case ThingDefs.Panzerung:
                    returnThing = CTRLPanzerung.AddNewThing();
                    break;
                default:
                    break;
            }
            RefreshThingList();
            return returnThing;
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
            lstThings.Remove(lstThings.Find((x) => x.Object == tToRemove));
        }

        public string MakeName()
        {
            return Person.strMakeName;
        }

        /// <summary>
        /// retval: List of Pairs: Key is Name for waht is at the string (attribut or cyberdeck orso)
        /// </summary>
        /// <param name="strDelimiter"></param>
        /// <returns></returns>
        /// 
        public List<KeyValuePair<string, string>> ToCSV(string strDelimiter)
        {
            var lstReturn = new List<KeyValuePair<string, string>>();
            const string strNewLine = "\n";
            string strNew = "sep="+strDelimiter+strNewLine;
        
            lstReturn.Add( CTRLAttribut.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLConnection.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLCyberDeck.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLFernkampfwaffe.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLFertigkeit.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLHandlung.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLImplantat.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLItem.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLKommlink.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLMunition.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLNachteil.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLNahkampfwaffe.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLPanzerung.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLProgramm.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLSin.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLVehikel.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLVorteil.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            return lstReturn;
        }

        public void FromCSV(char strDelimiter, string strImport, ThingDefs eThing)
        {
            const char strNewLine = '\n';
            switch (eThing)
            {
                case ThingDefs.UndefTemp:
                    break;
                case ThingDefs.Undef:
                    break;
                case ThingDefs.Handlung:
                    CTRLHandlung.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Fertigkeit:
                    CTRLFertigkeit.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Item:
                    CTRLItem.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Programm:
                    CTRLProgramm.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Munition:
                    CTRLMunition.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Implantat:
                    CTRLImplantat.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Vorteil:
                    CTRLVorteil.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Nachteil:
                    CTRLNachteil.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Connection:
                    CTRLConnection.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Sin:
                    CTRLSin.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Attribut:
                    CTRLAttribut.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Nahkampfwaffe:
                    CTRLNahkampfwaffe.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Fernkampfwaffe:
                    CTRLFernkampfwaffe.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Kommlink:
                    CTRLKommlink.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.CyberDeck:
                    CTRLCyberDeck.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Vehikel:
                    CTRLVehikel.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Panzerung:
                    CTRLPanzerung.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Eigenschaft:
                    break;
                default:
                    break;
            }
            RefreshThingList();
        }


        }
    }
