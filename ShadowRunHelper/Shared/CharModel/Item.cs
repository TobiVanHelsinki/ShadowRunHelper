using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharModel
{
    public class Item : Thing
    {

        private bool? besitz = true;
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
        private double anzahl = 1;
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
        
        public override Thing Copy(ref Thing target)
        {
            if (target == null)
            {
                target = new Item();
            }
            base.Copy(ref target);
            Item TargetS = (Item)target;
            TargetS.Aktiv = Aktiv;
            TargetS.Anzahl = Anzahl;
            TargetS.Besitz = Besitz;
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
                    this.Aktiv = bool.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Item_Anzahl/Text"))
                {
                    this.Anzahl = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Item_Besitz/Text"))
                {
                    this.Besitz = bool.Parse(item.Value);
                    continue;
                }
            }
        }
    }
}
