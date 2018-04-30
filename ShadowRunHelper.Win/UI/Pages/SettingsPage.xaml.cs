using Microsoft.AppCenter.Analytics;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TAMARIN.IO;
using TAPPLICATION;
using TAPPLICATION.IO;
using TAPPLICATION.Model;
using TLIB;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper.UI
{
    public sealed partial class SettingsPage : Page
    {
        readonly SettingsModel Settings = SettingsModel.Instance;
        readonly AppModel Model = AppModel.Instance;
        readonly string AppKontaktEmail = Constants.APP_PUBLISHER_MAILTO ?? "" ;
        readonly string Inhaber = Constants.APP_PUBLISHER ?? "" ;
        readonly string AppVersionBuild = Constants.APP_VERSION_BUILD_DELIM ?? "" ;
        readonly string AppReviewLink = Constants.APP_STORE_REVIEW_LINK ?? "" ;
        readonly string eMail = Constants.APP_PUBLISHER_MAIL ?? "" ;
        readonly string MoreAppsLink = Constants.APP_MORE_APPS ?? "" ;
        readonly string AppLink = Constants.APP_STORE_LINK ?? "" ;
        readonly List<HelpEntry> Help = Constants.HelpList;

        public SettingsPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        void CheckIAP()
        {
            if (!Constants.IAP_HIDEADS)
            {
                Ad_MainPageRight.ApplicationId = Constants.AD_APPID;
                Ad_MainPageBottom.ApplicationId = Constants.AD_APPID;
                Ad_MainPageRight.AdUnitId = Constants.AD_ADID_MainPageRight;
                Ad_MainPageBottom.AdUnitId = Constants.AD_ADID_MainPageBottom;
            }
            else
            {
                Ad_MainPageRight.Visibility = Visibility.Collapsed;
                Ad_MainPageRight.Width = 0;
                Ad_MainPageBottom.Visibility = Visibility.Collapsed;
                Ad_MainPageBottom.Height = 0;
                Ad_MainPageRightBox.Visibility = Visibility.Collapsed;
                Ad_MainPageBottomBox.Visibility = Visibility.Collapsed;
                Trigger_Ads.States.Remove(Trigger_Ads.States[0]);
                Trigger_Ads.States.Remove(Trigger_Ads.States[0]);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CheckIAP();

            switch ((ProjectPagesOptions)e.Parameter)
            {
                case ProjectPagesOptions.SettingsMain:
                case ProjectPagesOptions.SettingsOptions:
                case ProjectPagesOptions.SettingsHelp:
                    MainNavigation.SelectedItem = MainNavigation.Items.FirstOrDefault(
                        x => (x as PivotItem).Tag.ToString() == ((int)e.Parameter).ToString());
                    break;
                default:
                    break;
            }
            base.OnNavigatedTo(e);  
        }

        #region Gui Stuff ##########################################
        /// <summary>
        /// Select the right Notification Template
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.RemovedItems)
            {
                try
                {
                    ((ListViewItem)(sender as ListView).ContainerFromItem(item)).ContentTemplate = Notification;
                }
                catch (Exception)
                {
                }
            }
            foreach (var item in e.AddedItems)
            {
                try
                {
                    ListViewItem o = ((ListViewItem)(sender as ListView).ContainerFromItem(item));
                    try
                    {
                        o.ContentTemplate = NotificationX;
                    }
                    catch (Exception)
                    {
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        /// <summary>
        /// To Show the right Exception template at notification listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentControl_Loaded(object sender, RoutedEventArgs e)
        {
            if ((sender as ContentControl).DataContext != null)
            {
                (sender as ContentControl).ContentTemplate = ExceptionTemplate;
            }
        }
        #endregion
        #region Additional Settings Stuff ##########################################
        /// <summary>
        /// use to prevent the "Toggled-Efect" from beeing enebled at startup
        /// </summary>
        bool FolderMode_HasFocus = false;
        void FolderMode_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FolderMode_HasFocus)
            {
                return;
            }
            FolderMode_HasFocus = true;
        }
        async void FolderMode_Toggled(object sender, RoutedEventArgs e)
        {
            if (((ToggleSwitch)sender).IsOn && FolderMode_HasFocus)
            {
                try
                {
                    SharedSettingsModel.I.ORDNERMODE_PFAD = (await UwpIO.GetFolder(new FileInfoClass() { Fileplace = Place.Extern, FolderToken = Constants.ACCESSTOKEN_FOLDERMODE }, UserDecision.AskUser)).Path;
                }
                catch (Exception) { }
            }
            FolderMode_HasFocus = true;
        }

        /// <summary>
        /// use to prevent the "Toggled-Efect" from beeing enebled at startup
        /// </summary>
        bool Intern_Sync_HasFocus = false;
        
        private void Intern_Sync_GotFocus(object sender, RoutedEventArgs e)
        {
            Intern_Sync_HasFocus = true;
        }
        private async void Intern_Sync_Toggled(object sender, RoutedEventArgs e)
        {
            if (Intern_Sync_HasFocus)
            {
                try
                {
                    var t = new FileInfoClass((sender as ToggleSwitch).IsOn ? Place.Roaming : Place.Local, "", SharedConstants.INTERN_SAVE_CONTAINER);
                    var s = new FileInfoClass((sender as ToggleSwitch).IsOn ? Place.Local : Place.Roaming, "", SharedConstants.INTERN_SAVE_CONTAINER);
                    //await SharedIO.CopyLocalRoaming((sender as ToggleSwitch).IsOn ? Place.Roaming : Place.Local);
                    await SharedIO.CurrentIO.CopyAllFiles(t,s);
                }
                catch (Exception ex)
                {
                    AppModel.Instance.NewNotification(StringHelper.GetString("Notification_Error_SwitchingInterFolder"), ex);
                }
            }
            Intern_Sync_HasFocus = false;
        }
        #endregion
        #region Buying Stuff

        async void IAP_ADS(object sender, RoutedEventArgs e)
        {
            await IAP.Buy(Constants.IAP_FEATUREID_ADFREE);
            CheckIAP();
        }

        async void IAP_ADS_365(object sender, RoutedEventArgs e)
        {
            await IAP.Buy(Constants.IAP_FEATUREID_ADFREE_365);
            CheckIAP();

        }

        async void IAP_Tee(object sender, RoutedEventArgs e)
        {
            await IAP.Buy(Constants.IAP_FEATUREID_TEE);
            CheckIAP();
        }
        #endregion
        #region Analytics

        #endregion
        void MainNavigation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((MainNavigation.SelectedItem as PivotItem).Tag)
            {
                case "31":
                    Analytics.TrackEvent("Nav_Sett_Info");
                    break;
                case "32":
                    Analytics.TrackEvent("Nav_Sett_Sett");
                    break;
                case "34":
                    Analytics.TrackEvent("Nav_Sett_Help");
                    break;
                case "36":
                    Analytics.TrackEvent("Nav_Sett_Buy");
                    break;
                case "35":
                    Analytics.TrackEvent("Nav_Sett_Not");
                    break;
                default:
                    break;
            }
        }
    }
}
