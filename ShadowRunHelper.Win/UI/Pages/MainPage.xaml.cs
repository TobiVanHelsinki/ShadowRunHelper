using Microsoft.Toolkit.Uwp.UI.Animations;
using ShadowRunHelper.Model;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TAPPLICATION.Model;
using TLIB;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.Devices.Input;
using Windows.UI.Popups;
using Windows.UI.Shell;
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
#if DEBUG

        void Exception(object sender, RoutedEventArgs e)
        {
            throw new Exception(Constants.TESTEXCEPTIONTEXT);
        }

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
        void Debug3(object sender, RoutedEventArgs e)
        {
            Model.NewNotification(" Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test" + r.Next(), isLightNotification: true);
        }
        void Debug4(object sender, RoutedEventArgs e)
        {
            Model.NewNotification("Test" + r.Next(), isLightNotification: false);
        }
#endif
        void CreateDebugChar(object sender, RoutedEventArgs e)
        {
#if DEBUG
             Model.MainObject = CharHolder.CreateCharWithStandardContent();
            foreach (var item in TypeHelper.ThingTypeProperties.Where(x => x.Usable))
            {
                CharModel.Thing NewThing;
                try
                {
                    NewThing = Model.MainObject.Add(item.ThingType);
                }
                catch (Exception)
                {
                    continue;
                }
                NewThing.Bezeichner = "TestName";
                NewThing.Notiz = "Dies ist eine Notiz\nLorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
                NewThing.Wert = 5;
                NewThing.Zusatz = "+ xW55";
                NewThing.Typ = "Typ";
            }
#endif
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
            Model.PropertyChanged += Model_PropertyChanged;
            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += (s, p) => TitleBarStuff();
            CoreApplication.GetCurrentView().TitleBar.IsVisibleChanged += (s, p) => TitleBarStuff();
#if DEBUG
            Debug_CreateDebugChar.Visibility = Visibility.Visible;
#endif
            TaskBarStuff();
            //Debug_TimeAnalyser.Stop("MainPage()");
            TipFading = new Timer(TipFadeOut, null, -1, -1);
            TipVisibility = new Timer(TipMakeInvisible, null, -1, -1);
            SizeChanged += MainPage_SizeChanged;
        }

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth < 400)
            {
                MainBar1.DefaultLabelPosition = CommandBarDefaultLabelPosition.Bottom;
                MainBarBorder.BorderThickness = new Thickness(1);
                Task.Run( async ()=> {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                    {
                        MainBarBorder.BorderThickness = new Thickness(0);
                    });
                });
            }
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Model.MainObject):
                    TaskBarStuff();
                    break;
                case nameof(Model.IsUIOperationInProgress):
                        ProgressRing.IsActive = Model.IsUIOperationInProgress;
                    break;
                case nameof(Model.IsDisplayingTip):
                    RefreshTip();
                    break;
                default:
                    break;
            }
        }

        #region Tip-System
        Timer TipFading;
        Timer TipVisibility;

        async void TipFadeOut(object state)
        {
            SystemHelper.WriteLine("TipFadeOut1: " + DateTime.Now.TimeOfDay);
            TipFading.Change(-1, -1);
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                Tip_Fade.Value = 0;
                Tip_Fade.Duration = 1500;
                Tip_Fade.StartAnimation();
            });
            TipVisibility.Change(1500, -1);
        }
        async void TipMakeInvisible(object state)
        {
            SystemHelper.WriteLine("TipMakeInvisible: " + DateTime.Now.TimeOfDay);
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                TipTextWindow.Visibility = Visibility.Collapsed;
                SystemHelper.WriteLine("TipMakeInvisible2: " + DateTime.Now.TimeOfDay);
            });
        }

        private void RefreshTip()
        {
            if (Model.IsDisplayingTip && !SettingsModel.I.DISABLE_TIPS)
            {
                TipText.Text = Constants.TipList.RandomElement();
                TipTextWindow.Visibility = Visibility.Visible;
                Tip_Fade.Value = 1;
                Tip_Fade.Duration = 500;
                Tip_Fade.StartAnimation();
            }
            else
            {
                TipFading.Change(0, -1);
            }
        }

        private void TipTextWindow_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TipVisibility.Change(-1, -1);
            Tip_Fade.Value = 1;
            Tip_Fade.Duration = 0;
            TipFading.Change(2000, -1);
        }
        #endregion
        void TaskBarStuff()
        {
            var appView = ApplicationView.GetForCurrentView();
            appView.Title = Model.MainObject != null ? Model.MainObject.Person.Alias : Package.Current.DisplayName;
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
        void TutorialStateChanged(int StateNumber, bool Highlight)
        {
            Style StyleToBeApplied = Highlight ? Tutorial.HighlightBorderStyle_XAML : Tutorial.UnhighlightBorderStyle_XAML;
            switch (StateNumber)
            {
                case 2:
                    MainBarBorder.Style = StyleToBeApplied;
                    break;
                default:
                    break;
            }
        }

        #region navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Debug_TimeAnalyser.Start("PMain.OnNavigatedTo");
            //base.OnNavigatedTo(e);
            NavigationRequested(ProjectPages.Char, ProjectPagesOptions.Nothing);
            if (SettingsModel.I.LAST_APP_VERSION != Constants.APP_VERSION_BUILD_DELIM)
            {
                Model.NewNotification(
                    string.Format(StringHelper.GetString("Notification_NewVersion"), Constants.APP_VERSION_BUILD_DELIM) + "\n\n" +
                    StringHelper.GetString("Notification_NewVersion_"+ Constants.APP_VERSION_BUILD_DELIM.Replace('.','_')),true,10);
                SettingsModel.I.LAST_APP_VERSION = Constants.APP_VERSION_BUILD_DELIM;
            }
            //Debug_TimeAnalyser.Stop("PMain.OnNavigatedTo");
        }
        void NavigationRequested(ProjectPages p, ProjectPagesOptions po)
        {
            switch (p)
            {
                case ProjectPages.Char:
                    if (Model.MainObject != null)
                    {
                    SettingsModel.I.LAST_PAGE = ProjectPages.Char;
                        MyFrame.Navigate(typeof(CharPage), po);
                        //Nav_Char.Content.
                    }
                    else
                    {
                    SettingsModel.I.LAST_PAGE = ProjectPages.Administration;
                        MyFrame.Navigate(typeof(AdministrationPage), po);
                    }
                    break;
                case ProjectPages.Administration:
                    SettingsModel.I.LAST_PAGE = ProjectPages.Administration;
                    MyFrame.Navigate(typeof(AdministrationPage), po);
                    break;
                case ProjectPages.Settings:
                    SettingsModel.I.LAST_PAGE = ProjectPages.Settings;
                    MyFrame.Navigate(typeof(SettingsPage), po);
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region Notification

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
                        messageDialog.Commands.Add(new UICommand(StringHelper.GetString("UI_Cntnt_GoTo_Notifications/Content"), (x) => GoToNotifications()));
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
                    ExampleInAppNotification.Show(item.Message, item.ShownTime);
                    Notification_Fade.Value = 1;
                    Notification_Fade.StartAnimation();
                }
            }
        }
        void ExampleInAppNotification_Closing(object sender, Microsoft.Toolkit.Uwp.UI.Controls.InAppNotificationClosingEventArgs e)
        {
            Notification_Fade.Value = 0;
            Notification_Fade.StartAnimation();
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
            if (SettingsModel.I.DEBUG_FEATURES)
            {
                //Debug_TimeAnalyser.Finish();
            }
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
