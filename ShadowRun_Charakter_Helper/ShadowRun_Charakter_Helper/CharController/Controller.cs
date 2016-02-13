using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Controller<T> where T : CharModel.Model, new()
    {
        public Dictionary<String, String> Ressources = new Dictionary<String, String>();
        protected string DicCD_Bezeichner = "noch nicht benannt";
        protected string DicCD_Typ = "";
        protected double DicCD_Wert = 0;
        protected string DicCD_Zusatz = "";
        protected string DicCD_Notiz = "";

        public Controller.HashDictionary HD;
        public int HD_ID { get; set; }

        public void setHD(Controller.HashDictionary hD)
        {
            this.HD = hD;
            if (this.HD_ID == 0)
            {
                this.HD_ID = HD.getFreeKey();
            }
            add_to_HD();
        }

        protected void add_to_HD()
        {
            HD.Data.Add(this.HD_ID, new Models.DictionaryCharEntry(DicCD_Bezeichner, DicCD_Typ, DicCD_Wert, DicCD_Zusatz, DicCD_Notiz));
        }

        public void remove_from_HD()
        {
            HD.Data.Remove(this.HD_ID);
        }


        public Controller()
        {
            addRessources();
            DicCD_Typ = this.GetType().ToString();
            Ressources.TryGetValue(DicCD_Typ, out DicCD_Typ);
        }
        public Controller(T obj)
        {
            addRessources();
            DicCD_Typ = this.GetType().ToString();
            Ressources.TryGetValue(DicCD_Typ, out DicCD_Typ);
        }
        public void Aktualisierungen()
        {
            //ToDo:
        }


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