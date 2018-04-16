using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TLIB;
using TAMARIN;

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
            if (item?.Object != null && ForbiddenThingTypes != null 
            && !ForbiddenThingTypes.Contains(item.Object.ThingType)
            && !HasCircularReference(item.Object))
            {
                base.InsertItem(index, item);
            }
            else
            {
                AppModel.Instance.NewNotification(String.Format(StringHelper.GetString("Notification_Warning_NotAddLinkedEntry"),item.Object.Bezeichner));
            }
        }

        bool HasCircularReference(Thing newThing)
        {
            foreach (var item in newThing.LinkedThings)
            {
                if (item.Object == thisThing)
                {
                    return true;
                }
                //if (item.Object.GetHashCode() == thisThing.GetHashCode())
                //{
                //    return true;
                //}
                //if (thisThing.Equals(item.Object))
                //{
                //    return true;
                //}

                if (HasCircularReference(item.Object))
                {
                    return true;
                }
            }
            return false;
        }

        Action CurrentTODO;
        Thing thisThing;

        public ObservableThingListEntryCollection(Thing thing)
        {
            thisThing = thing;
        }

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
