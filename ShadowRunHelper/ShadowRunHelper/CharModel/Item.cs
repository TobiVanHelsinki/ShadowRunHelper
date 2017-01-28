using System;

namespace ShadowRunHelper.CharModel
{
    public class Item : CharModel.Thing
    {
        //public override double GetValue(string ID = "")
        //{
        //    return Wert;
        //}
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
            this.ThingType = Ressourcen.TypNamen.ThingDefs.Item;
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

        public void Reset()
        {
            base.Reset();
            Aktiv = false;
            Anzahl = 0;
            Besitz = false;
        }
    }
}
