namespace ShadowRunHelper.CharModel
{
    public class Nahkampfwaffe : Waffe
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
            this.ThingType = ThingDefs.Nahkampfwaffe;
        }


        public Nahkampfwaffe Copy(ref Nahkampfwaffe target)
        {
            if (target == null)
            {
                target = new Nahkampfwaffe();
            }
            base.Copy((Thing)target);
            target.Reichweite = Reichweite;
            return target;
        }

        public new void Reset()
        {
            base.Reset();
            Reichweite = 0;
        }

        public new string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Reichweite;
            strReturn += Delimiter;
            return strReturn;
        }
    }
}
