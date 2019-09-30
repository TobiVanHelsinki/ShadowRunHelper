using Rg.Plugins.Popup.Pages;
using ShadowRunHelper.Model;
using System;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Linq;
using ShadowRunHelperViewer.UI.Resources;

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
            SizeDefiningElement_SizeChanged(this, new EventArgs());
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

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            (sender as Slider).Value = Math.Round(e.NewValue, 0);
        }

        private void SizeDefiningElement_SizeChanged(object sender, EventArgs e)
        {
            var NewWidth = (sender as VisualElement).Width /*- Column1Max - Column2Max - Column3Max*/; //HACK
            //DamageGrid.WidthRequest = NewWidth < 40 ? 40 : NewWidth;
            //DamageGrid.MinimumWidthRequest = WidthRequest;
        }

        private void PopupPage_SizeChanged(object sender, EventArgs e) => (MainFrame.WidthRequest, MainFrame.HeightRequest) = Common.MaximumDimensions(Width, Height);
    }
}