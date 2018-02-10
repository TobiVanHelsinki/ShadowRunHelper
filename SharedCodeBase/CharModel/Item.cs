using System.Collections.Generic;
using TLIB_UWPFRAME;

namespace ShadowRunHelper.CharModel
{
    public class Item : Thing
    {

        private bool? besitz = true;
        [Used]
        public bool? Besitz
        {
            get { return besitz; }
            set
            {
                if (value != this.besitz)
                {
                    this.besitz = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool? aktiv = false;
        [Used]
        public bool? Aktiv
        {
            get { return aktiv; }
            set
            {
                if (value != this.aktiv)
                {
                    this.aktiv = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double anzahl = 1;
        [Used]
        public double Anzahl
        {
            get { return anzahl; }
            set
            {
                if (value != this.anzahl)
                {
                    this.anzahl = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Item()
        {
            this.ThingType = ThingDefs.Item;
        }

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Aktiv;
            strReturn += Delimiter;
            strReturn += Anzahl;
            strReturn += Delimiter;
            strReturn += Besitz;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlatformHelper.GetString("Model_Item_Aktiv/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Item_Anzahl/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Item_Besitz/Text");
            strReturn += Delimiter;
            return strReturn;
        }


        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlatformHelper.GetString("Model_Item_Aktiv/Text"))
                {
                    this.Aktiv = bool.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Item_Anzahl/Text"))
                {
                    this.Anzahl = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Item_Besitz/Text"))
                {
                    this.Besitz = bool.Parse(item.Value);
                    continue;
                }
            }
        }
    }
}
