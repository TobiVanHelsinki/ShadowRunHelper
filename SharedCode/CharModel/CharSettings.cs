using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TLIB;
using TLIB.PlatformHelper;

namespace ShadowRunHelper.CharModel
{
    public class CategoryOption : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ModelHelper.CallPropertyChanged(PropertyChanged, this, propertyName);
        }
        #endregion

        ThingDefs _ThingType;
        public ThingDefs ThingType
        {
            get { return _ThingType; }
            set { if (_ThingType != value) {  _ThingType = value; NotifyPropertyChanged(); } }
        }

        bool _vis = true;
        public bool Visibility
        {
            get { return _vis; }
            set { if (_vis != value) { _vis = value; NotifyPropertyChanged(); } }
        }
        int _pivot;
        public int Pivot
        {
            get { return _pivot; }
            set { if (_pivot != value) { _pivot = value; NotifyPropertyChanged(); } }
        }

        int _Order;
        public int Order
        {
            get { return _Order; }
            set { if (_Order != value) { _Order = value; NotifyPropertyChanged(); } }
        }
        public CategoryOption(ThingDefs item, int v1, int v2, bool v = true)
        {
            ThingType = item;
            Pivot = v1;
            Order = v2;
            Visibility = v;
        }
        public bool MaybeUserManipulated()
        {
            var std = TypeHelper.ThingTypeProperties.First(x => x.ThingType == ThingType);
            return !Visibility || Order != std.Order || Pivot != std.Pivot;
        }

    }

    public class CharSettings : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ModelHelper.CallPropertyChanged(PropertyChanged, this, propertyName);
        }
        #endregion

        public ObservableCollection<CategoryOption> CategoryOptions { get; set; } = new ObservableCollection<CategoryOption>();

        public void Refresh()
        {
            AddMissingCategories();
            RemoveUnwantedCategories();
            RemoveDoubleCategories();
            ResetOrdering();
            OrderList();
            foreach (var item in CategoryOptions)
            {
                item.PropertyChanged += (o,e) => NotifyPropertyChanged(e.PropertyName);
            }
        }

        private void RemoveUnwantedCategories()
        {
            var list = CategoryOptions.Where(x => !TypeHelper.ThingTypeProperties.Find(y => y.ThingType == x.ThingType).Usable).ToList();
            if (list.Count > 0)
            {
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();
#endif
            }
            foreach (var item in list)
            {
                CategoryOptions.Remove(item);
            }
        }

        private void RemoveDoubleCategories()
        {
            var ListToRemove = new List<CategoryOption>();
            var ToRemoveCollection = CategoryOptions.Where(x=> CategoryOptions.Count(y=>y.ThingType == x.ThingType) > 1);
            foreach (var item in CategoryOptions)
            {
                if (ListToRemove.Any(x=>x.ThingType == item.ThingType))
                {
                    continue;
                }
                var EntriesWithThatType = CategoryOptions.Where(x => item.ThingType == x.ThingType);
                if (EntriesWithThatType.Count() > 1)
                {
                    var MaybeManipulatedO = EntriesWithThatType.OrderBy(x=>x.MaybeUserManipulated());
                    foreach (var ToRem in MaybeManipulatedO.Take(MaybeManipulatedO.Count()-1))
                    {
                        ListToRemove.Add(ToRem);
                    }
                }
            }
            if (ListToRemove.Count > 0)
            {
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();
#endif
            }
            foreach (var item in ListToRemove)
            {
                CategoryOptions.Remove(item);
            }
        }
        private void AddMissingCategories()
        {
            CategoryOptions.AddRange(TypeHelper.ThingTypeProperties.
                Where(s => s.Usable && !CategoryOptions.Any(t=>t.ThingType == s.ThingType)).Select(x=>new CategoryOption(x.ThingType, x.Pivot, x.Order)));
        }
        public void ResetOrdering()
        {
            foreach (var item in CategoryOptions)
            {
                var n = TypeHelper.ThingTypeProperties.Find(x=> x.ThingType == item.ThingType);
                item.Order = n.Order;
                item.Pivot = n.Pivot;
            }
        }
        public void OrderList()
        {
            var or = CategoryOptions.OrderBy(x => (x.Pivot, x.Order)).ToList();
            CategoryOptions.Clear();
            CategoryOptions.AddRange(or);
        }
        public void ResetCategoryOptions()
        {
            //CategoryOptions.Clear();
            var JoinedList = CategoryOptions.Join(TypeHelper.ThingTypeProperties, x => x.ThingType, x => x.ThingType, (isnow, should) => (isnow, should));
            foreach (var (isnow, should) in JoinedList)
            {
                isnow.Pivot = should.Pivot;
                isnow.Order = should.Order;
                isnow.Visibility = should.Visibility;
            }
            //CategoryOptions.AddRange(TypeHelper.ThingTypeProperties.
            //    Where(s => s.Usable).Select(x => new CategoryOption(x.ThingType, x.Pivot, x.Order)));
        }
    }
}
