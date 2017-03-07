namespace ShadowRunHelper1_3.CharModel
{
    public class Nahkampfwaffe : CharModel.Waffe
    {
        private double reichweite = 0;
        public double Reichweite
        {
            get { return reichweite; }
            set
            {
                if (value != this.reichweite)
                {
                    this.reichweite = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Nahkampfwaffe()
        {
        }
    }
}
