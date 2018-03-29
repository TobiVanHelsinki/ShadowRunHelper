using ShadowRunHelper.Model;
using ShadowRunHelper.UI;
using System;
using System.Linq;
using TLIB_UWPFRAME.Model;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper
{
    public sealed partial class MainPage : Page
    {
        readonly AppModel Model = AppModel.Instance;
        NotificationsDialog Notifications = new NotificationsDialog();

        ResourceLoader res;

        public MainPage()
        {
            res = ResourceLoader.GetForCurrentView();
            InitializeComponent();
            Model.lstNotifications.CollectionChanged += (x, y) => ShowNotificationsIfNecessary();
            Model.TutorialStateChanged += TutorialStateChanged;
            Model.NavigationRequested += (x, y, z) => NavigationRequested(y, z);
            //CompatibilityChecks();
        }
        #region navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ShowNotificationsIfNecessary();
            Model.SetDependencies(Dispatcher);
            NavigationRequested(ProjectPages.Char, ProjectPagesOptions.Nothing);
        }

        void NavigationRequested(ProjectPages p, ProjectPagesOptions po)
        {
            switch (p)
            {
                case ProjectPages.Char:
                    if (Model.MainObject != null)
                    {
                        MyFrame.Navigate(typeof(CharPage), po);
                    }
                    else
                    {
                        MyFrame.Navigate(typeof(AdministrationPage), po);
                    }
                    break;
                case ProjectPages.Administration:
                    MyFrame.Navigate(typeof(AdministrationPage), po);
                    break;
                case ProjectPages.Settings:
                    MyFrame.Navigate(typeof(SettingsPage), po);
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region generel stuff

        private void TutorialStateChanged(int StateNumber, bool Highlight)
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
        void ShowNotificationsIfNecessary()
        {
            foreach (Notification item in Model.lstNotifications.Where((x) => x.bIsRead == false).OrderBy((x) => x.DateTime))
            {
                try
                {
                    var messageDialog = new MessageDialog(item.strMessage + "\n\n\n" + item.ThrownException?.Message);
                    messageDialog.Commands.Add(new UICommand(
                        "OK"));
                    messageDialog.DefaultCommandIndex = 0;
                    messageDialog.ShowAsync();
                    item.bIsRead = true;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
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
                UI.Edit.Edit_Person_Fast dialog = new UI.Edit.Edit_Person_Fast(Model.MainObject.Person);
                await dialog.ShowAsync();
            }
        }

        #endregion
        #region ButtonHandling
        async void OpenDB(object sender, RoutedEventArgs e)
        {
            if (AppModel.Instance.MainObject == null)
            {
                return;
            }
            CoreApplicationView newView = CoreApplication.CreateNewView();
            int newViewId = 0;
            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame frame = new Frame();
                frame.Navigate(typeof(DBPage), null);
                Window.Current.Content = frame;
                // You have to activate the window in order to show it later.
                Window.Current.Activate();

                newViewId = ApplicationView.GetForCurrentView().Id;
            });
            await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);

        }

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
        private void Infos_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            switch (e.Pointer.PointerDeviceType)
            {
                case Windows.Devices.Input.PointerDeviceType.Pen:
                case Windows.Devices.Input.PointerDeviceType.Mouse:
                    CustFontSize = 25;
                    break;
                case Windows.Devices.Input.PointerDeviceType.Touch:
                default:
                    CustFontSize = 45;
                    break;
            }
        }

        private void MP_Btn_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as Control).FontSize = CustFontSize;
        }

        #endregion

    }
}
