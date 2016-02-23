using ShadowRun_Charakter_Helper.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Controller<T> where T : CharModel.Model, new()
    {
        public int HD_ID { get; set; }
        protected string HD_Bezeichner = "nichts vorhanden";
        protected string HD_Typ = "error";
        protected double HD_Wert = 0;
        protected string HD_Zusatz = "";
        protected string HD_Notiz = "";

        public Dictionary<String, String> Ressources = new Dictionary<String, String>();

        public Controller.HashDictionary HD;
        

        /// <summary>
        /// Gibt dem Controller DAS HashDictionary \n 
        /// Sucht sich aus dem HD eine neue ID, wenn er keine hat \n 
        /// Fügt sich selbst anschließend dem HD hinzu \n 
        /// Registriert sich als Beobachter beim HD
        /// </summary>
        /// <param name="hD">HashDictionary des Chars</param>
        public void setHD(Controller.HashDictionary hD)
        {
            this.HD = hD;
            if (this.HD_ID == 0)
            {
                this.HD_ID = HD.getFreeKey();
            }
            add_to_HD();
        }

        /// <summary>
        /// Fügt sich selbst dem HD hinzu
        /// </summary>
        protected void add_to_HD()
        {
            HD.Add(this.HD_ID, new Model.DictionaryCharEntry(HD_Bezeichner, HD_Typ, HD_Wert, HD_Zusatz, HD_Notiz));
        }
        /// <summary>
        /// löscht sich aus dem HD
        /// </summary>
        public void remove_from_HD()
        {
            HD.Remove(this.HD_ID);
        }

        /// <summary>
        /// Konstruktor für neu
        /// </summary>
        public Controller()
        {
            addRessources();
            HD_Typ = this.GetType().ToString();
            Ressources.TryGetValue(HD_Typ, out HD_Typ);
        }
        
        /// <summary>
        /// Aktualisiert den Wert im HD
        /// </summary>
        //ToDo schreiben
        public void Aktualisierungen()
        {
        }

        /// <summary>
        /// Hilfmethode zum Bennenen
        /// </summary>
        /// <completionlist cref=""/>
        private void addRessources()
        {
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.Fertigkeit", "Fertigkeit");
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.Handlung", "Handlung");
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.Attribut", "Attribut");
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.Item", "Item");
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.Programm", "Programm");
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.Vorteil", "Vorteil");
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.Nachteil", "Nachteil");
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.Panzerung", "Panzerung");
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.CyberDeck", "CyberDeck");
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.Kommlink", "Kommlink");
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.Vehikel", "Vehikel");
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.Nahkampfwaffe", "Nahkampfwaffe");
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.Fernkampfwaffe", "Fernkampfwaffe");
        }
    }
}