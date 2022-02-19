using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ShadowRunHelper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        //event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        //{
        //    add
        //    {
        //        if (PropertyChanged is null)
        //        {
        //            PropertyChanged = new PropertyChangedEventHandler();
        //        }
        //        if (!PropertyChanged?.GetInvocationList()?.Contains(value) == true)
        //        {
        //            PropertyChanged += value;
        //        }
        //    }
        //    remove
        //    {
        //        if (PropertyChanged?.GetInvocationList()?.Contains(value) == true)
        //        {
        //            PropertyChanged -= value;
        //        }
        //    }
        //}
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region Variables
        AppModel Model => AppModel.Instance;
        //CharHolder MainObject => Model.MainObject;
        private CharHolder _MainObject;
        public CharHolder MainObject
        {
            get => _MainObject;
            set { if (_MainObject != value) { _MainObject = value; NotifyPropertyChanged(); } }
        }
        #endregion
        public MainPage()
        {
            ShadowRunHelper.IO.SharedIO.CurrentIO = new ShadowRunHelperViewer.Platform.UWP.IO();
            ShadowRunHelper.Model.SharedSettingsModel.PlatformSettings = new ShadowRunHelperViewer.Platform.UWP.Settings();
            ShadowRunHelper.Helper.PlatformHelper.Platform = new ShadowRunHelperViewer.Platform.UWP.PlatformHelper();
            AppModel.Initialize();
            TLIB.Log.Mode = TLIB.LogMode.Moderat;
            TLIB.Log.InMemoryLogMaxCount = 100;
            TLIB.Log.IsInMemoryLogEnabled = true;
            TLIB.Log.IsFileLogEnabled = false;
            TLIB.Log.IsConsoleLogEnabled = true;
            SettingsModel.Initialize();
            this.InitializeComponent();
            //NavigationCacheMode = NavigationCacheMode.Required;
            //Model.PropertyChanged += Model_PropertyChanged;
            TLIB.Log.Write("Welcome to this early access alpha. This App is slow, so please be patient. Click on the Open Button to select your charfile.");
        }
        
        private async void Open_Click(object sender, RoutedEventArgs e)
        {
            var fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            fileOpenPicker.FileTypeFilter.Add(".SRHChar");
            StorageFile pickedFile = await fileOpenPicker.PickSingleFileAsync();

            if (pickedFile != null)
            {
                if (!pickedFile.Name.EndsWith(".SRHChar"))
                {
                    TLIB.Log.Write("Wrong Filetype", TLIB.LogType.Error);
                }
                else
                {
                    TLIB.Log.Write("Loading File Now");
                }
                // File was picked, you can now use it
                MainObject = CharHolderIO.Deserialize(await FileIO.ReadTextAsync(pickedFile));
                TLIB.Log.Write("File Loaded");
            }
            else
            {
                // No file was picked or the dialog was cancelled.
            }
        }
        private async void Settings_Click(object sender, RoutedEventArgs e)
        {
            TLIB.Log.Write("This is just a debug Button");
            //var a = TestList.ItemsSource;
            var b = NameBlock;
        }
    }
}
