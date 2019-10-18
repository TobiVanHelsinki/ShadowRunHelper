///Author: Tobi van Helsinki

using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Adeptenkraft : Eigenschaft
    {
        string _Option = "";
        [Used_UserAttribute] //TODO zusammenführen mit Auswirkungen
        public string Option
        {
            get { return _Option; }
            set
            {
                if (value != this._Option)
                {
                    this._Option = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public static new IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Fertigkeit
            };

        public Adeptenkraft() : base()
        {
            LinkedThings.FilterOut = Filter;
        }
    }
}