using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Char_Connection
    {
        private ObservableCollection<Char_Connection> char_Connections;
        public int ID { get; set; }
        public string Name { get; set; }
        public double Loyal { get; set; }
        public double Connection { get; set; }
        public string Anmerkung { get; set; }

        public Char_Connection()
        {

        }
        public Char_Connection(ObservableCollection<Char_Connection> char_Connections)
        {
            ID = 1 + maxID(char_Connections);
        }

        public static int maxID(ObservableCollection<Char_Connection> Liste)
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