using System.Collections.Generic;
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
                if (value != rückstoß)
                {
                    rückstoß = value;
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
                if (value != modi)
                {
                    modi = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Fernkampfwaffe()
        {

            ThingType = ThingDefs.Fernkampfwaffe;
        }

        public override Thing Copy(ref Thing target)
        {
            if (target == null)
            {
                target = new Fernkampfwaffe();
            }
            base.Copy(ref target);
            Fernkampfwaffe TargetS = (Fernkampfwaffe)target;
            TargetS.Rückstoß = Rückstoß;
            TargetS.Modi = Modi;
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
        public override void FromCSV(Dictionary<string, string> dic)
        {
            var res = ResourceLoader.GetForCurrentView();
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == res.GetString("Model_Fernkampfwaffe_Rückstoß/Text"))
                {
                    Rückstoß = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Fernkampfwaffe_Modi/Text"))
                {
                    Modi= (item.Value);
                    continue;
                }
            }
        }
    }
}
