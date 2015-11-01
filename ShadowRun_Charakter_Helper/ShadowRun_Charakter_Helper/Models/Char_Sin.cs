
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Char_Sin
    {
        private ObservableCollection<Char_Sin> char_Sins;
        public int ID { get; set; }
        public string Name { get; set; }
        public double Stufe { get; set; }
        public string Verwendung { get; set; }
        public DateTime Geburtstag { get; set; }
        public string Zusätze { get; set; }

        public Char_Sin()
        {
           
        }

        public Char_Sin(ObservableCollection<Char_Sin> char_Sins)
        {
            ID = 1 + maxID_Sins(char_Sins);
        }

        public static int maxID_Sins(ObservableCollection<Char_Sin> Liste)
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
