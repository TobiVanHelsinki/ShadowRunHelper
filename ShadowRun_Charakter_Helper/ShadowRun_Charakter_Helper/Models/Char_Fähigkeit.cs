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
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public string Zusammensetzung { get; set; }
        public ObservableCollection<Attribut_ID> Zusammensetzung_A { get; set; }
        public ObservableCollection<Attribut_ID> Zusammensetzung_F { get; set; }
        public ObservableCollection<Attribut_ID> Zusammensetzung_I { get; set; }
        public string Anmerkung { get; set; }
        public double Pool_Calc { get; set; }
        public string Pool_Modifier { get; set; }
        public double Pool_User { get; set; }

        public Char_Fähigkeit()
        {
           
        }

        //public static void Pool_Berechnen(Char CurrentChar)
        //{
        //    for (int k = 0; k < CurrentChar.Char_Fähigkeiten.Count(); k++)
        //    {
        //        try
        //        {
        //            CurrentChar.Char_Fähigkeiten[k].Zusammensetzung_A = null;
        //            CurrentChar.Char_Fähigkeiten[k].Zusammensetzung_F = null;
        //            CurrentChar.Char_Fähigkeiten[k].Zusammensetzung_A = new List<int>();
        //            CurrentChar.Char_Fähigkeiten[k].Zusammensetzung_F = new List<int>();
        //            string[] TypSplitArray = CurrentChar.Char_Fähigkeiten[k].Zusammensetzung.Split(';');
        //            string[] TypSplitArray_F = TypSplitArray[0].Split(',');
        //            string[] TypSplitArray_A = TypSplitArray[1].Split(',');
        //            for (int i = 0; i < TypSplitArray_F.Count(); i++)
        //            {
        //                CurrentChar.Char_Fähigkeiten[k].Zusammensetzung_F.Add(Int32.Parse(TypSplitArray_F[i]));
        //            }
        //            for (int i = 0; i < TypSplitArray_A.Count(); i++)
        //            {
        //                CurrentChar.Char_Fähigkeiten[k].Zusammensetzung_A.Add(Int32.Parse(TypSplitArray_A[i]));
        //            }


        //            CurrentChar.Char_Fähigkeiten[k].Pool_Calc = 0;
        //            List<Char_Fertigkeit> Filtert_F = CurrentChar.Char_Fertigkeiten.Where(c => CurrentChar.Char_Fähigkeiten[k].Zusammensetzung_F.Contains(c.ID)).ToList();
        //            for (int i = 0; i < Filtert_F.Count(); i++)
        //            {
        //                CurrentChar.Char_Fähigkeiten[k].Pool_Calc += Filtert_F[i].Stufe;
        //            }

        //            List<Char_Attribut> Filtert_A = CurrentChar.Char_Attribute.Where(c => CurrentChar.Char_Fähigkeiten[k].Zusammensetzung_A.Contains(c.ID)).ToList();
        //            for (int i = 0; i < Filtert_A.Count(); i++)
        //            {
        //                CurrentChar.Char_Fähigkeiten[k].Pool_Calc += Filtert_A[i].Stufe;
        //            }
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }
        //}

        public void del_Zusammensetzung_A_Item(Attribut_ID ID)
        {

            return;
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

        internal static Char_Fähigkeit findByID(int selected_Fähigkeit, ObservableCollection<Char_Fähigkeit> char_Fähigkeiten)
        {
            for (int i = 0; i < char_Fähigkeiten.Count; i++)
            {
                if (char_Fähigkeiten[i].ID == selected_Fähigkeit) {
                    return char_Fähigkeiten[i];
                }

            }
            throw new MyException();
        }
    }
}
