namespace ShadowRunHelper.CharModel
{
    public class Nahkampfwaffe : Waffe
    {
        private double reichweite = 0;
        [Used_UserAttribute]
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
    }
}
