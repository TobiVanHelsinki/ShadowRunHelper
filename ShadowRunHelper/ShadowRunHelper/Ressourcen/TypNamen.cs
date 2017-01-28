using System;
using System.Collections.Generic;
namespace ShadowRunHelper
{
    public enum TCharState
    {
        EMPTY_CHAR = 0,
        LOAD_CHAR = 1,
        NEW_CHAR = 2,
    }
    public enum ThingDefs
    {
        UndefTemp = -2,
        Undef = -1,

        Handlung = 1,
        Fertigkeit = 2,
        Item = 3,
        Programm = 4,
        Munition = 5,
        Implantat = 6,
        Vorteil = 7,
        Nachteil = 8,
        Connection = 9,
        Sin = 10,
        Attribut = 11,
        Nahkampfwaffe = 12,
        Fernkampfwaffe = 13,
        Kommlink = 14,
        CyberDeck = 15,
        Vehikel = 16,
        Panzerung = 17,
        Eigenschaft = 18,
    }

    public static class TypNamen
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
