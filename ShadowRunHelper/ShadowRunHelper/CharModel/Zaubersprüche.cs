using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharModel
{
    public class Zaubersprüche : Thing
    {
        double reichweite = 0;
        public double Reichweite
        {
            get { return reichweite; }
            set
            {
                if (value != this.reichweite)
                {
                    this.reichweite = value;
                    NotifyPropertyChanged();
                }
            }
        }

        double _Dauer = 0;
        public double Dauer
        {
            get { return _Dauer; }
            set
            {
                if (value != this._Dauer)
                {
                    this._Dauer = value;
                    NotifyPropertyChanged();
                }
            }
        }

        double _Entzug = 0;
        public double Entzug
        {
            get { return _Entzug; }
            set
            {
                if (value != this._Entzug)
                {
                    this._Entzug = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Zaubersprüche()
        {
            this.ThingType = ThingDefs.Zaubersprüche;
        }

    }
}
