using ShadowRunHelper;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ShadowRunHelper.Model
{
    public class ObservableThingListEntryCollection : ObservableCollection<AllListEntry>
    {
        public ObservableThingListEntryCollection(List<ThingDefs> ForbiddenThingTypes) : base()
        {
            this.ForbiddenThingTypes = ForbiddenThingTypes;
        }
        public List<ThingDefs> ForbiddenThingTypes = new List<ThingDefs>();
        protected override void InsertItem(int index, AllListEntry item)
        {
            if (ForbiddenThingTypes.Contains(item.Object.ThingType))
            {
                return;
            }
            base.InsertItem(index, item);
        }
        Action CurrentTODO;
        public void OnCollectionChanged(Action TODO)
        {
            if (CurrentTODO != null)
            {
                foreach (var item in this)
                {
                    item.Object.PropertyChanged -= (u, c) => CurrentTODO();
                }
            }
            CurrentTODO = TODO;
            if (CurrentTODO != null)
            {
                CollectionChanged += (x, y) => DoAction();
            }
        }
        public void DoAction()
        {
            if (CurrentTODO != null)
            {
                CurrentTODO();
                foreach (var item in this)
                {
                    item.Object.PropertyChanged -= (u, c) => CurrentTODO();
                    item.Object.PropertyChanged += (u, c) => CurrentTODO();
                }
            }

        }

        public void OnCollectionChangedAndNow(Action TODO)
        {
            OnCollectionChanged(TODO);
            DoAction();
        }
        public double Recalculate()
        {
            return this.Aggregate<AllListEntry, double>(0, (accvalue, next) => accvalue + next.Object.GetPropertyValueOrDefault(next.PropertyID));
        }

    }
}
