namespace ShadowRunHelper.CharModel
{
    public class Zaubersprueche : Item
    {
        string reichweite = "";
        [Used_UserAttribute]
        public string Reichweite
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

        string _Dauer = "";
        [Used_UserAttribute]
        public string Dauer
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
        [Used_UserAttribute]
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
    }
}
