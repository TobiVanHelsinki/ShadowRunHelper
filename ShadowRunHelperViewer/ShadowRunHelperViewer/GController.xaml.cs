using System;
using System.Linq;
using ShadowRunHelper;
using ShadowRunHelper.CharController;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ShadowRunHelper.Model;

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
        IController _Controller;
        public IController Controller
        {
            get { return _Controller; }
            set { if (_Controller != value) { _Controller = value; NotifyPropertyChanged(); OnControllerChanged(); } }
        }
        private void OnControllerChanged()
        {
            if (Controller != null)
            {
                var Setting = AppModel.Instance.MainObject.Settings.CategoryOptions.FirstOrDefault(x=>x.ThingType == Controller.eDataTyp);
                IsVisible = Setting != null ? Setting.Visibility : true;
                Headline.Text = TypeHelper.ThingDefToString(Controller.eDataTyp, true);
                Headline.Text = "Kategorieüberschrift " + DateTime.Now;
            }
            else
            {
                IsVisible = false;
            }
        }

        public bool Expand
        {
            get { return (bool)GetValue(ExpandProperty); }
            set { SetValue(ExpandProperty, value); Items.IsVisible = value; }
        }
        public static readonly BindableProperty ExpandProperty =
            BindableProperty.Create(nameof(Expand), typeof(bool), typeof(GController), true, propertyChanged: UpdateVis);

        private static void UpdateVis(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is GController GC && newValue is bool b)
            {
                GC.Expand = b;
            }
        }

        public GController()
		{
			InitializeComponent();
            OnControllerChanged();
            BindingContextChanged += GController_BindingContextChanged;
        }

        private void GController_BindingContextChanged(object sender, EventArgs e)
        {
            Controller = BindingContext as IController;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Items.IsVisible = !Items.IsVisible;
        }
    }
}