
using System;

namespace ShadowRunHelper.CharModel
{
    public class Kommlink : Item
    {
        private double programmanzahl = 0;
        [Used_UserAttribute]
        public double Programmanzahl
        {
            get { return programmanzahl; }
            set
            {
                if (value != this.programmanzahl)
                {
                    this.programmanzahl = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ConnectProperty firewall;
        [Used_UserAttribute]
        public ConnectProperty Firewall
        {
            get { return firewall; }
            set
            {
                if (value != this.firewall)
                {
                    RefreshInnerPropertyChangedListener(ref firewall, value, this);
                    NotifyPropertyChanged();
                }
            }
        }

        private ConnectProperty datenverarbeitung;
        [Used_UserAttribute]
        public ConnectProperty Datenverarbeitung
        {
            get { return datenverarbeitung; }
            set
            {
                if (value != this.datenverarbeitung)
                {
                    RefreshInnerPropertyChangedListener(ref datenverarbeitung, value, this);
                    NotifyPropertyChanged();
                }
            }
        }

        private double _dSchaden = 0;
        [Used_UserAttribute]
        public double Schaden
        {
            get { return _dSchaden; }
            set
            {
                if (value != this._dSchaden)
                {
                    this._dSchaden = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double _dSchadenMax = 0;
        [Used_UserAttribute]
        public double SchadenMax
        {
            get { return _dSchadenMax; }
            set
            {
                if (value != this._dSchadenMax)
                {
                    this._dSchadenMax = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Kommlink() : base()
        {
            PropertyChanged += (x, y) => RefreshSchadenLimit();
        }

        private void RefreshSchadenLimit()
        {
            SchadenMax = 8 + Math.Ceiling(Wert / 2);
        }
    }
}