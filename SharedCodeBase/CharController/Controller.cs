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
            string ret = "sep=" + strDelimiter + strNewLine;
            if (this.Data.Count >= 1)
            {
                ret += this.Data[0].HeaderToCSV(strDelimiter);
            }
            ret += strNewLine;
            foreach (T item in this.Data)
            {
                ret += item.ToCSV(strDelimiter);
                ret += strNewLine;
            }
            return ret;
        }

        public void CSV2Data(char strDelimiter, char strNewLine, string strReadFile)
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


        #endregion
    }
}