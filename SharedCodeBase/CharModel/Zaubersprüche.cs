namespace ShadowRunHelper.CharModel
{
    public class Zaubersprueche : Item
    {
        double reichweite = 0;
        [Used]
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
        [Used]
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
        [Used]
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

        public Zaubersprueche()
        {
            ThingType = ThingDefs.Zaubersprueche;
        }
    }
}
