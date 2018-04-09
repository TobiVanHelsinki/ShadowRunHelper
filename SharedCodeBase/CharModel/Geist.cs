namespace ShadowRunHelper.CharModel
{
    public class Geist : Item
    {
        string _Dienste = "";
        [Used_UserAttribute]
        public string Dienste
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
        bool? _Geb_Reg = false;
        [Used_UserAttribute]
        public bool? Geb_Reg
        {
            get { return _Geb_Reg; }
            set
            {
                if (value != this._Geb_Reg)
                {
                    this._Geb_Reg = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
    }
}
