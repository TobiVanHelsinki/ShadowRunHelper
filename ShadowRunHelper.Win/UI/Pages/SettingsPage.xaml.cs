using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TLIB_UWPFRAME;
using TLIB_UWPFRAME.IO;
using TLIB_UWPFRAME.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRunHelper
{
    class CharPageConfigListViewItem
    {
        public string Name
        {
            get; set;
        }
        public ThingDefs Code
        {
            get; set;
        }
        public bool Active
        {
            get;
            set;
        }
    }
    /// <summary>
    /// Eine leere Seite, die eigenstaendig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {

        readonly SettingsModel Settings = SettingsModel.Instance;
        readonly AppModel Model = AppModel.Instance;
        readonly string eMail = Constants.APP_CONTACT_MAIL;
        readonly string Inhaber = Constants.AUTHOR;
        readonly string AppVersionBuild = Constants.APP_VERSION_BUILD_DELIM;
        readonly string AppReviewLink = Constants.APP_STORE_REVIEW_LINK;
        readonly string AppKontaktEmail = Constants.APP_CONTACT_MAILTO;
        readonly string MoreAppsLink = Constants.APP_MORE_APPS;
        readonly string AppLink = Constants.APP_STORE_LINK;
        readonly List<HelpEntry> Help = Constants.HelpList;

        public SettingsPage()
        {
            InitializeComponent();
            CharPageConfigListView.ItemsSource = Settings.BlockListOptions.Select(i => new CharPageConfigListViewItem() { Active = i.vis, Code = i.ThingType, Name = TypenHelper.ThingDefToString(i.ThingType, true) }).Where(x=>x.Name != "");
        }

        void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Settings.BlockListOptions = CharPageConfigListView.ItemsPanelRoot.Children.Select(i =>
                (((i as ListViewItem).Content as CharPageConfigListViewItem).Code,((i as ListViewItem).Content as CharPageConfigListViewItem).Active)
            );
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
                    SharedSettingsModel.I.ORDNERMODE_PFAD = (await WinIO.GetFolder(new FileInfoClass() { Fileplace = Place.Extern, FolderToken = Constants.ACCESSTOKEN_FOLDERMODE }, UserDecision.AskUser)).Path;
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
                    await SharedIO.CopyLocalRoaming((sender as ToggleSwitch).IsOn ? Place.Roaming : Place.Local);
                }
                catch (Exception ex)
                {
                    AppModel.Instance.NewNotification(CrossPlatformHelper.GetString("Notification_Error_SwitchingInterFolder"), ex);
                }
            }
            Intern_Sync_HasFocus = false;
        }
        #endregion

    }
}
