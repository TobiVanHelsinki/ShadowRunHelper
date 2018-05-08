using Microsoft.Toolkit.Uwp.UI.Animations;
using ShadowRunHelper.Model;
using System;
using System.Collections;
using System.Linq;
using System.Threading;
using TAPPLICATION.Model;
using TLIB;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.Devices.Input;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper.UI
{
    public sealed partial class MainPage : Page
    {
        readonly AppModel Model = AppModel.Instance;
        NotificationsDialog Notifications = new NotificationsDialog();
        ResourceLoader res;

        #region Debug
        void Debug1(object sender, RoutedEventArgs e)
        {
            DebugSettings DebugSettings = Application.Current.DebugSettings;
            if (System.Diagnostics.Debugger.IsAttached)
            {
                DebugSettings.IsOverdrawHeatMapEnabled = !DebugSettings.IsOverdrawHeatMapEnabled;
            }
        }
        void Debug2(object sender, RoutedEventArgs e)
        {
            DebugSettings DebugSettings = Application.Current.DebugSettings;
            if (System.Diagnostics.Debugger.IsAttached)
            {
                DebugSettings.IsTextPerformanceVisualizationEnabled = !DebugSettings.IsTextPerformanceVisualizationEnabled;
            }
        }
        Random r = new Random();
        private void Debug3(object sender, RoutedEventArgs e)
        {
            Model.NewNotification(" Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test" + r.Next(), isLightNotification: true);
        }
        private void Debug4(object sender, RoutedEventArgs e)
        {
            Model.NewNotification("Test" + r.Next(), isLightNotification: false);
        }

        #endregion
        public MainPage()
        {
            res = ResourceLoader.GetForCurrentView();
            InitializeComponent();
#pragma warning disable CS4014
            Model.lstNotifications.CollectionChanged += (x, y) => ShowNotificationsIfNecessary(y.NewItems);
#pragma warning restore CS4014
            Model.TutorialStateChanged += TutorialStateChanged;
            Model.NavigationRequested += NavigationRequested;
        }

        public void TitleBarStuff()
        {
            ApplicationViewTitleBar AppTitlebar = ApplicationView.GetForCurrentView().TitleBar;
            CoreApplicationViewTitleBar CurrentTitlebar = CoreApplication.GetCurrentView().TitleBar;

            CurrentTitlebar.ExtendViewIntoTitleBar = true;
            AppTitlebar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
            AppTitlebar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;

            TitleColumnR.MinWidth = CurrentTitlebar.SystemOverlayRightInset;
            TitleColumnL.MinWidth = CurrentTitlebar.SystemOverlayLeftInset;

            Window.Current.SetTitleBar(AppTitleBar);
        }

        #region navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ShowNotificationsIfNecessary(Model.lstNotifications);
            Model.SetDependencies(Dispatcher);
            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += (s, p) => TitleBarStuff();
            CoreApplication.GetCurrentView().TitleBar.IsVisibleChanged += (s, p) => TitleBarStuff();
            TitleBarStuff();
            NavigationRequested(ProjectPages.Char, ProjectPagesOptions.Nothing);
        }
        void NavigationRequested(ProjectPages p, ProjectPagesOptions po)
        {
            switch (p)
            {
                case ProjectPages.Char:
                    if (Model.MainObject != null)
                    {
                    SettingsModel.I.LastPage = ProjectPages.Char;
                        MyFrame.Navigate(typeof(CharPage), po);
                        //Nav_Char.Content.
                    }
                    else
                    {
                    SettingsModel.I.LastPage = ProjectPages.Administration;
                        MyFrame.Navigate(typeof(AdministrationPage), po);
                    }
                    break;
                case ProjectPages.Administration:
                    SettingsModel.I.LastPage = ProjectPages.Administration;
                    MyFrame.Navigate(typeof(AdministrationPage), po);
                    break;
                case ProjectPages.Settings:
                    SettingsModel.I.LastPage = ProjectPages.Settings;
                    MyFrame.Navigate(typeof(SettingsPage), po);
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region global intrest stuff

        void TutorialStateChanged(int StateNumber, bool Highlight)
        {
            Style StyleToBeApplied = Highlight ? Tutorial.HighlightBorderStyle_XAML : Tutorial.UnhighlightBorderStyle_XAML;
            switch (StateNumber)
            {
                case 1:
                    MainBarBorder.Style = StyleToBeApplied;
                    break;
                default:
                    break;
            }
        }
        Semaphore ShowNotificationsInProgress = new Semaphore(0,1);
        async void ShowNotificationsIfNecessary(IList newItems)
        {
            using (ShowNotificationsInProgress)
            {
                foreach (Notification item in newItems.Cast<Notification>().Where((x) => !x.IsRead && !x.IsLight).OrderBy((x) => x.OccuredAt))
                {
                    try
                    {
                        var messageDialog = new MessageDialog(item.Message);
                        messageDialog.Commands.Add(new UICommand(StringHelper.GetString("Close")));
                        messageDialog.Commands.Add(new UICommand(StringHelper.GetString("UI_OpenNotifications"), (x) => GoToNotifications()));
                        messageDialog.DefaultCommandIndex = 0;
                        await messageDialog.ShowAsync();
                        item.IsRead = true;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                foreach (Notification item in newItems.Cast<Notification>().Where((x) => !x.IsRead && x.IsLight).OrderBy((x) => x.OccuredAt))
                {
                    ExampleInAppNotification.Show(item.Message, 6000);
                    Fade.Value = 1;
                    Fade.StartAnimation();
                }
            }
        }
        private void ExampleInAppNotification_Closing(object sender, Microsoft.Toolkit.Uwp.UI.Controls.InAppNotificationClosingEventArgs e)
        {
            Fade.Value = 0;
            Fade.StartAnimation();
        }
        void ExampleInAppNotification_PointerPressed(object sender, PointerRoutedEventArgs e) => GoToNotifications();
        void GoToNotifications()
        {
            ExampleInAppNotification.Dismiss();
            Model.RequestNavigation(ProjectPages.Settings, ProjectPagesOptions.SettingsNots);
        }
        #endregion
        #region Header Button Handler
        void Plus_Click(object sender, RoutedEventArgs e)
        {
            if (Model.MainObject != null)
            {
                string Controller_Name = (((FrameworkElement)sender).Name);

                if (Controller_Name.Contains("Karma_Gesamt"))
                {
                    Model.MainObject.Person.Karma_Gesamt++;
                }
                else if (Controller_Name.Contains("Karma_Aktuell"))
                {
                    Model.MainObject.Person.Karma_Aktuell++;
                }
                else if (Controller_Name.Contains("Edge_Gesamt"))
                {
                    Model.MainObject.Person.Edge_Gesamt++;
                }
                else if (Controller_Name.Contains("Edge_Aktuell"))
                {
                    Model.MainObject.Person.Edge_Aktuell++;
                }
                else if (Controller_Name.Contains("Initiative"))
                {
                    Model.MainObject.Person.Initiative++;
                }
                else if (Controller_Name.Contains("Runs"))
                {
                    Model.MainObject.Person.Runs++;
                }
            }
        }

        void Minus_Click(object sender, RoutedEventArgs e)
        {
            if (Model.MainObject != null)
            {
                string Controller_Name = (((FrameworkElement)sender).Name);

                if (Controller_Name.Contains("Karma_Gesamt"))
                {
                    Model.MainObject.Person.Karma_Gesamt--;
                }
                else if (Controller_Name.Contains("Karma_Aktuell"))
                {
                    Model.MainObject.Person.Karma_Aktuell--;
                }
                else if (Controller_Name.Contains("Edge_Gesamt"))
                {
                    Model.MainObject.Person.Edge_Gesamt--;
                }
                else if (Controller_Name.Contains("Edge_Aktuell"))
                {
                    Model.MainObject.Person.Edge_Aktuell--;
                }
                else if (Controller_Name.Contains("Initiative"))
                {
                    Model.MainObject.Person.Initiative--;
                }
                else if (Controller_Name.Contains("Runs"))
                {
                    Model.MainObject.Person.Runs--;
                }
            }
        }

        async void Edit_Click(object sender, RoutedEventArgs e)
        {
            String Controller_Name = ((String)((Button)sender).Name);

            if (Controller_Name.Contains("Person2"))
            {
                Edit_Person_Fast dialog = new Edit_Person_Fast(Model.MainObject.Person);
                await dialog.ShowAsync();
            }
        }

        #endregion
        #region ButtonHandling

        void Ui_Nav_Char(object sender, RoutedEventArgs e)
        {
            NavigationRequested(ProjectPages.Char, ProjectPagesOptions.Nothing);
        }

        void Ui_Nav_Admin(object sender, RoutedEventArgs e)
        {
            NavigationRequested(ProjectPages.Administration, ProjectPagesOptions.Nothing);
        }

        void Ui_Nav_Settings(object sender, RoutedEventArgs e)
        {
            NavigationRequested(ProjectPages.Settings, ProjectPagesOptions.Nothing);
        }

        #endregion
        #region DynamicSize

        public int CustFontSize { get; set; }
        public PointerDeviceType CurrentPointerDeviceType { get; set; }
        void Infos_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            CurrentPointerDeviceType = e.Pointer.PointerDeviceType;
            switch (e.Pointer.PointerDeviceType)
            {
                case PointerDeviceType.Pen:
                case PointerDeviceType.Mouse:
                    CustFontSize = 25;
                    break;
                case PointerDeviceType.Touch:
                default:
                    CustFontSize = 45;
                    break;
            }
        }

        void Flyout_Opening(object sender, object e)
        {
            switch (CurrentPointerDeviceType)
            {
                case PointerDeviceType.Touch:
                    (sender as Flyout).Placement = Windows.UI.Xaml.Controls.Primitives.FlyoutPlacementMode.Full;
                    break;
                case PointerDeviceType.Pen:
                    (sender as Flyout).Placement = Windows.UI.Xaml.Controls.Primitives.FlyoutPlacementMode.Top;
                    break;
                case PointerDeviceType.Mouse:
                    (sender as Flyout).Placement = Windows.UI.Xaml.Controls.Primitives.FlyoutPlacementMode.Top;
                    break;
                default:
                    break;
            }
        }
        void Flyout_Closed(object sender, object e)
        {
            (sender as Flyout).Placement = Windows.UI.Xaml.Controls.Primitives.FlyoutPlacementMode.Top;
        }
        void MP_Btn_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as Control).FontSize = CustFontSize;
        }
        void Viewbox_Loaded(object sender, RoutedEventArgs e)
        {
            if (CurrentPointerDeviceType == PointerDeviceType.Touch)
            {
                var ViewBox = sender as FrameworkElement;
                var Parent = ((sender as FrameworkElement).Parent as FlyoutPresenter);
                if (ViewBox.ActualWidth >= Parent.ActualWidth)
                {
                    ViewBox.Width = Parent.ActualWidth;
                    ViewBox.Height = Parent.ActualHeight;
                }
            }
        }


        #endregion

        void EditBox_GotFocus(object sender, RoutedEventArgs e) => SharePageFunctions.EditBox_SelectAll(sender, e);

        void EditBox_PreviewKeyDown(object sender, KeyRoutedEventArgs e) => SharePageFunctions.EditBox_UpDownKeys(sender, e);

    }
}
