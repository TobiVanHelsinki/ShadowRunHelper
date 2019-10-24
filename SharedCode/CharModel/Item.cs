///Author: Tobi van Helsinki

namespace ShadowRunHelper.CharModel
{
    public class Item : Thing
    {
        private bool? besitz = true;
        [Used_UserAttribute]
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
        [Used_UserAttribute]
        public bool? Aktiv
        {
            get { return aktiv; }
            set
            {
                if (value != this.aktiv)
                {
                    aktiv = value;
                    RefreshCharProperties();
                    NotifyPropertyChanged();
                }
            }
        }

        private double anzahl = 1;
        [Used_UserAttribute]
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
            RefreshCharProperties();
        }

        private void RefreshCharProperties()
        {
            foreach (var item in GetConnects())
            {
                item.Active = aktiv == true;
            }
        }
    }
}