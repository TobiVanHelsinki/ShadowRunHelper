
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Char_Fertigkeit
    {
      
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public double Stufe { get; set; }
        public string Anmerkung { get; set; }
        public string Art { get; set; }

        public Char_Fertigkeit()
        {
            
        }

        public Char_Fertigkeit(ObservableCollection<Char_Fertigkeit> char_Fertigkeiten)
        {
            ID = 1 + maxID(char_Fertigkeiten);
        }

        public static int maxID(ObservableCollection<Char_Fertigkeit> Liste)
        {
            int temp = 0;
            for (int i = Liste.Count; i > 0; i--)
            {
                if (Liste[i - 1].ID > temp)
                {
                    temp = Liste[i - 1].ID;
                }
            }
            return temp;
        }
    }
}
