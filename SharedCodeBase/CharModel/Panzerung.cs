using System.Collections.Generic;
using TLIB;

namespace ShadowRunHelper.CharModel
{
    public class Panzerung : Item
    {
        /// <summary>
        /// fuer SR4 - ballisitisch waere dann thing.wert
        /// </summary>
        //private double stoss = 0;
        //public double Stoss
        //{
        //    get { return stoss; }
        //    set
        //    {
        //        if (value != this.stoss)
        //        {
        //            this.stoss = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        private double kapazitaet = 0;
        public double Kapazitaet
        {
            get { return kapazitaet; }
            set
            {
                if (value != this.kapazitaet)
                {
                    this.kapazitaet = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Panzerung()
        {
            ThingType = ThingDefs.Panzerung;
        }

        public override Thing Copy(Thing target = null)
        {
            if (target == null)
            {
                target = new Panzerung();
            }
            return base.Copy(target);
        }

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            //strReturn += Stoss;
            //strReturn += Delimiter;
            strReturn += Kapazitaet;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlatformHelper.GetString("Model_Panzerung_Stoss/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Panzerung_Kapazitaet/Text");
            strReturn += Delimiter;
            return strReturn;
        }
        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                //if (item.Key == CrossPlatformHelper.GetString("Model_Panzerung_Stoss/Text"))
                //{
                    //Stoss = double.Parse(item.Value);
                    //continue;
                //}
                if (item.Key == CrossPlatformHelper.GetString("Model_Panzerung_Kapazitaet/Text"))
                {
                    Kapazitaet = double.Parse(item.Value);
                    continue;
                }
            }
        }
    }
}
