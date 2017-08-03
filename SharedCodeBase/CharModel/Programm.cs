using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLIB;

namespace ShadowRunHelper.CharModel
{
    public class Programm : Item
    {
        private string optionen = "";
        public string Optionen
        {
            get { return optionen; }
            set
            {
                if (value != this.optionen)
                {
                    this.optionen = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Programm()
        {
            this.ThingType = ThingDefs.Programm;

        }


        public override Thing Copy( Thing target = null)
        {
            if (target == null)
            {
                target = new Programm();
            }
            base.Copy( target);
            ((Programm)target).Optionen = Optionen;
            return target;
        }

        public override void Reset()
        {
            base.Reset();
            Optionen = "";
        }

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Optionen;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlatformHelper.GetString("Model_Programm_Optionen/Text");
            strReturn += Delimiter;
            return strReturn;
        }

        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlatformHelper.GetString("Model_Programm_Optionen/Text"))
                {
                    Optionen = item.Value;
                    continue;
                }
            }
        }
    }
}
