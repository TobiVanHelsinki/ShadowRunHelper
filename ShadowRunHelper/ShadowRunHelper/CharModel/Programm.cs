using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

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


        public Programm Copy(ref Programm target)
        {
            if (target == null)
            {
                target = new Programm();
            }
            base.Copy((Thing)target);
            target.Optionen = Optionen;
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
            var res = ResourceLoader.GetForCurrentView();
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += res.GetString("Model_Programm_Optionen/Text");
            strReturn += Delimiter;
            return strReturn;
        }
    }
}
