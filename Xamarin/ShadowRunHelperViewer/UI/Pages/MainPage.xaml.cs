using PCLStorage;
using ShadowRunHelper;
using ShadowRunHelper.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
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
            AppModel.Instance.RequestNavigation(ProjectPages.Administration);
            TLIB.Log.NewLogArrived += Log_NewLogArrived;
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
                case ShadowRunHelper.ProjectPages.undef:
                    break;
                case ShadowRunHelper.ProjectPages.Char:
                    if (AppModel.Instance.MainObject is CharHolder ch)
                    {
                        NavigatoToSingleInstanceOf<CharPage>(true, (x) => x.Activate(ch));
                    }
                    else
                    {
                        NavigatoToSingleInstanceOf<AdministrationPage>(false, (x) => x.Activate());
                    }
                    break;
                case ShadowRunHelper.ProjectPages.Administration:
                    NavigatoToSingleInstanceOf<AdministrationPage>();
                    break;
                case ShadowRunHelper.ProjectPages.Settings:
                    NavigatoToSingleInstanceOf<SettingsPage>();
                    break;
                default:
                    break;
            }
        }

        void CharPage(object sender, System.EventArgs e) => AppModel.Instance.RequestNavigation(ProjectPages.Char);

        void Administration(object sender, EventArgs e) => AppModel.Instance.RequestNavigation(ProjectPages.Administration);
        void Settings(object sender, EventArgs e) => AppModel.Instance.RequestNavigation(ProjectPages.Settings);

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

            Task.Run(() =>
            {
                try
                {
                    return new T();
                }
                catch (Exception)
                {
                    return null;
                }
            }).ContinueWith((t) =>
            {
                if (t.IsCompleted)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        try
                        {
                            ContentPlace.Content = t.Result;
                            afterLoad?.Invoke(t.Result);
                        }
                        catch (Exception)
                        {
                            if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
                        }
                    });
                }
            });
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

        #region DEBUG
        void Decarrot(object sender, EventArgs e)
        {
#if DEBUG
            //sssss
#endif
        }

        async void Debug(object sender, EventArgs e)
        {
#if DEBUG
            try
            {
                var rootFolder = FileSystem.Current.RoamingStorage;
                foreach (var item in (await rootFolder.GetFilesAsync()))
                {
                    System.Console.WriteLine(item.Path);
                }
                var folder = await rootFolder.CreateFolderAsync("MySubFolder", CreationCollisionOption.OpenIfExists);
                var file = await folder.CreateFileAsync("answer.txt", CreationCollisionOption.ReplaceExisting);
                await file.WriteAllTextAsync("42");
            }
            catch (Exception)
            {
            }
#endif

        }
        #endregion

        private void Log_NewLogArrived(TLIB.LogMessage logmessage)
        {
            TAPPLICATION.PlatformHelper.ExecuteOnUIThreadAsync(() =>
            {
                //LogButton.IsVisible = true;
                //LogView.Text = TLIB.Log.InMemoryLog.Reverse<string>().Aggregate((a, c) => a += Environment.NewLine + c);
            });
        }

        private void ShowLog(object sender, EventArgs e)
        {
            LogView.IsVisible = !LogView.IsVisible;
        }
    }
}