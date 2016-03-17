﻿namespace ShadowRunHelper.CharModel
{
    public class Fernkampfwaffe : CharModel.Waffe
    {
        private double rückstoß = 0;
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
        private string modi = "";
        public string Modi
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