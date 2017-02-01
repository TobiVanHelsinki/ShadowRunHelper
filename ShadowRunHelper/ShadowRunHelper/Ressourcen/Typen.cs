using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources;

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

    public static class TypenHelper
    {
        public static string ThingDefToString(ThingDefs eThingDefToConvert, bool Mehrzahl)
        {
            var res = ResourceLoader.GetForCurrentView();
            if (Mehrzahl == false)
            {
                switch (eThingDefToConvert)
                {
                    case ThingDefs.UndefTemp:
                        return "";
                    case ThingDefs.Undef:
                        return "";
                    case ThingDefs.Handlung:
                        return res.GetString("Model_Handlung_/Text");
                    case ThingDefs.Fertigkeit:
                        return res.GetString("Model_Fertigkeit_/Text");
                    case ThingDefs.Item:
                        return res.GetString("Model_Item_/Text");
                    case ThingDefs.Programm:
                        return res.GetString("Model_Programm_/Text");
                    case ThingDefs.Munition:
                        return res.GetString("Model_Munition_/Text");
                    case ThingDefs.Implantat:
                        return res.GetString("Model_Implantat_/Text");
                    case ThingDefs.Vorteil:
                        return res.GetString("Model_Vorteil_/Text");
                    case ThingDefs.Nachteil:
                        return res.GetString("Model_Nachteil_/Text");
                    case ThingDefs.Connection:
                        return res.GetString("Model_Connection_/Text");
                    case ThingDefs.Sin:
                        return res.GetString("Model_Sin_/Text");
                    case ThingDefs.Attribut:
                        return res.GetString("Model_Attribut_/Text");
                    case ThingDefs.Nahkampfwaffe:
                        return res.GetString("Model_Nahkampfwaffe_/Text");
                    case ThingDefs.Fernkampfwaffe:
                        return res.GetString("Model_Fernkampfwaffe_/Text");
                    case ThingDefs.Kommlink:
                        return res.GetString("Model_Kommlink_/Text");
                    case ThingDefs.CyberDeck:
                        return res.GetString("Model_CyberDeck_/Text");
                    case ThingDefs.Vehikel:
                        return res.GetString("Model_Vehikel_/Text");
                    case ThingDefs.Panzerung:
                        return res.GetString("Model_Panzerung_/Text");
                    default:
                        break;
                }
            }
            else
            {
                switch (eThingDefToConvert)
                {
                    case ThingDefs.UndefTemp:
                        return "";
                    case ThingDefs.Undef:
                        return "";
                    case ThingDefs.Handlung:
                        return res.GetString("Model_HandlungM_/Text");
                    case ThingDefs.Fertigkeit:
                        return res.GetString("Model_FertigkeitM_/Text");
                    case ThingDefs.Item:
                        return res.GetString("Model_ItemM_/Text");
                    case ThingDefs.Programm:
                        return res.GetString("Model_ProgrammM_/Text");
                    case ThingDefs.Munition:
                        return res.GetString("Model_MunitionM_/Text");
                    case ThingDefs.Implantat:
                        return res.GetString("Model_ImplantatM_/Text");
                    case ThingDefs.Vorteil:
                        return res.GetString("Model_VorteilM_/Text");
                    case ThingDefs.Nachteil:
                        return res.GetString("Model_NachteilM_/Text");
                    case ThingDefs.Connection:
                        return res.GetString("Model_ConnectionM_/Text");
                    case ThingDefs.Sin:
                        return res.GetString("Model_SinM_/Text");
                    case ThingDefs.Attribut:
                        return res.GetString("Model_AttributM_/Text");
                    case ThingDefs.Nahkampfwaffe:
                        return res.GetString("Model_NahkampfwaffeM_/Text");
                    case ThingDefs.Fernkampfwaffe:
                        return res.GetString("Model_FernkampfwaffeM_/Text");
                    case ThingDefs.Kommlink:
                        return res.GetString("Model_KommlinkM_/Text");
                    case ThingDefs.CyberDeck:
                        return res.GetString("Model_CyberDeckM_/Text");
                    case ThingDefs.Vehikel:
                        return res.GetString("Model_VehikelM_/Text");
                    case ThingDefs.Panzerung:
                        return res.GetString("Model_PanzerungM_/Text");
                    default:
                        break;
                }
            }
            return "";
        }
    }

    public enum FolderMode
    {
        Intern, Extern
    }
}
