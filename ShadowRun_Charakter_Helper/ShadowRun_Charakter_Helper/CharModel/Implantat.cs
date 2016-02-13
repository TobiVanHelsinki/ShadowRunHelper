using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Implantat : Item
    {
        public double essenz;
        public double Essenz
        {
            get { return essenz; }
            set
            {
                if (value != this.essenz)
                {
                    this.essenz = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Implantat(int dicCD_ID)
        {
            this.DicCD_ID = dicCD_ID;
        }
        public Implantat()
        {
            
        }

    }
}
