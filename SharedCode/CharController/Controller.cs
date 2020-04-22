//Author: Tobi van Helsinki


using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using SharedCode.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TLIB;

namespace ShadowRunHelper.CharController
{
    public class Controller<T> : INotifyPropertyChanged, IController<T> where T : Thing, new()
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion NotifyPropertyChanged

        /// <summary>
        /// GUI-Binding Target
        /// </summary>
        public virtual ObservableCollection<T> Data { get; protected set; }

        public T this[int index] => Data[index];

        public virtual int IndexOf(Thing t)
        {
            return Data.IndexOf(t as T);
        }

        public virtual IEnumerable<Thing> GetData()
        {
            return Data;
        }

        public virtual IEnumerable<Thing> GetAllData()
        {
            return Data;
        }

        protected readonly ThingDefs _eDataTyp;
        public ThingDefs eDataTyp => _eDataTyp;

        public virtual void RegisterEventAtData(Action Method)
        {
            void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => Method();
            Data.CollectionChanged -= Data_CollectionChanged;
            Data.CollectionChanged += Data_CollectionChanged;
        }

        public void RegisterEventAtData(Action<object, PropertyChangedEventArgs> Method)
        {
            void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => Method(sender, new PropertyChangedEventArgs(nameof(Data)));
            Data.CollectionChanged -= Data_CollectionChanged;
            Data.CollectionChanged += Data_CollectionChanged;
        }

        public virtual Thing AddNewThing()
        {
            return AddNewThing(Activator.CreateInstance<T>());
        }

        public virtual Thing AddNewThing(Thing newThing)
        {
            Data.Add((T)newThing);
            newThing.Order = Data.MaxOrDefault(x => x.Order) + 1;
            return newThing;
        }

        public virtual void RemoveThing(Thing tToRemove)
        {
            tToRemove.NotifiyDeletion();
            Data.Remove((T)tToRemove);
        }

        public bool ClearData()
        {
            try
            {
                Data.Clear();
            }
            catch (Exception ex)
            {
                Log.Write("Could not", ex, logType: LogType.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Konstruktor fuer neu
        /// </summary>
        public Controller(ThingDefs OtherDef = ThingDefs.Undef)
        {
            Data = new ObservableCollection<T>();
            _eDataTyp = OtherDef == ThingDefs.Undef ? TypeHelper.TypeToThingDef(typeof(T)) : OtherDef;
        }

        public void SaveCurrentOrdering()
        {
            var i = 1;
            foreach (var item in Data)
            {
                item.Order = i;
                i++;
            }
        }

        public void OrderData(Ordering order)
        {
            var data = Data.ToArray();
            ClearData();
            Data.AddRange(data.OrderBy(x => (order switch
            {
                Ordering.ABC => x.Bezeichner,
                Ordering.Type => x.Typ,
                _ => x.Order.ToString()
            })));
        }
    }
}