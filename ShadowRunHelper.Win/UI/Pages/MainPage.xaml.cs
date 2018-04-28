using ShadowRunHelper.Model;
using System;
using System.Linq;
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

        #endregion
        public MainPage()
        {
            res = ResourceLoader.GetForCurrentView();
            InitializeComponent();
#pragma warning disable CS4014
            Model.lstNotifications.CollectionChanged += (x, y) => ShowNotificationsIfNecessary();
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
            ShowNotificationsIfNecessary();
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
        #region global stuff

        void TutorialStateChanged(int StateNumber, bool Highlight)
        {
            Style StyleToBeApplied = Highlight ? Tutorial.HighlightBorderStyle_XAML : Tutorial.UnhighlightBorderStyle_XAML;
            switch (StateNumber)
            {
                case 1:
                    MainBarBorder.Style = StyleToBeApplied;
                    break;
                case 21:
                    StatusControlBorder.Style = StyleToBeApplied;
                    break;
                default:
                    //MainBarBorder.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    //StatusControlBorder.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    break;
            }
        }
        bool ShowNotificationsInProgress = false;

        async void ShowNotificationsIfNecessary()
        {
            if (ShowNotificationsInProgress)
            {
                return;
            }
            ShowNotificationsInProgress = true;
            bool DisplayMore = true;
            foreach (Notification item in Model.lstNotifications.Where((x) => x.IsRead == false).OrderBy((x) => x.OccuredAt))
            {
                if (!DisplayMore)
                {
                    break;
                }
                try
                {
                    var messageDialog = new MessageDialog(item.Message + "\n\n\n" + item.ThrownException?.Message);
                    messageDialog.Commands.Add(new UICommand(StringHelper.GetString("OK")));
                    messageDialog.Commands.Add(new UICommand(StringHelper.GetString("CloseNotifications"), (x) => DisplayMore = false));
                    messageDialog.DefaultCommandIndex = 0;
                    await messageDialog.ShowAsync();
                    item.IsRead = true;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            ShowNotificationsInProgress = false;
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
        public PointerDeviceType CustFontCurrentPointerDeviceType { get; set; }
        void Infos_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            CustFontCurrentPointerDeviceType = e.Pointer.PointerDeviceType;
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
            switch (CustFontCurrentPointerDeviceType)
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

        #endregion

        void EditBox_GotFocus(object sender, RoutedEventArgs e) => SharePageFunctions.EditBox_SelectAll(sender, e);

        void EditBox_PreviewKeyDown(object sender, KeyRoutedEventArgs e) => SharePageFunctions.EditBox_UpDownKeys(sender, e);

    }
}
