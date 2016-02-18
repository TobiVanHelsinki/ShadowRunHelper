﻿namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Fernkampfwaffe : CharModel.Waffe
    {
        public double rückstoß;
        public double Rückstoß
        {
            get { return rückstoß; }
            set
            {
                if (value != this.rückstoß)
                {
                    this.rückstoß = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double modi;
        public double Modus
        {
            get { return modi; }
            set
            {
                if (value != this.modi)
                {
                    this.modi = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Fernkampfwaffe()
        {
        }
    }
}
