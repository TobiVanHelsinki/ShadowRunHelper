using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Panzerung : CharModel.Item
    {
        public double ballistik { get; set; }
        public double Ballistik
        {
            get { return ballistik; }
            set
            {
                if (value != this.ballistik)
                {
                    this.ballistik = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double stoß;
        public double Stoß
        {
            get { return stoß; }
            set
            {
                if (value != this.stoß)
                {
                    this.stoß = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Panzerung(int dicCD_ID)
        {
            this.DicCD_ID = dicCD_ID;
            Random rand;
            rand = new Random();

            this.Bezeichner = "Tes" + rand.Next();
        }

        public Panzerung()
        {

        }


        //public static implicit operator int(Panzerung v)
        //{
        //    try
        //    {
        //        return v;
        //    }
        //    catch (Exception)
        //    {

        //        throw new Exception("Konvertierung von T in Panzerung fehlgeschlagen");
        //    }
                
                
        //}
    }
}
