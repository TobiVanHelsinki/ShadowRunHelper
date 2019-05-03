using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TLIB;

namespace ShadowRunHelper.CharModel
{
    public class CharCalcProperty : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region Implicit Converter
        public static implicit operator CharCalcProperty(double d)
        {
            return new CharCalcProperty { BaseValue = d };
        }
        #endregion
        [JsonIgnore]
        public double Value { get; private set; }

        double _BaseValue;
        public double BaseValue
        {
            get { return _BaseValue; }
            set { if (_BaseValue.CompareTo(value) != 0) { _BaseValue = value; Recalculate(); NotifyPropertyChanged(); } }
        }

        ObservableCollection<CharCalcProperty> _Connected;
        public ObservableCollection<CharCalcProperty> Connected
        {
            get
            {
                if (_Connected == null)
                {
                    _Connected = new ObservableCollection<CharCalcProperty>();
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

        bool _Active = true;
        public bool Active
        {
            get { return _Active; }
            set { if (_Active != value) { _Active = value; Recalculate(); } }
        }

        public CharCalcProperty()
        {
            DeletionNotification += CharProperty_DeletionNotification;
        }

        void Recalculate()
        {
            var OldValue = Value;
            Value = Active ? BaseValue + Connected?.Select(x => x.Value).Sum() ?? 0.0 : 0.0;
            if (OldValue != Value)
            {
                NotifyPropertyChanged(nameof(Value));
            }
        }

        #region Any Property Changed
        void Connected_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems.OfType<CharCalcProperty>().ToList())
                {
                    item.PropertyChanged -= ConnectedItem_PropertyChanged;
                }
            }
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems.OfType<CharCalcProperty>().ToList())
                {
                    if (HasCircularReference(item) || IsForbiddenType(item))
                    {
                        Connected.Remove(item);
                    }
                    else
                    {
                        item.PropertyChanged += ConnectedItem_PropertyChanged;
                    }
                }
            }
            Recalculate();
        }
        bool HasCircularReference(CharCalcProperty added) //TODO Unit test
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

        bool IsForbiddenType(CharCalcProperty item)
        {
            //TODO Implement Filter
            //public IEnumerable<ThingDefs> FilterOut { get; set; }
            return false;
        }

        void ConnectedItem_PropertyChanged(object sender, PropertyChangedEventArgs e) => Recalculate();
        #endregion
        #region Deletion Handling

        static event EventHandler<CharCalcProperty> DeletionNotification;
        internal void ParentDeleted()
        {
            DeletionNotification.Invoke(this, this);
        }

        private void CharProperty_DeletionNotification(object sender, CharCalcProperty e)
        {
            var ToRemove = Connected.Where(x => x == e).ToList();
            foreach (var item in ToRemove)
            {
                Connected.Remove(item);
            }
            Recalculate();
        }

        internal CharCalcProperty Copy(CharCalcProperty target = null)
        {
            if (target == null)
            {
                target = (CharCalcProperty)Activator.CreateInstance(this.GetType());
            }
            target.Active = Active;
            target.BaseValue = BaseValue;
            target.Connected.Clear();
            target.Connected.AddRange(Connected);
            return target;
        }
        #endregion
    }
}
