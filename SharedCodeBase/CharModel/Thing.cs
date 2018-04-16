using ShadowRunHelper.Model;
using ShadowRunHelper.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TAMARIN;
using TAMARIN.Model;
using TLIB;
using Newtonsoft.Json;

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
            ModelHelper.CallPropertyChangedAtDispatcher(PropertyChanged, this, propertyName);
        }

        #endregion
        public Thing()
        {
            LinkedThings = new ObservableThingListEntryCollection(this);
            ThingType = TypeHelper.TypeToThingDef(GetType());
            LinkedThings.OnCollectionChangedCall(OnLinkedThingsChanged);
            PropertyChanged += (s, e) => { if (e.PropertyName == "Wert") OnLinkedThingsChanged(); };
        }

        #region Properties
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
        ObservableThingListEntryCollection _LinkedThings;
        [Used_List]
        public ObservableThingListEntryCollection LinkedThings
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
            {
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
                {
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
                var CollectionTarget = (pair.GetValue(target) as ObservableThingListEntryCollection);
                var CollectionThis = (pair.GetValue(this) as ObservableThingListEntryCollection);
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
                catch (Exception)
                {
                    return false;
                }
            }
            foreach (var item in GetProperties(target))
            {
                try
                {
                    item.SetValue(target, item.GetValue(this));
                }
                catch (Exception)
                {
                    ret = false;
                }
            }
            foreach (var pair in ReflectionHelper.GetProperties(target, typeof(Used_ListAttribute)))
            {
                try
                {
                    var CollectionTarget = (pair.GetValue(target) as ObservableThingListEntryCollection);
                    var CollectionThis = (pair.GetValue(this) as ObservableThingListEntryCollection);
                    CollectionTarget.Clear();
                    CollectionTarget.AddRange(CollectionThis.Select(item => new AllListEntry(item.Object.Copy(), item.DisplayName, item.PropertyID)));
                }
                catch (Exception)
                {
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
                (item.GetValue(this) as ObservableThingListEntryCollection).Clear();
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
                strReturn += StringHelper.GetString("Model_"+ item.DeclaringType.Name + "_"+ item.Name + "/Text");
                strReturn += Delimiter;
            }
            return strReturn;
        }
        public virtual void FromCSV(Dictionary<string, string> dic)
        {
            var Props = GetProperties(this).Reverse().Select(p => (StringHelper.GetString("Model_" + p.DeclaringType.Name + "_" + p.Name + "/Text"),p));
            foreach (var item in dic)
            {
                var currentProp = Props.FirstOrDefault(p => p.Item1 == item.Key);
                currentProp.Item2?.SetValue(this, ConvertToRightType(item.Value, currentProp.Item2.GetValue(this)));
            }
        }
        static object ConvertToRightType(object Value, object Target)
        {
            object ret;
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
            return typ + (typ != "" ? ": " : "") + bezeichner + " " + ValueOf("Wert") + (Zusatz != "" ? "+" : "") + Zusatz;
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

            float retval = 0;
            if (TypeHelper.ThingDefToString(ThingType, false).ToLower().Contains(text))
            {
                retval += 0.4f;
            }
            if (this.ToString().ToLower().Contains(text.ToLower()))
            {
                retval += 0.4f;
            }
            if (this.ToString().ToLower() == (text.ToLower()))
            {
                retval += 0.1f;
            }
            return retval;
        }
    }
}
