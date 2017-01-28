﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper.CharModel
{
    public class Connection : Thing
    {
        //public override double GetValue(string ID = "")
        //{
        //    return Wert;
        //}
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
            this.ThingType = ThingDefs.Connection;
        }

        public Connection Copy(Connection target = null)
        {
            if (target == null)
            {
                target = new Connection();
            }
            base.Copy(target);
            target.Loyal = Loyal;
            target.Alias = Alias;
            return target;
        }

        public new void Reset()
        {
            Alias = "";
            Loyal = 0;
            base.Reset();
        }
    }
}
