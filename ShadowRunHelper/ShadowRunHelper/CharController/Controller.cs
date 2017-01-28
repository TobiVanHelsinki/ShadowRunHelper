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
            return newTee;
            //TODO invoke Event Data Changed (Adding, tAdd)
            throw new NotImplementedException();
        }
        public void RemoveThing(T tRem)
        {
            Data.Remove(tRem);
            //TODO invoke Event Data Changed (Removing, tRem)
            throw new NotImplementedException();
        }

        public ObservableCollection<T> GetElements()
        {
            return Data;
        }

        /// <summary>
        /// Konstruktor für neu
        /// </summary>
        public cController()
        {
        }

    }
}