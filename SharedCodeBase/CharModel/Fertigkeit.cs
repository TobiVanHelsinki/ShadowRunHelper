using ShadowRunHelper.Model;
using SharedCodeBase.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Core;
using static ShadowRunHelper.CharModel.Handlung;

namespace ShadowRunHelper.CharModel
{
    public class Fertigkeit : Thing
    {
        List<ThingDefs> lstForbidden = new List<ThingDefs>() { ThingDefs.Handlung , ThingDefs.Fertigkeit };
        public ObservableThingListEntryCollection PoolZusammensetzung;

        private double _Pool = 0;
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

        public override Thing Copy(Thing target = null)
        {
            if (target == null)
            {
                target = new Fertigkeit();
            }
            base.Copy(target);
            Fertigkeit target2 = target as Fertigkeit;
        
            foreach (AllListEntry item in PoolZusammensetzung)
            {
                target2.PoolZusammensetzung.Add(new AllListEntry() { Object = item.Object.Copy(), PropertyID = item.PropertyID, DisplayName = item.DisplayName });
            }
            return target;
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
