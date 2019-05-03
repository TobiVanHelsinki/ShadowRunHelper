using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace FeaturesUWP.Features
{
    interface IUi
    {
        void TaskBarStuff();
        void TitleBarStuff();
    }
    class WinUi : IUi
    {
        public void TaskBarStuff()
        {
            //    var appView = ApplicationView.GetForCurrentView();
            //    appView.Title = Model.MainObject != null ? Model.MainObject.Person.Alias : Package.Current.DisplayName;
        }

        public void TitleBarStuff()
        {
        //    ApplicationViewTitleBar AppTitlebar = ApplicationView.GetForCurrentView().TitleBar;
        //    CoreApplicationViewTitleBar CurrentTitlebar = CoreApplication.GetCurrentView().TitleBar;

        //    CurrentTitlebar.ExtendViewIntoTitleBar = true;
        //    AppTitlebar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
        //    AppTitlebar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;

        //    TitleColumnR.MinWidth = CurrentTitlebar.SystemOverlayRightInset - 15;
        //    TitleColumnL.MinWidth = CurrentTitlebar.SystemOverlayLeftInset;

        //    Window.Current.SetTitleBar(AppTitleBar);
    }
}
}
