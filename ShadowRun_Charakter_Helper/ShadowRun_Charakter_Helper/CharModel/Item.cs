namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Item : CharModel.Model
    {
        public bool besitz;
        public bool Besitz
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
        public bool aktiv;
        public bool Aktiv
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
        public double anzahl;
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
        }

        public Item(int dicCD_ID)
        {
            this.DicCD_ID = dicCD_ID;
        }
    }
}
