//Author: Tobi van Helsinki

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ShadowRunHelper.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlCenter : ContentView, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion NotifyPropertyChanged

        public CharHolder Model { get; set; }

        public ControlCenter(CharHolder Char)
        {
            Model = Char;
            InitializeComponent();
            BindingContext = this;
        }

        private double _GeneralInput;
        public double GeneralInput
        {
            get => _GeneralInput;
            set { if (_GeneralInput != value) { _GeneralInput = value; NotifyPropertyChanged(); } }
        }

        private void MoneyPlus(object sender, EventArgs e)
        {
            if (GeneralInput != 0)
            {
                Model.Person.Kontostand += GeneralInput;
                GeneralInput = 0;
                return;
            }
            Model.Person.Kontostand++;
        }

        private void MoneyMinus(object sender, EventArgs e)
        {
            if (GeneralInput != 0)
            {
                Model.Person.Kontostand -= GeneralInput;
                GeneralInput = 0;
                return;
            }
            Model.Person.Kontostand--;
        }

        private void EdgePlus(object sender, EventArgs e)
        {
            if (GeneralInput != 0)
            {
                Model.Person.Edge_Aktuell += GeneralInput;
                GeneralInput = 0;
                return;
            }
            Model.Person.Edge_Aktuell++;
        }

        private void EdgeMinus(object sender, EventArgs e)
        {
            if (GeneralInput != 0)
            {
                Model.Person.Edge_Aktuell -= GeneralInput;
                GeneralInput = 0;
                return;
            }
            Model.Person.Edge_Aktuell--;
        }

        private void EdgeGesamtPlus(object sender, EventArgs e)
        {
            if (GeneralInput != 0)
            {
                Model.Person.Edge_Gesamt += GeneralInput;
                GeneralInput = 0;
                return;
            }
            Model.Person.Edge_Gesamt++;
        }

        private void EdgeGesamtMinus(object sender, EventArgs e)
        {
            if (GeneralInput != 0)
            {
                Model.Person.Edge_Gesamt -= GeneralInput;
                GeneralInput = 0;
                return;
            }
            Model.Person.Edge_Gesamt--;
        }

        private void KarmaGesamtMinus(object sender, EventArgs e)
        {
            if (GeneralInput != 0)
            {
                Model.Person.Karma_Gesamt -= GeneralInput;
                GeneralInput = 0;
                return;
            }
            Model.Person.Karma_Gesamt--;
        }

        private void KarmaGesamtPlus(object sender, EventArgs e)
        {
            if (GeneralInput != 0)
            {
                Model.Person.Karma_Gesamt += GeneralInput;
                GeneralInput = 0;
                return;
            }
            Model.Person.Karma_Gesamt++;
        }

        private void KarmaMinus(object sender, EventArgs e)
        {
            if (GeneralInput != 0)
            {
                Model.Person.Karma_Aktuell -= GeneralInput;
                GeneralInput = 0;
                return;
            }
            Model.Person.Karma_Aktuell--;
        }

        private void KarmaPlus(object sender, EventArgs e)
        {
            if (GeneralInput != 0)
            {
                Model.Person.Karma_Aktuell += GeneralInput;
                GeneralInput = 0;
                return;
            }
            Model.Person.Karma_Aktuell++;
        }

        private void RunsMinus(object sender, EventArgs e)
        {
            if (GeneralInput != 0)
            {
                Model.Person.Runs -= GeneralInput;
                GeneralInput = 0;
                return;
            }
            Model.Person.Runs--;
        }

        private void RunsPlus(object sender, EventArgs e)
        {
            if (GeneralInput != 0)
            {
                Model.Person.Runs += GeneralInput;
                GeneralInput = 0;
                return;
            }
            Model.Person.Runs++;
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            (sender as Slider).Value = Math.Round(e.NewValue, 0);
        }
    }
}