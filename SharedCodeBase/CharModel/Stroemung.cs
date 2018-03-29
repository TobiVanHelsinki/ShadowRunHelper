namespace ShadowRunHelper.CharModel
{
    public class Stroemung : Thing
    {
        // Bezeichner ist Stroemung
        // Wert ist Wandlungsgrad
        string _Paragon = "";
        [Used_UserAttribute]
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
       
        string _Echos = "";
        [Used_UserAttribute]
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
    }
}
