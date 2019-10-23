///Author: Tobi van Helsinki

using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using TLIB;

namespace ShadowRunHelper.Model
{
    public class LinkList : ObservableCollection<AllListEntry>
    {
        public IEnumerable<ThingDefs> FilterOut { get; set; }

        //protected override void InsertItem(int index, AllListEntry item)
        //{
        //if (item?.Object != null && FilterOut != null
        //&& !FilterOut.Contains(item.Object.ThingType)
        //&& !HasCircularReference(item.Object))
        //{
        //    base.InsertItem(index, item);
        //}
        //else
        //{
        //    Log.Write(CustomManager.GetString("Notification_Warning_NotAddLinkedEntry"));
        //}
        //}

        private bool HasCircularReference(Thing newThing)
        {
            foreach (var item in newThing.LinkedThings)
            {
                if (item.Object == thisThing)
                {
                    return true;
                }
                if (HasCircularReference(item.Object))
                {
                    return true;
                }
            }
            return false;
        }

        Action CollectionChangeCallback;
        Thing thisThing;

        public LinkList(Thing thing)
        {
            thisThing = thing;
        }

        public void OnCollectionChangedCall(Action _CollectionChangeCallback)
        {
            //if (CollectionChangeCallback != null)
            //{
            //    foreach (var item in this)
            //    {
            //        item.Object.PropertyChanged -= CallCallback;
            //    }
            //}
            //CollectionChangeCallback = _CollectionChangeCallback;
            //if (CollectionChangeCallback != null)
            //{
            //    CollectionChanged += This_CollectionChanged;
            //    This_CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, this));
            //}
        }

        private void This_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //CollectionChangeCallback?.Invoke();
            //if (e.NewItems != null)
            //{
            //    foreach (var item in e.NewItems.OfType<AllListEntry>().ToList())
            //    {
            //        item.Object.PropertyChanged += CallCallback;
            //        item.Object.PropertyChanged += CheckForDeletion;
            //    }
            //}
            //if (e.OldItems != null)
            //{
            //    foreach (var item in e.OldItems.OfType<AllListEntry>().ToList())
            //    {
            //        item.Object.PropertyChanged -= CallCallback;
            //        item.Object.PropertyChanged -= CheckForDeletion;
            //    }
            //}
        }

        private void CallCallback(object sender, PropertyChangedEventArgs e) => CollectionChangeCallback?.Invoke();

        private void CheckForDeletion(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == Constants.THING_DELETED_TOKEN)
            //{
            //    var ToRemove = this.Where(x => x.Object == sender as Thing).ToList();
            //    foreach (var item in ToRemove)
            //    {
            //        Remove(item);
            //    }
            //}
        }

        public double Recalculate()
        {
            return -42;
            //return this.Aggregate(0.0, (accu, curr) => accu + curr.Object.ValueOf(curr.PropertyID));
        }
    }
}