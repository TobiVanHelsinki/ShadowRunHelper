using ShadowRunHelper.Model;
using ShadowRunHelper.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TLIB_UWPFRAME;
using TLIB_UWPFRAME.Model;
using TLIB_UWPFRAME.Resources;

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
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ModelHelper.CallPropertyChangedAtDispatcher(PropertyChanged, this, propertyName);
        }
        public Thing()
        {
            ThingType = TypeHelper.TypeToThingDef(GetType());
        }
        ThingDefs thingType = 0;
        [Newtonsoft.Json.JsonIgnore]
        public ThingDefs ThingType
        {
            get { return thingType; }
            protected set
            {
                if (value != thingType)
                {
                    thingType = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int Order { get; set; }
        string notiz = "";
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
        string zusatz = "";
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
        double wert = 0;
        [Used_UserAttribute]
        public double Wert
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
        string typ = "";
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
        string bezeichner = "";
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

        public virtual double GetPropertyValueOrDefault(string ID = "")
        {
            if (!UseForCalculation())
            {
                return 0;
            }
            if (ID == null || ID == "")
            {
                return Wert;
            }
            try
            {
                return double.Parse(GetProperties(this).First(x => x.Name == ID).ToString());
            }
            catch (Exception)
            {
                return Wert;
            }
        }

        protected virtual bool UseForCalculation()
        {
            return true;
        }

        public static IEnumerable<PropertyInfo> GetProperties(object obj)
        {
            return Helper.GetProperties(obj, typeof(Used_UserAttribute));
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

            foreach (var pair in Helper.GetProperties(target, typeof(Used_ListAttribute)))
            {
                var CollectionTarget = (pair.GetValue(target) as ObservableThingListEntryCollection);
                var CollectionThis = (pair.GetValue(this) as ObservableThingListEntryCollection);
                CollectionTarget.AddRange(CollectionThis.Select(item => new AllListEntry() { Object = item.Object.Copy(), PropertyID = item.PropertyID, DisplayName = item.DisplayName }));
            }

            return target;
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
            foreach (var item in Helper.GetProperties(this, typeof(Used_ListAttribute)))
            {
                (item.GetValue(this) as ObservableThingListEntryCollection).Clear();
            }
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
                strReturn += CrossPlatformHelper.GetString("Model_"+ item.DeclaringType.Name + "_"+ item.Name + "/Text");
                strReturn += Delimiter;
            }
            return strReturn;
        }
        public virtual void FromCSV(Dictionary<string, string> dic)
        {
            var Props = GetProperties(this).Reverse().Select(p => (CrossPlatformHelper.GetString("Model_" + p.DeclaringType.Name + "_" + p.Name + "/Text"),p));
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
            return typ +": "+ bezeichner + " " + wert + "+" + Zusatz;
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
