
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Char_Panzerung
    {
        private ObservableCollection<Char_Panzerung> char_Panzerungen;
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public double Ballistik { get; set; }
        public double Stoß { get; set; }
        public string Anmerkung { get; set; }

        public Char_Panzerung()
        {
            
        }

        public Char_Panzerung(ObservableCollection<Char_Panzerung> char_Panzerungen)
        {
            ID = 1 + maxID_Panzer(char_Panzerungen);
        }

        public static int maxID_Panzer(ObservableCollection<Char_Panzerung> Liste)
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

