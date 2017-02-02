using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharModel
{
    public class Item : Thing
    {

        private bool? besitz = false;
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
        private double anzahl = 0;
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

        public override double GetValue([CallerMemberName] string ID = "")
        {
            return  (this.Aktiv == true) ? base.GetValue(ID) : 0;
        }

        public Item Copy(ref Item target)
        {
            if (target == null)
            {
                target = new Item();
            }
            base.Copy((Thing)target);
            target.Aktiv = Aktiv;
            target.Anzahl = Anzahl;
            target.Besitz = Besitz;
            return target;
        }

        public override void Reset()
        {
            base.Reset();
            Aktiv = false;
            Anzahl = 0;
            Besitz = false;
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
            var res = ResourceLoader.GetForCurrentView();
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += res.GetString("Model_Item_Aktiv/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Item_Anzahl/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Item_Besitz/Text");
            strReturn += Delimiter;
            return strReturn;
        }


        public override void FromCSV(Dictionary<string, string> dic)
        {
            var res = ResourceLoader.GetForCurrentView();
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == res.GetString("Model_Item_Aktiv/Text"))
                {
                    this.Aktiv = Boolean.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Item_Anzahl/Text"))
                {
                    this.Anzahl = Int64.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Item_Besitz/Text"))
                {
                    this.Besitz = Boolean.Parse(item.Value);
                    continue;
                }
            }
        }
    }
}
