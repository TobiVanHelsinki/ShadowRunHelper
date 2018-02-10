using System.Collections.Generic;
using TLIB_UWPFRAME;

namespace ShadowRunHelper.CharModel
{
    public class Implantat : Item
    {
        private double essenz = 0;
        [Used]
        public double Essenz
        {
            get { return essenz; }
            set
            {
                if (value != this.essenz)
                {
                    this.essenz = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double kapazitaet = 0;
        [Used]
        public double Kapazitaet
        {
            get { return kapazitaet; }
            set
            {
                if (value != this.kapazitaet)
                {
                    this.kapazitaet = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _Auswirkung = "";
        [Used]
        public string Auswirkung
        {
            get { return _Auswirkung; }
            set
            {
                if (value != this._Auswirkung)
                {
                    this._Auswirkung = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Implantat()
        {
            ThingType = ThingDefs.Implantat;
        }
    }
}
