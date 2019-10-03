
using ShadowRunHelper.CharModel;
using SharedCode.Ressourcen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TAPPLICATION;
using TLIB;

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
        Nothing = 0,

        CharNewChar = 10,
        Char_Action = 11,
        Char_Items = 12,
        Char_Battle = 13,
        Char_Person = 14,
        Char_Notes = 15,
        Char_Settings = 16,

        SettingsMain = 31,
        SettingsOptions = 32,
        SettingsCategories = 33,
        SettingsHelp = 34,
        SettingsNots = 35,
        SettingsBuy = 36,
        Import = 37,
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
        Adeptenkraft = 19,
        Geist = 20,
        Foki = 21,
        Stroemung = 22,
        Tradition = 23,
        Zaubersprueche = 24,
        Berechnet = 25,
        KomplexeForm = 26,
        Sprite = 27,
        Widgets = 28,
        Wandlung = 29,
        Initiation = 30,
        Note = 31,
        Favorite = 32, 
    }
    public class ThingTypeProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PlatformHelper.CallPropertyChanged(PropertyChanged, this, propertyName);
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
        string _DisplayName = "";
        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; NotifyPropertyChanged(); NotifyPropertyChanged("DisplayNameSingular"); NotifyPropertyChanged("DisplayNamePlural"); }
        }
        public string DisplayNameSingular
        {
            get { return "" + _DisplayName + "_"; }
        }
        public string DisplayNamePlural
        {
            get { return "" + _DisplayName + "M_"; }
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

        public ThingTypeProperty(Type type, ThingDefs item, int pivot, int order)
        {
            Type = type;
            ThingType = item;
            Pivot = pivot;
            Order = order;
        }

        public ThingTypeProperty(Type type, ThingDefs item, int v1, int v2, string Display) : this(type, item, v1, v2)
        {
            DisplayName = Display;
        }

        public override string ToString()
        {
            return DisplayNamePlural;
        }
    }
    public static class TypeHelper
    {
        public const int ThingDefsCount = 27;

        public static List<ThingTypeProperty> ThingTypeProperties { get; set; } = new List<ThingTypeProperty>() {
           new ThingTypeProperty(null,ThingDefs.Undef, -1, -1){ Usable = false },
           new ThingTypeProperty(null,ThingDefs.UndefTemp, -1, -1){ Usable = false },
           new ThingTypeProperty(typeof(Handlung),ThingDefs.Handlung, 0, 0, "Handlung"),
           new ThingTypeProperty(typeof(Fertigkeit),ThingDefs.Fertigkeit, 0, 1, "Fertigkeit"),
           new ThingTypeProperty(typeof(Item),ThingDefs.Item, 1, 0, "Item"),
           new ThingTypeProperty(typeof(Programm),ThingDefs.Programm, 1, 4, "Programm"),
           new ThingTypeProperty(typeof(Munition),ThingDefs.Munition, 2, 4, "Munition"),
           new ThingTypeProperty(typeof(Implantat),ThingDefs.Implantat, 3, 3, "Implantat"),
           new ThingTypeProperty(typeof(Vorteil),ThingDefs.Vorteil, 3, 9, "Vorteil"),
           new ThingTypeProperty(typeof(Nachteil),ThingDefs.Nachteil, 3, 10, "Nachteil"),
           new ThingTypeProperty(typeof(Connection),ThingDefs.Connection, 3, 2, "Connection"),
           new ThingTypeProperty(typeof(Sin),ThingDefs.Sin, 3, 6, "SIN"),
           new ThingTypeProperty(typeof(Attribut),ThingDefs.Attribut, 3, 0, "Attribut"),
           new ThingTypeProperty(typeof(Berechnet),ThingDefs.Berechnet, 3, 1, "Berechnet"),
           new ThingTypeProperty(typeof(Nahkampfwaffe),ThingDefs.Nahkampfwaffe, 2, 1, "Nahkampfwaffe"),
           new ThingTypeProperty(typeof(Fernkampfwaffe),ThingDefs.Fernkampfwaffe, 2, 0, "Fernkampfwaffe"),
           new ThingTypeProperty(typeof(Kommlink),ThingDefs.Kommlink, 1, 2, "Kommlink"),
           new ThingTypeProperty(typeof(CyberDeck),ThingDefs.CyberDeck, 1, 3, "CyberDeck"),
           new ThingTypeProperty(typeof(Vehikel),ThingDefs.Vehikel, 1, 1, "Vehikel"),
           new ThingTypeProperty(typeof(Panzerung),ThingDefs.Panzerung, 2, 2, "Panzerung"),
           new ThingTypeProperty(typeof(Eigenschaft),ThingDefs.Eigenschaft, -1, -1, "Eigenschaft"){ Usable = false },
           new ThingTypeProperty(typeof(Adeptenkraft),ThingDefs.Adeptenkraft, 0, 3, "Adeptenkraft"),
           new ThingTypeProperty(typeof(Foki),ThingDefs.Foki, 1, 5, "Foki"),
           new ThingTypeProperty(typeof(Geist),ThingDefs.Geist, 1, 6, "Geist"),
           new ThingTypeProperty(typeof(Stroemung),ThingDefs.Stroemung, 3, 5, "Stroemung"),
           new ThingTypeProperty(typeof(Tradition),ThingDefs.Tradition, 3, 4, "Tradition"),
           new ThingTypeProperty(typeof(Zaubersprueche),ThingDefs.Zaubersprueche,0,2, "Zaubersprueche"),
           new ThingTypeProperty(typeof(KomplexeForm),ThingDefs.KomplexeForm,0,4, "KomplexeForm"),
           new ThingTypeProperty(typeof(Sprite),ThingDefs.Sprite,1,8, "Sprite"),
           new ThingTypeProperty(typeof(Widgets),ThingDefs.Widgets,1,7, "Widgets"),
           new ThingTypeProperty(typeof(Wandlung),ThingDefs.Wandlung,3,8, "Wandlung"),
           new ThingTypeProperty(typeof(Initiation),ThingDefs.Initiation,3,7, "Initiation"),
           new ThingTypeProperty(typeof(Note),ThingDefs.Note,4,0, "Note"),
           new ThingTypeProperty(typeof(Note),ThingDefs.Favorite,4,1, "Favorite"),
        };


        public static string ThingDefToString(this ThingDefs eThingDefToConvert, bool Mehrzahl)
        {
            try
            {
                string ret;
                if (Mehrzahl)
                {
                    ret=ModelResources.ResourceManager.GetString(ThingTypeProperties.Find(x => x.ThingType == eThingDefToConvert).DisplayNamePlural);
                }
                else
                {
                    ret = ModelResources.ResourceManager.GetString(ThingTypeProperties.Find(x => x.ThingType == eThingDefToConvert).DisplayNameSingular);
                }
                if (string.IsNullOrEmpty(ret))
                {
                    ret = "NA: " + eThingDefToConvert.ToString();
                }
                return ret;
            }
            catch (Exception ex)
            {
                TAPPLICATION.Debugging.TraceException(ex);
                return "NA: " + eThingDefToConvert.ToString();
            }
        }
        public static Type ThingDefToType(this ThingDefs eThingDefToConvert)
        {
            return ThingTypeProperties.FirstOrDefault(x => x.ThingType == eThingDefToConvert).Type;
        }

        public static ThingDefs TypeToThingDef(this Type type)
        {
            var t = ThingTypeProperties.FirstOrDefault(x => x.Type == type);
            return t == null ? ThingDefs.Undef : t.ThingType;
        }

        public static ThingDefs Obj2ThingDef(this int tag)
        {
            return (ThingDefs)tag;
        }
        public static ThingDefs Obj2ThingDef(this string Name)
        {
            return ThingTypeProperties.FirstOrDefault(t => t.ThingType.ToString() == Name)?.ThingType ?? ThingDefs.Undef;
        }
        public static ThingDefs Obj2ThingDef(this object tag)
        {
            return (ThingDefs)Int16.Parse(tag.ToString());
        }

        /// <summary>
        /// climbs the ThingDefs hierarchi up until the method found something for that type
        /// How it works:
        /// uses method at the name of the type of key.
        /// if result is null, gets the ancestor of type of key and repeats this until ancestor is thing or result is not null
        /// </summary>
        /// <param name="key">the key from wich in hierarchi you whish to start searching</param>
        /// <param name="method">the method takes the TypeName as string</param>
        /// <returns></returns>
        public static T HierarchieUpSearch<T>(this ThingDefs key, Func<string, T> method)
        {
            var ancestor = key.ThingDefToType();
            if (ancestor is null)
            {
                return default;
            }
            T result;
            do
            {
                result = method(ancestor.Name);
                ancestor = ancestor.BaseType;
            }
            while (result is null && ancestor != typeof(Thing).BaseType);
            return result;
        }
    }

}
