using Newtonsoft.Json;
using ShadowRunHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using TAPPLICATION.Model;
using TLIB;
using TAMARIN.Settings;

namespace ShadowRunHelper.CharModel
{
    public class CategoryOption : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ModelHelper.CallPropertyChangedAtDispatcher(PropertyChanged, this, propertyName);
        }
      

        ThingDefs _ThingType;
        public ThingDefs ThingType
        {
            get { return _ThingType; }
            set { _ThingType = value; NotifyPropertyChanged(); }
        }

        bool _vis = true;
        public bool Visibility
        {
            get { return _vis; }
            set { _vis = value; NotifyPropertyChanged(); }
        }
        int _pivot;
        public int Pivot
        {
            get { return _pivot; }
            set { _pivot = value; NotifyPropertyChanged(); }
        }

        int _Order;
        public int Order
        {
            get { return _Order; }
            set { _Order = value; NotifyPropertyChanged(); }
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
            ModelHelper.CallPropertyChangedAtDispatcher(PropertyChanged, this, propertyName);
        }
        #endregion

        ObservableCollection<CategoryOption> _categoryOptions = new ObservableCollection<CategoryOption>();
        public ObservableCollection<CategoryOption> CategoryOptions { get => _categoryOptions; set => _categoryOptions = value; }

        public void Refresh()
        {
            AddMissingCategories();
            RemoveUnwantedCategories();
            RemoveDoubleCategories();
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

        public void ResetCategoryOptions()
        {
            CategoryOptions.Clear();
            CategoryOptions.AddRange(TypeHelper.ThingTypeProperties.
                Where(s => s.Usable).Select(x => new CategoryOption(x.ThingType, x.Pivot, x.Order)));
        }
    }
}
