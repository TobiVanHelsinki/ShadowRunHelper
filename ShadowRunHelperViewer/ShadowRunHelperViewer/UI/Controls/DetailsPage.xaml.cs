using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TLIB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ObservableCollection<MyClass> Source { get; set; } = new ObservableCollection<MyClass>();
        public Thing MyThing { get; set; }
        public bool Editable { get; set; }
        public DetailsPage(Thing thing, bool editable)
        {
            MyThing = thing;
            Editable = editable;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            try
            {
                BindingContext = this;
                Source.AddRange(CreateItems(MyThing));
            }
            catch (Exception ex)
            {
                TAPPLICATION.Debugging.TraceException(ex);
            }
            base.OnAppearing();
        }

        private static IEnumerable<MyClass> CreateItems(Thing MyType)
        {
            foreach (var item in Thing.GetProperties(MyType).Reverse())
            {
                yield return new MyClass
                {
                    Name = CustomManager.GetString("Model_" + item.DeclaringType.Name +  "_" + item.Name + "/Text") + ": ",
                    Value = item.GetValue(MyType)?.ToString()
                };
            };
        }
    }
    public class MyClass : INotifyPropertyChanged
	{
        #region NotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        string _Name;
        public string Name
        {
            get { return _Name; }
            set { if (_Name != value) { _Name = value; NotifyPropertyChanged(); } }
        }
        string _Value;
        public string Value
        {
            get { return _Value; }
            set { if (_Value != value) { _Value = value; NotifyPropertyChanged(); } }
        }
    }
}