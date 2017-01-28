﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper.CharModel
{
    public class Connection : Thing
    {
        private string alias = "";
        public string Alias
        {
            get { return alias; }
            set
            {
                if (value != this.alias)
                {
                    this.alias = value;
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
