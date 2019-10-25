///Author: Tobi van Helsinki

using ShadowRunHelper;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.Platform;
using SharedCode.Ressourcen;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TAPPLICATION.IO;
using TLIB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdministrationPage : ContentView, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public new event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion NotifyPropertyChanged

        public ObservableCollection<ExtendetFileInfo> CharList { get; set; } = new ObservableCollection<ExtendetFileInfo>();

        public AdministrationPage()
        {
            InitializeComponent();
            BindingContext = this;
            RefreshCharList();
        }

        private async void ListView_Refreshing(object sender, EventArgs e)
        {
            if (sender is ListView lv)
            {
                await RefreshCharList();
                lv.EndRefresh();
            }
        }

        private async Task RefreshCharList()
        {
            WarningFrame.IsVisible = !SettingsModel.I.FOLDERMODE;
            try
            {
                var savepathfiles = (await SharedIO.CurrentIO.GetFiles(SharedIO.CurrentSaveDir, Constants.LST_FILETYPES_CHAR)).ToList();
                Device.BeginInvokeOnMainThread(() =>
                {
                    CharList.Clear();
                    CharList.AddRange(savepathfiles);
                });
            }
            catch (Exception ex)
            {
                Log.Write("Error reading directory", ex);
            }
        }

        #region Open Files

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is ExtendetFileInfo charfile)
            {
                try
                {
                    foreach (var item in AppModel.Instance.MainObjects.ToList())
                    {
                        AppModel.Instance.RemoveMainObject(item);
                    }
                    var newchar = await CharHolderIO.Load(charfile);
                    (Application.Current.MainPage as MainPage)?.NavigatoToSingleInstanceOf<CharPage>(true, (x) => x.Activate(newchar));
                }
                catch (Exception ex)
                {
                    Log.Write("Error reading file", ex);
                }
            }
        }

        private void OpenFile(object sender, EventArgs e)
        {
            FilePickerExample();
        }

        private async void FilePickerExample()
        {
            try
            {
                var charfile = await SharedIO.CurrentIO.PickFile(Constants.LST_FILETYPES_CHAR, "NextChar");
                var newchar = await CharHolderIO.Load(charfile);
                AppModel.Instance.AddMainObject(newchar);
                AppModel.Instance.RequestNavigation(ProjectPages.Char);
                SharedIO.SaveAtCurrentPlace(newchar);
                //(Application.Current.MainPage as MainPage)?.NavigatoToSingleInstanceOf<CharPage>(true, (x) => x.Activate(newchar));
                //TODO das ist komplett am model vorbei, aber dafür auch flexibler
                //TODO ich sollte überlegen, Model.MainObject abzuschaffen. für das speichern kann es sich im eigenen konstruktor registrieren
                //Dann bekommen alle anderen nicht mehr mit, wenn sich ein object ändert. aber muss das überhaupt jemand wissen?
            }
            catch (Exception ex)
            {
                Log.Write("Error reading file", ex);
            }
        }
        #endregion Open Files

        #region Other File Action

        private void FileRename(object sender, EventArgs e)
        {
        }

        private void FileCopy(object sender, EventArgs e)
        {
            if (sender is MenuItem v && v.BindingContext is ExtendetFileInfo file)
            {
                try
                {
                    var newName = Path.Combine(Path.GetDirectoryName(file.FullName),
                    Path.GetFileNameWithoutExtension(file.FullName) + "-" + AppResources.CopiedFileName +
                    Path.GetExtension(file.FullName));
                    SharedIO.CurrentIO.CopyTo(file, new FileInfo(newName));
                    RefreshCharList();
                }
                catch (PathTooLongException ex)
                {
                    Log.Write("Path too long", ex, logType: LogType.Error);
                }
                catch (Exception ex2)
                {
                    Log.Write("Error at copy", ex2, logType: LogType.Error);
                }
            }
        }

        private void FileDelete(object sender, EventArgs e)
        {
            if (sender is MenuItem v && v.BindingContext is ExtendetFileInfo file)
            {
                Log.DisplayChoice(UiResources.Delete, UiResources.FileDeleteTip, new Options() { },
                    (UiResources.Yes, async () => { await SharedIO.CurrentIO.RemoveFile(file); RefreshCharList(); }
                ),
                    (UiResources.No, () => { }
                )
                    );
            }
        }

        private async void FileExport(object sender, EventArgs e)
        {
            if (sender is MenuItem v && v.BindingContext is ExtendetFileInfo file)
            {
                try
                {
                    var saveDir = await SharedIO.CurrentIO.PickFolder("ExportChar");
                    await SharedIO.CurrentIO.CopyTo(file, new FileInfo(Path.Combine(saveDir.FullName, Path.GetFileName(file.FullName))));
                }
                catch (PathTooLongException ex)
                {
                    Log.Write("Path too long", ex, logType: LogType.Error);
                }
                catch (Exception ex2)
                {
                    Log.Write("Error at copy", ex2, logType: LogType.Error);
                }
            }
        }
        #endregion Other File Action

        #region Create

        private void NewChar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var newchar = CharHolderGenerator.CreateCharWithStandardContent();
                SettingsModel.I.COUNT_CREATIONS++;
                (Application.Current.MainPage as MainPage)?.NavigatoToSingleInstanceOf<CharPage>(true, (x) => x.Activate(newchar));
            }
            catch (Exception ex)
            {
                Log.Write("Error reading file", ex);
            }
        }

        private async void ExampleChar_Clicked(object sender, EventArgs e)
        {
            try
            {
                CharHolderIO.CopyPreSavedCharToCurrentLocation(CharHolderIO.PreSavedChar.ExampleChar);
            }
            catch (Exception ex)
            {
                Log.Write("Error reading file", ex);
            }
            RefreshCharList();
        }
        #endregion Create

        #region Design

        public void Activate()
        {
            Features.Ui.IsCustomTitleBarEnabled = true; //TODO Dispse?
            Features.Ui.SetCustomTitleBar(DependencyService.Get<IFormsInteractions>().GetRenderer(TitleBar));
            Features.Ui.CustomTitleBarChanges += CustomTitleBarChanges; //TODO Dispose
            Features.Ui.TriggerCustomTitleBarChanges();
        }

        private void CustomTitleBarChanges(double LeftSpace, double RigthSpace, double Heigth)
        {
            TitleBar.MinimumHeightRequest = Heigth;
            Intro1Text.Margin = new Thickness(Math.Abs(LeftSpace), 0, Math.Abs(RigthSpace), 0);
            Intro2Text.Margin = new Thickness(Math.Abs(LeftSpace), 0, Math.Abs(RigthSpace), 0);
        }
        #endregion Design

        #region Responsive Design

        private void TemplateSizeChanged(object sender, EventArgs e)
        {
            if (sender is StackLayout layout)
            {
                var attributes = layout.FindByName<StackLayout>("attributes");
                if (layout.Width < 500)
                {
                    layout.Orientation = StackOrientation.Vertical;
                    attributes.HorizontalOptions = LayoutOptions.Start;
                    layout.Margin = new Thickness(10, 2, 10, 0);
                }
                else
                {
                    layout.Orientation = StackOrientation.Horizontal;
                    attributes.HorizontalOptions = LayoutOptions.EndAndExpand;
                    layout.Margin = new Thickness(10);
                }
            }
        }
        #endregion Responsive Design
    }
}