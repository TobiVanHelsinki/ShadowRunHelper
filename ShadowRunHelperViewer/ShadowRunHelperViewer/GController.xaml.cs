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
            set { if (_Controller != value) { _Controller = value; NotifyPropertyChanged(); } }
        }
        private void OnControllerChanged()
        {
            if (Controller != null)
            {
                var Setting = AppModel.Instance.MainObject.Settings.CategoryOptions.FirstOrDefault(x=>x.ThingType == Controller.eDataTyp);
                if (Setting != null)
                {
                    IsVisible = Setting.Visibility;
                }
                Headline.Text = TypeHelper.ThingDefToString(Controller.eDataTyp, true);
                Headline.Text = "Kategorieüberschrift " + DateTime.Now;
            }
            else
            {

            }
        }

        public GController()
		{
			InitializeComponent ();
            BindingContextChanged += GController_BindingContextChanged;
        }

        private void GController_BindingContextChanged(object sender, EventArgs e)
        {
            Controller = BindingContext as IController;
            OnControllerChanged();
        }

    }
}