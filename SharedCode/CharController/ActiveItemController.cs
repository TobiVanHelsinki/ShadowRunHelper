//Author: Tobi van Helsinki


using ShadowRunHelper.CharModel;
using SharedCode.Resources;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace ShadowRunHelper.CharController
{
    public class ActiveItemController<T> : Controller<T> where T : Item, new()
    {
        public T ActiveItem { get; set; }

        public ActiveItemController()
        {
            ActiveItem = new T
            {
                Bezeichner = ModelResources._Active
            };
            ActiveItem.PropertyChanged += RefreshOriginItem;
            Data.CollectionChanged += Data_CollectionChanged;
        }

        public override IEnumerable<Thing> GetAllData()
        {
            return new[] { ActiveItem }.Concat(base.GetAllData());
        }

        protected bool bIsRefreshInProgress = false;

        protected virtual void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshActiveItem(this, new PropertyChangedEventArgs(""));
            foreach (var item in Data)
            {
                item.PropertyChanged += RefreshActiveItem;
            }
        }

        /// <summary>
        /// RefreshOriginItem
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <exception cref="System.Reflection.TargetException">Ignore.</exception>
        /// <exception cref="System.MethodAccessException">Ignore.</exception>
        /// <exception cref="System.Reflection.TargetInvocationException">Ignore.</exception>
        protected virtual void RefreshOriginItem(object sender, PropertyChangedEventArgs e)
        {
            if (bIsRefreshInProgress)
            {
                return;
            }
            bIsRefreshInProgress = true;
            var originitem = Data.FirstOrDefault(x => x.Aktiv == true);
            if (originitem != null)
            {
                ActiveItem.Bezeichner = originitem?.Bezeichner;
                var propertyInfo = typeof(T).GetProperty(e.PropertyName);
                if (propertyInfo?.PropertyType?.IsValueType == true)
                {
                    propertyInfo.SetValue(originitem, propertyInfo.GetValue(ActiveItem));
                }
                else
                {
                    ActiveItem.TryCloneInto(originitem);
                }
            }
            else
            {
                ActiveItem.Reset();
            }
            ActiveItem.Bezeichner = ModelResources._Active;
            bIsRefreshInProgress = false;
        }

        /// <summary>
        /// sets a new active item from the list of all items.should occur, when user changes active item
        /// </summary>
        protected virtual void RefreshActiveItem(object sender, PropertyChangedEventArgs e)
        {
            if (bIsRefreshInProgress)
            {
                return;
            }
            bIsRefreshInProgress = true;

            var originitem = Data.FirstOrDefault(x => x.Aktiv == true);
            if (originitem != null)
            {
                originitem.TryCloneInto(ActiveItem);
            }
            else
            {
                ActiveItem.Reset();
            }
            ActiveItem.Bezeichner = ModelResources._Active;
            bIsRefreshInProgress = false;
        }
    }
}