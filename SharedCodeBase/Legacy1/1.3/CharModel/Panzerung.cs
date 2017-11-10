using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRunHelper1_3.CharModel
{
    public class Panzerung : CharModel.Item
    {
        private double stoss = 0;
        public double Stoss
        {
            get { return stoss; }
            set
            {
                if (value != this.stoss)
                {
                    this.stoss = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double kapazitaet = 0;
        public double Kapazitaet
        {
            get { return kapazitaet; }
            set
            {
                if (value != this.kapazitaet)
                {
                    this.kapazitaet = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Panzerung()
        {

        }


    }
}
