//Author: Tobi van Helsinki

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ShadowRunHelper;
using ShadowRunHelper.Model;
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
        }
        #endregion ViewMode

        protected override bool OnBackButtonPressed()
        {
            if (ContentPlace.Content is CharPage page)
            {
                return page.OnBackButtonPressed();
            }
            else
            {
                return base.OnBackButtonPressed();
            }
        }

        public MainPage()
        {
            AppModel.Instance.NavigationRequested += Instance_NavigationRequested;
            InitializeComponent();
            AppModel.Instance.PropertyChanged += Instance_PropertyChanged;
            Log.DisplayMessageRequested += Log_DisplayMessageRequested;
            Instance = this;
            BindingContext = this;
            Features.Ui.CustomTitleBarChanges += CustomTitleBarChanges;
        }

        private void CustomTitleBarChanges(double LeftSpace, double RigthSpace, double Heigth)
        {
            MenuDrawerHeader.Margin = new Thickness(0, Heigth, 0, 0);
        }

        private void Log_DisplayMessageRequested(LogMessage logmessage)
        {
            DisplayAlert(logmessage.LogType.ToString(), logmessage.Message, "OK");
        }

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Features.Ui.DisplayCurrentCharName();
        }

        private void SetWaitingContent()
        {
            ContentPlace.Content = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = "Hey, here you can read a tipp",
            };
        }

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
                        NavigatoToSingleInstanceOf<CharPage>(true, (x) => SetSubMenuItems(x.Activate(pageOptions, ch)));
                    }
                    else
                    {
                        goto Administration;
                    }
                    break;
                case ProjectPages.Administration:
                Administration:
                    NavigatoToSingleInstanceOf<AdministrationPage>(false, (x) => SetSubMenuItems(x?.Activate(pageOptions)));
                    break;
                case ProjectPages.Settings:
                    NavigatoToSingleInstanceOf<MiscPage>(false, (x) => SetSubMenuItems(x.AfterLoad(pageOptions)));
                    break;
                default:
                    break;
            }
        }

        private void Nav_CharPage(object sender, System.EventArgs e)
        {
            AppModel.Instance.RequestNavigation(ProjectPages.Char);
        }

        private void Nav_Admin(object sender, EventArgs e)
        {
            AppModel.Instance.RequestNavigation(ProjectPages.Administration);
        }

        private void Nav_Settings(object sender, EventArgs e)
        {
            AppModel.Instance.RequestNavigation(ProjectPages.Settings, ProjectPagesOptions.SettingsOptions);
        }

        private void Nav_Info(object sender, EventArgs e)
        {
            AppModel.Instance.RequestNavigation(ProjectPages.Settings, ProjectPagesOptions.SettingsMain);
        }

        /// <summary>
        /// NavigatoToSingleInstanceOf
        /// </summary>
        /// <param name="slpash"></param>
        /// <param name="afterLoad">a action that is performed at the ui threat</param>
        /// <exception cref="ObjectDisposedException"></exception>
        private void NavigatoToSingleInstanceOf<T>(bool slpash = false, Action<T> afterLoad = null) where T : View, new()
        {
            if (ContentPlace.Content is IDisposable disposable)
            {
                disposable.Dispose();
            }
            ContentPlace.Content = null;
            if (slpash)
            {
                SetWaitingContent();
            }
            try
            {
                var t = new T();
                ContentPlace.Content = t;
                afterLoad?.Invoke(t);
            }
            catch (Exception ex)
            {
                Log.Write("Could not Set Content", ex, logType: LogType.Error);
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }
            }
            //Task.Run(() =>
            //{
            //    try
            //    {
            //        return new T();
            //    }
            //    catch (Exception)
            //    {
            //        return null;
            //    }
            //}).ContinueWith((t) =>
            //{
            //    if (t.IsCompleted)
            //    {
            //        Device.BeginInvokeOnMainThread(() =>
            //        {
            //            try
            //            {
            //                ContentPlace.Content = t.Result;
            //                afterLoad?.Invoke(t.Result);
            //            }
            //            catch (Exception ex)
            //            {
            //                Log.Write("Could not Set Content", ex, logType: LogType.Error);
            //                if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
            //            }
            //        });
            //    }
            //});
        }
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