//Author: Tobi van Helsinki

namespace ShadowRunHelper.CharModel
{
    public class Helper : Thing
    {
        string _Dienste = "";
        [Used_UserAttribute]
        public string Dienste //TODO kann eigentlich zu anzahl umfunktioniert werden
        {
            get { return _Dienste; }
            set
            {
                if (value != this._Dienste)
                {
                    this._Dienste = value;
                    NotifyPropertyChanged();
                }
            }
        }

        bool? Registriert = false;
        [Used_UserAttribute]
        public bool? Geb_Reg
        {
            get { return Registriert; }
            set
            {
                if (value != this.Registriert)
                {
                    this.Registriert = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}