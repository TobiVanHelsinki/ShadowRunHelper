using ShadowRunHelper.Model;
using SharedCodeBase.Model;
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Fertigkeit : Thing
    {
        List<ThingDefs> lstForbidden = new List<ThingDefs>() { ThingDefs.Handlung , ThingDefs.Fertigkeit };
        public ObservableThingListEntryCollection PoolZusammensetzung;

        private double _Pool = 0;
        [Used]
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

        public Fertigkeit()
        {
            ThingType = ThingDefs.Fertigkeit;
            PoolZusammensetzung = new ObservableThingListEntryCollection(lstForbidden);
            PoolZusammensetzung.CollectionChanged += (u, c) => { CollectionChanged(); };
            CollectionChanged();
            PropertyChanged += (x, y) => { if (y.PropertyName == "Wert") Recalculate(); };
        }

        public override Thing Copy(Thing target)
        {
            Fertigkeit target2  = (Fertigkeit)base.Copy(target);
        
            foreach (AllListEntry item in PoolZusammensetzung)
            {
                target2.PoolZusammensetzung.Add(new AllListEntry() { Object = item.Object.Copy(), PropertyID = item.PropertyID, DisplayName = item.DisplayName });
            }
            return target2;
        }

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
                    temp += item.Object.GetValue(item.PropertyID);
                }
                catch (System.Exception)
                {
                }
            }
            Pool = temp;
        }
    }
}
