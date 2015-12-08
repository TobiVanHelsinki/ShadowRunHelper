
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Char_Vorteil
    {

        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public double Stufe { get; set; }
        public string Zusammensetzung { get; set; }
        public string Auswirkungen { get; set; }
        public string Anmerkungen { get; set; }

        public Char_Vorteil()
        {

        }

        public Char_Vorteil(ObservableCollection<Char_Vorteil> char_Vorteile)
        {
            ID = 1 + maxID_Vorteile(char_Vorteile);
        }



        public static int maxID_Vorteile(ObservableCollection<Char_Vorteil> Liste)
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

