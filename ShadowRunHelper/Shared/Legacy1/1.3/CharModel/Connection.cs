using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper1_3.CharModel
{
    public class Connection : Person
    {
        private double wert = 0;
        public double Wert
        {
            get { return wert; }
            set
            {
                if (value != this.wert)
                {
                    this.wert = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double loyal = 0;
        public double Loyal
        {
            get { return loyal; }
            set
            {
                if (value != this.loyal)
                {
                    this.loyal = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Connection()
        {

        }
    }
}
