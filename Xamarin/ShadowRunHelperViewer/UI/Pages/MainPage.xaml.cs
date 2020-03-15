//Author: Tobi van Helsinki

using System;
using ShadowRunHelper;
using ShadowRunHelper.Model;
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

        public static async void Test()
        {
            var answere = await Instance.DisplayActionSheet("Titleee", "No!", "Kaboom", "Ice", "Tea", "shark");
            Log.Write(answere, true);
        }

        #region ViewMode
        public event EventHandler ViewModeChanged;

        private ViewModes _CurrentViewMode;
        public ViewModes CurrentViewMode
        {
            get => _CurrentViewMode;
            set { if (_CurrentViewMode != value) { _CurrentViewMode = value; ViewModeChanged?.Invoke(this, new EventArgs()); } }
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
            IsMenuOpen = Width > UIConstants.MinWidthDesktop;
        }
        #endregion ViewMode

        public MainPage()
        {
            AppModel.Instance.NavigationRequested += Instance_NavigationRequested;
            InitializeComponent();
            AppModel.Instance.PropertyChanged += Instance_PropertyChanged;
            Log.DisplayMessageRequested += Log_DisplayMessageRequested;
            Instance = this;
            ViewModeChanged += MainPage_ViewModeChanged;
        }

        private void Log_DisplayMessageRequested(LogMessage logmessage)
        {
            DisplayAlert(logmessage.LogType.ToString(), logmessage.Message, "OK");
        }

        private void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Features.Ui.DisplayCurrentCharName();
        }

        private void Instance_NavigationRequested(ProjectPages page, ProjectPagesOptions PageOptions = ProjectPagesOptions.Nothing)
        {
            switch (page)
            {
                case ProjectPages.undef:
                    break;
                case ProjectPages.Char:
                    if (AppModel.Instance.MainObject is CharHolder ch)
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

        private void SetWaitingContent()
        {
            ContentPlace.Content = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = "Hey, here you can read a tipp",
            };
        }

        protected override bool OnBackButtonPressed()
        {
            if (ContentPlace.Content is CharPage page && page.OnBackButtonPressed())
            {
                return true;
            }
            else
            {
                return base.OnBackButtonPressed();
            }
        }

        #region Menu
        private bool _IsMenuOpen;
        public bool IsMenuOpen
        {
            get => _IsMenuOpen;
            set { _IsMenuOpen = value; SetMenuAppearence(); }
        }

        private void SetMenuAppearence()
        {
            switch (CurrentViewMode)
            {
                case ViewModes.Mobile://Floating Menu
                case ViewModes.Tall:
                    Grid.SetColumnSpan(ContentPlace, 2);
                    Grid.SetColumn(ContentPlace, 0);
                    break;
                case ViewModes.NotSet:
                case ViewModes.Wide:
                default:
                    //Splitmenu
                    Grid.SetColumnSpan(ContentPlace, 1);
                    Grid.SetColumn(ContentPlace, 1);
                    break;
            }
            if (IsMenuOpen)
            {
                MainMenu.IsVisible = true;
                MenuColumn.Width = new GridLength(1, GridUnitType.Auto);
            }
            else
            {
                MainMenu.IsVisible = false;
                MenuColumn.Width = new GridLength(0, GridUnitType.Absolute);
            }
        }

        private void MainPage_ViewModeChanged(object sender, EventArgs e) => SetMenuAppearence();

        private void ToggleMenu(object sender, EventArgs e) => IsMenuOpen = !IsMenuOpen;

        #endregion Menu

        private int i = 10;

        private void Button_Clicked(object sender, EventArgs e)
        {
            MenuColumn.Width = new GridLength(i, GridUnitType.Absolute);
            i += 10;
        }
    }
}