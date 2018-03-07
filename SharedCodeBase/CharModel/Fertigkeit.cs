using ShadowRunHelper.Model;
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Fertigkeit : Thing
    {
        List<ThingDefs> lstForbidden = new List<ThingDefs>() { ThingDefs.Handlung , ThingDefs.Fertigkeit };
        [Used_List]
        public ObservableThingListEntryCollection PoolZusammensetzung { get; set; }

        private double _Pool = 0;
        [Used_UserAttribute]
        public double Pool
        {
            get { return _Pool; }
            set
            {
                if (value != _Pool)
                {
                    _Pool = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Fertigkeit() : base()
        {
            PoolZusammensetzung = new ObservableThingListEntryCollection(lstForbidden);
            PoolZusammensetzung.CollectionChanged += (u, c) => { CollectionChanged(); };
            CollectionChanged();
            PropertyChanged += (x, y) => { if (y.PropertyName == "Wert") Recalculate(); };
        }


        public static IEnumerable<ThingDefs> Filter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Connection, ThingDefs.Fertigkeit
            };

        private void CollectionChanged()
        {
            NotifyPropertyChanged();
            Recalculate();
            foreach (var item in PoolZusammensetzung)
            {
                item.Object.PropertyChanged -= (u, c) => { Recalculate(); };
                item.Object.PropertyChanged += (u, c) => { Recalculate(); };
            }
        }

        private void Recalculate()
        {
            double temp = Wert;
            foreach (AllListEntry item in PoolZusammensetzung)
            {

                try
                {
                    temp += item.Object.GetPropertyValueOrDefault(item.PropertyID);
                }
                catch (System.Exception)
                {
                }
            }
            Pool = temp;
        }
    }
}
