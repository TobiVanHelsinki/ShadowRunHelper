//Author: Tobi van Helsinki

///Author: Tobi van Helsinki

using Newtonsoft.Json;
using ShadowRunHelper.Model;
using SharedCode.Ressourcen;
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
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class Used_UserAttribute : Attribute
    {
        public bool UIRelevant { get; set; } = true;

        public Used_UserAttribute()
        {
        }
    }

    public class Thing : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public virtual event PropertyChangedEventHandler PropertyChanged;
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                if (!PropertyChanged?.GetInvocationList()?.Contains(value) == true)
                {
                    PropertyChanged += value;
                }
            }
            remove
            {
                if (PropertyChanged?.GetInvocationList()?.Contains(value) == true)
                {
                    PropertyChanged -= value;
                }
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PlatformHelper.CallPropertyChanged(PropertyChanged, this, propertyName);
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (IsSeperator)
            {
                PlatformHelper.CallPropertyChanged(PropertyChanged, this, nameof(IsSeperator));
            }
        }
        #endregion NotifyPropertyChanged

        [JsonIgnore]
        public virtual IEnumerable<ThingDefs> Filter { get; private set; } = new List<ThingDefs>();

        #region Properties
        [JsonIgnore]
        public bool IsSeperator
        {
            get
            {
                return string.IsNullOrEmpty(Bezeichner) && Value?.Value == 1337;
            }
            set
            {
                Bezeichner = "";
                Value.Connected.Clear();
                Value.BaseValue = value ? 1337 : 0;
                Value.Active = value;
            }
        }

        private ThingDefs thingType = 0;
        public ThingDefs ThingType
        {
            get => thingType;
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
        [Used_User]
        public string Notiz
        {
            get => notiz;
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
        [Used_User]
        public string Zusatz
        {
            get => zusatz;
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
        [Used_User]
        [Obsolete(Constants.ObsoleteCalcProperty)]
        public virtual double Wert
        {
            get => wert;
            set
            {
                if (value != wert)
                {
                    wert = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ConnectProperty _Value;
        [Used_User]
        public ConnectProperty Value
        {
            get => _Value;
            set
            {
                RefreshInnerPropertyChangedListener(ref _Value, value, this);
                NotifyPropertyChanged();
            }
        }

        protected static void RefreshInnerPropertyChangedListener(ref ConnectProperty _Value, ConnectProperty newValue, Thing I)
        {
            if (_Value != newValue)
            {
                if (_Value != null)
                {
                    _Value.PropertyChanged -= I.ConnectProperty_PropertyChanged;
                }
                _Value = newValue;
                if (_Value != null)
                {
                    _Value.PropertyChanged += I.ConnectProperty_PropertyChanged;
                }
            }
        }

        private void ConnectProperty_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        protected string typ = "";
        [Used_User]
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

        private bool _IsFavorite;
        [Used_User]
        public bool IsFavorite
        {
            get => _IsFavorite;
            set { if (_IsFavorite != value) { _IsFavorite = value; NotifyPropertyChanged(); } }
        }

        private int _FavoriteIndex;
        [Used_User(UIRelevant = false)]
        public int FavoriteIndex
        {
            get => _FavoriteIndex;
            set { if (_FavoriteIndex != value) { _FavoriteIndex = value; NotifyPropertyChanged(); } }
        }

        #endregion Properties

        public Thing()
        {
            ThingType = TypeHelper.TypeToThingDef(GetType());
            foreach (var item in GetPropertiesConnects())
            {
                //Create New ConnectProps
                try
                {
                    var display = ModelResources.ResourceManager.GetStringSafe(item.DeclaringType.Name + "_" + item.Name);
                    var value = new ConnectProperty(item.Name, this, display);
                    item.SetValue(this, value);
                }
                catch (Exception)
                {
                    if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
                }
            }
            LinkedThings = new LinkList(this);
        }

        public override string ToString() =>
            string.Format("{0:00}", Value.Value) + "|"
                + ThingType + "|"
                + (!string.IsNullOrEmpty(typ) ? typ + ": " : "")
                + bezeichner
                + (!string.IsNullOrEmpty(Zusatz) ? " " + Zusatz : "");

        /// <summary>
        /// Copies own Values into target if possible
        /// </summary>
        /// <param name="target"></param>
        /// <returns>true if no erros occured, false if some properties migth not get copied</returns>
        public virtual bool TryCloneInto(Thing target = null)
        {
            var retval = true;
            if (target == null)
            {
                try
                {
                    target = (Thing)Activator.CreateInstance(GetType());
                }
                catch (Exception ex)
                {
                    Log.Write("Could not", ex, logType: LogType.Error);
                    return false;
                }
            }
            foreach (var item in GetProperties().Where(x => x.PropertyType != typeof(ConnectProperty)))
            {
                try
                {
                    item.SetValue(target, item.GetValue(this));
                }
                catch (Exception ex)
                {
                    Log.Write("Could not", ex, logType: LogType.Error);
                    retval = false;
                }
            }
            foreach (var item in GetProperties().Where(x => x.PropertyType == typeof(ConnectProperty)))
            {
                try
                {
                    var oldconnectprop = item.GetValue(this) as ConnectProperty;
                    var newconnectprop = item.GetValue(target) as ConnectProperty;
                    oldconnectprop.TryCloneInto(newconnectprop);
                }
                catch (Exception ex)
                {
                    Log.Write("Could not", ex, logType: LogType.Error);
                    retval = false;
                }
            }
            return retval;
        }

        /// <summary>
        /// Sets all used members to default.
        /// </summary>
        public virtual void Reset()
        {
            foreach (var item in GetProperties())
            {
                try
                {
                    if (item.PropertyType == typeof(string))
                    {
                        item.SetValue(this, "");
                    }
                    else if (item.PropertyType == typeof(ConnectProperty))
                    {
                        if (item.GetValue(this) is ConnectProperty cp)
                        {
                            cp.Reset();
                        }
                        else
                        {
#if DEBUG
                            if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
#else
                            Log.Write("Could not Reset ConnectProperty: " + item.Name, logType: LogType.Error);
#endif
                        }
                    }
                    else
                    {
                        item.SetValue(this, Activator.CreateInstance(item.PropertyType));
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
#else
                    Log.Write("Could not Reset Property: " + item.Name, ex, logType: LogType.Error);
#endif
                }
            }
        }

        /// <summary>
        /// No clue
        /// </summary>
        public void NotifiyDeletion()
        {
            Reset();
            foreach (var item in GetProperties())
            {
                NotifyPropertyChanged(item.Name);
            }
            NotifyPropertyChanged(Constants.THING_DELETED_TOKEN);
            foreach (var item in GetConnects())
            {
                item.ParentDeleted();
            }
        }

        #region SearchAndCompare

        /// <summary>
        /// Determines, if the Both Things have same Name und ThingType, so that they can be seen as
        /// "the same"
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static bool HasSameIdentifiers(Thing t1, Thing t2)
        {
            return t1.ThingType == t2.ThingType ? t1.Bezeichner == t2.Bezeichner ? true : false : false;
        }

        /// <summary> Checks, if this object has Similarities to the parameter, will be overridden
        /// by others </summary> <param name="i_t"></param> <returns> 1 -> type correct, name
        /// correct </returns> <returns> >0.5 -> type correct, name somehow</returns> <returns> 0.5
        /// -> type incorrect, name correct</returns> <returns> <0.5 -> type incorrect, name
        /// somehow</returns> <returns> <0.5 -> type correct, name incorrect</returns> <returns> 0
        /// -> type incorrect, name incorrect</returns>
        public virtual float SimilaritiesTo(string text)
        {
            var searchtext = text.ToLower();
            var mytext = ToString().ToLower();
            var mytextes = mytext.Split(' ', '-', '/', '_');
            if (mytext.Contains("charis"))
            {
            }

            float retval = 0;
            if (ToString().ToLower().StartsWith(searchtext))
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

        #endregion SearchAndCompare

        #region Obsolete Calculations
        private LinkList _LinkedThings;
        [Obsolete(Constants.ObsoleteCalcProperty)]
        public LinkList LinkedThings
        {
            get => _LinkedThings;
            set
            {
                if (_LinkedThings != value && value != null)
                {
                    _LinkedThings = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion Obsolete Calculations

        #region Helper Methods to get certain Properties

        /// <summary>
        /// Gets all properties with the Used_UserAttribute.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> GetProperties()
        {
            return ReflectionHelper.GetProperties(this, typeof(Used_UserAttribute));
        }

        /// <summary>
        /// Gets all properties with the Used_UserAttribute oftype ConnectProperty
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> GetPropertiesConnects()
        {
            return GetProperties().Where(p => p.PropertyType == typeof(ConnectProperty));
        }

        /// <summary>
        /// Gets references to the objects instances of all properties with the Used_UserAttribute
        /// oftype ConnectProperty
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConnectProperty> GetConnects()
        {
            return GetPropertiesConnects().Select(x => x.GetValue(this)).OfType<ConnectProperty>();
        }
        #endregion Helper Methods to get certain Properties
    }
}