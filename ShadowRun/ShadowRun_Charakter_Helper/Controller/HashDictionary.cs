using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowRun_Charakter_Helper.CharController;

namespace ShadowRun_Charakter_Helper.Controller
{
    public delegate void ChangedEventHandler(object sender, EventArgs e);

    public class HashDictionary
    {
        public event ChangedEventHandler Changed;

        public Dictionary<int, Model.DictionaryCharEntry> Data { get; set; }

        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
            Debug.WriteLine("This is OnChanged of HD " + this.Data.ToString());
        }

        public void Add(int key, Model.DictionaryCharEntry value)
        {
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

        public HashDictionary()
        {
            Data = new Dictionary<int, Model.DictionaryCharEntry>();
        }

        public int getFreeKey()
        {
            int temp = 0;
            try
            {
                if (Data.Keys.Count != 0)
                {
                    temp = Data.Keys.Max()+1;
                }
            }
            catch (Exception)
            {
                throw new Exception("Konnte keinen neuen Key aus dem Dictionary erhalten");
            }
            if (temp == 0)
            {
                temp = 1; // da 0 als Zahl nicht vergeben wird
            }

            return temp;
        }
    }
}
