
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Char_Programm
    {
        private ObservableCollection<Char_Programm> char_Programme;
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public string Optionen { get; set; }
        public string Notizen { get; set; }

        public Char_Programm()
        {
           
        }

        public Char_Programm(ObservableCollection<Char_Programm> char_Programme)
        {
            ID = 1 + maxID_Programme(char_Programme);
        }
        public static int maxID_Programme(ObservableCollection<Char_Programm> Liste)
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

