//Author: Tobi van Helsinki

///Author: Tobi van Helsinki

using Newtonsoft.Json;
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public abstract class Waffe : Item
    {
        string _SchadenTyp = "";
        [Used_UserAttribute]
        public string SchadenTyp
        {
            get { return _SchadenTyp; }
            set
            {
                if (value != _SchadenTyp)
                {
                    _SchadenTyp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        ConnectProperty _DK;
        [Used_User]
        public ConnectProperty DK
        {
            get { return _DK; }
            set
            {
                if (_DK != value)
                {
                    RefreshInnerPropertyChangedListener(ref _DK, value, this);

                    NotifyPropertyChanged();
                }
            }
        }

        ConnectProperty _Precision;
        [Used_User]
        public ConnectProperty Precision
        {
            get => _Precision;
            set
            {
                RefreshInnerPropertyChangedListener(ref _Precision, value, this);
                NotifyPropertyChanged();
            }
        }

        [JsonIgnore]
        public override IEnumerable<ThingDefs> Filter => StaticFilter;

        static readonly IEnumerable<ThingDefs> StaticFilter = new[]
            {
                ThingDefs.Handlung, ThingDefs.Fertigkeit, ThingDefs.Connection, ThingDefs.Sin
            };
    }
}