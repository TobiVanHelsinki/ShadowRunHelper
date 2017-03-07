using System.Collections.Generic;


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

        public override Thing Copy(ref Thing target)
        {
            if (target == null)
            {
                target = new Nahkampfwaffe();
            }
            base.Copy(ref target);
            ((Nahkampfwaffe)target).Reichweite = Reichweite;
            return target;
        }

        public override void Reset()
        {
            base.Reset();
            Reichweite = 0;
        }

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Reichweite;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlattformHelper.GetString("Model_Nahkampfwaffe_Reichweite/Text");
            strReturn += Delimiter;
            return strReturn;
        }

        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlattformHelper.GetString("Model_Nahkampfwaffe_Reichweite/Text"))
                {
                    Reichweite = double.Parse(item.Value);
                    continue;
                }
            }
        }
    }
}
