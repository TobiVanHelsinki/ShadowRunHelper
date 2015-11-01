using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Char_Fernkampfwaffe
    {
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public double Schaden { get; set; }
        public char Schaden_Typ { get; set; }
        public string Modus { get; set; }
        public double Rückstoß { get; set; }
        public string Munition { get; set; }
        public string PB { get; set; }

        public Char_Fernkampfwaffe()
        {

        }

        public Char_Fernkampfwaffe(ObservableCollection<Char_Fernkampfwaffe> char_Fernkampfwaffen)
        {
            ID = 1 + maxID(char_Fernkampfwaffen);
        }

        public static int maxID(ObservableCollection<Char_Fernkampfwaffe> Liste)
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
