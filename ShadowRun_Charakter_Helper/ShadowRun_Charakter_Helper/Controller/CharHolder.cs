using ShadowRun_Charakter_Helper.CharModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Controller
{
    public class CharHolder
    {
        public Dictionary<int, Models.DictionaryCharEntry> DicCD = new Dictionary<int, Models.DictionaryCharEntry>();
        public int DicCD_getFreeKey()
        {
            int temp = 0;
            try
            {
                IDictionaryEnumerator Enum = DicCD.GetEnumerator();


                while (DicCD.ContainsKey(temp))
                {
                        temp++;
                }

                //do
                //{
                //    if (temp < (int)Enum.Key)
                //    {
                //        temp = (int)Enum.Entry.Key;
                //    }
                //} while (Enum.MoveNext() != false);
            }
            catch (Exception)
            {
                throw new Exception("Konnte keinen neuen Key aus dem Dictionary erhalten");
            }



            return temp;
        }

        public ObservableCollection<Handlung> Handlungen { get; set; }

        public void remove(Handlung del)
        {
            DicCD.Remove(del.DicCD_ID);
            Handlungen.Remove((Handlung)del);
        }

        public void add<T>()
        {
            int ID = DicCD_getFreeKey();

            if (typeof(T) == typeof(Handlung))
            {
                Handlungen.Add(new Handlung(ID));
            }
            else if (typeof(T) == typeof(Panzerung))
            {
               // PanzerungsController.add(ID);
            }
        }
        public CharController.Panzerung PanzerungsController { get; set; }

        


        public ObservableCollection<Fertigkeit> CharData_Fertigkeiten { get; set; }

        public CharHolder()
        {
            Handlungen = new ObservableCollection<Handlung>();
            CharData_Fertigkeiten = new ObservableCollection<Fertigkeit>();
            PanzerungsController = new CharController.Panzerung();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}