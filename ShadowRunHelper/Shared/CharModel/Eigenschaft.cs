
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public abstract class Eigenschaft : Thing
    {
        private string auswirkungen = "";
        public string Auswirkungen
        {
            get { return auswirkungen; }
            set
            {
                if (value != auswirkungen)
                {
                    auswirkungen = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public override Thing Copy(ref Thing target)
        {
            if (target == null)
            {
                throw new System.ArgumentNullException();
            }
            base.Copy(ref target);
            ((Eigenschaft)target).Auswirkungen = Auswirkungen;
            return target;
        }

        public override void Reset()
        {
            Auswirkungen = "";
            base.Reset();
        }

        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlattformHelper.GetString("Model_Eigenschaft_Auswirkungen/Text");
            strReturn += Delimiter;
            return strReturn;
        }
        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Auswirkungen;
            strReturn += Delimiter;
            return strReturn;
        }
        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlattformHelper.GetString("Model_Eigenschaft_Auswirkungen/Text"))
                {
                    Auswirkungen = (item.Value);
                    continue;
                }
            }
        }
    }
}
