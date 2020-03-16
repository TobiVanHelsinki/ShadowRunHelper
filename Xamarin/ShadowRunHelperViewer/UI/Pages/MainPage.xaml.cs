//Author: Tobi van Helsinki

using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using ShadowRunHelper;
using ShadowRunHelper.Model;
using Syncfusion.SfNavigationDrawer.XForms;
using TAPPLICATION.IO;
using TLIB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Pages
{
    public enum ViewModes
    {
        NotSet, Wide, Tall, Mobile
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public static MainPage Instance;

        #region ViewMode

        public delegate void ViewModeChangedEventHandler(ViewModes oldMode, ViewModes newMode, double leftTopSpacing);
        public event ViewModeChangedEventHandler ViewModeChanged;

        private ViewModes _CurrentViewMode;
        public ViewModes CurrentViewMode
        {
            get => _CurrentViewMode;
            set { if (_CurrentViewMode != value) { ViewModeChanged?.Invoke(_CurrentViewMode, value, HamburgerFrame.Width); _CurrentViewMode = value; } }
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

        private void Instance_NavigationRequested(ProjectPages page, ProjectPagesOptions PageOptions = ProjectPagesOptions.Nothing)
        {
            switch (page)
            {
                case ProjectPages.undef:
                    break;
                case ProjectPages.Char:
                    if (AppModel.Instance?.MainObject is CharHolder ch)
                    {
                        NavigatoToSingleInstanceOf<CharPage>(true, (x) => x.Activate(ch));
                    }
                    else
                    {
                        goto Administration;
                    }
                    break;
                case ProjectPages.Administration:
                Administration:
                    NavigatoToSingleInstanceOf<AdministrationPage>(false, (x) => x?.Activate());
                    break;
                case ProjectPages.Settings:
                    NavigatoToSingleInstanceOf<MiscPage>(false, (x) => x.AfterLoad());
                    break;
                default:
                    break;
            }
        }

        private void CharPage(object sender, System.EventArgs e)
        {
            AppModel.Instance.RequestNavigation(ProjectPages.Char);
        }

        private void Administration(object sender, EventArgs e)
        {
            AppModel.Instance.RequestNavigation(ProjectPages.Administration);
        }

        private void Settings(object sender, EventArgs e)
        {
            AppModel.Instance.RequestNavigation(ProjectPages.Settings);
        }

        /// <summary>
        /// NavigatoToSingleInstanceOf
        /// </summary>
        /// <param name="slpash"></param>
        /// <param name="afterLoad">a action that is performed at the ui threat</param>
        /// <exception cref="ObjectDisposedException"></exception>
        public void NavigatoToSingleInstanceOf<T>(bool slpash = false, Action<T> afterLoad = null) where T : View, new()
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
            set { if (_IsMenuOpen != value) { _IsMenuOpen = value; MenuDrawerDefault.IsOpen = IsMenuOpen; ViewModeChanged?.Invoke(CurrentViewMode, CurrentViewMode, HamburgerFrame.Width); } }
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

        private void ToggleMenu(object sender, EventArgs e) => IsMenuOpen = !IsMenuOpen;

        #endregion Menu

        #region Char Menu

        private void Char_Save(object sender, EventArgs e)
        {
            AppModel.Instance?.MainObject?.SetSaveTimerTo(0, true);
        }

        private void Char_SaveIntern(object sender, EventArgs e)
        {
            SharedIO.SaveAtCurrentPlace(AppModel.Instance?.MainObject);
        }

        private async void Char_SaveExtern(object sender, EventArgs e)
        {
            SharedIO.Save(AppModel.Instance?.MainObject, new FileInfo(Path.Combine((await SharedIO.CurrentIO.PickFolder()).FullName, AppModel.Instance?.MainObject?.FileInfo.Name)));
        }

        private void Char_OpenFolder(object sender, EventArgs e)
        {
            SharedIO.CurrentIO.OpenFolder(AppModel.Instance?.MainObject?.FileInfo.Directory);
        }

        private void Char_SubtractLifeStyleCost(object sender, EventArgs e)
        {
            AppModel.Instance?.MainObject?.SubtractLifeStyleCost();
        }

        private void Char_CharSettings(object sender, EventArgs e)
        {
            // TODO Display a full screen element with navigation
        }

        private void Char_Repair(object sender, EventArgs e)
        {
            AppModel.Instance?.MainObject?.Repair();
        }

        private void Char_Unload(object sender, EventArgs e)
        {
            AppModel.Instance?.RemoveMainObject(AppModel.Instance?.MainObject);
            AppModel.Instance.RequestNavigation(ProjectPages.Administration);
        }
        #endregion Char Menu
    }
}