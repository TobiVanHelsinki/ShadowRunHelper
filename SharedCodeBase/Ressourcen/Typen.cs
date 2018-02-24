

using ShadowRunHelper.CharModel;
using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TLIB_UWPFRAME;
using TLIB_UWPFRAME.Model;

namespace ShadowRunHelper
{
    public enum ProjectPages
    {
        undef = 0,
        Char = 1,
        Administration = 2,
        Settings = 3,
    }
    public enum ProjectPagesOptions
    {
        SettingsMain = 31,
        SettingsOptions = 32,
        SettingsCategories = 33,
        SettingsHelp = 34,
        Nothing = 35,
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
        Stroemung_Wandlung = 22,
        Tradition_Initiation = 23,
        Zaubersprueche = 24,
        Berechnet = 25,
    }
    public class ThingTypeProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ModelHelper.CallPropertyChangedAtDispatcher(PropertyChanged, this, propertyName);
        }
        Type _Type;
        public Type Type
        {
            get { return _Type; }
            set { _Type = value; NotifyPropertyChanged(); }
        }

        ThingDefs _ThingType;
        public ThingDefs ThingType
        {
            get { return _ThingType; }
            set { _ThingType = value; NotifyPropertyChanged(); }
        }

        bool _vis = true;
        public bool Visibility
        {
            get { return _vis; }
            set { _vis = value; NotifyPropertyChanged(); }
        }
        int _pivot;
        public int Pivot
        {
            get { return _pivot; }
            set { _pivot = value; NotifyPropertyChanged(); }
        }
        string _DisplayNameSingular;
        public string DisplayNameSingular
        {
            get { return _DisplayNameSingular; }
            set { _DisplayNameSingular = value; NotifyPropertyChanged(); }
        }
        string _DisplayNamePlural;
        public string DisplayNamePlural
        {
            get { return _DisplayNamePlural; }
            set { _DisplayNamePlural = value; NotifyPropertyChanged(); }
        }
        bool _Usable = true;
        public bool Usable
        {
            get { return _Usable; }
            set { _Usable = value; NotifyPropertyChanged(); }
        }


        public ThingTypeProperty(Type type, ThingDefs item, int v1, int v2)
        {
            Type = type;
            ThingType = item;
            Pivot = v1;
            Order = v2;
        }

        int _Order;
        public int Order
        {
            get { return _Order; }
            set { _Order = value; NotifyPropertyChanged(); }
        }
    }
    public static class TypeHelper
    {
        public const int ThingDefsCount = 27;

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
                    case ThingDefs.Berechnet:
                        return CrossPlatformHelper.GetString("Model_Berechnet_/Text");
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
                    case ThingDefs.Stroemung_Wandlung:
                        return CrossPlatformHelper.GetString("Model_Stroemung_Wandlung_/Text");
                    case ThingDefs.Tradition_Initiation:
                        return CrossPlatformHelper.GetString("Model_Tradition_Initiation_/Text");
                    case ThingDefs.Zaubersprueche:
                        return CrossPlatformHelper.GetString("Model_Zaubersprueche_/Text");
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
                    case ThingDefs.Berechnet:
                        return CrossPlatformHelper.GetString("Model_BerechnetM_/Text");
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
                    case ThingDefs.Stroemung_Wandlung:
                        return CrossPlatformHelper.GetString("Model_Stroemung_WandlungM_/Text");
                    case ThingDefs.Tradition_Initiation:
                        return CrossPlatformHelper.GetString("Model_Tradition_InitiationM_/Text");
                    case ThingDefs.Zaubersprueche:
                        return CrossPlatformHelper.GetString("Model_ZauberspruecheM_/Text");
                    default:
                        break;
                }
            }
            return "";
        }
        public static Type ThingDefToType(ThingDefs eThingDefToConvert)
        {
            return ThingTypeProperties.FirstOrDefault(x => x.ThingType == eThingDefToConvert).Type;
        }

        public static List<ThingTypeProperty> ThingTypeProperties = new List<ThingTypeProperty>() {
           new ThingTypeProperty(null,ThingDefs.Undef, 0, 0){ Usable = false },
           new ThingTypeProperty(null,ThingDefs.UndefTemp, 0, 0){ Usable = false },
           new ThingTypeProperty(typeof(Eigenschaft),ThingDefs.Eigenschaft, 0, 0){ Usable = false },
           new ThingTypeProperty(typeof(Handlung),ThingDefs.Handlung, 0, 0),
           new ThingTypeProperty(typeof(Fertigkeit),ThingDefs.Fertigkeit, 0, 1),
           new ThingTypeProperty(typeof(Item),ThingDefs.Item, 1, 0),
           new ThingTypeProperty(typeof(Programm),ThingDefs.Programm, 0, 0),
           new ThingTypeProperty(typeof(Munition),ThingDefs.Munition, 0, 0),
           new ThingTypeProperty(typeof(Implantat),ThingDefs.Implantat, 0, 0),
           new ThingTypeProperty(typeof(Vorteil),ThingDefs.Vorteil, 0, 0),
           new ThingTypeProperty(typeof(Nachteil),ThingDefs.Nachteil, 0, 0),
           new ThingTypeProperty(typeof(Connection),ThingDefs.Connection, 0, 0),
           new ThingTypeProperty(typeof(Sin),ThingDefs.Sin, 0, 0),
           new ThingTypeProperty(typeof(Attribut),ThingDefs.Attribut, 0, 0),
           new ThingTypeProperty(typeof(Berechnet),ThingDefs.Berechnet, 0, 0),
           new ThingTypeProperty(typeof(Nahkampfwaffe),ThingDefs.Nahkampfwaffe, 0, 0),
           new ThingTypeProperty(typeof(Fernkampfwaffe),ThingDefs.Fernkampfwaffe, 0, 0),
           new ThingTypeProperty(typeof(Kommlink),ThingDefs.Kommlink, 0, 0),
           new ThingTypeProperty(typeof(CyberDeck),ThingDefs.CyberDeck, 0, 0),
           new ThingTypeProperty(typeof(Vehikel),ThingDefs.Vehikel, 0, 0),
           new ThingTypeProperty(typeof(Panzerung),ThingDefs.Panzerung, 0, 0),
           new ThingTypeProperty(typeof(Eigenschaft),ThingDefs.Eigenschaft, 0, 0){ Usable = false },
           new ThingTypeProperty(typeof(Adeptenkraft_KomplexeForm),ThingDefs.Adeptenkraft_KomplexeForm, 0, 0),
           new ThingTypeProperty(typeof(Foki_Widgets),ThingDefs.Foki_Widgets, 0, 0),
           new ThingTypeProperty(typeof(Geist_Sprite),ThingDefs.Geist_Sprite, 0, 0),
           new ThingTypeProperty(typeof(Stroemung_Wandlung),ThingDefs.Stroemung_Wandlung, 0, 0),
           new ThingTypeProperty(typeof(Tradition_Initiation),ThingDefs.Tradition_Initiation, 0, 0),
           new ThingTypeProperty(typeof(Zaubersprueche),ThingDefs.Zaubersprueche,0,0)
        };

        public static ThingDefs TypeToThingDef(Type type)
        {
            try
            {
                return ThingTypeProperties.Find(x => x.Type == type).ThingType;
            }
            catch (Exception)
            {
                return ThingDefs.Undef;
            }
        }

        public static ThingDefs Obj2ThingDef(int tag)
        {
            return (ThingDefs)tag;
        }
        public static ThingDefs Obj2ThingDef(string Name)
        {
            return ThingTypeProperties.First(t => t.ThingType.ToString() == Name).ThingType;
        }
        public static ThingDefs Obj2ThingDef(object tag)
        {
            return (ThingDefs)Int16.Parse(tag.ToString());
        }
    }

    public enum FolderMode
    {
        Intern, Extern
    }
}
