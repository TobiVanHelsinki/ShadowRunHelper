using System;
using System.Collections.Generic;
namespace ShadowRunHelper1_3.Ressourcen
{
    class TypNamen
    {
        public static string GetName_Controller(string input)
        {
            Dictionary<String, String> Ressources = new Dictionary<String, String>();
            Ressources.Add("ShadowRunHelper.CharController.Fertigkeit", "Fertigkeit");
            Ressources.Add("ShadowRunHelper.CharController.Handlung", "Handlung");
            Ressources.Add("ShadowRunHelper.CharController.Attribut", "Attribut");
            Ressources.Add("ShadowRunHelper.CharController.Item", "Item");
            Ressources.Add("ShadowRunHelper.CharController.Programm", "Programm");
            Ressources.Add("ShadowRunHelper.CharController.Vorteil", "Vorteil");
            Ressources.Add("ShadowRunHelper.CharController.Nachteil", "Nachteil");
            Ressources.Add("ShadowRunHelper.CharController.Panzerung", "Panzerung");
            Ressources.Add("ShadowRunHelper.CharController.CyberDeck", "CyberDeck");
            Ressources.Add("ShadowRunHelper.CharController.Kommlink", "Kommlink");
            Ressources.Add("ShadowRunHelper.CharController.Vehikel", "Vehikel");
            Ressources.Add("ShadowRunHelper.CharController.Nahkampfwaffe", "Nahkampfwaffe");
            Ressources.Add("ShadowRunHelper.CharController.Fernkampfwaffe", "Fernkampfwaffe");

            Ressources.TryGetValue(input, out input);

            return input;
        }
    }
}
