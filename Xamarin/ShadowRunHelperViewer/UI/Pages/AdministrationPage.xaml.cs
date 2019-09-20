using ShadowRunHelper;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        #endregion

        ObservableCollection<ExtendetFileInfo> _CharList = new ObservableCollection<ExtendetFileInfo>();
        public ObservableCollection<ExtendetFileInfo> CharList
        {
            get { return _CharList; }
            set { if (_CharList != value) { _CharList = value; NotifyPropertyChanged(); } }
        }

        public AdministrationPage()
        {
            InitializeComponent();
            BindingContext = this;
            RefreshCharList();

            Features.Ui.TopUiSizeChanged += Ui_TopUiSizeChanged; //TODO Dispose
        }

        private void Ui_TopUiSizeChanged(double LeftSpace, double RigthSpace)
        {
            TitleBar.Padding = new Thickness(LeftSpace, 0, RigthSpace, 0);
        }


        async void RefreshCharList()
        {
            if (!SettingsModel.I.FOLDERMODE)
            {
                if (Content is Grid g)
                {
                    var item = new Frame { Padding = 5, Content = new Label() { BackgroundColor = Color.Crimson, Margin = 5, 
                        Text = "Warning, you are saving your chars locally at the moment. It is strongly recommendet to use an synchronized folder like OneDrive, DropBox and co." }
                    };
                    Grid.SetRow(item, 1);
                    g.Children.Add(item);
                }
            }
            try
            {
                var savepathfiles = (await SharedIO.CurrentIO.GetFiles(SharedIO.CurrentSaveDir, Constants.LST_FILETYPES_CHAR)).ToList();
                Device.BeginInvokeOnMainThread(() => { CharList.Clear(); CharList.AddRange(savepathfiles); });
            }
            catch (Exception ex)
            {
                Log.Write("Error reading directory", ex);
            }
        }

        private void ListView_Refreshing(object sender, EventArgs e)
        {
            RefreshCharList();
        }


        #region Save and Load
        //async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is ExtendetFileInfo charfile)
            {
                try
                {
                    var newchar = await CharHolderIO.Load(charfile);
                    (Application.Current.MainPage as MainPage)?.NavigatoToSingleInstanceOf<CharPage>(true, (x) => x.Activate(newchar));
                }
                catch (Exception ex)
                {
                    Log.Write("Error reading file", ex);
                }
            }
        }


        async void FilePickerExample()
        {
            try
            {
                var charfile = await SharedIO.CurrentIO.PickFile(Constants.LST_FILETYPES_CHAR, "NextChar");
                var newchar = await CharHolderIO.Load(charfile);
                (Application.Current.MainPage as MainPage)?.NavigatoToSingleInstanceOf<CharPage>(true, (x) => x.Activate(newchar));
            }
            catch (Exception ex)
            {
                Log.Write("Error reading file", ex);
            }
        }
        #endregion

        #region Copy and Move

        #endregion

        #region Create
        void NewChar_Clicked(object sender, EventArgs e)
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

        async void ExampleChar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await CharHolderIO.CopyPreSavedCharToCurrentLocation(CharHolderIO.PreSavedChar.ExampleChar);
            }
            catch (Exception ex)
            {
                Log.Write("Error reading file", ex);
            }
            RefreshCharList();
        }
        #endregion

        private void TemplateSizeChanged(object sender, EventArgs e)
        {
            if (sender is StackLayout layout)
            {
                var attributes = layout.FindByName<StackLayout>("attributes");
                if (layout.Width < 500)
                {
                    layout.Orientation = StackOrientation.Vertical;
                    attributes.HorizontalOptions = LayoutOptions.Start;
                    layout.Margin = new Thickness(10,2,10,0);
                }
                else
                {
                    layout.Orientation = StackOrientation.Horizontal;
                    attributes.HorizontalOptions = LayoutOptions.EndAndExpand;
                    layout.Margin = new Thickness(10);
                }
            }
        }

        private void OpenFile(object sender, EventArgs e)
        {
            FilePickerExample();
        }
    }
}