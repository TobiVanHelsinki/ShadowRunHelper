//Author: Tobi van Helsinki

using ShadowRunHelper;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.Platform;
using SharedCode.Ressourcen;
using System;
using System.Collections.Generic;
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
            SettingsModel.I.PropertyChanged += Settings_PropertyChanged;

            Features.Ui.CustomTitleBarChanges += CustomTitleBarChanges;
        }

        /// <summary>
        /// When the offline warning is tapped, this method changes the setting for foldermode;
        /// triggering the user to choose a path.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OfflineFolderChooser(object sender, EventArgs e)
        {
            SettingsModel.I.FOLDERMODE_PATH = "";
            SettingsModel.I.FOLDERMODE = true;
        }

        /// <summary>
        /// if the settings of the folder containing save data are changed while this page is open,
        /// the list needs to get refreshed. this is the case when the user dismisses the offline warning
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="PropertyChangedEventArgs"/> instance containing the event data.
        /// </param>
        private async void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SettingsModel.I.FOLDERMODE) || e.PropertyName == nameof(SettingsModel.I.FOLDERMODE_PATH))
            {
                await RefreshCharList();
            }
        }

        #region Char List

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
        #endregion Char List

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
                    AppModel.Instance.MainObject = (newchar);
                    AppModel.Instance.RequestNavigation(ProjectPages.Char);
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
                await SharedIO.SaveAtCurrentPlace(newchar);
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
                AppModel.Instance.MainObject = (newchar);
                AppModel.Instance.RequestNavigation(ProjectPages.Char);
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

        public IEnumerable<SubMenuAction> Activate(ProjectPagesOptions pageOptions)
        {
            Features.Ui.IsCustomTitleBarEnabled = true; //TODO Dispse?
            Features.Ui.SetCustomTitleBar(DependencyService.Get<IFormsInteractions>().GetRenderer(TitleBar));
            Features.Ui.CustomTitleBarChanges += CustomTitleBarChanges; //TODO Dispose
            Features.Ui.TriggerCustomTitleBarChanges();
            return new[] {
                    new SubMenuAction(UiResources.NewChar,"\xf234",new Command(()=>NewChar_Clicked(this, new EventArgs()))),
                    new SubMenuAction(UiResources.ImportChar,"\xf56f",new Command(()=>OpenFile(this, new EventArgs()))),
                    new SubMenuAction(UiResources.CreateExampleChar,"\xf501",new Command(()=>ExampleChar_Clicked(this, new EventArgs()))),
                };
        }

        private void CustomTitleBarChanges(double LeftSpace, double RigthSpace, double Heigth)
        {
            TitleBar.MinimumHeightRequest = Heigth;
            Intro1Text.Margin = new Thickness(Math.Abs(LeftSpace), 0, /*Math.Abs(RigthSpace)*/5, 0);
            Intro2Text.Margin = new Thickness(Math.Abs(LeftSpace), 0, /*Math.Abs(RigthSpace)*/5, 0);
        }

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
        #endregion Design
    }
}