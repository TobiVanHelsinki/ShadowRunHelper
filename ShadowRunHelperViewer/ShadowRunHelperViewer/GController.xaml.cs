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
                Resources.TryGetValue(key, out object X);
                CurrentTemplate = X as DataTemplate;
                Resources.TryGetValue(key + "X", out X);
                CurrentTemplateX = X as DataTemplate;

                //ViewCell a = Items.TemplatedItems.OfType<ViewCell>().FirstOrDefault(x => x.BindingContext == e.SelectedItem);
                //Extend
                Items.ItemTemplate = CurrentTemplate ?? FallbackTemplate;

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

        DataTemplate CurrentTemplate;
        DataTemplate CurrentTemplateX;
        
        private readonly DataTemplate FallbackTemplate;
        public GController()
        {
            InitializeComponent();
            Resources.TryGetValue("Fallback", out object X);
            FallbackTemplate = X as DataTemplate;

            OnControllerChanged();
            BindingContextChanged += GController_BindingContextChanged;
        }
        private void Hide(object sender, EventArgs e)
        {
            IsVisible = false;
        }

        private void Items_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //ViewCell a = Items.TemplatedItems.OfType<ViewCell>().FirstOrDefault(x => x.BindingContext == e.SelectedItem);
            //if (a != null)
            {
                //Resources.TryGetValue("NahkampfwaffeX", out object cell);
                //object v = (cell as DataTemplate).CreateContent();
                //Items.TemplatedItems. = v as View;
                //a.View = v as View;
            }
        }
    }

}