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
                IDictionaryEnumerator Enum = Data.GetEnumerator();


                while (Data.ContainsKey(temp))
                {
                    temp++;
                }

            }
            catch (Exception)
            {
                throw new Exception("Konnte keinen neuen Key aus dem Dictionary erhalten");
            }



            return temp;
        }

    }
}
