using ShadowRunHelper.Model;
using System.Collections.ObjectModel;
using static ShadowRunHelper.CharModel.Handlung;

namespace ShadowRunHelper.CharModel
{
    public class Fertigkeit : Thing
    {
        public ObservableCollection<ThingListEntry> PoolZusammensetzung;

        private double _Pool = 0;
        public double Pool
        {
            get { return _Pool; }
            set
            {
                if (value != this._Pool)
                {
                    this._Pool = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Fertigkeit()
        {
            this.ThingType = ThingDefs.Fertigkeit;
            PoolZusammensetzung = new ObservableCollection<ThingListEntry>();
            PoolZusammensetzung.CollectionChanged += (u, c) => { CollectionChanged(); };
            CollectionChanged();
        }

        private void CollectionChanged()
        {
            Recalculate();
            foreach (var item in PoolZusammensetzung)
            {
                item.Object.PropertyChanged -= (u, c) => { Recalculate(); };
                item.Object.PropertyChanged += (u, c) => { Recalculate(); };
            }
        }

        private void Recalculate()
        {
            double temp = 0;
            foreach (ThingListEntry item in PoolZusammensetzung)
            {
                temp += item.Object.GetValue(item.strProperty);
            }
            Pool = temp;
        }
    }
}
