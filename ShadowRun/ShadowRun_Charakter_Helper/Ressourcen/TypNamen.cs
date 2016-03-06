using System;
using System.Collections.Generic;
namespace ShadowRun_Charakter_Helper.Ressourcen
{
    class TypNamen
    {
        public static string GetName_Controller(string input)
        {
            Dictionary<String, String> Ressources = new Dictionary<String, String>();
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

            Ressources.TryGetValue(input, out input);

            return input;
        }
    }
}
