///Author: Tobi van Helsinki

using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using SharedCode.Ressourcen;
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

        public virtual IEnumerable<Thing> GetElements()
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
            var newThing = Activator.CreateInstance<T>();
            newThing.Order = Data.MaxOrDefault(x => x.Order) + 1;
            Data.Add(newThing);
            return newThing;
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
            IEnumerable<T> OrderedData;
            switch (order)
            {
                case Ordering.ABC:
                    OrderedData = Data.OrderBy(x => x.Bezeichner).ToList();
                    break;
                case Ordering.Type:
                    OrderedData = Data.OrderBy(x => x.Typ).ToList();
                    break;
                case Ordering.Original:
                default:
                    OrderedData = Data.OrderBy(x => x.Order).ToList();
                    break;
            }
            ClearData();
            Data.AddRange(OrderedData);
        }

        public void RefreshIdentifiers(object controller)
        {
            foreach (var item in controller.GetType().GetProperties().Where(x => x.PropertyType == typeof(T) && x.CanRead && x.CanWrite))
            {
                if (item.GetValue(controller) is T thing)
                {
                    var name = typeof(T).Name + "_" + item.Name;
                    try
                    {
                        thing.Bezeichner = ModelResources.ResourceManager.GetString(name);
                    }
                    catch (Exception ex)
                    {
                        thing.Bezeichner = Constants.NoResourceFallback;
                        Log.Write("Could not get res string for " + name, ex, logType: LogType.Error);
                    }
                }
            }
        }
    }
}