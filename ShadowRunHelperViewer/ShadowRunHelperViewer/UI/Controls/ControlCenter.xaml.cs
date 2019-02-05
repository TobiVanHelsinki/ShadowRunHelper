using Rg.Plugins.Popup.Pages;
using ShadowRunHelper.Model;
using System;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlCenter : PopupPage,  INotifyPropertyChanged
	{
        #region NotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public CharHolder Model { get; set; }
        public ControlCenter(CharHolder Char)
        {
            Model = Char;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            try
            {
                BindingContext = this;
            }
            catch (Exception ex)
            {
                TAPPLICATION.Debugging.TraceException(ex);
            }
            base.OnAppearing();
        }

        int _MoneyIO;
        public int MoneyIO
        {
            get { return _MoneyIO; }
            set { if (_MoneyIO != value) { _MoneyIO = value; NotifyPropertyChanged(); } }
        }

        private void MoneyIOMinus(object sender, EventArgs e)
        {
            Model.Person.Kontostand -= MoneyIO;
            MoneyIO = 0;
        }

        private void MoneyIOPlus(object sender, EventArgs e)
        {
            Model.Person.Kontostand += MoneyIO;
            MoneyIO = 0;
        }
        private void EdgePlus(object sender, EventArgs e)
        {
            Model.Person.Edge_Aktuell++;
        }
        private void EdgeMinus(object sender, EventArgs e)
        {
            Model.Person.Edge_Aktuell--;
        }

        private void EdgeGesamtPlus(object sender, EventArgs e)
        {
            Model.Person.Edge_Gesamt++;
        }

        private void EdgeGesamtMinus(object sender, EventArgs e)
        {
            Model.Person.Edge_Gesamt--;
        }

        private void KarmaGesamtMinus(object sender, EventArgs e)
        {
            Model.Person.Karma_Gesamt--;
        }

        private void KarmaGesamtPlus(object sender, EventArgs e)
        {
            Model.Person.Karma_Gesamt++;
        }

        private void KarmaMinus(object sender, EventArgs e)
        {
            Model.Person.Karma_Aktuell--;
        }

        private void KarmaPlus(object sender, EventArgs e)
        {
            Model.Person.Karma_Aktuell++;
        }

        private void RunsMinus(object sender, EventArgs e)
        {
            Model.Person.Runs--;
        }

        private void RunsPlus(object sender, EventArgs e)
        {
            Model.Person.Runs++;
        }
    }
}