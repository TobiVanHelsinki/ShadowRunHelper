using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public new void Reset()
        {
            base.Reset();
            Optionen = "";
        }

        public new string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Optionen;
            strReturn += Delimiter;
            return strReturn;
        }
    }
}
