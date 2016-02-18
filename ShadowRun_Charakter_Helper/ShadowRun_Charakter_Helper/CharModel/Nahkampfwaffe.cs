namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Nahkampfwaffe : CharModel.Waffe
    {
        public double reichweite;
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
