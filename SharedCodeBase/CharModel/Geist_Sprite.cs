

using System.Collections.Generic;
using TLIB_UWPFRAME;

namespace ShadowRunHelper.CharModel
{
    public class Geist_Sprite : Item
    {
        string _Dienste = "";
        [Used]
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
        [Used]
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
            ThingType = ThingDefs.Geist_Sprite;
        }
    }
}
