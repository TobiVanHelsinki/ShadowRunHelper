using System.Linq;

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
                    this.aktiv = value;
                    NotifyPropertyChanged();
                    RefreshCharProperties();
                }
            }
        }

        private void RefreshCharProperties()
        {
            foreach (var item in GetProperties(this).Where(x => x.PropertyType == typeof(CharProperty)).Select(x => x.GetValue(this)).OfType<CharProperty>())
            {
                item.Active = aktiv == true;
            }
        }

        private double anzahl = 1;

        public Item()
        {
            RefreshCharProperties();
        }

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

        protected override bool UseForCalculation()
        {
            return aktiv == false ? false : true;
        }
    }
}
