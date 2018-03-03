using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using TLIB_UWPFRAME.Resources;

namespace ShadowRunHelper.CharController
{
    public class Controller<T> : IController<T> where T : Thing, new()
    {
        /// <summary>
        /// GUI-Binding Target
        /// </summary>
        public ObservableCollection<T> Data { get; protected set; }
        public IEnumerable<Thing> GetElements() => Data;

        protected readonly ThingDefs _eDataTyp;
        public ThingDefs eDataTyp { get => _eDataTyp; }

        public virtual void RegisterEventAtData(Action Method)
        {
            Data.CollectionChanged -= (x, y) => Method();
            Data.CollectionChanged += (x, y) => Method();
        }

        public virtual Thing AddNewThing()
        {
            var newThing = (T)Activator.CreateInstance(TypeHelper.ThingDefToType(eDataTyp));
            Data.Add(newThing);
            return newThing;
        }
        public virtual Thing AddNewThing(Thing newThing)
        {
            Data.Add((T)newThing);
            return newThing;
        }
        public virtual void RemoveThing(Thing tToRemove)
        {
            Data.Remove((T)tToRemove);
        }


        public bool ClearData()
        {
            try
            {
                Data.Clear();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public virtual IEnumerable<AllListEntry> GetElementsForThingList()
        {
            return Data.Select(item => new AllListEntry(item, ""));
        }

        /// <summary>
        /// Konstruktor fuer neu
        /// </summary>
        public Controller()
        {
            Data = new ObservableCollection<T>();
            _eDataTyp = TypeHelper.TypeToThingDef(typeof(T));
        }

        #region CSV
        public string Data2CSV(char strDelimiter, char strNewLine)
        {
            return IO.CSV_Converter.Data2CSV(strDelimiter, strNewLine, Data);
        }

        public void CSV2Data(char strDelimiter, char strNewLine, string strReadFile)
        {
            Data.AddRange(IO.CSV_Converter.CSV2Data<T>(strDelimiter, strNewLine, strReadFile));
        }


        #endregion
    }
}