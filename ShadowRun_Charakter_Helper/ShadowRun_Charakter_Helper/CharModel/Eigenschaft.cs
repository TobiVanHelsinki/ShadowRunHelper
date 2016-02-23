using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Eigenschaft : Model
    {
        private double auswirkungen;
        public double Auswirkungen
        {
            get { return auswirkungen; }
            set
            {
                if (value != this.auswirkungen)
                {
                    this.auswirkungen = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Eigenschaft()
        {
            
        }

    }
}
