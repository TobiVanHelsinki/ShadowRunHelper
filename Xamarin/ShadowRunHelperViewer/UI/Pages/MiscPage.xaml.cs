using ShadowRunHelper;
using ShadowRunHelperViewer.Platform;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiscPage : ContentView, INotifyPropertyChanged
	{
        #region NotifyPropertyChanged
		public new event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public SettingsModel Settings => SettingsModel.Instance;
        public MiscPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        #region Design

        public void Activate()
        {
            Features.Ui.IsCustomTitleBarEnabled = true; //TODO Dispse?
            Features.Ui.SetCustomTitleBar(DependencyService.Get<IFormsInteractions>().GetRenderer(TitleBar));
            Features.Ui.CustomTitleBarChanges += CustomTitleBarChanges; //TODO Dispose
            Features.Ui.TriggerCustomTitleBarChanges();
        }

        private void CustomTitleBarChanges(double LeftSpace, double RigthSpace, double Heigth)
        {
            TitleBar.MinimumHeightRequest = Heigth;
            Intro1Text.Margin = new Thickness(Math.Abs(LeftSpace), 0, Math.Abs(RigthSpace), 0);
        }
        #endregion

    }
}