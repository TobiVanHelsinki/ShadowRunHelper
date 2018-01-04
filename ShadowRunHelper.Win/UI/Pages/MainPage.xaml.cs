﻿using ShadowRunHelper.Model;
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
        ResourceLoader res;

        public MainPage()
        {
            res = ResourceLoader.GetForCurrentView();
            InitializeComponent();
            Model.lstNotifications.CollectionChanged += (x, y) => ShowError();
            Model.TutorialStateChanged += TutorialStateChanged;
            CompatibilityChecks();
        }
        #region navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ShowError();
            Model.SetDependencies(Dispatcher);
            NavigationRequested(ProjectPages.Char);
        }

        Action<ProjectPages> NavigationMethod;
        void NavigationRequested(ProjectPages e)
        {
            NavigationMethod = NavigationRequested;
            switch (e)
            {
                case ProjectPages.Char:
                    if (Model.MainObject != null)
                    {
                        MyFrame.Navigate(typeof(CharPage), NavigationMethod);
                    }
                    else
                    {
                        MyFrame.Navigate(typeof(AdministrationPage), NavigationMethod);
                    }
                    break;
                case ProjectPages.Administration:
                    MyFrame.Navigate(typeof(AdministrationPage), NavigationMethod);
                    break;
                case ProjectPages.Settings:
                    MyFrame.Navigate(typeof(SettingsPage), NavigationMethod);
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
        async void ShowError()
        {
            foreach (Notification item in Model.lstNotifications.Where((x) => x.bIsRead == false).OrderBy((x) => x.DateTime))
            {
                try
                {
                    var messageDialog = new MessageDialog(item.strMessage + " \n \n" + item.ThrownException.Message);
                    messageDialog.Commands.Add(new UICommand(
                        "OK"));
                    messageDialog.DefaultCommandIndex = 0;
                    await messageDialog.ShowAsync();
                }
                catch (Exception)
                {
                    continue;
                }
                item.bIsRead = true;
            }
        }
        #endregion
        #region Header Button Handler
        void Plus_Click(object sender, RoutedEventArgs e)
        {
            if (Model.MainObject != null)
            {
                string Controller_Name = ((string)((Button)sender).Name);

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
                string Controller_Name = ((string)((Button)sender).Name);

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
                UI.Edit.Edit_Person2 dialog = new UI.Edit.Edit_Person2(Model.MainObject.Person);
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
            NavigationRequested(ProjectPages.Char);
        }

        void Ui_Nav_Admin(object sender, RoutedEventArgs e)
        {
            NavigationRequested(ProjectPages.Administration);
        }

        void Ui_Nav_Settings(object sender, RoutedEventArgs e)
        {
            NavigationRequested(ProjectPages.Settings);
        }
        #endregion
        #region Compatibility (Update 4)
        void CommandBar_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsPropertyPresent("Windows.UI.Xaml.Controls.CommandBar", "DefaultLabelPosition"))
            {
                (sender as CommandBar).DefaultLabelPosition = CommandBarDefaultLabelPosition.Right;
            }
        }
        #endregion
        #region Compatibility (Update 5) Fluent Design
        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.AcrylicBrush"))
            {
                try
                {
                    (sender as Grid).Background = (AcrylicBrush)Resources["SystemControlAccentAcrylicElementAccentMediumHighBrush"];
                }
                catch (Exception)
                {
                    try
                    {
                        (sender as Grid).Background = (SolidColorBrush)Resources["SystemControlAccentAcrylicElementAccentMediumHighBrush"];
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.RevealBrush"))
            {
                (sender as Button).Style = (Style)Resources["ButtonRevealStyle"];
            }
        }

        private void AppBarButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.RevealBrush"))
            {
                (sender as AppBarButton).Style = (Style)Resources["AppBarButtonRevealLabelsOnRightStyle"];
            }
        }

        void CompatibilityChecks()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.AcrylicBrush"))
            {
                MainBar1.Background = (AcrylicBrush)Resources["SystemControlAccentAcrylicWindowAccentMediumHighBrush_MoreOpac"];
                MainBar2.Background = (AcrylicBrush)Resources["SystemControlAccentAcrylicWindowAccentMediumHighBrush_MoreOpac"];
            }
            else
            {
                //SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(255, 202, 24, 37));

                //MainBarBorder.Background = myBrush;
            }
        }
        #endregion
    }
}
