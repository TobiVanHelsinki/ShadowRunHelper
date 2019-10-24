///Author: Tobi van Helsinki

using ShadowRunHelper.CharModel;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace ShadowRunHelper.CharController
{
    public class ActiveItemController<T> : Controller<T> where T : Item, new()
    {
        public T ActiveItem { get; }

        public ActiveItemController()
        {
            ActiveItem = new T();
            ActiveItem.PropertyChanged += RefreshOriginItem;
            Data.CollectionChanged += Data_CollectionChanged;
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
            var item = Data.FirstOrDefault(x => x.Aktiv == true);
            var propertyInfo = typeof(T).GetProperty(e.PropertyName);
            if (propertyInfo.PropertyType.IsValueType)
            {
                propertyInfo.SetValue(item, propertyInfo.GetValue(ActiveItem));
            }
            bIsRefreshInProgress = false;
        }

        /// <summary>
        /// sets a new active item from the list of all items.should occur, when user changes active item
        /// </summary>
        protected virtual void RefreshActiveItem(object sender, PropertyChangedEventArgs e)
        {
            // aber ich könnte auch einfach statt active deck immer ein anderes einsetzen. dann müssten sich aber die registriere immer neu registrieren ...
            // außer, ich schaffe es, nur den registrierten besheid zu geben, sie sollen sich auf ein neues ziel registrieren!
            if (bIsRefreshInProgress)
            {
                return;
            }
            bIsRefreshInProgress = true;

            var item = Data.FirstOrDefault(x => x.Aktiv == true);
            if (item != null)
            {
                item.TryCloneInto(ActiveItem);
            }
            else
            {
                ActiveItem.Reset();
            }
            bIsRefreshInProgress = false;
        }
    }
}