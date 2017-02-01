using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRunHelper.CharModel
{
    public class Panzerung : Item
    {
        private double stoß = 0;
        public double Stoß
        {
            get { return stoß; }
            set
            {
                if (value != this.stoß)
                {
                    this.stoß = value;
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

        public Panzerung()
        {
            this.ThingType = ThingDefs.Panzerung;

        }

        public new string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Stoß;
            strReturn += Delimiter;
            strReturn += Kapazität;
            strReturn += Delimiter;
            return strReturn;
        }

    }
}
