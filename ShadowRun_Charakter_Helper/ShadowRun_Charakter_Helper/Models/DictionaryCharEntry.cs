using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class DictionaryCharEntry
    {
        protected String Bezeichner;
        protected String Typ;
        protected int Wert;
        protected String Zusatz;
        protected String Notiz;

        public DictionaryCharEntry(String bezeichner, String typ, int wert, String zusatz,  String notiz)
        {
            this.Bezeichner = bezeichner;
            this.Typ = typ;
            this.Wert = wert;
            this.Zusatz = zusatz;
            this.Notiz = notiz;
        }

        public DictionaryCharEntry()
        {
        }
    }
}
