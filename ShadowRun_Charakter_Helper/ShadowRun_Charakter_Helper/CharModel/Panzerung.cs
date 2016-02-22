using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Panzerung : CharModel.Item
    {
        private double ballistik { get; set; }
        public double Ballistik
        {
            get { return ballistik; }
            set
            {
                if (value != this.ballistik)
                {
                    this.ballistik = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double stoß;
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

        public Panzerung()
        {

        }
    }
}
