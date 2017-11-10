namespace ShadowRunHelper1_3.CharModel
{
    public class Fernkampfwaffe : CharModel.Waffe
    {
        private double rueckstoss = 0;
        public double Rueckstoss
        {
            get { return rueckstoss; }
            set
            {
                if (value != this.rueckstoss)
                {
                    this.rueckstoss = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string modi = "";
        public string Modi
        {
            get { return modi; }
            set
            {
                if (value != this.modi)
                {
                    this.modi = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Fernkampfwaffe()
        {
        }
    }
}
