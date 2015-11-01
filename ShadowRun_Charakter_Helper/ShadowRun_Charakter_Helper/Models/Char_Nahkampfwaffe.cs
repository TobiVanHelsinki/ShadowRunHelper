
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Char_Nahkampfwaffe
    {
        private ObservableCollection<Char_Nahkampfwaffe> char_Nahkampfwaffen;
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public double Schaden { get; set; }
        public char Schaden_Typ { get; set; }
        public double Reichweite { get; set; }
        public string PB { get; set; }

        public Char_Nahkampfwaffe()
        {

        }
        public Char_Nahkampfwaffe(ObservableCollection<Char_Nahkampfwaffe> char_Nahkampfwaffen)
        {
            ID = 1 + maxID_Nachkampfwaffen(char_Nahkampfwaffen);
        }

        public static int maxID_Nachkampfwaffen(ObservableCollection<Char_Nahkampfwaffe> Liste)
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

