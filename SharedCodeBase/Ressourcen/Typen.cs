
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
    public enum FolderMode
    {
        Intern, Extern
    }

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
        KomplexeForm = 26,
        Sprite = 27,
        Widgets = 28,
        Wandlung = 29,
        Initiation = 30,
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

        ThingDefs _ThingType = ThingDefs.UndefTemp;
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
        string _DisplayNameSingular = "";
        public string DisplayNameSingular
        {
            get { return _DisplayNameSingular; }
            set { _DisplayNameSingular = value; NotifyPropertyChanged(); }
        }
        string _DisplayNamePlural = "";
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

        int _Order;
        public int Order
        {
            get { return _Order; }
            set { _Order = value; NotifyPropertyChanged(); }
        }

        public ThingTypeProperty(Type type, ThingDefs item, int v1, int v2)
        {
            Type = type;
            ThingType = item;
            Pivot = v1;
            Order = v2;
        }

        public ThingTypeProperty(Type type, ThingDefs item, int v1, int v2, string singular, string plural) : this(type, item, v1, v2)
        {
            DisplayNameSingular = singular;
            DisplayNamePlural = plural;
        }

        public override string ToString()
        {
            return DisplayNamePlural;
        }
    }
    public static class TypeHelper
    {
        public const int ThingDefsCount = 27;
        private static List<ThingTypeProperty> thingTypeProperties = new List<ThingTypeProperty>() {
           new ThingTypeProperty(null,ThingDefs.Undef, 0, 0){ Usable = false },
           new ThingTypeProperty(null,ThingDefs.UndefTemp, 0, 0){ Usable = false },
           new ThingTypeProperty(typeof(Eigenschaft),ThingDefs.Eigenschaft, 0, 0){ Usable = false },
           new ThingTypeProperty(typeof(Handlung),ThingDefs.Handlung, 0, 0,"Model_Handlung_/Text","Model_HandlungM_/Text"),
           new ThingTypeProperty(typeof(Fertigkeit),ThingDefs.Fertigkeit, 0, 1,"Model_Fertigkeit_/Text","Model_FertigkeitM_/Text"),
           new ThingTypeProperty(typeof(Item),ThingDefs.Item, 1, 0,"Model_Item_/Text","Model_ItemM_/Text"),
           new ThingTypeProperty(typeof(Programm),ThingDefs.Programm, 0, 0,"Model_Programm_/Text","Model_ProgrammM_/Text"),
           new ThingTypeProperty(typeof(Munition),ThingDefs.Munition, 0, 0,"Model_Munition_/Text","Model_MunitionM_/Text"),
           new ThingTypeProperty(typeof(Implantat),ThingDefs.Implantat, 0, 0,"Model_Implantat_/Text","Model_ImplantatM_/Text"),
           new ThingTypeProperty(typeof(Vorteil),ThingDefs.Vorteil, 0, 0,"Model_Vorteil_/Text","Model_VorteilM_/Text"),
           new ThingTypeProperty(typeof(Nachteil),ThingDefs.Nachteil, 0, 0,"Model_Nachteil_/Text","Model_NachteilM_/Text"),
           new ThingTypeProperty(typeof(Connection),ThingDefs.Connection, 0, 0,"Model_Connection_/Text","Model_ConnectionM_/Text"),
           new ThingTypeProperty(typeof(Sin),ThingDefs.Sin, 0, 0,"Model_Sin_/Text","Model_SinM_/Text"),
           new ThingTypeProperty(typeof(Attribut),ThingDefs.Attribut, 0, 0,"Model_Attribut_/Text","Model_AttributM_/Text"),
           new ThingTypeProperty(typeof(Berechnet),ThingDefs.Berechnet, 0, 0,"Model_Berechnet_/Text","Model_BerechnetM_/Text"),
           new ThingTypeProperty(typeof(Nahkampfwaffe),ThingDefs.Nahkampfwaffe, 0, 0,"Model_Nahkampfwaffe_/Text","Model_NahkampfwaffeM_/Text"),
           new ThingTypeProperty(typeof(Fernkampfwaffe),ThingDefs.Fernkampfwaffe, 0, 0,"Model_Fernkampfwaffe_/Text","Model_FernkampfwaffeM_/Text"),
           new ThingTypeProperty(typeof(Kommlink),ThingDefs.Kommlink, 0, 0,"Model_Kommlink_/Text","Model_KommlinkM_/Text"),
           new ThingTypeProperty(typeof(CyberDeck),ThingDefs.CyberDeck, 0, 0,"Model_CyberDeck_/Text","Model_CyberDeckM_/Text"),
           new ThingTypeProperty(typeof(Vehikel),ThingDefs.Vehikel, 0, 0,"Model_Vehikel_/Text","Model_VehikelM_/Text"),
           new ThingTypeProperty(typeof(Panzerung),ThingDefs.Panzerung, 0, 0,"Model_Panzerung_/Text","Model_PanzerungM_/Text"),
           new ThingTypeProperty(typeof(Eigenschaft),ThingDefs.Eigenschaft, 0, 0,"Model_Eigenschaft_/Text","Model_EigenschaftgM_/Text"){ Usable = false },
           new ThingTypeProperty(typeof(Adeptenkraft_KomplexeForm),ThingDefs.Adeptenkraft_KomplexeForm, 0, 0,"Model_Adeptenkraft_KomplexeForm_/Text","Model_Adeptenkraft_KomplexeFormM_/Text"),
           new ThingTypeProperty(typeof(Foki_Widgets),ThingDefs.Foki_Widgets, 0, 0,"Model_Foki_Widgets_/Text","Model_Foki_WidgetsM_/Text"),
           new ThingTypeProperty(typeof(Geist_Sprite),ThingDefs.Geist_Sprite, 0, 0,"Model_Geist_Sprite_/Text","Model_Geist_SpriteM_/Text"),
           new ThingTypeProperty(typeof(Stroemung_Wandlung),ThingDefs.Stroemung_Wandlung, 0, 0,"Model_Stroemung_Wandlung_/Text","Model_Stroemung_WandlungM_/Text"),
           new ThingTypeProperty(typeof(Tradition_Initiation),ThingDefs.Tradition_Initiation, 0, 0,"Model_Tradition_Initiation_/Text","Model_Tradition_InitiationM_/Text"),
           new ThingTypeProperty(typeof(Zaubersprueche),ThingDefs.Zaubersprueche,0,0,"Model_Zaubersprueche_/Text","Model_ZauberspruecheM_/Text"),

           new ThingTypeProperty(typeof(KomplexeForm),ThingDefs.KomplexeForm,0,0,"Model_KomplexeForm_/Text","Model_KomplexeFormM_/Text"),
           new ThingTypeProperty(typeof(Sprite),ThingDefs.Sprite,0,0,"Model_Sprite_/Text","Model_SpriteM_/Text"),
           new ThingTypeProperty(typeof(Widgets),ThingDefs.Widgets,0,0,"Model_Widgets_/Text","Model_WidgetsM_/Text"),
           new ThingTypeProperty(typeof(Wandlung),ThingDefs.Wandlung,0,0,"Model_Wandlung_/Text","Model_WandlungM_/Text"),
           new ThingTypeProperty(typeof(Initiation),ThingDefs.Initiation,0,0,"Model_Initiation_/Text","Model_InitiationM_/Text"),
        };

        public static List<ThingTypeProperty> ThingTypeProperties { get => thingTypeProperties; set => thingTypeProperties = value; }


        public static string ThingDefToString(ThingDefs eThingDefToConvert, bool Mehrzahl)
        {
            try
            {
                if (Mehrzahl)
                {
                    return CrossPlatformHelper.GetString(ThingTypeProperties.Find(x => x.ThingType == eThingDefToConvert).DisplayNamePlural);
                }
                else
                {
                    return CrossPlatformHelper.GetString(ThingTypeProperties.Find(x => x.ThingType == eThingDefToConvert).DisplayNameSingular);
                }
            }
            catch (Exception ex)
            {
                return "N/A";
            }
        }
        public static Type ThingDefToType(ThingDefs eThingDefToConvert)
        {
            return ThingTypeProperties.FirstOrDefault(x => x.ThingType == eThingDefToConvert).Type;
        }

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

}
