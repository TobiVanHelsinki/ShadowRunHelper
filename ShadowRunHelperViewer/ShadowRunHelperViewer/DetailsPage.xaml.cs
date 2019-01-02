using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
        public DetailsPage(Thing thing)
        {
            MyThing = thing;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            try
            {
                //string key = TypeHelper.ThingDefToString(MyThing.ThingType, false);
                //Resources.TryGetValue(key, out object CustomTemplate);
                //if (CustomTemplate == null)
                //{
                BindingContext = this;
                Source.AddRange(CreatePanels(MyThing));
                //MainContent.IsVisible = false;
                //Grid.SetColumnSpan(V, 4);
                //Grid.SetRow(V, 2);
                //if (Content is Grid G)
                //{
                //    G.Children.Add(V);
                //}
                //}
                //else
                //{
                //    MainContent.ControlTemplate = CustomTemplate as ControlTemplate;
                //}
            }
            catch (Exception ex)
            {
                TAPPLICATION.Debugging.TraceException(ex);
            }
            base.OnAppearing();
        }

        private static IEnumerable<MyClass> CreatePanels(Thing MyType)
        {
            //var ret = new Grid();
            var enumerable = Thing.GetProperties(MyType);
            //ret.SetValue(Grid.RowProperty, enumerable.Count());
            //ret.SetValue(Grid.ColumnProperty, 2);
            int rowcounter = 0;
            foreach (var item in enumerable.Reverse())
            {
                //if (item.Name == nameof(Thing.Notiz))
                //{
                //    continue;
                //}
                var panel = new MyClass
                {
                    Name = item.Name,
                    Value = item.GetValue(MyType)?.ToString()
                };
                //var label = new Label
                //{
                //    Text = item.Name
                //};
                //var content = new Label
                //{
                //    Text = item.GetValue(MyType)?.ToString()
                //};
                //Grid.SetRow(label, rowcounter);
                //Grid.SetColumn(content, 1);
                //Grid.SetRow(content, rowcounter);
                //panel.Children.Add(label);
                //panel.Children.Add(content);
                rowcounter++;
                yield return panel;
            };
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
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