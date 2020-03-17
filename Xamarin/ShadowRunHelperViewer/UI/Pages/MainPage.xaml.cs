﻿//Author: Tobi van Helsinki

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Toast;
using ShadowRunHelper;
using ShadowRunHelper.Model;
using SharedCode.Ressourcen;
using Syncfusion.SfNavigationDrawer.XForms;
using TLIB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Pages
{
    public enum ViewModes
    {
        NotSet, Wide, Tall, Mobile
    }

    public class SubMenuAction
    {
        public SubMenuAction(string text, string icon, ICommand clickHandler)
        {
            Text = text;
            Icon = icon;
            ClickHandler = clickHandler;
        }

        public string Text { get; set; }
        public string Icon { get; set; }
        public ICommand ClickHandler { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public static MainPage Instance;

        #region NotifyPropertyChanged
        public virtual event PropertyChangedEventHandler PropertyChanged;
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                if (!PropertyChanged?.GetInvocationList()?.Contains(value) == true)
                {
                    PropertyChanged += value;
                }
            }
            remove
            {
                if (PropertyChanged?.GetInvocationList()?.Contains(value) == true)
                {
                    PropertyChanged -= value;
                }
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion NotifyPropertyChanged

        public MainPage()
        {
            AppModel.Instance.NavigationRequested += Instance_NavigationRequested;
            InitializeComponent();
            BindingContext = this;
            AppModel.Instance.PropertyChanged += AppModel_PropertyChanged;
            AppModel_PropertyChanged(this, new PropertyChangedEventArgs(""));
            Log.DisplayMessageRequested += Log_DisplayMessageRequested;
            Instance = this;
            Features.Ui.CustomTitleBarChanges += CustomTitleBarChanges;
            DisableBusy();
        }

        public bool OnKeyDown(string key)
        {
            return false;
        }

        public bool ShallExit = false;
        public bool isDoubleBackPressed = false;

        protected override bool OnBackButtonPressed()
        {
            if (isDoubleBackPressed)
            {
                ShallExit = true;
                return true;
            }
            else
            {
                if (ContentPlace.Content is IBackButton page)
                {
                    if (page.OnBackButtonPressed())
                    {
                        return true;
                    }
                }

                isDoubleBackPressed = true;
                CrossToastPopUp.Current.ShowToastMessage(UiResources.TapAgainToExit, Plugin.Toast.Abstractions.ToastLength.Short);
                Device.StartTimer(TimeSpan.FromSeconds(3), () =>
                {
                    isDoubleBackPressed = false;
                    return false;
                });
                return true;
            }
        }

        private void Log_DisplayMessageRequested(LogMessage logmessage)
        {
            DisplayAlert(logmessage.LogType.ToString(), logmessage.Message, "OK");
        }

        private void AppModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NavCharBtn.IsEnabled = AppModel.Instance.MainObject != null;
            Features.Ui.DisplayCurrentCharName();
        }

        #region Design

        private void CustomTitleBarChanges(double LeftSpace, double RigthSpace, double Heigth)
        {
            MenuDrawerHeader.Margin = new Thickness(0, Heigth, 0, 0);
            //TODO auch DrawerHeaderHeight beeinflussen (odersogar "nur"); dann auf mobile testen, ob space weg ist.
        }

        #endregion Design

        #region ViewMode

        public delegate void ViewModeChangedEventHandler(ViewModes oldMode, ViewModes newMode);
        public event ViewModeChangedEventHandler ViewModeChanged;

        private ViewModes _CurrentViewMode;
        public ViewModes CurrentViewMode
        {
            get => _CurrentViewMode;
            set { if (_CurrentViewMode != value) { ViewModeChanged?.Invoke(_CurrentViewMode, value); _CurrentViewMode = value; } }
        }

        private void ContentPage_SizeChanged(object sender, EventArgs e)
        {
            if (Height > UIConstants.MinHeightDesktop && Height > Width)
            {
                CurrentViewMode = ViewModes.Tall;
            }
            else if (Width > UIConstants.MinWidthDesktop && Height <= Width)
            {
                CurrentViewMode = ViewModes.Wide;
            }
            else if (Width <= UIConstants.MinWidthDesktop && Height <= UIConstants.MinHeightDesktop)
            {
                CurrentViewMode = ViewModes.Mobile;
            }
            else
            {
                CurrentViewMode = ViewModes.NotSet;
            }
            IsMenuOpen = false;
            MenuDrawerDefault.TouchThreshold = (float)Width;
            var newSize = (int)Math.Max(Math.Min(Busyindicator.Height, Busyindicator.Width), 50);
            Busyindicator.ViewBoxHeight = newSize;
            Busyindicator.ViewBoxWidth = newSize;
        }
        #endregion ViewMode

        #region BuisyIndicator

        private readonly IEnumerable<Syncfusion.SfBusyIndicator.XForms.AnimationTypes> AllowedAnimationTypes = new[] {
            Syncfusion.SfBusyIndicator.XForms.AnimationTypes.Ball,
            Syncfusion.SfBusyIndicator.XForms.AnimationTypes.Box,
            Syncfusion.SfBusyIndicator.XForms.AnimationTypes.DoubleCircle,
            Syncfusion.SfBusyIndicator.XForms.AnimationTypes.HorizontalPulsingBox,
            Syncfusion.SfBusyIndicator.XForms.AnimationTypes.MovieTimer,
            Syncfusion.SfBusyIndicator.XForms.AnimationTypes.Rectangle,
            Syncfusion.SfBusyIndicator.XForms.AnimationTypes.RollingBall,
            Syncfusion.SfBusyIndicator.XForms.AnimationTypes.SingleCircle,
            Syncfusion.SfBusyIndicator.XForms.AnimationTypes.SlicedCircle,
            Syncfusion.SfBusyIndicator.XForms.AnimationTypes.ZoomingTarget,
        };

        private void ChangeAnimation(object sender, EventArgs e) => Busyindicator.AnimationType = (Syncfusion.SfBusyIndicator.XForms.AnimationTypes)1 + (int)Busyindicator.AnimationType;

        public async Task EnableBusy(bool withTip = true, bool blockUI = true, string tipText = null)
        {
            Busyindicator.AnimationType = AllowedAnimationTypes.RandomElement();
            Busyindicator.IsEnabled = true;
            busyindicatorContainer.IsVisible = true;
            Busyindicator.IsBusy = true;
            Busyindicator.EnableAnimation = true;
            await busyindicatorContainer.FadeTo(100, 250);
            if (withTip)
            {
                TipText.Text = tipText ?? Constants.TipList.RandomElement();
            }
            if (blockUI)
            {
                MenuDrawer.IsEnabled = false;
            }
        }

        public async Task DisableBusy()
        {
            await busyindicatorContainer.FadeTo(0, 1500);
            Busyindicator.IsEnabled = false;
            busyindicatorContainer.IsVisible = false;
            Busyindicator.IsBusy = false;
            Busyindicator.EnableAnimation = false;
            MenuDrawer.IsEnabled = true;
        }
        #endregion BuisyIndicator

        #region Navigation

        private void Instance_NavigationRequested(ProjectPages page, ProjectPagesOptions pageOptions = ProjectPagesOptions.Nothing)
        {
            switch (page)
            {
                case ProjectPages.undef:
                    break;
                case ProjectPages.Char:
                    if (AppModel.Instance?.MainObject is CharHolder ch)
                    {
                        CreateContentAndNavigateTo<CharPage>((x) => SetSubMenuItems(x.Activate(pageOptions, ch)));
                    }
                    else
                    {
                        goto Administration;
                    }
                    break;
                case ProjectPages.Administration:
                Administration:
                    CreateContentAndNavigateTo<AdministrationPage>((x) => SetSubMenuItems(x?.Activate(pageOptions)));
                    ContentPlace.PropertyChanged += ContentPlace_PropertyChanged;
                    break;
                case ProjectPages.Settings:
                    CreateContentAndNavigateTo<MiscPage>((x) => SetSubMenuItems(x.AfterLoad(pageOptions)));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// HACK Workaround tp prevent empty page at android after loading. problem is: the content
        /// get's set to "null" after init
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="PropertyChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void ContentPlace_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ContentView.Content) && ContentPlace.Content == null)
            {
                AppModel.Instance.RequestNavigation(ProjectPages.Administration);
            }
        }

        /// <summary>
        /// NavigatoToSingleInstanceOf
        /// </summary>
        /// <param name="afterLoad">a action that is performed at the ui threat</param>
        /// <exception cref="ObjectDisposedException"></exception>
        private void CreateContentAndNavigateTo<T>(Action<T> afterLoad = null) where T : View, new()
        {
            if (ContentPlace.Content is IDisposable disposable)
            {
                disposable.Dispose();
            }
            ContentPlace.Content = null;
            try
            {
                var t = new T();
                ContentPlace.Content = t;
                afterLoad?.Invoke(t);
            }
            catch (Exception ex)
            {
                Log.Write("Could not Set Content", ex);
            }
        }

        private void Nav_CharPage(object sender, EventArgs e) => AppModel.Instance.RequestNavigation(ProjectPages.Char);

        private void Nav_Admin(object sender, EventArgs e) => AppModel.Instance.RequestNavigation(ProjectPages.Administration);

        private void Nav_Settings(object sender, EventArgs e) => AppModel.Instance.RequestNavigation(ProjectPages.Settings, ProjectPagesOptions.SettingsOptions);

        private void Nav_Info(object sender, EventArgs e) => AppModel.Instance.RequestNavigation(ProjectPages.Settings, ProjectPagesOptions.SettingsMain);
        #endregion Navigation

        #region Menu
        private bool _IsMenuOpen;
        public bool IsMenuOpen
        {
            get => _IsMenuOpen;
            set { if (_IsMenuOpen != value) { _IsMenuOpen = value; MenuDrawerDefault.IsOpen = IsMenuOpen; ViewModeChanged?.Invoke(CurrentViewMode, CurrentViewMode); } }
        }

        /// <summary>
        /// Syncs the IsMenuOpen Property with the drawer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="ObjectDisposedException"></exception>
        private void SfNavigationDrawer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SfNavigationDrawer.IsOpen))
            {
                IsMenuOpen = MenuDrawerDefault.IsOpen;
            }
        }

        private void SfNavigationDrawer_PropertyChanged(object sender, Xamarin.Forms.PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(SfNavigationDrawer.IsOpen))
            {
                IsMenuOpen = MenuDrawerDefault.IsOpen;
            }
        }

        public void ToggleMenu(object sender, EventArgs e)
        {
            IsMenuOpen = !IsMenuOpen;
        }

        private ObservableCollection<SubMenuAction> _SubMenuItems = new ObservableCollection<SubMenuAction>();
        public ObservableCollection<SubMenuAction> SubMenuItems
        {
            get => _SubMenuItems;
            set { if (_SubMenuItems != value) { _SubMenuItems = value; NotifyPropertyChanged(); } }
        }

        private void SetSubMenuItems(IEnumerable<SubMenuAction> actions)
        {
            SubMenuItems.Clear();
            SubMenuItems.AddRange(actions);
        }
        #endregion Menu
    }
}