using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Char_Drone_Fahrzeug
    {
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Typ { get; set; }
        public string Anmerkung { get; set; }

        public string Schaden { get; set; }
        public string Größe { get; set; }
        public string Handling { get; set; }
        public string Beschleunigung { get; set; }
        public string Gewicht { get; set; }
        public string Pilot { get; set; }
        public string Rumpf { get; set; }
        public string Panzer { get; set; }
        public string Sensor { get; set; }

        public string Geschwindigkeit { get; set; }

        public Char_Drone_Fahrzeug()
        {

        }
        public Char_Drone_Fahrzeug(ObservableCollection<Char_Drone_Fahrzeug> char_Dronen_Fahrzeuge)
        {
            ID = 1 + maxID(char_Dronen_Fahrzeuge);
        }

        public static int maxID(ObservableCollection<Char_Drone_Fahrzeug> Liste)
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