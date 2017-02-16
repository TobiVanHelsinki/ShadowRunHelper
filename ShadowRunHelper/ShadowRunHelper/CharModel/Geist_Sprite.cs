
namespace ShadowRunHelper.CharModel
{
    class Geist_Sprite : Thing
    {
        string _Dienste = "";
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
        
        public Geist_Sprite()
        {
            this.ThingType = ThingDefs.Geist_Sprite;
        }
    }
}
