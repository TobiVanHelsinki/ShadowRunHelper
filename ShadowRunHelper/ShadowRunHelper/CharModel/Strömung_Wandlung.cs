
namespace ShadowRunHelper.CharModel
{
    class Strömung_Wandlung : Thing
    {
        string _Strömung = "";
        public string Strömung
        {
            get { return _Strömung; }
            set
            {
                if (value != this._Strömung)
                {
                    this._Strömung = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string _Paragon = "";
        public string Paragon
        {
            get { return _Paragon; }
            set
            {
                if (value != this._Paragon)
                {
                    this._Paragon = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double _Wandlungsgrad = 0;
        public double Wandlungsgrad
        {
            get { return _Wandlungsgrad; }
            set
            {
                if (value != this._Wandlungsgrad)
                {
                    this._Wandlungsgrad = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string _Echos = "";
        public string Echos
        {
            get { return _Echos; }
            set
            {
                if (value != this._Echos)
                {
                    this._Echos = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public Strömung_Wandlung()
        {
            this.ThingType = ThingDefs.Strömung_Wandlung;
        }
    }
}
