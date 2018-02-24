using Newtonsoft.Json;
using ShadowRunHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using TLIB_UWPFRAME.Model;
using TLIB_UWPFRAME.Resources;
using TLIB_UWPFRAME.Settings;

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

        bool _vis;
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

        public void AddMissingCategories()
        {
            CategoryOptions.AddRange(TypeHelper.ThingTypeProperties.
                Where(s => !CategoryOptions.Any(t=>t.ThingType == s.ThingType)).Select(x=>new CategoryOption(x.ThingType, x.Pivot, x.Order)));
        }

        internal void ResetCategoryOptions()
        {
            throw new NotImplementedException();
        }
    }
}
