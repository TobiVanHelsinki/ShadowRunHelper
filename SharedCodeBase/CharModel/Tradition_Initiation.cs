
namespace ShadowRunHelper.CharModel
{
    public class Tradition : Thing
    {
        //Bezeichner ist Tradition
        //initiationsgrad ist Wert
        string _Schutzpatron = "";
        [Used_UserAttribute]
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
   
        string _Metamagie = "";
        [Used_UserAttribute]
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
    }
}
