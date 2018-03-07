
using ShadowRunHelper.Model;
using System.Collections.Generic;
using System.Linq;

namespace ShadowRunHelper.CharModel
{
    public class Attribut : Thing
    {
        [Used_List]
        public ObservableThingListEntryCollection Addidtions { get; set; }

        public static IEnumerable<ThingDefs> Filter = TypeHelper.ThingTypeProperties.Where(x => 
        x.ThingType != ThingDefs.Implantat ||
        x.ThingType != ThingDefs.Vorteil ||
        x.ThingType != ThingDefs.Nachteil
        ).Select(x => x.ThingType);

        private double _WertAfterCalc = 0;
        [Used_UserAttribute]
        public double WertAfterCalc
        {
            get { return _WertAfterCalc; }
            set
            {
                if (value != _WertAfterCalc)
                {
                    _WertAfterCalc = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Attribut() : base()
        {
            Addidtions = new ObservableThingListEntryCollection(Filter);
            Addidtions.OnCollectionChangedAndNow(() => { WertAfterCalc = Addidtions.Recalculate(); });
        }

        public override double GetPropertyValueOrDefault(string ID = "")
        {
            if (ID == "Wert")
            {
                return WertAfterCalc;
            }
            return base.GetPropertyValueOrDefault(ID);
        }


    }
}
