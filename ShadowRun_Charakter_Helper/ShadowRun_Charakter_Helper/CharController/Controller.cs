using ShadowRun_Charakter_Helper.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Controller<T> where T : CharModel.Model, new()
    {
        public Dictionary<String, String> Ressources = new Dictionary<String, String>();
        protected string DicCD_Bezeichner = "nichts vorhanden";
        protected string DicCD_Typ = "error";
        protected double DicCD_Wert = 0;
        protected string DicCD_Zusatz = "";
        protected string DicCD_Notiz = "";

        public Controller.HashDictionary HD;
        public int HD_ID { get; set; }

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
            HD.Add(this.HD_ID, new Model.DictionaryCharEntry(DicCD_Bezeichner, DicCD_Typ, DicCD_Wert, DicCD_Zusatz, DicCD_Notiz));
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
            DicCD_Typ = this.GetType().ToString();
            Ressources.TryGetValue(DicCD_Typ, out DicCD_Typ);
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
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.CyberDecks", "CyberDecks");
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.Kommlink", "Kommlink");
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.Nahkampfwaffen", "Nahkampfwaffen");
            Ressources.Add("ShadowRun_Charakter_Helper.CharController.Fernkampfwaffen", "Fernkampfwaffen");
        }
    }
}