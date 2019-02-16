using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TLIB;
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
            //TODO Not own class here; use Custom class AS Properties
            foreach (var item in Thing.GetProperties(MyType).Reverse())
            {
                yield return new MyClass(MyType, item);
            }
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
        Thing MyThing;
        PropertyInfo Prop;

        public MyClass(Thing myThing, PropertyInfo prop)
        {
            MyThing = myThing;
            Prop = prop;
            MyThing.PropertyChanged += MyThing_PropertyChanged;
        }

        private void MyThing_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Prop.Name)
            {
                NotifyPropertyChanged(nameof(Value));
            }
        }

        public string Name
        {
            get { return CustomManager.GetString("Model_" + Prop.DeclaringType.Name + "_" + Prop.Name + "/Text") + ": "; }
        }
        public dynamic Value
        {
            get { return Prop.GetValue(MyThing); }
            set { Prop.SetValue(MyThing, value); }
        }
    }
}
