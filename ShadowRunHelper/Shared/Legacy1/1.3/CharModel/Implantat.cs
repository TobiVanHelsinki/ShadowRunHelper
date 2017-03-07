using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRunHelper1_3.CharModel
{
    public class Implantat : Item
    {
        private double essenz = 0;
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

        private double kapazität = 0;
        public double Kapazität
        {
            get { return kapazität; }
            set
            {
                if (value != this.kapazität)
                {
                    this.kapazität = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Implantat()
        {
            
        }

    }
}
