namespace ShadowRunHelper.CharModel
{
    public abstract class Waffe : Item
    {
        /// <summary>
        /// Praezision
        /// </summary>
        private double pool = 0;
        [Used_UserAttribute]
        public double Praezision
        {
            get { return pool; }
            set
            {
                if (value != pool)
                {
                    pool = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string schadenTyp = "";
        [Used_UserAttribute]
        public string SchadenTyp
        {
            get { return schadenTyp; }
            set
            {
                if (value != schadenTyp)
                {
                    schadenTyp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// PB could be english, means DK "Durschlagskompensation"
        /// </summary>
        private double pB = 0; // DK
        [Used_UserAttribute]
        public double PB
        {
            get { return pB; }
            set
            {
                if (value != pB)
                {
                    pB = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
