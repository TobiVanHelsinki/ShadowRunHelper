using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;
using System;
using System.Linq;
using ShadowRunHelper.Model;
using Shared;
using TLIB;
using Windows.UI.Xaml;
using Windows.Foundation.Metadata;
using TAPPLICATION.Model;

namespace ShadowRunHelper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotificationsDialog : ContentDialog
    {
        readonly ObservableCollection<Notification> Notifications = AppModel.Instance.lstNotifications;
        readonly IEnumerable<Notification> NewNotifications = AppModel.Instance.lstNotifications.Where(x=>!x.bIsRead);

        public int NotificationsMax
        {
            get { return NewNotifications.Count(); }
        }
        public int NotificationsUnread
        {
            get { return NewNotifications.Where(x=> !x.bIsRead).Count(); }
        }


        public NotificationsDialog()
        {
            InitializeComponent();
            //Notifications.CollectionChanged += LstNotifications_CollectionChanged;
        }

        private void LstNotifications_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Hide();
#pragma warning disable CS4014
            ShowAsync();
#pragma warning restore CS4014
            //ListView.ItemsSource = lstNotifications.Where((x) => x.bIsRead == false).OrderBy((x) => x.DateTime).Select(item => item.strMessage + "\n\n\n" + item.ThrownException?.Message);
        }

        /// <summary>
        /// Save Selection as new Zusammensetzung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
    
            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }
        private void BtnPrev_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }

        private void BtnNext_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }
        void BtnExit_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Hide();
        }
        #region ApplyNewStyles
        void Button_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.RevealBrush"))
            {
                (sender as Button).Style = (Style)Resources["ButtonRevealStyle"];
            }
        }

        void AppBarButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.RevealBrush"))
            {
                (sender as AppBarButton).Style = (Style)Resources["AppBarButtonRevealLabelsOnRightStyle"];
            }
        }
        #endregion


    }

}
