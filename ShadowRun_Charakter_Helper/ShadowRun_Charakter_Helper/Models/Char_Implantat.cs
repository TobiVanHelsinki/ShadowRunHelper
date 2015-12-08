using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Char_Implantat
    {
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public double Stufe { get; set; }
        public double Essenz { get; set; }
        public string Anmerkung { get; set; }

        public Char_Implantat()
        {

        }

        public Char_Implantat(ObservableCollection<Char_Implantat> char_Implantate)
        {
            ID = 1 + maxID(char_Implantate);
        }

        public static int maxID(ObservableCollection<Char_Implantat> Liste)
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
