using ShadowRunHelper.Model;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace ShadowRunHelper
{
    class WinUi : IUi
    {
        bool _IsTopUiSizeEnabled;
        public bool IsTopUiSizeEnabled
        {
            get { return _IsTopUiSizeEnabled; }
            set { if (_IsTopUiSizeEnabled != value) { _IsTopUiSizeEnabled = value; TitleBar_LayoutMetricsChanged(null, null);  } }
        }


        public event TopUiSizeChangedEventHandler TopUiSizeChanged;

        public void DisplayCurrentCharName()
        {
            var appView = ApplicationView.GetForCurrentView();
            appView.Title = AppModel.Instance.MainObject != null ? AppModel.Instance.MainObject.Person.Alias : Package.Current.DisplayName;
        }

        public void GetTopUiSizeChanged()
        {
            TitleBar_LayoutMetricsChanged(null, null);
        }

        public void RegisterTopUiSizeChanged(object VisualElement)
        {
            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += TitleBar_LayoutMetricsChanged;
            CoreApplication.GetCurrentView().TitleBar.IsVisibleChanged += TitleBar_LayoutMetricsChanged;
            Window.Current.SetTitleBar(VisualElement as UIElement);
        }

        private void TitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            //var AppTitlebar = ApplicationView.GetForCurrentView().TitleBar;
            //AppTitlebar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
            //AppTitlebar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;

            var CurrentTitlebar = CoreApplication.GetCurrentView().TitleBar;
            CurrentTitlebar.ExtendViewIntoTitleBar = IsTopUiSizeEnabled;
            if (!IsTopUiSizeEnabled)
            {
                TopUiSizeChanged?.Invoke(0, 0);
            }
            else
            {
                TopUiSizeChanged?.Invoke(CurrentTitlebar.SystemOverlayLeftInset, CurrentTitlebar.SystemOverlayRightInset - 15);
            }
        }
    }
}
