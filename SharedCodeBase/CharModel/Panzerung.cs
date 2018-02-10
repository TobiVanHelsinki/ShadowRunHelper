namespace ShadowRunHelper.CharModel
{
    public class Panzerung : Item
    {
        private double kapazitaet = 0;
        [Used_UserAttribute]
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
    }
}
