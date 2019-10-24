///Author: Tobi van Helsinki

using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using TLIB;
using System.ComponentModel;
using SharedCode.Ressourcen;

namespace ShadowRunHelper.CharController
{
    public class Controller<T> : IController<T> where T : Thing, new()
    {
        /// <summary>
        /// GUI-Binding Target
        /// </summary>
        public ObservableCollection<T> Data { get; protected set; }

        public T this[int index] => Data[index];

        public virtual IEnumerable<Thing> GetElements() => Data;

        protected readonly ThingDefs _eDataTyp;
        public ThingDefs eDataTyp { get => _eDataTyp; }

        public virtual void RegisterEventAtData(Action Method)
        {
            Data.CollectionChanged -= (x, y) => Method();
            Data.CollectionChanged += (x, y) => Method();
        }

        public void RegisterEventAtData(Action<object, PropertyChangedEventArgs> Method)
        {
            //TODO nicht mit annoynmen meth
            Data.CollectionChanged -= (x, y) => Method(x, new PropertyChangedEventArgs(""));
            Data.CollectionChanged += (x, y) => Method(x, new PropertyChangedEventArgs(""));
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

        public virtual IEnumerable<AllListEntry> GetElementsForThingList()
        {
            return Data.Select(item => new AllListEntry(item));
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
            int i = 1;
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