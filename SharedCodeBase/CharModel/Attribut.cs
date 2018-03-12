
using ShadowRunHelper.Model;
using System.Collections.Generic;
using System.Linq;

namespace ShadowRunHelper.CharModel
{
    public class Attribut : Thing
    {
        ObservableThingListEntryCollection _Addidtions = new ObservableThingListEntryCollection();
        [Used_List]
        public ObservableThingListEntryCollection Addidtions
        {
            get { return _Addidtions; }
            set
            {
                if (_Addidtions != value && value != null)
                {
                    _Addidtions = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public static IEnumerable<ThingDefs> Filter = TypeHelper.ThingTypeProperties.Where(x => 
        x.ThingType != ThingDefs.Implantat &&
        x.ThingType != ThingDefs.Vorteil &&
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
            Addidtions.SetFilter(Filter);
            Addidtions.OnCollectionChangedCall(() => { WertAfterCalc = Wert + Addidtions.Recalculate(); });
            PropertyChanged += (s, e) => { if (e.PropertyName == "Wert") WertAfterCalc = Wert + Addidtions.Recalculate(); };

        }

        public override double ValueOf(string ID = "")
        {
            if (ID == "Wert" || ID == "")
            {
                return WertAfterCalc;
            }
            return base.ValueOf(ID);
        }


    }
}
