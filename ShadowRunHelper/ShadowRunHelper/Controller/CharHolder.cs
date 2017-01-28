using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelper.Ressourcen;
using static ShadowRunHelper.Ressourcen.TypNamen;

namespace ShadowRunHelper.Controller
{
    /// <summary>
    /// Hält einen Char mit samst Controlern und Daten
    /// </summary>
    public class CharHolder
    {
        public string APP_VERSION_NUMBER = Variablen.APP_VERSION_NUMBER;
        public ObservableCollection<CharModel.Thing> lstAll;
        public CharController.cController<CharModel.Handlung> CTRLHandlung { get; set; }
        public CharController.cController<CharModel.Fertigkeit> CTRLFertigkeit { get; set; }
        public CharController.cController<CharModel.Item> CTRLItem { get; set; }
        public CharController.cController<CharModel.Programm> CTRLProgramm { get; set; }
        public CharController.cController<CharModel.Munition> CTRLMunition { get; set; }
        public CharController.cController<CharModel.Implantat> CTRLImplantat { get; set; }
        public CharController.cController<CharModel.Vorteil> CTRLVorteil { get; set; }
        public CharController.cController<CharModel.Nachteil> CTRLNachteil { get; set; }
        public CharController.cController<CharModel.Connection> CTRLConnection { get; set; }
        public CharController.cController<CharModel.Sin> CTRLSin { get; set; }

        public CharController.cAttributController CTRLAttribut { get; set; }
        public CharController.cNahkampfwaffeController CTRLNahkampfwaffe { get; set; }
        public CharController.cFernkampfwaffeController CTRLFernkampfwaffe { get; set; }
        public CharController.cKommlinkController CTRLKommlink { get; set; }
        public CharController.cCyberDeckController CTRLCyberDeck { get; set; }
        public CharController.cVehikelController CTRLVehikel { get; set; }
        public CharController.cPanzerungController CTRLPanzerung { get; set; }

        public CharModel.Person Person { get; set; }

        /// <summary>
        /// Konstruktor nutzen, um neue Controller und Objekte zu erhalten
        /// </summary>
        public CharHolder()
        {
            lstAll = new ObservableCollection<CharModel.Thing>();
            CTRLHandlung = new CharController.cController<CharModel.Handlung>();
            CTRLFertigkeit = new CharController.cController<CharModel.Fertigkeit>();
            CTRLItem= new CharController.cController<CharModel.Item>();
            CTRLProgramm = new CharController.cController<CharModel.Programm>();
            CTRLMunition = new CharController.cController<CharModel.Munition>();
            CTRLImplantat = new CharController.cController<CharModel.Implantat>();
            CTRLVorteil = new CharController.cController<CharModel.Vorteil>();
            CTRLNachteil = new CharController.cController<CharModel.Nachteil>();
            CTRLConnection = new CharController.cController<CharModel.Connection>();
            CTRLSin = new CharController.cController<CharModel.Sin>();

            CTRLAttribut = new CharController.cAttributController();
            CTRLNahkampfwaffe = new CharController.cNahkampfwaffeController();
            CTRLFernkampfwaffe = new CharController.cFernkampfwaffeController();
            CTRLKommlink = new CharController.cKommlinkController();
            CTRLCyberDeck = new CharController.cCyberDeckController();
            CTRLVehikel = new CharController.cVehikelController();
            CTRLPanzerung = new CharController.cPanzerungController();

            Person = new CharModel.Person();

            
        }


        internal void Add(ThingDefs thingDefs)
        {
            CharModel.Thing tToAdd = null;
            switch (thingDefs)
            {
                case ThingDefs.Handlung:
                    tToAdd = CTRLHandlung.AddNewThing();
                    break;
                case ThingDefs.Fertigkeit:
                    tToAdd = CTRLFertigkeit.AddNewThing();
                    break;
                case ThingDefs.Item:
                    break;
                case ThingDefs.Programm:
                    break;
                case ThingDefs.Munition:
                    break;
                case ThingDefs.Implantat:
                    break;
                case ThingDefs.Vorteil:
                    break;
                case ThingDefs.Nachteil:
                    break;
                case ThingDefs.Connection:
                    break;
                case ThingDefs.Sin:
                    break;
                case ThingDefs.Attribut:
                    tToAdd = CTRLAttribut.AddNewThing();
                    break;
                case ThingDefs.Nahkampfwaffe:
                    break;
                case ThingDefs.Fernkampfwaffe:
                    break;
                case ThingDefs.Kommlink:
                    break;
                case ThingDefs.CyberDeck:
                    tToAdd = CTRLCyberDeck.AddNewThing();
                    break;
                case ThingDefs.Vehikel:
                    break;
                case ThingDefs.Panzerung:
                    break;
                default:
                    break;
            }
            if (tToAdd == null)
            {
                throw new WrongTypeException();
            }
            lstAll.Add(tToAdd);
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
                    break;
                case ThingDefs.Programm:
                    break;
                case ThingDefs.Munition:
                    break;
                case ThingDefs.Implantat:
                    break;
                case ThingDefs.Vorteil:
                    break;
                case ThingDefs.Nachteil:
                    break;
                case ThingDefs.Connection:
                    break;
                case ThingDefs.Sin:
                    break;
                case ThingDefs.Attribut:
                    CTRLAttribut.RemoveThing((Attribut)tToRemove);
                    break;
                case ThingDefs.Nahkampfwaffe:
                    break;
                case ThingDefs.Fernkampfwaffe:
                    break;
                case ThingDefs.Kommlink:
                    break;
                case ThingDefs.CyberDeck:
                    CTRLCyberDeck.RemoveThing((CyberDeck)tToRemove);
                    break;
                case ThingDefs.Vehikel:
                    break;
                case ThingDefs.Panzerung:
                    break;
                default:
                    break;
            }
            if (tToRemove == null)
            {
                throw new NotImplementedException();
            }
            lstAll.Remove(tToRemove);
            throw new NotImplementedException();
        }
    }
}