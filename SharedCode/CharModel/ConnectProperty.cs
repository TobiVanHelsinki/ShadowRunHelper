//Author: Tobi van Helsinki

///Author: Tobi van Helsinki

using Newtonsoft.Json;
using SharedCode.Ressourcen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TLIB;

namespace ShadowRunHelper.CharModel
{
    public class ConnectProperty : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion NotifyPropertyChanged

        #region Implicit Converter

        public static implicit operator ConnectProperty(double d) => new ConnectProperty("implicit", new Thing(), "implicit") { BaseValue = d };

        #endregion Implicit Converter

        [JsonIgnore]
        public double Value { get; private set; }

        [JsonIgnore]
        public double TrueValue { get; private set; }

        private double _BaseValue;
        public double BaseValue
        {
            get => _BaseValue;
            set { if (_BaseValue.CompareTo(value) != 0) { _BaseValue = value; Recalculate(); NotifyPropertyChanged(); } }
        }

        private ObservableCollection<ConnectProperty> _Connected;
        public ObservableCollection<ConnectProperty> Connected
        {
            get
            {
                if (_Connected == null)
                {
                    _Connected = new ObservableCollection<ConnectProperty>();
                    _Connected.CollectionChanged += Connected_CollectionChanged;
                }
                return _Connected;
            }
            set
            {
                if (_Connected != null)
                {
                    _Connected.CollectionChanged -= Connected_CollectionChanged;
                }
                if (_Connected != value)
                {
                    _Connected = value;
                    NotifyPropertyChanged();
                }
                if (_Connected != null)
                {
                    _Connected.CollectionChanged += Connected_CollectionChanged;
                }
                Recalculate();
            }
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if (_Name != value)
                {
                    _Name = value; NotifyPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        public string DisplayName { get; set; }

        private Thing _Owner;
        public Thing Owner
        {
            get => _Owner;
            set
            {
                if (_Owner != value)
                {
                    _Owner = value; NotifyPropertyChanged();
                }
            }
        }

        private bool _Active = true;
        public bool Active
        {
            get => _Active;
            set { if (_Active != value) { _Active = value; Recalculate(); } }
        }

        public ConnectProperty() => DeletionNotification += CharProperty_DeletionNotification;

        public ConnectProperty(string name, Thing owner, string displayName) : this()
        {
            DisplayName = displayName;
            Name = name;
            Owner = owner;
        }

        public ConnectProperty TryCloneInto(ConnectProperty target)
        {
            target.Connected.AddRange(Connected);
            target.Active = Active;
            target.DisplayName = DisplayName;
            target.Name = Name;
            target.BaseValue = BaseValue;
            return target;
        }

        public override string ToString() => "Prop:" + (Name ?? "(null)") + "|Owner:" + (Owner?.ToString() ?? "(null)");

        private void Recalculate()
        {
            var oldTrueValue = TrueValue;
            var oldValue = Value;
            var tempSum = 0.0;
            foreach (var item in Connected)
            {
                if (HasCircularReference(item) || IsForbiddenType(item))
                {
                    Log.Write("Thing has caused a circular reference: " + item.Owner + "." + item.DisplayName + ". I will try to remove it asap.", logType: LogType.Error, true);
                    try
                    {
                        Connected.Remove(item);
                    }
                    catch { }
                }
                else
                {
                    tempSum += item.Value;
                }
            }
            TrueValue = BaseValue + tempSum;
            //TrueValue = BaseValue + Connected?.Select(x => x.Value).Sum() ?? 0.0;
            Value = Active ? TrueValue : 0.0;
            if (oldTrueValue != TrueValue)
            {
                NotifyPropertyChanged(nameof(TrueValue));
            }
            if (oldValue != Value)
            {
                NotifyPropertyChanged(nameof(Value));
            }
        }

        /// <summary>
        /// Sets properties used for connections and value to default value
        /// </summary>
        /// <returns></returns>
        public ConnectProperty Reset()
        {
            Connected.Clear();
            Active = false;
            BaseValue = 0;
            return this;
        }

        /// <summary>
        /// Removes the parent and property information.
        /// </summary>
        /// <returns></returns>
        public ConnectProperty ResetParent()
        {
            Owner = null;
            Name = null;
            DisplayName = null;
            return this;
        }

        #region Adding

        private void Connected_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems.OfType<ConnectProperty>().ToList())
                {
                    item.PropertyChanged -= ConnectedItem_PropertyChanged;
                }
            }
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems.OfType<ConnectProperty>().ToList())
                {
                    if (!HasCircularReference(item) && !IsForbiddenType(item))
                    {
                        item.PropertyChanged += ConnectedItem_PropertyChanged;
                    }
                }
            }
            Recalculate();
        }

        private bool HasCircularReference(ConnectProperty added) //TODO Unit test
        {
            foreach (var item in added.Connected)
            {
                if (item == this || HasCircularReference(item))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsForbiddenType(ConnectProperty item)
        {
            return false;
        }

        private void ConnectedItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Recalculate();
        }
        #endregion Adding

        #region Deletion Handling

        private static event EventHandler<ConnectProperty> DeletionNotification;

        internal void ParentDeleted()
        {
            DeletionNotification.Invoke(this, this);
        }

        private void CharProperty_DeletionNotification(object sender, ConnectProperty e)
        {
            var ToRemove = Connected.Where(x => x == e).ToList();
            foreach (var item in ToRemove)
            {
                Connected.Remove(item);
            }
            Recalculate();
        }
        #endregion Deletion Handling
    }
}