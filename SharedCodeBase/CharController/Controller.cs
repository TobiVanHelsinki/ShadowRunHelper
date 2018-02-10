using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace ShadowRunHelper.CharController
{
    public class Controller<T> : IController<T> where T : Thing, new()
    {
        /// <summary>
        /// GUI-Binding Target
        /// </summary>
        public ObservableCollection<T> Data { get; protected set; }
        protected readonly ThingDefs _eDataTyp;
        public ThingDefs eDataTyp { get => _eDataTyp; }

        public virtual void RegisterEventAtData(Action Method)
        {
            Data.CollectionChanged += (x, y) => Method();
        }

        public virtual T AddNewThing()
        {
            T newTee = new T();
            Data.Add(newTee);
            return newTee;
        }
        public virtual T AddNewThing(T newTee)
        {
            Data.Add(newTee);
            return newTee;
        }
        public virtual void RemoveThing(T tRem)
        {
            Data.Remove(tRem);
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
            _eDataTyp = TypenHelper.TypeToThingDef(typeof(T));
        }

        public (string ThingType, string Content) MultipleCSVExport(string strDelimiter, string strNewLine, string strNew)
        {
            string strTemp = strNew;
            if (this.Data.Count >= 1)
            {
                strTemp += this.Data[0].HeaderToCSV(strDelimiter);
            }
            strTemp += strNewLine;
            foreach (T item in this.Data)
            {
                strTemp += item.ToCSV(strDelimiter);
                strTemp += strNewLine;
            }
            return (TypenHelper.ThingDefToString(eDataTyp, true), strTemp);
        }

        public void MultipleCSVImport(char strDelimiter, char strNewLine, string strReadFile)
        {
            string[] Lines = strReadFile.Split(strNewLine);
            string[] Headar = { };
            for (int i = 0; i < Lines.Length; i++) //start at 2 to overjump first lines
            {
                // key = propertie name, value = value
                string[] CSVEntries = Lines[i].Split(strDelimiter);
                if (CSVEntries.Length < 5)
                {
                    continue;
                }
                if (Headar.Length < CharModel.Thing.nThingPropertyCount) 
                {
                    Headar = Lines[i].Split(strDelimiter);
                    continue;
                }
                Dictionary<string, string> Dic = new Dictionary<string, string>();
                int j = 0;
                foreach (var itemstring in CSVEntries)
                {
                    try
                    {
                        Dic.Add(Headar[j], itemstring);
                    }
                    catch (Exception)
                    {
                         continue;
                    }
                    j++;
                }
                (this.AddNewThing())?.FromCSV(Dic);
            }
        }

        public IEnumerable<Thing> GetElements()
        {
            return Data;
        }

    }
}