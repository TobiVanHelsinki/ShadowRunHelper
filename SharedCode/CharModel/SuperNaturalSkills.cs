namespace ShadowRunHelper.CharModel
{
    public class SuperNaturalSkills : Thing
    {
        string _Target;
        [Used_UserAttribute]
        public string Target
        {
            get { return _Target; }
            set { if (_Target != value) { _Target = value; NotifyPropertyChanged(); } }
        }
        string _Duration;
        [Used_UserAttribute]
        public string Duration
        {
            get { return _Duration; }
            set { if (_Duration != value) { _Duration = value; NotifyPropertyChanged(); } }
        }
        string _Fading;
        [Used_UserAttribute]
        public string Fading
        {
            get { return _Fading; }
            set { if (_Fading != value) { _Fading = value; NotifyPropertyChanged(); } }
        }

        string reichweite = "";
        [Used_UserAttribute] //TODO in Target überführen
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
        [Used_UserAttribute] //TODO in Duration überführen
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
        [Used_UserAttribute] //TODO in Fading überführen
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
