namespace ShadowRunHelper.CharModel
{
    public class Panzerung : Item
    {
        private double kapazitaet = 0;
        [Used]
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
    }
}
