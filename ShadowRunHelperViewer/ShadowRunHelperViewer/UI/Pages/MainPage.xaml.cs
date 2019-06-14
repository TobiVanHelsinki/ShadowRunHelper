using PCLStorage;
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
            NavigatoToSingleInstanceOf<AdministrationPage>();
            TLIB.Log.NewLogArrived += Log_NewLogArrived;
        }

        private void Instance_NavigationRequested(ShadowRunHelper.ProjectPages page, ShadowRunHelper.ProjectPagesOptions PageOptions)
        {
            switch (page)
            {
                case ShadowRunHelper.ProjectPages.undef:
                    break;
                case ShadowRunHelper.ProjectPages.Char:
                    if (AppModel.Instance.MainObject is CharHolder ch)
                    {
                        NavigatoToSingleInstanceOf<CharPage>(true, (x) => x.Activate(ch)); // TODO Neccesary because of activation through file double click through AppHolder
                    }
                    else
                    {
                        NavigatoToSingleInstanceOf<AdministrationPage>();
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

        void CharPage(object sender, System.EventArgs e) => NavigatoToSingleInstanceOf<CharPage>(true, (x) => x.Activate(CharHolderGenerator.TestAllCats(3)));
        void Administration(object sender, EventArgs e) => NavigatoToSingleInstanceOf<AdministrationPage>();
        void Settings(object sender, EventArgs e) => NavigatoToSingleInstanceOf<SettingsPage>();

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
                Task.Run(() => { return new T(); }).ContinueWith((t) =>
                {
                    try
                    {
                        if (t.IsCompleted)
                        {
                            Device.BeginInvokeOnMainThread(() => { afterLoad?.Invoke(t.Result); ContentPlace.Content = t.Result; });
                        }
                    }
                    catch (Exception)
                    {
                        if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
                    }
                });
            }
            catch (Exception)
            {
                if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
            }
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
            if (ContentPlace.Content is CharPage CP)
            {
                CP.OnBackButtonPressed();
            }
            return base.OnBackButtonPressed();
        }

        #region DEBUG
        void Decarrot(object sender, EventArgs e)
        {
        }

        async void Debug(object sender, EventArgs e)
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
        #endregion

        private void Log_NewLogArrived(TLIB.LogMessage logmessage)
        {
            LogButton.IsVisible = true;
            LogView.Text = TLIB.Log.InMemoryLog.Reverse<string>().Aggregate((a, c) => a += Environment.NewLine + c);
        }

        private void ShowLog(object sender, EventArgs e)
        {
            LogView.IsVisible = !LogView.IsVisible;
        }
    }
}