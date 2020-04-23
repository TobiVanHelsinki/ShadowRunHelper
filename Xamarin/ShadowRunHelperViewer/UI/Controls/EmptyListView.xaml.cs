using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SharedCode.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.ControlsOther
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmptyListView : ContentView, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public new event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public System.Collections.IEnumerable ItemsSource
        {
            get => (System.Collections.IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(System.Collections.IEnumerable), typeof(EmptyListView), null, propertyChanged: ItemsSourceChanged);

        private string _TitleText = UiResources.EmptyHere;
        public string TitleText
        {
            get => _TitleText;
            set { if (_TitleText != value) { _TitleText = value; NotifyPropertyChanged(); } }
        }
        private string _BodyText = UiResources.CreateSomethingNew;
        public string BodyText
        {
            get => _BodyText;
            set { if (_BodyText != value) { _BodyText = value; NotifyPropertyChanged(); } }
        }
        private string _Icon = "\xf067";
        public string Icon
        {
            get => _Icon;
            set { if (_Icon != value) { _Icon = value; NotifyPropertyChanged(); } }
        }

        public event EventHandler Clicked;
        public EmptyListView()
        {
            InitializeComponent();
            MainContent.BindingContext = this;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Clicked?.Invoke(sender, e);
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EmptyListView elv)
            {
                if (oldValue is INotifyCollectionChanged collection)
                {
                    collection.CollectionChanged -= elv.Collection_CollectionChanged;
                }
                if (newValue is INotifyCollectionChanged collectionNew)
                {
                    collectionNew.CollectionChanged += elv.Collection_CollectionChanged;
                }
                elv.Collection_CollectionChanged(elv, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                IsVisible = ItemsSource?.GetEnumerator()?.MoveNext() == false;
            }
            catch (InvalidOperationException)
            {
                IsVisible = false;
            }
        }

        private void Button_SizeChanged(object sender, EventArgs e)
        {
            if (sender is View v)
            {
                v.HeightRequest = v.Width;
            }
        }
    }
}