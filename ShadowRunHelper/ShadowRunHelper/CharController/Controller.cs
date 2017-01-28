using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ShadowRunHelper.CharController
{
    public class cController<T> : IController<T> where T : CharModel.Thing, new()
    {
        /// <summary>
        /// GUI-Binding Target
        /// </summary>
        public ObservableCollection<T> Data;

        public T AddNewThing()
        {
            T newTee = new T();
            Data.Add(newTee);
            return null;
        }
        public void RemoveThing(T tRem)
        {
            Data.Remove(tRem);
        }

        public List<KeyValuePair<Thing, string>> GetElements()
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
    }
}