using Newtonsoft.Json;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TLIB;

namespace ShadowRunHelper.CharModel
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class Used_UserAttribute : Attribute
    {
        public Used_UserAttribute() { }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class Used_CalcAttribute : Attribute
    {
        public Used_CalcAttribute() { }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class Used_ListAttribute : Attribute
    {
        public Used_ListAttribute() { }
    }


    public abstract class Thing : INotifyPropertyChanged, ICSV
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

        #endregion
        public Thing()
        {
            LinkedThings = new LinkList(this);
            ThingType = TypeHelper.TypeToThingDef(GetType());
            LinkedThings.OnCollectionChangedCall(OnLinkedThingsChanged);
            PropertyChanged += (s, e) => { if (e.PropertyName == "Wert") OnLinkedThingsChanged(); };
        }

        #region Properties
        [JsonIgnore]
        public bool IsSeperator { get => string.IsNullOrEmpty(Bezeichner) && Wert == 1337;  }

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
        protected double wert = 0;
        [Used_UserAttribute]
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

        #endregion
        #region Calculations
        LinkList _LinkedThings;
        [Used_List]
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
        [Used_UserAttribute]
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
        bool _IsFavorite;
        [Used_UserAttribute]
        public bool IsFavorite
        {
            get { return _IsFavorite; }
            set { if (_IsFavorite != value) { _IsFavorite = value; NotifyPropertyChanged(); } }
        }

        int _FavoriteIndex;
        [Used_UserAttribute]
        public int FavoriteIndex
        {
            get { return _FavoriteIndex; }
            set { if (_FavoriteIndex != value) { _FavoriteIndex = value; NotifyPropertyChanged(); } }
        }

        protected virtual void OnLinkedThingsChanged()
        {
            WertCalced = Wert + LinkedThings.Recalculate();
        }

        public double ValueOf(string ID)
        {
            if (UseForCalculation())
            {
                return InternValueOf(ID);
            }
            return 0;
        }
        protected virtual double InternValueOf(string ID)
        {
            if (ID == null || ID == "" || ID == "Wert")
            {
                return WertCalced;
            }
            try
            {
                return (double)GetProperties(this).First(x => x.Name == ID).GetValue(this);
            }
            catch (Exception ex)
 { TAPPLICATION.Debugging.TraceException(ex);
                return 0;
            }
        }
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
 { TAPPLICATION.Debugging.TraceException(ex);
                    return 0;
                }
            }
            return 0;
        }
        protected virtual bool UseForCalculation()
        {
            return true;
        }


        #endregion
        public static IEnumerable<PropertyInfo> GetProperties(object obj)
        {
            return ReflectionHelper.GetProperties(obj, typeof(Used_UserAttribute));
        }
        public static IEnumerable<PropertyInfo> GetPropertiesLists(object obj)
        {
            return ReflectionHelper.GetProperties(obj, typeof(Used_ListAttribute));
        }

        public virtual Thing Copy(Thing target = null)
        {
            if (target == null)
            {
                target = (Thing)Activator.CreateInstance(this.GetType());
            }
            foreach (var item in GetProperties(target))
            {
                item.SetValue(target, item.GetValue(this));
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
 { TAPPLICATION.Debugging.TraceException(ex);
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
 { TAPPLICATION.Debugging.TraceException(ex);
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
 { TAPPLICATION.Debugging.TraceException(ex);
                    ret = false;
                }
            }
            return ret;
        }
        public virtual void Reset()
        {
            foreach (var item in GetProperties(this))
            {
                if (item.DeclaringType == typeof(string))
                {
                    item.SetValue(this, "");
                }
                else if (item.DeclaringType == typeof(char))
                {
                    item.SetValue(this, ' ');
                }
                else if (item.DeclaringType == typeof(bool?))
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
        }

        #region CSV
        public virtual string ToCSV(char Delimiter)
        {
            string strReturn = "";
            foreach (var item in GetProperties(this).Reverse())
            {
                strReturn += item.GetValue(this);
                strReturn += Delimiter;
            }
            return strReturn;
        }
        public virtual string HeaderToCSV(char Delimiter)
        {
            string strReturn = "";
            foreach (var item in GetProperties(this).Reverse())
            {
                strReturn += PlatformHelper.GetString("Model_"+ item.DeclaringType.Name + "_"+ item.Name + "/Text");
                strReturn += Delimiter;
            }
            return strReturn;
        }
        public virtual void FromCSV(Dictionary<string, string> dic)
        {
            var Props = GetProperties(this).Reverse().Select(p => (PlatformHelper.GetString("Model_" + p.DeclaringType.Name + "_" + p.Name + "/Text"),p));
            foreach (var item in dic)
            {
                var currentProp = Props.FirstOrDefault(p => p.Item1 == item.Key);
                currentProp.Item2?.SetValue(this, ConvertToRightType(item.Value, currentProp.Item2.GetValue(this)));
            }
        }
        static object ConvertToRightType(object Value, object Target)
        {
            switch (Target)
            {
                case int i:
                    Int32.TryParse(Value.ToString(), out var I);
                    return I;
                case double d:
                    Double.TryParse(Value.ToString(), out var D);
                    return D;
                case char c:
                    Char.TryParse(Value.ToString(), out var C);
                    return c;
                case bool b:
                    Boolean.TryParse(Value.ToString(), out var B);
                    return B;
                default:
                    return Value;
            }
        }
        #endregion

        public override string ToString()
        {
            return
                ValueOf("Wert") + " "
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
}
