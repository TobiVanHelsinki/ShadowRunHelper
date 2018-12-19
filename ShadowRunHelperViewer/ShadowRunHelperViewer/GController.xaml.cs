using System;
using ShadowRunHelper;
using ShadowRunHelper.CharController;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
                Headline.Text = TypeHelper.ThingDefToString(Controller.eDataTyp, true);
                Headline.Text = "temp test cat " + DateTime.Now;
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