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
        private double firewall = 0;
        [Used_UserAttribute]
        public double Firewall
        {
            get { return firewall; }
            set
            {
                if (value != this.firewall)
                {
                    this.firewall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double datenverarbeitung = 0;
        [Used_UserAttribute]
        public double Datenverarbeitung
        {
            get { return datenverarbeitung; }
            set
            {
                if (value != this.datenverarbeitung)
                {
                    this.datenverarbeitung = value;
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
        [Used_CalcAttribute]
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

        void RefreshSchadenLimit()
        {
            SchadenMax= 8 + Math.Ceiling(Wert / 2);
        }
    }
}