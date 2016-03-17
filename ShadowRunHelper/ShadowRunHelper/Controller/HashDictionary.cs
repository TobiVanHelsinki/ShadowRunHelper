using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowRunHelper.CharController;

namespace ShadowRunHelper.Controller
{
    public delegate void ChangedEventHandler(object sender, EventArgs e);
    public delegate void HDlockedHandler(object sender, EventArgs e);


    public class HashDictionary
    {
        public event ChangedEventHandler Changed;
        public event HDlockedHandler Toggle;
        private bool HDConsistendState = true;
        /// <summary>
        /// <paramref name="Alter Eintrag"/>
        /// <paramref name="Neuer Eintrag"/>
        /// </summary>
        public Dictionary<int, int> AlteHDEntrys { get; set; }

        public Dictionary<int, Model.DictionaryCharEntry> Data { get; set; }

        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
            Debug.WriteLine("This is OnChanged of HD " + this.Data.ToString());
        }

        protected virtual void OnToggle(EventArgs e)
        {
            if (Toggle != null)
                Toggle(this, e);
            Debug.WriteLine("This is OnToggle of HD ");
        }

        public void Add(int key, Model.DictionaryCharEntry value)
        {
            //key überprüfen
            Data.Add(key, value);
        }

        public void Remove(int key)
        {
            Data.Remove(key);
            OnChanged(EventArgs.Empty);

        }

        public Model.DictionaryCharEntry this[int index]
        {
            set
            {
                Data[index] = value;
                OnChanged(EventArgs.Empty);
            }

            get
            {
                return Data[index];
            }
        }


        public TSystem.TResult toggleHDEdit(bool state)
        {
            System.Diagnostics.Debug.WriteLine("toggleHDEdit");
            HDConsistendState = state;
            if (HDConsistendState)
            {
                System.Diagnostics.Debug.WriteLine("HD ist im ConsistendState, OnToggle wird ausgeführt");
                OnToggle(EventArgs.Empty);
            }
            return TSystem.TResult.NO_ERROR;
        }

        public HashDictionary()
        {
            Data = new Dictionary<int, Model.DictionaryCharEntry>();
            AlteHDEntrys = new Dictionary<int, int>();
        }

        public int getFreeKey()
        {
            int temp = 0;
            //try
            //{

            //}
            //catch (Exception)
            //{
            //    throw new Exception("Konnte keinen neuen Key aus dem Dictionary erhalten");
            //}


            for (temp = 1; temp < Data.Count + 1; temp++)
            {
                if (!Data.ContainsKey(temp))
                {
                    return temp;
                }
            }
            return temp;
        }

        public int getFreeMaxKey()
        {
            int o_ntemp = int.MaxValue;
            try
            {
                while (Data.Keys.Contains(o_ntemp))
                {
                    o_ntemp--;
                }
            }
            catch (Exception)
            {
                throw new Exception("Konnte keinen neuen Key aus dem Dictionary erhalten");
            }
            if (o_ntemp == 0)
            {
                o_ntemp = int.MaxValue; // da 0 als Zahl nicht vergeben wird
            }

            return o_ntemp;
        }
    }
}
