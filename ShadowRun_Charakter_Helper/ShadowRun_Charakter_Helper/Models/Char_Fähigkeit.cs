using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Char_Fähigkeit
    {
        private ObservableCollection<Char_Fähigkeit> char_Fähigkeiten;

        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public string Zusammensetzung { get; set; }
        public string Anmerkung { get; set; }
        public double Pool_Calc { get; set; }
        public string Pool_Modifier { get; set; }
        public double Pool_User { get; set; }

        public Char_Fähigkeit()
        {
           
        }

        public Char_Fähigkeit(ObservableCollection<Char_Fähigkeit> Liste)
        {
            ID = 1 + maxID(Liste);
        }

        public static int maxID(ObservableCollection<Char_Fähigkeit> Liste)
        {
            int temp = 0;
            for (int i= Liste.Count; i>0;i--)
            {
                if (Liste[i-1].ID > temp)
                {
                    temp = Liste[i-1].ID;
                }
            }

            return temp;
        }
    }
}
