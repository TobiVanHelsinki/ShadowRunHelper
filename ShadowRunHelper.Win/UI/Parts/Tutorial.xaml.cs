using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using SharedCodeBase.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TLIB;
using TLIB.Model;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// Die Elementvorlage "Inhaltsdialog" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRunHelper.UI
{
    public sealed partial class Tutorial : ContentDialog, INotifyPropertyChanged
    {
        public static Windows.UI.Xaml.Media.SolidColorBrush HighlightBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
        public static Windows.UI.Xaml.Thickness HighlightThickness = new Windows.UI.Xaml.Thickness(2);
        public static Style HighlightBorderStyle_XAML = (Style)App.Current.Resources["Tut1Highlight"];
        public static Style UnhighlightBorderStyle_XAML = (Style)App.Current.Resources["Tut1Unhighlight"];

        bool ShouldExit;
        int MinStateCount = 0;
        int MaxStateCount = 0;
        int _StateCounter = 0;
        int StateCounter { get { return _StateCounter; }
            set {
                if (value < MinStateCount)
                {
                    _StateCounter = MinStateCount;
                    NotifyPropertyChanged("RelativStateCounter");
                }
                else if (value > MaxStateCount)
                {
                    _StateCounter = MaxStateCount;
                    ShouldExit = true;
                    NotifyPropertyChanged("RelativStateCounter");
                }
                else
                {
                    _StateCounter = value;
                    NotifyPropertyChanged("RelativStateCounter");
                }
            }
        }
        int RelativMaxStateCount = 0;
        //int RelativMinStateCount = 0;
        int RelativStateCounter
        {
            get
            {
                return StateCounter-MinStateCount;
            }
            set
            {
               
                _StateCounter = value + MinStateCount;
                NotifyPropertyChanged();
            }
        }

        AppModel ViewModel = AppModel.Instance;

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ModelHelper.CallPropertyChangedAtDispatcher(PropertyChanged, this, propertyName);
        }

        public Tutorial(int Start, int Ende)
        {
            MinStateCount = Start;
            MaxStateCount = Ende;
            _StateCounter = Start;
            RelativMaxStateCount = Ende-Start;
            
            InitializeComponent();
            Title = CrossPlatformHelper.GetString(string.Format("Tut_TitleState_{0,0:D2}", Start));
            ViewModel.TutorialStateChanged += StateChanged;
            ViewModel.TutorialChangedState(StateCounter, true);
        }

        private void StateChanged(int StateNumber, bool Highlight)
        {
            if (Highlight)
            {
                try
                {
                    TutorialText.Text = CrossPlatformHelper.GetString(string.Format("Tut_State_{0,0:D2}", StateNumber));
                }
                catch (System.Exception ex)
                {
                }
                
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
            if (ShouldExit)
            {
                BtnExit_Click(null, null);
                return;
            }
            ViewModel.TutorialChangedState(StateCounter, true);
        }

        void BtnExit_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.TutorialChangedState(StateCounter, false);
            Hide();
        }
        bool IsInHiddenState = false;
        void BtnHide_Click(object sender, RoutedEventArgs e)
        {
            if (IsInHiddenState)
            {
                Background.Opacity = 100;
                TutorialText.Foreground.Opacity = 100;
                Counter.Foreground.Opacity = 100;
            }
            else
            {
                Background.Opacity = 0;
                TutorialText.Foreground.Opacity = 0;
                Counter.Foreground.Opacity = 0;
            }
            IsInHiddenState = !IsInHiddenState;
        }

        #region ApplyNewStyles
        void Button_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.RevealBrush"))
            {
                (sender as Button).Style = (Style)Resources["ButtonRevealStyle"];
            }
        }

        void AppBarButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.RevealBrush"))
            {
                (sender as AppBarButton).Style = (Style)Resources["AppBarButtonRevealLabelsOnRightStyle"];
            }
        }
        #endregion
    }
}
