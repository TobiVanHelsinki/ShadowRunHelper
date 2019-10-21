///Author: Tobi van Helsinki

using PCLStorage;
using ShadowRunHelper;
using ShadowRunHelper.Model;
using System;
using System.Threading.Tasks;
using TLIB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            AppModel.Instance.NavigationRequested += Instance_NavigationRequested;
            InitializeComponent();
            AppModel.Instance.PropertyChanged += Instance_PropertyChanged;
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

        private void CharPage(object sender, System.EventArgs e) => AppModel.Instance.RequestNavigation(ProjectPages.Char);

        private void Administration(object sender, EventArgs e) => AppModel.Instance.RequestNavigation(ProjectPages.Administration);

        private void Settings(object sender, EventArgs e) => AppModel.Instance.RequestNavigation(ProjectPages.Settings);

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
                if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
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
    }
}