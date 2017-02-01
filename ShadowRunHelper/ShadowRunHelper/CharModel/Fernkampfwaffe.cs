using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharModel
{
    public class Fernkampfwaffe : Waffe
    {
        private double rückstoß = 0;
        public double Rückstoß
        {
            get { return rückstoß; }
            set
            {
                if (value != this.rückstoß)
                {
                    this.rückstoß = value;
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

            this.ThingType = ThingDefs.Fernkampfwaffe;
        }

        public Fernkampfwaffe Copy(Fernkampfwaffe target = null)
        {
            if (target == null)
            {
                target = new Fernkampfwaffe();
            }
            base.Copy(target);
            target.Rückstoß = Rückstoß;
            target.Modi = Modi;
            return target;
        }

        public override void Reset()
        {
            Rückstoß = 0;
            Modi = "";
            base.Reset();
        }

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Rückstoß;
            strReturn += Delimiter;
            strReturn += Modi;
            strReturn += Delimiter;
            return strReturn;
        }


        public override string HeaderToCSV(string Delimiter)
        {
            var res = ResourceLoader.GetForCurrentView();
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += res.GetString("Model_Fernkampfwaffe_Rückstoß/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Fernkampfwaffe_Modi/Text");
            strReturn += Delimiter;
            return strReturn;
        }
    }
}
