///Author: Tobi van Helsinki

using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Panzerung : Item
    {
        private ConnectProperty _Capacity;
        [Used_UserAttribute]
        public ConnectProperty Capacity
        {
            get => _Capacity;
            set
            {
                if (value != _Capacity)
                {
                    RefreshInnerPropertyChangedListener(ref _Capacity, value, this);
                    NotifyPropertyChanged();
                }
            }
        }

        public override IEnumerable<ThingDefs> Filter => StaticFilter;

        private static readonly IEnumerable<ThingDefs> StaticFilter = new[]
            {
                ThingDefs.Handlung, ThingDefs.Fertigkeit, ThingDefs.Connection, ThingDefs.Sin
            };
    }
}