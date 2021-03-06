﻿//Author: Tobi van Helsinki


using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ShadowRunHelper.CharController
{
    public interface IController
    {
        /// <summary>
        /// returns all connectable things of this controller
        /// </summary>
        /// <returns></returns>
        IEnumerable<Thing> GetAllData();

        /// <summary>
        /// Returns all things of this controller
        /// </summary>
        /// <returns></returns>
        IEnumerable<Thing> GetData();

        /// <summary>
        /// Used to pass throgh the possibility to acces the Data Object for registration of its HasChanged-Methods without knowing its type
        /// </summary>
        /// <param name="Method"></param>
        void RegisterEventAtData(Action<object, PropertyChangedEventArgs> Method);

        void RegisterEventAtData(Action Method);

        ThingDefs eDataTyp { get; }

        Thing AddNewThing();

        Thing AddNewThing(Thing newThing);

        void RemoveThing(Thing tToRemove);

        bool ClearData();

        void OrderData(Ordering order);

        void SaveCurrentOrdering();

        int IndexOf(Thing t);
    }

    public enum Ordering
    {
        ABC = 1,
        Type = 2,
        Original = 3,
    }

    public interface IController<T> : IController
    {
        ObservableCollection<T> Data { get; }
        T this[int index]
        {
            get;
        }
    }
}