using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;
using System;
using System.Linq;
using ShadowRunHelper.Model;
using Shared;
using TLIB_UWPFRAME.Resources;
using TLIB_UWPFRAME.Model;

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
            ShowAsync();
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

   

    }
 
}
