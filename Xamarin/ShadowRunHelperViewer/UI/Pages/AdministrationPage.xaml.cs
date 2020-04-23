//Author: Tobi van Helsinki

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ShadowRunHelper;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.Platform;
using ShadowRunHelperViewer.UI.Resources;
using SharedCode.Resources;
using Syncfusion.XForms.PopupLayout;
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
            MainPage.Instance.EnableBusy();
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
            if (!SettingsModel.I.FOLDERMODE && e.PropertyName == nameof(SettingsModel.I.FOLDERMODE) || e.PropertyName == nameof(SettingsModel.I.FOLDERMODE_PATH))
            {
                await RefreshCharList();
                MainPage.Instance.DisableBusy();
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
                    CharList.AddRange(savepathfiles.OrderByDescending(x => x.LastAccessTime));
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
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        LoadFileInBackgroundSynchron(charfile);
                        break;
                    default:
                        await LoadFileInBackgroundAsync(charfile);
                        break;
                }
            }
        }

        private async void OpenFile(object sender, EventArgs e)
        {
            var charfile = await SharedIO.CurrentIO.PickFile(Constants.LST_FILETYPES_CHAR, "NextChar");
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    try
                    {
                        LoadFileInBackgroundSynchron(new ExtendetFileInfo(charfile.FullName), (newchar) => SharedIO.SaveAtCurrentPlace(newchar));
                    }
                    catch (Exception ex)
                    {
                        Log.Write("Error reading file", ex);
                    }
                    break;
                default:
                    try
                    {
                        await LoadFileInBackgroundAsync(new ExtendetFileInfo(charfile.FullName), (newchar) => SharedIO.SaveAtCurrentPlace(newchar));
                    }
                    catch (Exception ex)
                    {
                        Log.Write("Error reading file", ex);
                    }
                    break;
            }
        }

        private async Task LoadFileInBackgroundAsync(ExtendetFileInfo charfile, Action<CharHolder> afterLoad = null)
        {
            MainPage.Instance.EnableBusy();
            try
            {
                var newchar = await CharHolderIO.Load(charfile);
                AppModel.Instance.MainObject = (newchar);
                AppModel.Instance.RequestNavigation(ProjectPages.Char);
                afterLoad?.Invoke(newchar);
            }
            catch (Exception ex)
            {
                Log.Write("Error reading file", ex);
            }
            MainPage.Instance.DisableBusy();
        }

        private static void LoadFileInBackgroundSynchron(ExtendetFileInfo charfile, Action<CharHolder> afterLoad = null)
        {
            MainPage.Instance.EnableBusy();
            var b = new BackgroundWorker();
            b.DoWork += async (object sender, DoWorkEventArgs e) =>
            {
                if (e.Argument is ExtendetFileInfo charfile)
                {
                    System.Threading.Thread.Sleep(1000);
                    try
                    {
                        e.Result = await CharHolderIO.Load(charfile);
                    }
                    catch (Exception ex)
                    {
                        Log.Write("Error reading file", ex);
                    }
                }
            };
            b.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                if (e.Result is CharHolder newchar)
                {
                    AppModel.Instance.MainObject = newchar;
                    System.Diagnostics.Debug.WriteLine("                           Admin1");
                    AppModel.Instance.RequestNavigation(ProjectPages.Char);
                    System.Diagnostics.Debug.WriteLine("                           Admin2");
                    afterLoad?.Invoke(newchar);
                    System.Diagnostics.Debug.WriteLine("                           Admin3");
                }
                MainPage.Instance.DisableBusy();
                System.Diagnostics.Debug.WriteLine("                           Admin4");
            };
            try
            {
                b.RunWorkerAsync(charfile);
            }
            catch (InvalidOperationException)
            {
                //TODO Report
            }
        }
        #endregion Open Files

        #region Other File Action

        private void MoreMenu(object sender, EventArgs e)
        {
            if (sender is View v && Common.FindParent<SfPopupLayout>(sender as Element) is SfPopupLayout popup)
            {
                popup.PopupView.ContentTemplate = Resources["MoreTemplate"] as DataTemplate;
                popup.PopupView.AnimationMode = AnimationMode.Zoom;
                popup.PopupView.AutoSizeMode = AutoSizeMode.Both;
                popup.PopupView.ShowHeader = false;
                popup.PopupView.ShowFooter = false;
                popup.PopupView.BindingContext = v.BindingContext;
                popup.ShowAtTouchPoint();
                popup.ShowRelativeToView(v ?? popup, RelativePosition.AlignToLeftOf);
            }
        }

        private void FileRename(object sender, EventArgs e)
        {
        }

        private void FileCopy(object sender, EventArgs e)
        {
            if (sender is View v && v.BindingContext is ExtendetFileInfo file && Common.FindParent<SfPopupLayout>(sender as Element) is SfPopupLayout popup)
            {
                try
                {
                    var newName = Path.Combine(Path.GetDirectoryName(file.FullName),
                    Path.GetFileNameWithoutExtension(file.FullName) + "-" + AppResources.CopiedFileName +
                    Path.GetExtension(file.FullName));
                    SharedIO.CurrentIO.CopyTo(file, new FileInfo(newName));
                    RefreshCharList();
                    popup.IsOpen = false;
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
            if (sender is View v && v.BindingContext is ExtendetFileInfo file && Common.FindParent<SfPopupLayout>(sender as Element) is SfPopupLayout popup)
            {
                Log.DisplayChoice(UiResources.Delete, UiResources.FileDeleteTip, new Options() { },
                    (AppResources.Yes, async () => { await SharedIO.CurrentIO.RemoveFile(file); RefreshCharList(); }
                ),
                    (AppResources.No, () => { }
                )
                    );
                popup.IsOpen = false;
            }
        }

        private async void FileExport(object sender, EventArgs e)
        {
            if (sender is View v && v.BindingContext is ExtendetFileInfo file && Common.FindParent<SfPopupLayout>(sender as Element) is SfPopupLayout popup)
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
                popup.IsOpen = false;
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
                AppModel.Instance.RequestNavigation(ProjectPages.Char, ProjectPagesOptions.CharNewChar);
            }
            catch (Exception ex)
            {
                Log.Write("Error reading file", ex);
            }
        }

        private void DebugChar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var newchar = CharHolderGenerator.TestAllCats(10);
                SettingsModel.I.COUNT_CREATIONS++;
                AppModel.Instance.MainObject = (newchar);
                AppModel.Instance.RequestNavigation(ProjectPages.Char);
            }
            catch (Exception ex)
            {
                Log.Write("Error reading file", ex);
            }
        }

        private void ExampleChar_Clicked(object sender, EventArgs e)
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
                    new SubMenuAction(UiResources.CreateNewChar,"\xf234",new Command(()=>NewChar_Clicked(this, new EventArgs()))),
                    new SubMenuAction(UiResources.ImportChar,"\xf56f",new Command(()=>OpenFile(this, new EventArgs()))),
                    new SubMenuAction(UiResources.CreateExampleChar,"\xf501",new Command(()=>ExampleChar_Clicked(this, new EventArgs()))),
                    new SubMenuAction(UiResources.OpenFolder,"\xf07c",new Command(()=> SharedIO.CurrentIO.OpenFolder(SharedIO.CurrentSaveDir))),
#if DEBUG
                new SubMenuAction("Debug Char","\xf188",new Command(()=> DebugChar_Clicked(this, new EventArgs()))),
                new SubMenuAction("Debug Message","\xf188",new Command(()=> Log.Write("Ein Wilder Test erscheint!", LogType.Warning, true))),
#endif
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
            if (sender is View layout)
            {
                var attributes = layout.FindByName<StackLayout>("attributes");
                if (layout.Width < 500)
                {
                    //layout.Orientation = StackOrientation.Vertical;
                    //attributes.HorizontalOptions = LayoutOptions.Start;
                    Grid.SetRow(attributes, 1);
                    Grid.SetColumn(attributes, 0);
                    layout.Margin = new Thickness(10, 2, 10, 0);
                }
                else
                {
                    Grid.SetRow(attributes, 0);
                    Grid.SetColumn(attributes, 1);
                    //layout.Orientation = StackOrientation.Horizontal;
                    //attributes.HorizontalOptions = LayoutOptions.EndAndExpand;
                    layout.Margin = new Thickness(10);
                }
            }
        }
        #endregion Design
    }
}