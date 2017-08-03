using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using SharedCodeBase.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TLIB;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Inhaltsdialog" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRunHelper.UI
{
    public sealed partial class Tutorial1 : ContentDialog, INotifyPropertyChanged
    {
        public static Windows.UI.Xaml.Media.SolidColorBrush HighlightBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
        public static Windows.UI.Xaml.Thickness HighlightThickness = new Windows.UI.Xaml.Thickness(2);
        int MaxStateCount = 5;
        int _StateCounter = 0;
        int StateCounter { get { return _StateCounter; } set {
                if (value < 0)
                {
                    _StateCounter = 0;
                    NotifyPropertyChanged();
                }
                else if (value > MaxStateCount)
                {
                    BtnExit_Click(null, null);
                }
                else
                {
                    _StateCounter = value;
                    NotifyPropertyChanged();
                }
                }
        }

        AppModel ViewModel = AppModel.Instance;

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ModelResources.CallPropertyChanged(PropertyChanged, this, propertyName);
        }

        public Tutorial1()
        {
            InitializeComponent();
            Title = CrossPlatformHelper.GetString("Tut1Dialog/Title");
            ViewModel.TutorialStateChanged += StateChanged;
        }

        private void StateChanged(int StateNumber, bool Highlight)
        {
            if (Highlight)
            {
                TutorialText.Text = CrossPlatformHelper.GetString("Tut1_State_" + StateNumber);
            };
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }
        
        private void BtnPrev_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.TutorialChangedState(StateCounter, false);
            StateCounter--;
            ViewModel.TutorialChangedState(StateCounter, true);
        }

        private void BtnNext_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.TutorialChangedState(StateCounter, false);
            StateCounter++;
            ViewModel.TutorialChangedState(StateCounter, true);
        }

        private void BtnExit_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.TutorialChangedState(StateCounter, false);
            Hide();
        }
    }
}
