
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class KomplexeForm : Thing
    {
        string _Option = "";
        [Used_UserAttribute]
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
        public static IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Fertigkeit
            };

        public KomplexeForm() : base()
        {
            LinkedThings.FilterOut = Filter;
        }
    }
}
