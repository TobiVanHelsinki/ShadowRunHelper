///Author: Tobi van Helsinki

namespace ShadowRunHelper.CharModel
{
    public class Nahkampfwaffe : Waffe
    {
        private double reichweite = 0;
        [Used_UserAttribute]
        public double Reichweite
        {
            get => reichweite;
            set
            {
                if (value != reichweite)
                {
                    reichweite = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}