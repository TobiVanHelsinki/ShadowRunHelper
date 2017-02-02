using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ShadowRunHelper.CharController
{
    public class cController<T> : IController<T> where T : Thing, new()
    {
        /// <summary>
        /// GUI-Binding Target
        /// </summary>
        public ObservableCollection<T> Data { get; protected set; }
        public ThingDefs eDataTyp { get; protected set; }

        public T AddNewThing()
        {
            T newTee = new T();
            Data.Add(newTee);
            return newTee;
        }
        public void RemoveThing(T tRem)
        {
            Data.Remove(tRem);
        }

        public List<KeyValuePair<Thing, string>> GetElementsForThingList()
        {
            List<KeyValuePair<Thing, string>> lstReturn = new List<KeyValuePair<Thing, string>>();
            foreach (var item in Data)
            {
                lstReturn.Add(new KeyValuePair<Thing, string>(item, ""));
            }
            return lstReturn;
        }

        /// <summary>
        /// Konstruktor für neu
        /// </summary>
        public cController()
        {
            Data = new ObservableCollection<T>();
            Data.CollectionChanged += Data_CollectionChanged;
        }

        private void Data_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        public KeyValuePair<string, string> MultipleCSVExport(string strDelimiter, string strNewLine, string strNew)
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
            return new KeyValuePair<string, string>(strTemp, TypenHelper.ThingDefToString(this.eDataTyp, true));
        }



        public void MultipleCSVImport(char strDelimiter, char strNewLine, string strReadFile)
        {
            string[] Lines = strReadFile.Split(strNewLine);
            string[] Headar = Lines[0].Split(strDelimiter);
            for (int i = 1; i < Lines.Length; i++) //start at 1 to overjump first line
            {
                // key = propertie name, value = value
                Dictionary<string, string> Dic = new Dictionary<string, string>();
                int j = 0;
                foreach (var itemstring in Lines[i].Split(strDelimiter))
                {
                    Dic.Add(Headar[j], itemstring);
                    j++;
                }
                (this.AddNewThing())?.FromCSV(Dic);
            }
        }
    }
}