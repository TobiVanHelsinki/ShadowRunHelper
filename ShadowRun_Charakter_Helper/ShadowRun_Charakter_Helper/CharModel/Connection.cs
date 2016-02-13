using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Connection : Person
    {
        private double connect;
        public double Connecti
        {
            get { return connect; }
            set
            {
                if (value != this.connect)
                {
                    this.connect = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double loyal;
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
