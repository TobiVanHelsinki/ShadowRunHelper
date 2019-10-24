///Author: Tobi van Helsinki

using System;
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Adeptenkraft : Eigenschaft
    {
        string _Option = "";
        [Obsolete(Constants.ObsoleteCalcProperty)]
        public string Option
        {
            get { return _Option; }
            set
            {
                if (value != this._Option)
                {
                    _Option = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}