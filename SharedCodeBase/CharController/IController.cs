using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ShadowRunHelper.CharController
{
    public interface IController
    {
        /// <summary>
        /// To populate the Link List - will be overridden by singlecontroller
        /// </summary>
        /// <returns></returns>
        IEnumerable<AllListEntry> GetElementsForThingList();

        /// <summary>
        /// To populate the "All Things" List just return your data. used for search
        /// </summary>
        /// <returns></returns>
        IEnumerable<Thing> GetElements();

        /// <summary>
        /// retval: List of Tuple: item1 (ThingType) is Name for waht is at the string (attribut or cyberdeck orso)
        /// </summary>
        /// <param name="strDelimiter"></param>
        /// <returns></returns>
        /// 
        (string ThingType, string Content) MultipleCSVExport(string strDelimiter, string strNewLine, string strNew);

        /// <summary>
        /// Used to pass throgh the possibility to acces the Data Object for registration of its HasChanged-Methods without knowing its type
        /// </summary>
        /// <param name="Method"></param>
        void RegisterEventAtData(Action Method);


        ThingDefs eDataTyp { get; }

}

    public interface IController<T> : IController
    {
        ObservableCollection<T> Data { get;}
        T AddNewThing(T newTee);
        void RemoveThing(T tRem);
    }
}