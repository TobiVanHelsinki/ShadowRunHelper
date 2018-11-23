using Microsoft.AppCenter.Analytics;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

    
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                var Page = MainNavigation.Items.FirstOrDefault(x => (x as PivotItem).Tag.ToString() == ((int)e.Parameter).ToString());
                if (Page != null)
                {
                    MainNavigation.SelectedItem = Page;
                }
            }
            catch (Exception)
 { TAPPLICATION.Debugging.TraceException();
            }
            if (Settings.IAP_PREMIUM_BADGE)
            {
                premiumbadge.Visibility = Visibility.Visible;
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
 { TAPPLICATION.Debugging.TraceException();
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
 { TAPPLICATION.Debugging.TraceException();
                    }
                }
                catch (Exception)
 { TAPPLICATION.Debugging.TraceException();
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
        #region Buying Stuff

        async void IAP_ADS(object sender, RoutedEventArgs e)
        {
            await Features.IAP.Buy(Constants.IAP_FEATUREID_ADFREE);
        }

        async void IAP_ADS_365(object sender, RoutedEventArgs e)
        {
            await Features.IAP.Buy(Constants.IAP_FEATUREID_ADFREE_365);
        }

        async void IAP_Tee(object sender, RoutedEventArgs e)
        {
            await Features.IAP.Buy(Constants.IAP_FEATUREID_TEE);
        }
        #endregion
        void MainNavigation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((MainNavigation.SelectedItem as PivotItem).Tag)
            {
                case "31":
                    Features.Analytics.TrackEvent("Nav_Sett_Info");
                    break;
                case "32":
                    Features.Analytics.TrackEvent("Nav_Sett_Sett");
                    break;
                case "34":
                    Features.Analytics.TrackEvent("Nav_Sett_Help");
                    break;
                case "36":
                    Features.Analytics.TrackEvent("Nav_Sett_Buy");
                    break;
                case "35":
                    Features.Analytics.TrackEvent("Nav_Sett_Not");
                    break;
                default:
                    break;
            }
        }
    }
}
