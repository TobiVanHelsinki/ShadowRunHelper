
namespace ShadowRunHelper.CharModel
{
    class Tradition_Initiation : Thing
    {
        string _Tradition = "";
        public string Tradition
        {
            get { return _Tradition; }
            set
            {
                if (value != this._Tradition)
                {
                    this._Tradition = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string _Schutzpatron = "";
        public string Schutzpatron
        {
            get { return _Schutzpatron; }
            set
            {
                if (value != this._Schutzpatron)
                {
                    this._Schutzpatron = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double _initiationsgrad = 0;
        public double initiationsgrad
        {
            get { return _initiationsgrad; }
            set
            {
                if (value != this._initiationsgrad)
                {
                    this._initiationsgrad = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string _Metamagie = "";
        public string Metamagie
        {
            get { return _Metamagie; }
            set
            {
                if (value != this._Metamagie)
                {
                    this._Metamagie = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Tradition_Initiation()
        {
            this.ThingType = ThingDefs.Tradition_Initiation;
        }
    }
}
