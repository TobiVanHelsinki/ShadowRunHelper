using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Char_Attribut
    {
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public double Stufe { get; set; }
        public string Stufe_Modifier { get; set; }

        public Char_Attribut()
        {

        }

        internal static object getNAMEbyID(object value)
        {

            throw new NotImplementedException();


        }

        public Char_Attribut(int ID, string Bezeichnung, double Stufe, string Stufe_Modifier)
        {
            this.ID = ID;
        }

        public Char_Attribut(ObservableCollection<Char_Attribut> Liste)
        {
            ID = 1 + maxID(Liste);
        }

        public static int maxID(ObservableCollection<Char_Attribut> Liste)
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
