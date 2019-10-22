///Author: Tobi van Helsinki

using Newtonsoft.Json;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TAPPLICATION;
using TLIB;

namespace ShadowRunHelper.CharModel
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class Used_UserAttribute : Attribute
    {
        public bool UIRelevant { get; set; }

        public Used_UserAttribute()
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class Used_CalcAttribute : Attribute
    {
        public Used_CalcAttribute()
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class Used_ListAttribute : Attribute
    {
        public Used_ListAttribute()
        {
        }
    }

    public class Thing : INotifyPropertyChanged, ICSV
    {
        public const uint nThingPropertyCount = 5;

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PlatformHelper.CallPropertyChanged(PropertyChanged, this, propertyName);
            if (IsSeperator)
            {
                PlatformHelper.CallPropertyChanged(PropertyChanged, this, nameof(IsSeperator));
            }
        }

        #endregion INotifyPropertyChanged

        public Thing()
        {
            foreach (var item in this.GetType().GetProperties().Where(x => x.PropertyType == typeof(CharCalcProperty)))
            {
                try
                {
                    item.SetValue(this, new CharCalcProperty(item.Name, this));
                }
                catch (Exception)
                {
                }
            }
            LinkedThings = new LinkList(this);
            ThingType = TypeHelper.TypeToThingDef(GetType());
            LinkedThings.OnCollectionChangedCall(OnLinkedThingsChanged);
            PropertyChanged += (s, e) => { if (e.PropertyName == "Wert") OnLinkedThingsChanged(); };
        }

        #region Properties
        [JsonIgnore]
        public bool IsSeperator { get => string.IsNullOrEmpty(Bezeichner) && Wert == 1337; }

        ThingDefs thingType = 0;
        public ThingDefs ThingType
        {
            get { return thingType; }
            set
            {
                if (value != thingType)
                {
                    thingType = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Order { get; set; }
        protected string notiz = "";
        [Used_UserAttribute]
        public string Notiz
        {
            get { return notiz; }
            set
            {
                if (value != notiz)
                {
                    notiz = value;
                    NotifyPropertyChanged();
                }
            }
        }

        protected string zusatz = "";
        [Used_UserAttribute]
        public string Zusatz
        {
            get { return zusatz; }
            set
            {
                if (value != zusatz)
                {
                    zusatz = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Obsolete(Constants.ObsoleteCalcProperty)]
        protected double wert = 0;
        [Used_UserAttribute]
        [Obsolete(Constants.ObsoleteCalcProperty)]
        public virtual double Wert
        {
            get { return wert; }
            set
            {
                if (value != wert)
                {
                    wert = value;
                    NotifyPropertyChanged();
                }
            }
        }

        CharCalcProperty _Value;
        [Used_UserAttribute]
        public CharCalcProperty Value
        {
            get { return _Value; }
            set { if (_Value != value) { _Value = value; NotifyPropertyChanged(); } }
        }

        protected string typ = "";
        [Used_UserAttribute]
        public string Typ
        {
            get => typ;
            set
            {
                if (value != typ)
                {
                    typ = value;
                    NotifyPropertyChanged();
                }
            }
        }

        protected string bezeichner = "";
        [Used_User]
        public string Bezeichner
        {
            get => bezeichner;
            set
            {
                if (value != bezeichner)
                {
                    bezeichner = value;
                    NotifyPropertyChanged();
                }
            }
        }

        bool _IsFavorite;
        [Used_UserAttribute]
        public bool IsFavorite
        {
            get { return _IsFavorite; }
            set { if (_IsFavorite != value) { _IsFavorite = value; NotifyPropertyChanged(); } }
        }

        int _FavoriteIndex;
        [Used_UserAttribute(UIRelevant = false)]
        public int FavoriteIndex
        {
            get { return _FavoriteIndex; }
            set { if (_FavoriteIndex != value) { _FavoriteIndex = value; NotifyPropertyChanged(); } }
        }

        #endregion Properties

        #region Calculations
        LinkList _LinkedThings;
        [Used_List]
        [Obsolete(Constants.ObsoleteCalcProperty)]
        public LinkList LinkedThings
        {
            get { return _LinkedThings; }
            set
            {
                if (_LinkedThings != value && value != null)
                {
                    _LinkedThings = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _WertCalced = 0;
        [JsonIgnore]
        [Used_UserAttribute(UIRelevant = false)]
        [Obsolete(Constants.ObsoleteCalcProperty)]
        public double WertCalced
        {
            get { return _WertCalced; }
            set
            {
                if (value != _WertCalced)
                {
                    _WertCalced = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Obsolete(Constants.ObsoleteCalcProperty)]
        protected virtual void OnLinkedThingsChanged()
        {
            WertCalced = Wert + LinkedThings.Recalculate();
        }

        [Obsolete(Constants.ObsoleteCalcProperty)]
        public double ValueOf(string ID)
        {
            if (UseForCalculation())
            {
                return InternValueOf(ID);
            }
            return 0;
        }

        [Obsolete(Constants.ObsoleteCalcProperty)]
        protected virtual double InternValueOf(string ID)
        {
            if (ID == null || ID == "" || ID == "Wert")
            {
                return WertCalced;
            }
            try
            {
                var v = GetProperties(this).First(x => x.Name == ID).GetValue(this);
                if (v is double d)
                {
                    return d;
                }
                if (v is int i)
                {
                    return i;
                }
                if (v is CharCalcProperty c)
                {
                    return c.Value;
                }
                if (v is null)
                {
                    return 0;
                }
                return (double)v;
            }
            catch (Exception ex)
            {
                Log.Write("Could not", ex, logType: LogType.Error);
                return 0;
            }
        }

        [Obsolete(Constants.ObsoleteCalcProperty)]
        public double RawValueOf(string ID)
        {
            if (UseForCalculation())
            {
                if (ID == null || ID == "" || ID == "Wert")
                {
                    return Wert;
                }
                try
                {
                    return (double)GetProperties(this).First(x => x.Name == ID).GetValue(this);
                }
                catch (Exception ex)
                {
                    Log.Write("Could not", ex, logType: LogType.Error);
                    return 0;
                }
            }
            return 0;
        }

        [Obsolete(Constants.ObsoleteCalcProperty)]
        protected virtual bool UseForCalculation()
        {
            return true;
        }

        #endregion Calculations

        public static IEnumerable<PropertyInfo> GetProperties(object obj)
        {
            return ReflectionHelper.GetProperties(obj, typeof(Used_UserAttribute));
        }

        [Obsolete(Constants.ObsoleteCalcProperty)]
        public static IEnumerable<PropertyInfo> GetPropertiesLists(object obj)
        {
            return ReflectionHelper.GetProperties(obj, typeof(Used_ListAttribute));
        }

        public static IEnumerable<CharCalcProperty> GetCharProperties(object obj)
        {
            return ReflectionHelper.GetProperties(obj, typeof(Used_UserAttribute)).Where(x => x.PropertyType == typeof(CharCalcProperty)).Select(x => x.GetValue(obj)).OfType<CharCalcProperty>();
        }

        /// <summary>
        /// Copies own Values into target
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual Thing Copy(Thing target = null)
        {
            if (target == null)
            {
                target = (Thing)Activator.CreateInstance(this.GetType());
            }
            foreach (var item in GetProperties(target))
            {
                if (item.PropertyType == typeof(CharCalcProperty))
                {
                    item.SetValue(target, (item.GetValue(this) as CharCalcProperty)?.Copy());
                }
                else
                {
                    item.SetValue(target, item.GetValue(this));
                }
            }

            foreach (var pair in ReflectionHelper.GetProperties(target, typeof(Used_ListAttribute)))
            {
                var CollectionTarget = (pair.GetValue(target) as LinkList);
                var CollectionThis = (pair.GetValue(this) as LinkList);
                CollectionTarget.Clear();
                CollectionTarget.AddRange(CollectionThis.Select(item => new AllListEntry(item.Object.Copy(), item.DisplayName, item.PropertyID)));
            }

            return target;
        }

        public virtual bool TryCopy(Thing target = null)
        {
            bool ret = true;
            if (target == null)
            {
                try
                {
                    target = (Thing)Activator.CreateInstance(this.GetType());
                }
                catch (Exception ex)
                {
                    Log.Write("Could not", ex, logType: LogType.Error);
                    return false;
                }
            }
            foreach (var item in GetProperties(target))
            {
                try
                {
                    item.SetValue(target, item.GetValue(this));
                }
                catch (Exception ex)
                {
                    Log.Write("Could not", ex, logType: LogType.Error);
                    ret = false;
                }
            }
            foreach (var pair in ReflectionHelper.GetProperties(target, typeof(Used_ListAttribute)))
            {
                try
                {
                    var CollectionTarget = (pair.GetValue(target) as LinkList);
                    var CollectionThis = (pair.GetValue(this) as LinkList);
                    CollectionTarget.Clear();
                    CollectionTarget.AddRange(CollectionThis.Select(item => new AllListEntry(item.Object.Copy(), item.DisplayName, item.PropertyID)));
                }
                catch (Exception ex)
                {
                    Log.Write("Could not", ex, logType: LogType.Error);
                    ret = false;
                }
            }
            return ret;
        }

        public virtual void Reset()
        {
            foreach (var item in GetProperties(this))
            {
                if (item.PropertyType == typeof(string))
                {
                    item.SetValue(this, "");
                }
                else if (item.PropertyType == typeof(char))
                {
                    item.SetValue(this, ' ');
                }
                else if (item.PropertyType == typeof(bool?))
                {
                    item.SetValue(this, false);
                }
                else
                {
                    item.SetValue(this, default);
                }
            }
            foreach (var item in ReflectionHelper.GetProperties(this, typeof(Used_ListAttribute)))
            {
                (item.GetValue(this) as LinkList).Clear();
            }
        }

        public void NotifiyDeletion()
        {
            Reset();
            foreach (var item in GetProperties(this))
            {
                NotifyPropertyChanged(item.Name);
            }
            NotifyPropertyChanged(Constants.THING_DELETED_TOKEN);
            foreach (var item in GetCharProperties(this))
            {
                item.ParentDeleted();
            }
        }

        #region CSV

        public virtual string ToCSV(char Delimiter)
        {
            return GetProperties(this).Reverse().Select
                (item => item.GetValue(this) + Delimiter.ToString()).Aggregate((a, s) => a + s);
        }

        public virtual string HeaderToCSV(char Delimiter)
        {
            return GetProperties(this).Reverse().Select
                (item => CustomManager.GetString("Model_" + item.DeclaringType.Name + "_" + item.Name + "/Text") + Delimiter.ToString())
                .Aggregate((a, s) => a + s);
        }

        public virtual void FromCSV(Dictionary<string, string> dic)
        {
            var Props = GetProperties(this).Reverse().Select(p => (CustomManager.GetString("Model_" + p.DeclaringType.Name + "_" + p.Name + "/Text"), p));
            foreach (var item in dic)
            {
                var currentProp = Props.FirstOrDefault(p => p.Item1 == item.Key);
                currentProp.p?.SetValue(this, ConvertToRightType(item.Value, currentProp.p.GetValue(this)));
            }
        }

        private static object ConvertToRightType(object Value, object Target)
        {
            switch (Target)
            {
                case int _:
                    int.TryParse(Value.ToString(), out var I);
                    return I;
                case double _:
                    double.TryParse(Value.ToString(), out var D);
                    return D;
                case char _:
                    char.TryParse(Value.ToString(), out var C);
                    return C;
                case bool _:
                    bool.TryParse(Value.ToString(), out var B);
                    return B;
                default:
                    return Value;
            }
        }
        #endregion CSV

        public override string ToString()
        {
            return
                Value.Value + "/"
                + (!string.IsNullOrEmpty(typ) ? typ + ": " : "")
                + bezeichner
                + (!string.IsNullOrEmpty(Zusatz) ? " +" + Zusatz : "");
        }

        /// <summary>
        /// Determines, if the Both Things have same Name und ThingType, so that they can be seen as "the same"
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static bool HasSameIdentifiers(Thing t1, Thing t2)
        {
            return t1.ThingType == t2.ThingType ? t1.Bezeichner == t2.Bezeichner ? true : false : false;
        }

        /// <summary>
        /// Checks, if this object has Similarities to the parameter, will be overridden by others
        /// </summary>
        /// <param name="i_t"></param>
        /// <returns> 1    -> type   correct, name correct </returns>
        /// <returns> >0.5 -> type   correct, name somehow</returns>
        /// <returns> 0.5  -> type incorrect, name correct</returns>
        /// <returns> <0.5  -> type incorrect, name somehow</returns>
        /// <returns> <0.5 -> type   correct, name incorrect</returns>
        /// <returns> 0    -> type incorrect, name incorrect</returns>
        public virtual float SimilaritiesTo(string text)
        {
            var searchtext = text.ToLower();
            var mytext = ToString().ToLower();
            var mytextes = mytext.Split(' ', '-', '/', '_');
            if (mytext.Contains("charis"))
            {
            }

            float retval = 0;
            if (this.ToString().ToLower().StartsWith(searchtext))
            {
                retval += 0.7f;
            }
            else
            {
                var t = mytextes.FirstOrDefault(x => x.StartsWith(searchtext));
                if (!string.IsNullOrEmpty(t))
                {
                    retval += 0.4f - Array.IndexOf(mytextes, t) * 0.02f;
                }
            }
            if (TypeHelper.ThingDefToString(ThingType, false).ToLower().StartsWith(text))
            {
                retval += 0.2f;
            }
            //if (this.ToString().ToLower() == (searchtext))
            //{
            //    retval += 0.1f;
            //}
            return retval;
        }
    }

    public static class ThingExt
    {
        /// <summary>
        /// returns infos about all members whoose type is CharCalcProperty
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetCalcProps(this Thing t, Type type)
        {
            //TLIB.ReflectionHelper.GetProperties();
            return t.GetType().GetProperties().Where(p => p.PropertyType == type);
        }

        /// <summary>
        /// returns all members whoose type is CharCalcProperty
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<CharCalcProperty> GetCalcs(this Thing t)
        {
            return t.GetCalcProps(typeof(CharCalcProperty)).Select(p => p.GetValue(t, null) as CharCalcProperty);
        }
    }
}