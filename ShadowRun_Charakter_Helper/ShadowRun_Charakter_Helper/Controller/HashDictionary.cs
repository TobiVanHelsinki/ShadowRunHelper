using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Controller
{
    public class HashDictionary
    {
        public Dictionary<int, Models.DictionaryCharEntry> Data;

        public HashDictionary()
        {
            Data = new Dictionary<int, Models.DictionaryCharEntry>();
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
