using Rg.Plugins.Popup.Services;
using ShadowRunHelper;
using ShadowRunHelper.CharController;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GController : ContentView, INotifyPropertyChanged
	{
        #region NotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region Hold IController
        IController _Controller;
        public IController Controller
        {
            get { return _Controller; }
            set { if (_Controller != value) { _Controller = value; NotifyPropertyChanged(); OnControllerChanged(); } }
        }
        private void GController_BindingContextChanged(object sender, EventArgs e)
        {
            Controller = BindingContext as IController;
        }

        private void OnControllerChanged()
        {
            if (Controller != null)
            {
                string key = TypeHelper.ThingDefToString(Controller.eDataTyp, false);
                Resources.TryGetValue(key, out object CustomTemplate);
                if (CustomTemplate is DataTemplate DT)
                {
                    Items.ItemTemplate = DT;
                }

                Resources.TryGetValue(key+"_H", out CustomTemplate);
                if (CustomTemplate is ControlTemplate HL)
                {
                    Items_H.ControlTemplate = HL;
                }

                Setting = AppModel.Instance.MainObject.Settings.CategoryOptions.FirstOrDefault(x => x.ThingType == Controller.eDataTyp);
                IsVisible = Setting.Visibility && IsVisible;
                Headline.Text = TypeHelper.ThingDefToString(Controller.eDataTyp, true);
            }
            else
            {
                IsVisible = false;
            }
        }
        #endregion
        #region Expand Listview
        public bool Expand
        {
            get { return (bool)GetValue(ExpandProperty); }
            set { SetValue(ExpandProperty, value); Items.IsVisible = value; }
        }


        public static readonly BindableProperty ExpandProperty =
            BindableProperty.Create(nameof(Expand), typeof(bool), typeof(GController), true, propertyChanged: UpdateListVis);

        private static void UpdateListVis(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is GController GC && newValue is bool b)
            {
                GC.Expand = b;
            }
        }

        #endregion
        public CategoryOption Setting { get; private set; }

        public GController()
        {
            InitializeComponent();
            OnControllerChanged();
            BindingContextChanged += GController_BindingContextChanged;
        }

        private void Hide(object sender, EventArgs e)
        {
            IsVisible = false;
        }

        async void Items_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Thing t)
            {
                var a = Items.TemplatedItems[0].BindingContext;
                await PopupNavigation.Instance.PushAsync(new DetailsPage(t, false));
                Items.SelectedItem = null;
            }
        }
    }

}