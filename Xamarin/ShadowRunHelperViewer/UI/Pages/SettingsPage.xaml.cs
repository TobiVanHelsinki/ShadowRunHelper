using ShadowRunHelper;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentView, INotifyPropertyChanged
	{
        #region NotifyPropertyChanged
		public new event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public SettingsModel Settings => SettingsModel.Instance;
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = this;
            Features.Ui.IsCustomTitleBarEnabled = false;
        }
    }
}