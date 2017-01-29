using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ShadowRunHelper.CharModel;

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
            lstThings.AddRange(CTRLHandlung.GetElementsForThingList());
            lstThings.AddRange(CTRLFertigkeit.GetElementsForThingList());
            lstThings.AddRange(CTRLItem.GetElementsForThingList());
            lstThings.AddRange(CTRLProgramm.GetElementsForThingList());
            lstThings.AddRange(CTRLMunition.GetElementsForThingList());
            lstThings.AddRange(CTRLImplantat.GetElementsForThingList());
            lstThings.AddRange(CTRLVorteil.GetElementsForThingList());
            lstThings.AddRange(CTRLNachteil.GetElementsForThingList());
            lstThings.AddRange(CTRLConnection.GetElementsForThingList());
            lstThings.AddRange(CTRLSin.GetElementsForThingList());
            //liefern immer das selbe
            lstThings.AddRange(CTRLAttribut.GetElementsForThingList());
            lstThings.AddRange(CTRLNahkampfwaffe.GetElementsForThingList());
            lstThings.AddRange(CTRLFernkampfwaffe.GetElementsForThingList());
            lstThings.AddRange(CTRLKommlink.GetElementsForThingList());
            lstThings.AddRange(CTRLCyberDeck.GetElementsForThingList());
            lstThings.AddRange(CTRLVehikel.GetElementsForThingList());
            lstThings.AddRange(CTRLPanzerung.GetElementsForThingList());
        }

        /// <summary>
        /// Konstruktor nutzen, wenn Daten der Controller und Objekte bereits vorhanden, Parmas sind die ID der MultiController
        /// </summary>

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
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
    }
}