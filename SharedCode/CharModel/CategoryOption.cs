//Author: Tobi van Helsinki

using ShadowRunHelper.Helper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TLIB;

namespace ShadowRunHelper.CharModel
{
    public class CategoryOption : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PlatformHelper.CallPropertyChanged(PropertyChanged, this, propertyName);
        }
        #endregion NotifyPropertyChanged

        private ThingDefs _ThingType;
        public ThingDefs ThingType
        {
            get => _ThingType;
            set { if (_ThingType != value) { _ThingType = value; NotifyPropertyChanged(); } }
        }

        private bool _vis = true;
        public bool Visibility
        {
            get => _vis;
            set { if (_vis != value) { _vis = value; NotifyPropertyChanged(); } }
        }

        private int _pivot;
        public int Pivot
        {
            get => _pivot;
            set { if (_pivot != value) { _pivot = value; NotifyPropertyChanged(); } }
        }

        private int _Order;
        public int Order
        {
            get => _Order;
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
            ThingTypeProperty std = TypeHelper.ThingTypeProperties.First(x => x.ThingType == ThingType);
            return !Visibility || Order != std.Order || Pivot != std.Pivot;
        }

        public override string ToString()
        {
            return ThingType + "/" + Pivot + "/" + Order + "/" + (Visibility ? "" : "Not") + "Visible";
        }
    }

    public class CharSettings : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PlatformHelper.CallPropertyChanged(PropertyChanged, this, propertyName);
        }
        #endregion PropertyChanged

        public ObservableCollection<CategoryOption> CategoryOptions { get; set; } = new ObservableCollection<CategoryOption>();

        public void Refresh()
        {
            AddMissingCategories();
            RemoveUnwantedCategories();
            RemoveDoubleCategories();
            ResetOrdering();
            OrderList();
            foreach (CategoryOption item in CategoryOptions)
            {
                item.PropertyChanged += (o, e) => NotifyPropertyChanged(e.PropertyName);
            }
        }

        private void RemoveUnwantedCategories()
        {
            List<CategoryOption> list = CategoryOptions.Where(x => !TypeHelper.ThingTypeProperties.Find(y => y.ThingType == x.ThingType).Usable).ToList();
            if (list.Count > 0)
            {
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }
#endif
            }
            foreach (CategoryOption item in list)
            {
                CategoryOptions.Remove(item);
            }
        }

        private void RemoveDoubleCategories()
        {
            List<CategoryOption> ListToRemove = new List<CategoryOption>();
            IEnumerable<CategoryOption> ToRemoveCollection = CategoryOptions.Where(x => CategoryOptions.Count(y => y.ThingType == x.ThingType) > 1);
            foreach (CategoryOption item in CategoryOptions)
            {
                if (ListToRemove.Any(x => x.ThingType == item.ThingType))
                {
                    continue;
                }
                IEnumerable<CategoryOption> EntriesWithThatType = CategoryOptions.Where(x => item.ThingType == x.ThingType);
                if (EntriesWithThatType.Count() > 1)
                {
                    IOrderedEnumerable<CategoryOption> MaybeManipulatedO = EntriesWithThatType.OrderBy(x => x.MaybeUserManipulated());
                    foreach (CategoryOption ToRem in MaybeManipulatedO.Take(MaybeManipulatedO.Count() - 1))
                    {
                        ListToRemove.Add(ToRem);
                    }
                }
            }
            if (ListToRemove.Count > 0)
            {
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }
#endif
            }
            foreach (CategoryOption item in ListToRemove)
            {
                CategoryOptions.Remove(item);
            }
        }

        private void AddMissingCategories()
        {
            CategoryOptions.AddRange(TypeHelper.ThingTypeProperties.
                Where(s => s.Usable && !CategoryOptions.Any(t => t.ThingType == s.ThingType)).Select(x => new CategoryOption(x.ThingType, x.Pivot, x.Order)));
        }

        public void ResetOrdering()
        {
            foreach (CategoryOption item in CategoryOptions)
            {
                ThingTypeProperty n = TypeHelper.ThingTypeProperties.Find(x => x.ThingType == item.ThingType);
                item.Order = n.Order;
                item.Pivot = n.Pivot;
            }
        }

        public void OrderList()
        {
            List<CategoryOption> or = CategoryOptions.OrderBy(x => (x.Pivot, x.Order)).ToList();
            CategoryOptions.Clear();
            CategoryOptions.AddRange(or);
        }

        public void ResetCategoryOptions()
        {
            //CategoryOptions.Clear();
            IEnumerable<(CategoryOption isnow, ThingTypeProperty should)> JoinedList = CategoryOptions.Join(TypeHelper.ThingTypeProperties, x => x.ThingType, x => x.ThingType, (isnow, should) => (isnow, should));
            foreach ((CategoryOption isnow, ThingTypeProperty should) in JoinedList)
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