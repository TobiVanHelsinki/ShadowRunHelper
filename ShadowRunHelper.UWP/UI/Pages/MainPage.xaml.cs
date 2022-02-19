using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TLIB;
using Windows.ApplicationModel;
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
        AppModel Model => AppModel.Instance;
        NotificationsDialog Notifications = new NotificationsDialog();

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
            Log.Write(" Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test" + r.Next(), true);
        }
        void Debug4(object sender, RoutedEventArgs e)
        {
            Log.Write("Test" + r.Next(), false);
        }
#endif
        void CreateDebugChar(object sender, RoutedEventArgs e)
        {
#if DEBUG
             Model.MainObject = CharHolderGenerator.CreateCharWithStandardContent();
            foreach (var item in TypeHelper.ThingTypeProperties.Where(x => x.Usable))
            {
                CharModel.Thing NewThing;
                try
                {
                    NewThing = Model.MainObject.Add(item.ThingType);
                }
                catch (Exception ex)
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
            InitializeComponent();

            Model.lstNotifications.CollectionChanged += (x, y) => ShowNotificationsIfNecessary(y.NewItems);
            Model.PropertyChanged += Model_PropertyChanged;
            Model.TutorialStateChanged += TutorialStateChanged;
            Model.NavigationRequested += NavigationRequested;
            SizeChanged += MainPage_SizeChanged;
            AppHolder.StartInit();

            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += (s, p) => TitleBarStuff();
            CoreApplication.GetCurrentView().TitleBar.IsVisibleChanged += (s, p) => TitleBarStuff();
#if DEBUG
            Debug_CreateDebugChar.Visibility = Visibility.Visible;
#endif
            Features.Ui.DisplayCurrentCharName();
            TipFading = new Timer(TipFadeOut, null, -1, -1);
            TipVisibility = new Timer(TipMakeInvisible, null, -1, -1);
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
                    Features.Ui.DisplayCurrentCharName();
                    break;
                case nameof(Model.IsCharInProgress):
                    ProgressRing.IsActive = Model.IsCharInProgress;
                    RefreshTip();
                    ShowCharName();
                    if (!Model.IsCharInProgress)
                    {
                        Model.RequestNavigation(ProjectPages.Char);
                    }
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
            //System.Diagnostics.Debug.WriteLine("TipFadeOut1: " + DateTime.Now.TimeOfDay);
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
            //System.Diagnostics.Debug.WriteLine("TipMakeInvisible: " + DateTime.Now.TimeOfDay);
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                TipTextWindow.Visibility = Visibility.Collapsed;
                //System.Diagnostics.Debug.WriteLine("TipMakeInvisible2: " + DateTime.Now.TimeOfDay);
            });
        }
        void ShowCharName()
        {
            if (Model.IsCharInProgress)
            {
                //LoadingCharField.Visibility = Visibility.Visible;
                //LoadingCharField.Text = "Loading " + Model.CharInProgress.Name;
                //TODO
            }
            else
            {
                LoadingCharField.Visibility = Visibility.Collapsed;
            }
        }
        void RefreshTip()
        {
            if (Model.IsCharInProgress && !SettingsModel.I.DISABLE_TIPS)
            {
                TipText.Text = Constants.TipList.RandomElement()??"";
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
        

        public void TitleBarStuff()
        {
            ApplicationViewTitleBar AppTitlebar = ApplicationView.GetForCurrentView().TitleBar;
            CoreApplicationViewTitleBar CurrentTitlebar = CoreApplication.GetCurrentView().TitleBar;

            CurrentTitlebar.ExtendViewIntoTitleBar = true;
            AppTitlebar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
            AppTitlebar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;

            TitleColumnR.MinWidth = CurrentTitlebar.SystemOverlayRightInset - 15;
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
            NavigationRequested(ProjectPages.Char, ProjectPagesOptions.Nothing);
            if (SettingsModel.I.LAST_APP_VERSION != Constants.APP_VERSION_BUILD_DELIM)
            {
                Log.Write(CustomManager.GetString("VersionHistory"), true, 0);
                Log.Write(
                    string.Format(
                        CustomManager.GetString("Notification_NewVersion"),Constants.APP_VERSION_BUILD_DELIM
                        ), 10);
                SettingsModel.I.LAST_APP_VERSION = Constants.APP_VERSION_BUILD_DELIM;
            }
            if (Features.AppDataPorter.InProgress)
            {
                //Ask User, If YES, Import all
                new MultiButtonMessageDialog(CustomManager.GetString("Request_AppImport/Title")
                    , CustomManager.GetString("Request_AppImport/Text")
                    , (CustomManager.GetString("Request_AppImport/Yes"), () => Features.AppDataPorter.ImportAppPacket())
                    , (CustomManager.GetString("Request_AppImport/No"), null)
                    ).ShowAsync();
            }
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
                        messageDialog.Commands.Add(new UICommand(CustomManager.GetString("Close")));
                        messageDialog.Commands.Add(new UICommand(CustomManager.GetString("UI_Cntnt_GoTo_Notifications/Content"), (x) => GoToNotifications()));
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
        void Click_Save(object sender, RoutedEventArgs e)
        {
            Model?.MainObject?.SetSaveTimerTo(0, true);
        }

        async void Click_SaveAtCurrentPlace(object sender, RoutedEventArgs e)
        {
            try
            {
                var i = await SharedIO.SaveAtCurrentPlace(Model.MainObject);
                Model.MainObject.FileInfo = i;
            }
            catch (Exception ex)
            {
                Log.Write(CustomManager.GetString("Notification_Error_SaveFail"), ex);
            }
        }
        void Click_Delete(object sender, RoutedEventArgs e)
        {
            Model.RequestNavigation(ProjectPages.Administration);
            Model.MainObject = null;
        }

        void Click_SubtractLifeStyleCost(object sender, RoutedEventArgs e)
        {
            Model.MainObject.SubtractLifeStyleCost();
        }
        void Click_CharSettings(object sender, RoutedEventArgs e)
        {
            var d = new CharSettingsDialog();
            d.ShowAsync();
        }

        async void Click_SaveExtern(object sender, RoutedEventArgs e)
        {
            try
            {

                var Folder = await SharedIO.CurrentIO.PickFolder(Constants.ACCESSTOKEN_EXPORT);
                var File = new FileInfo(Path.Combine(Folder.FullName, Model.MainObject.FileInfo.Name));
                FileInfo SavePlace = await SharedIO.Save(Model.MainObject, Info: File);
                Model.MainObject.FileInfo = SavePlace;
            }
            catch (Exception ex)
            {
                Log.Write(CustomManager.GetString("Notification_Error_FileExportFail"), ex);
            }
        }
        void Click_UI_TxT_CSV_Cat_Exportport(object sender, RoutedEventArgs e)
        {
            SharedUIActions.UI_TxT_CSV_Cat_Exportport(Model.MainObject);
        }
        void Click_Repair(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.MainObject?.Repair();
            }
            catch (Exception ex)
            {
                Log.Write(CustomManager.GetString("Notification_Error_RepairFail"), ex);
            }
        }
        void Click_OpenFolder(object sender, RoutedEventArgs e)
        {
            SharedIO.CurrentIO.OpenFolder(Model.MainObject.FileInfo.Directory);
        }
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

        #region Fav Stuff
        Button FavButton = null;
        private void FavButton_Loaded(object sender, RoutedEventArgs e)
        {
            FavButton = sender as Button;
        }
        void FavListItemClick(object sender, ItemClickEventArgs e)
        {
            Model.PendingScrollEntry = e.ClickedItem as Thing;
            (sender as ListView).SelectedItem = null;
            FavButton.Flyout.Hide();

        }
        #endregion
        IEnumerable<CategoryOption> LokalCategoryOptions => Model.MainObject.Settings.CategoryOptions;
        void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            switch (args.Reason)
            {
                case AutoSuggestionBoxTextChangeReason.UserInput:
                    (sender as AutoSuggestBox).ItemsSource = Model.MainObject.ThingList.Where(x => LokalCategoryOptions.First(y => y.ThingType == x.ThingType).Visibility).Where(x => x.SimilaritiesTo(sender.Text) > 0).OrderByDescending(x => x.SimilaritiesTo(sender.Text));
                    break;
                case AutoSuggestionBoxTextChangeReason.ProgrammaticChange:
                    break;
                case AutoSuggestionBoxTextChangeReason.SuggestionChosen:
                default:
                    break;
            }
        }

        void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                if (args.ChosenSuggestion != null)
                {
                    Model.PendingScrollEntry = (args.ChosenSuggestion as Thing);
                }
                else
                {
                    Model.PendingScrollEntry = (sender.ItemsSource as IOrderedEnumerable<Thing>).FirstOrDefault();

                    Model.PendingScrollEntry = Model.MainObject.ThingList.Where(x => LokalCategoryOptions.First(y => y.ThingType == x.ThingType).Visibility).OrderByDescending(x => x.SimilaritiesTo(sender.Text)).FirstOrDefault();
                }
                sender.ItemsSource = null;
                if (Model.PendingScrollEntry == null)
                {
                    return;
                }
            }
            catch { return; }
            sender.Text = "";
            sender.IsSuggestionListOpen = false;
        }
    }
}
