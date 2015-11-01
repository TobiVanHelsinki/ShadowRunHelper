using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Char_Kommlink
    {
        private ObservableCollection<Char_Kommlink> char_Kommlinks;
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public double Stufe { get; set; }
        public double AnzahlProgramme { get; set; }
        public string GrundKonfiguration { get; set; }
        public double Angriff { get; set; }
        public double Schleicher { get; set; }
        public double Firewall { get; set; }
        public double Datenverarbeitung { get; set; }

        public Char_Kommlink()
        {
           
        }
        public Char_Kommlink(ObservableCollection<Char_Kommlink> char_Kommlinks)
        {
            ID = 1 + maxID_Kommlink(char_Kommlinks);
        }

        public static int maxID_Kommlink(ObservableCollection<Char_Kommlink> Liste)
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
