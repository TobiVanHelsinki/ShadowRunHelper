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

        public void SetFilter(IEnumerable<ThingDefs> ForbiddenThingTypes)
        {
            this.ForbiddenThingTypes = ForbiddenThingTypes;
        }

        IEnumerable<ThingDefs> ForbiddenThingTypes;
        protected override void InsertItem(int index, AllListEntry item)
        {
            if (ForbiddenThingTypes.Contains(item.Object.ThingType))
            {
                return;
            }
            base.InsertItem(index, item);
        }
        Action CurrentTODO;
        public void OnCollectionChangedCall(Action TODO)
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
               DoAction();
            }
        }
        void DoAction()
        {
            CurrentTODO();
            foreach (var item in this)
            {
                item.Object.PropertyChanged -= (u, c) => CurrentTODO();
                item.Object.PropertyChanged += (u, c) => CurrentTODO();
                item.Object.PropertyChanged -= (u, c) => CheckForDeletion(c.PropertyName, item);
                item.Object.PropertyChanged += (u, c) => CheckForDeletion(c.PropertyName, item);
            }
        }
        public void CheckForDeletion(string PropertyName, AllListEntry item)
        {
            if (PropertyName == Constants.THING_DELETED_TOKEN) Remove(item);
        }

        public double Recalculate()
        {
            return this.Aggregate<AllListEntry, double>(0, (accvalue, next) => accvalue + next.Object.ValueOf(next.PropertyID));
        }

    }
}
