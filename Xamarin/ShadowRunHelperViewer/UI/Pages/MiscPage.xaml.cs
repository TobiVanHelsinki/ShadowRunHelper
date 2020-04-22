//Author: Tobi van Helsinki

using ShadowRunHelper;
using ShadowRunHelperViewer.Platform;
using ShadowRunHelperViewer.UI.Resources;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TAPPLICATION;
using ShadowRunHelper.Model;
using System.Collections.Generic;
using System.Windows.Input;
using TLIB;
using System.Collections.ObjectModel;
using SharedCode.Resources;

namespace ShadowRunHelperViewer.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiscPage : ContentView, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public new event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion NotifyPropertyChanged

        public SettingsModel Settings => SettingsModel.Instance;
        public AppModel Model => AppModel.Instance;
        public string AppKontaktEmail => SharedConstants.APP_PUBLISHER_MAILTO;
        public string Inhaber => SharedConstants.APP_PUBLISHER;
        public string AppVersionBuild => SharedConstants.APP_VERSION_BUILD_DELIM;
        public string AppReviewLink => SharedConstants.APP_STORE_REVIEW_LINK;
        public string EMail => SharedConstants.APP_PUBLISHER_MAIL;
        public string MoreAppsLink => SharedConstants.APP_MORE_APPS;
        public string AppLink => SharedConstants.APP_STORE_LINK;
        public List<HelpEntry> Help => Constants.HelpList;
        public ObservableCollection<LogMessage> Logs => Model.lstNotifications;

        public MiscPage()
        {
            Log.Write("some tests");
            InitializeComponent();
            BindingContext = this;
        }
        public ICommand ClickCommand => new Command<string>((url) =>
        {
            _ = Xamarin.Essentials.Launcher.TryOpenAsync(new Uri(url));
            //Device.OpenUri(new System.Uri(url));
        });

        #region Design

        public IEnumerable<SubMenuAction> AfterLoad(ProjectPagesOptions pageOptions)
        {
            Features.Ui.IsCustomTitleBarEnabled = true; //TODO Dispse?
            Features.Ui.SetCustomTitleBar(DependencyService.Get<IFormsInteractions>().GetRenderer(TitleBar));
            Features.Ui.CustomTitleBarChanges += CustomTitleBarChanges; //TODO Dispose
            Features.Ui.TriggerCustomTitleBarChanges();
            switch (pageOptions)
            {
                case ProjectPagesOptions.SettingsHelp:
                    NavigateTo("Help");
                    break;
                case ProjectPagesOptions.SettingsLog:
                    NavigateTo("Log");
                    break;
                case ProjectPagesOptions.SettingsBuy:
                    NavigateTo("Buy");
                    break;
                default:
                    NavigateTo("Infos");
                    break;
            }
            return new[] {
                    new SubMenuAction(UiResources.Options_U_Infos,"\xf129",new Command(()=>NavigateTo("Infos"))),
                    new SubMenuAction(UiResources.Options_U_Buy,"\xf07a",new Command(()=>NavigateTo("Buy"))),
                    new SubMenuAction(UiResources.Options_U_Notifications,"\xf0f3",new Command(()=>NavigateTo("Log"))),
                    new SubMenuAction(UiResources.Options_U_Hilfe,"\xf128",new Command(()=>NavigateTo("Help"))),
                };
        }

        private void CustomTitleBarChanges(double LeftSpace, double RigthSpace, double Heigth)
        {
            TitleBar.MinimumHeightRequest = Heigth;
            Intro1Text.Margin = new Thickness(Math.Abs(LeftSpace), 0, Math.Abs(RigthSpace), 0);
        }
        #endregion Design

        private void NavigateTo(object sender, EventArgs e)
        {
            if (sender is Button b && b.GetValue(Tag.TagProperty) is string s)
            {
                NavigateTo(s);
            }
        }

        private void NavigateTo(string key)
        {
            if (Resources.ContainsKey(key) && Resources[key] is DataTemplate dt)
            {
                Content.Content = dt.CreateContent() as View;
                AfterLoads();
            }
        }

        private void AfterLoads()
        {
            if (Content.Content.FindByName("PremiumBadgeImage") is Image image)
            {
                image.IsVisible = Settings.IAP_PREMIUM_BADGE;
            }
        }
    }
}