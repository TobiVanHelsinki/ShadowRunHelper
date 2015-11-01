
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Char_Item
    {
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public double Stufe { get; set; }
        public string Anmerkung { get; set; }

        public Char_Item()
        {

        }

        public Char_Item(ObservableCollection<Char_Item> char_Items)
        {
            ID = 1 + maxID_Items(char_Items);
        }

        public static int maxID_Items(ObservableCollection<Char_Item> Liste)
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
