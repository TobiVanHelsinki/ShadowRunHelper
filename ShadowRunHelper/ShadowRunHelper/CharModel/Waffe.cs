using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharModel
{
    public abstract class Waffe : Item
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

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Pool;
            strReturn += Delimiter;
            strReturn += SchadenTyp;
            strReturn += Delimiter;
            strReturn += PB;
            strReturn += Delimiter;
            return strReturn;
        }


        public override string HeaderToCSV(string Delimiter)
        {
            var res = ResourceLoader.GetForCurrentView();
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += res.GetString("Model_Waffe_PB/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Waffe_Pool/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Waffe_SchadenTyp/Text");
            strReturn += Delimiter;
            return strReturn;
        }
    }
}
