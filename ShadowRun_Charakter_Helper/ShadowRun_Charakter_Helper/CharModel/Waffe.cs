namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Waffe : CharModel.Item
    {
        public double pool;
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

        public double schaden;
        public double Schaden
        {
            get { return schaden; }
            set
            {
                if (value != this.schaden)
                {
                    this.schaden = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public char schadenTyp;
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

        public double pB;
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
        }
    }
}
