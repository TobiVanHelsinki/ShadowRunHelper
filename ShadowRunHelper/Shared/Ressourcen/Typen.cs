

using TLIB;

namespace ShadowRunHelper
{
    public enum ProjectPages
    {
        undef = 0,
        Char = 1,
        Verwaltung = 2,
        Settings = 3,
    }
    public enum TCharState
    {
        EMPTY_CHAR = 0,
        IN_USE = 1,
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
        Adeptenkraft_KomplexeForm = 19,
        Geist_Sprite = 20,
        Foki_Widgets = 21,
        Strömung_Wandlung = 22,
        Tradition_Initiation = 23,
        Zaubersprüche = 24,
    }

    public static class TypenHelper
    {
        public static string ThingDefToString(ThingDefs eThingDefToConvert, bool Mehrzahl)
        {
            if (Mehrzahl == false)
            {
                switch (eThingDefToConvert)
                {
                    case ThingDefs.UndefTemp:
                        return "";
                    case ThingDefs.Undef:
                        return "";
                    case ThingDefs.Handlung:
                        return CrossPlatformHelper.GetString("Model_Handlung_/Text");
                    case ThingDefs.Fertigkeit:
                        return CrossPlatformHelper.GetString("Model_Fertigkeit_/Text");
                    case ThingDefs.Item:
                        return CrossPlatformHelper.GetString("Model_Item_/Text");
                    case ThingDefs.Programm:
                        return CrossPlatformHelper.GetString("Model_Programm_/Text");
                    case ThingDefs.Munition:
                        return CrossPlatformHelper.GetString("Model_Munition_/Text");
                    case ThingDefs.Implantat:
                        return CrossPlatformHelper.GetString("Model_Implantat_/Text");
                    case ThingDefs.Vorteil:
                        return CrossPlatformHelper.GetString("Model_Vorteil_/Text");
                    case ThingDefs.Nachteil:
                        return CrossPlatformHelper.GetString("Model_Nachteil_/Text");
                    case ThingDefs.Connection:
                        return CrossPlatformHelper.GetString("Model_Connection_/Text");
                    case ThingDefs.Sin:
                        return CrossPlatformHelper.GetString("Model_Sin_/Text");
                    case ThingDefs.Attribut:
                        return CrossPlatformHelper.GetString("Model_Attribut_/Text");
                    case ThingDefs.Nahkampfwaffe:
                        return CrossPlatformHelper.GetString("Model_Nahkampfwaffe_/Text");
                    case ThingDefs.Fernkampfwaffe:
                        return CrossPlatformHelper.GetString("Model_Fernkampfwaffe_/Text");
                    case ThingDefs.Kommlink:
                        return CrossPlatformHelper.GetString("Model_Kommlink_/Text");
                    case ThingDefs.CyberDeck:
                        return CrossPlatformHelper.GetString("Model_CyberDeck_/Text");
                    case ThingDefs.Vehikel:
                        return CrossPlatformHelper.GetString("Model_Vehikel_/Text");
                    case ThingDefs.Panzerung:
                        return CrossPlatformHelper.GetString("Model_Panzerung_/Text");
                    case ThingDefs.Adeptenkraft_KomplexeForm:
                        return CrossPlatformHelper.GetString("Model_Adeptenkraft_KomplexeForm_/Text");
                    case ThingDefs.Geist_Sprite:
                        return CrossPlatformHelper.GetString("Model_Geist_Sprite_/Text");
                    case ThingDefs.Foki_Widgets:
                        return CrossPlatformHelper.GetString("Model_Foki_Widgets_/Text");
                    case ThingDefs.Strömung_Wandlung:
                        return CrossPlatformHelper.GetString("Model_Strömung_Wandlung_/Text");
                    case ThingDefs.Tradition_Initiation:
                        return CrossPlatformHelper.GetString("Model_Tradition_Initiation_/Text");
                    case ThingDefs.Zaubersprüche:
                        return CrossPlatformHelper.GetString("Model_Zaubersprüche_/Text");
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
                        return CrossPlatformHelper.GetString("Model_HandlungM_/Text");
                    case ThingDefs.Fertigkeit:
                        return CrossPlatformHelper.GetString("Model_FertigkeitM_/Text");
                    case ThingDefs.Item:
                        return CrossPlatformHelper.GetString("Model_ItemM_/Text");
                    case ThingDefs.Programm:
                        return CrossPlatformHelper.GetString("Model_ProgrammM_/Text");
                    case ThingDefs.Munition:
                        return CrossPlatformHelper.GetString("Model_MunitionM_/Text");
                    case ThingDefs.Implantat:
                        return CrossPlatformHelper.GetString("Model_ImplantatM_/Text");
                    case ThingDefs.Vorteil:
                        return CrossPlatformHelper.GetString("Model_VorteilM_/Text");
                    case ThingDefs.Nachteil:
                        return CrossPlatformHelper.GetString("Model_NachteilM_/Text");
                    case ThingDefs.Connection:
                        return CrossPlatformHelper.GetString("Model_ConnectionM_/Text");
                    case ThingDefs.Sin:
                        return CrossPlatformHelper.GetString("Model_SinM_/Text");
                    case ThingDefs.Attribut:
                        return CrossPlatformHelper.GetString("Model_AttributM_/Text");
                    case ThingDefs.Nahkampfwaffe:
                        return CrossPlatformHelper.GetString("Model_NahkampfwaffeM_/Text");
                    case ThingDefs.Fernkampfwaffe:
                        return CrossPlatformHelper.GetString("Model_FernkampfwaffeM_/Text");
                    case ThingDefs.Kommlink:
                        return CrossPlatformHelper.GetString("Model_KommlinkM_/Text");
                    case ThingDefs.CyberDeck:
                        return CrossPlatformHelper.GetString("Model_CyberDeckM_/Text");
                    case ThingDefs.Vehikel:
                        return CrossPlatformHelper.GetString("Model_VehikelM_/Text");
                    case ThingDefs.Panzerung:
                        return CrossPlatformHelper.GetString("Model_PanzerungM_/Text");
                    case ThingDefs.Adeptenkraft_KomplexeForm:
                        return CrossPlatformHelper.GetString("Model_Adeptenkraft_KomplexeFormM_/Text");
                    case ThingDefs.Geist_Sprite:
                        return CrossPlatformHelper.GetString("Model_Geist_SpriteM_/Text");
                    case ThingDefs.Foki_Widgets:
                        return CrossPlatformHelper.GetString("Model_Foki_WidgetsM_/Text");
                    case ThingDefs.Strömung_Wandlung:
                        return CrossPlatformHelper.GetString("Model_Strömung_WandlungM_/Text");
                    case ThingDefs.Tradition_Initiation:
                        return CrossPlatformHelper.GetString("Model_Tradition_InitiationM_/Text");
                    case ThingDefs.Zaubersprüche:
                        return CrossPlatformHelper.GetString("Model_ZaubersprücheM_/Text");
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
