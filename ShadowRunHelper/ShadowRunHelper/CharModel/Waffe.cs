namespace ShadowRunHelper.CharModel
{
    public class Waffe : Item
    {
        private double pool = 0;
        public double Pool
        {
            get { return pool; }
            set
            {
                if (value != this.pool)
                {
                    this.pool = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private char schadenTyp = ' ';
        public char SchadenTyp
        {
            get { return schadenTyp; }
            set
            {
                if (value != this.schadenTyp)
                {
                    this.schadenTyp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double pB = 0;
        public double PB
        {
            get { return pB; }
            set
            {
                if (value != this.pB)
                {
                    this.pB = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Waffe()
        {
            this.ThingType = ThingDefs.UndefTemp;
        }
    }
}
