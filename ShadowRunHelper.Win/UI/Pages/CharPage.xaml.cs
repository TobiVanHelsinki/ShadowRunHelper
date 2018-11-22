using Microsoft.Toolkit.Uwp.UI.Controls;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Input;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper.UI
{
    public sealed partial class CharPage : Page
    {
        #region Variables
        AppModel Model => AppModel.Instance;
        public Windows.System.Display.DisplayRequest Char_DisplayRequest;
        CharHolder MainObject => Model.MainObject;
        #endregion

        public CharPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            Model.PropertyChanged += Model_PropertyChanged;
        }

        void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Model.MainObject) && MainObject?.Person != null)
            {
                LoadNotes();
            }
            else if(e.PropertyName == nameof(Model.MainObject) && MainObject != null)
            {
            }
            else if(e.PropertyName == nameof(Model.PendingScrollEntry) && Model.PendingScrollEntry != null)
            {
                Pivot.SelectedIndex = TypeHelper.ThingTypeProperties.Find(x => x.ThingType == Model.PendingScrollEntry.ThingType).Pivot;
                ScrollIntoBlock();
            }
        }

        #region GUI Stuff
        private void Pivot_SizeChanged(object sender, SizeChangedEventArgs e) => AdjustHeaderWidth();

        void AdjustHeaderWidth()
        {
            var w = Pivot.ActualWidth / 6;
            if (w> 30 && w <= 65 && Math.Round(w) != Math.Round(PivotHeader1Border.MaxWidth))
            {
                PivotHeader1Border.MaxWidth = w;
                PivotHeader2Border.MaxWidth = w;
                PivotHeader3Border.MaxWidth = w;
                PivotHeader4Border.MaxWidth = w;
                PivotHeader5Border.MaxWidth = w;
            }
        }
        #endregion
        #region Navigation stuff
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (SettingsModel.I.DISPLAY_REQUEST)
            {
                try
                {
                    Char_DisplayRequest = new Windows.System.Display.DisplayRequest();
                    Char_DisplayRequest.RequestActive();
                }
                catch (Exception)
                {
                }
            }
            switch (((ProjectPagesOptions)e.Parameter))
            {
                case ProjectPagesOptions.CharNewChar:
                    Pivot.SelectedIndex = 0;
                    try
                    {
                        await new Edit_Person_Detail(MainObject.Person).ShowAsync();
                    }
                    catch (Exception)
                    {
                    }
                    break;
                case ProjectPagesOptions.Char_Action:
                    Pivot.SelectedIndex = 0;
                    break;
                case ProjectPagesOptions.Char_Items:
                    Pivot.SelectedIndex = 1;
                    break;
                case ProjectPagesOptions.Char_Battle:
                    Pivot.SelectedIndex = 2;
                    break;
                case ProjectPagesOptions.Char_Person:
                    Pivot.SelectedIndex = 3;
                    break;
                case ProjectPagesOptions.Char_Notes:
                    Pivot.SelectedIndex = 4;
                    break;
                case ProjectPagesOptions.Char_Settings:
                    Pivot.SelectedIndex = 5;
                    break;
                default:
                    break;
            }
            AdjustHeaderWidth();

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {

            if (SettingsModel.I.DISPLAY_REQUEST)
            {
                try
                {
                    Char_DisplayRequest.RequestRelease();
                }
                catch (Exception)
                {
                }
            }
            base.OnNavigatedFrom(e);
        }

        #endregion
        #region Gui-Model Stuff
       

        async void Edit_Person_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await new Edit_Person_Detail(MainObject.Person).ShowAsync();
            }
            catch (Exception)
            {
            }
        }
        #endregion
        #region Char Settings
        public class GroupInfoList<T> : List<object>
        {
            public object Key { get; set; }
            public new IEnumerator<object> GetEnumerator()
            {
                return (System.Collections.Generic.IEnumerator<object>)base.GetEnumerator();
            }
        }
        void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            MainObject.Settings.ResetCategoryOptions();
        }
        #endregion
        #region instant search Stuff

        List<(CategoryBlock Block, ScrollViewer sv)> LoadedCategoryBlocks = new List<(CategoryBlock Block, ScrollViewer sv)>();

        void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollIntoBlock();
            (sender as FrameworkElement).Loaded -= ScrollViewer_Loaded;
        }

        void CategoryBlockLoading(FrameworkElement sender, object args)
        {
            LoadedCategoryBlocks.Add((sender as CategoryBlock, ((sender as CategoryBlock).Parent as Panel).Parent as ScrollViewer));
        }

        void ScrollIntoBlock()
        {
            if (Model.PendingScrollEntry == null)
            {
                return;
            }
            try
            {
                // Listenauswahl
                var (Block, sv) = LoadedCategoryBlocks.FirstOrDefault(x=>x.Block.Controller.eDataTyp == Model.PendingScrollEntry.ThingType);
                double offset = (sv.Content as Panel).Children.TakeWhile(x => !x.Equals(Block)).Sum(x => x.DesiredSize.Height);
                // Scroll into ListView
                offset += Block.GetPositionAtListView(Model.PendingScrollEntry);
                Block.Select(Model.PendingScrollEntry);
                sv.ChangeView(null, offset - 100, null);
            }
            catch (Exception)
            {
                return;
            }
            Model.PendingScrollEntry = null;
        }
        #endregion

        #region REB
        void Previewer_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                var link = new Uri(e.Link);
                var linkOpen = Task.Run(() => Launcher.LaunchUriAsync(link));
            }
            catch
            {
            }
        }

        void Toolbar_Loaded(object sender, RoutedEventArgs e)
        {
            LoadNotes();
        }
        void EditZone_CharacterReceived(UIElement sender, Windows.UI.Xaml.Input.CharacterReceivedRoutedEventArgs args)
        {
            SaveNotes();
        }
        void EditZone_TextChanged(object sender, RoutedEventArgs e)
        {
            SaveNotes();
        }
        void LoadNotes()
        {
            try
            {
                EditZone.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, MainObject.Person.Notizen);
            }
            catch (Exception)
            {
            }
        }
        void SaveNotes()
        {
            try
            {
                if (MainObject?.Person != null)
                {
                    EditZone.Document.GetText(Windows.UI.Text.TextGetOptions.FormatRtf, out string text);
                    MainObject.Person.Notizen = text;
                }
            }
            catch (Exception)
            {
            }
        }




        #endregion

        #region DynamicSize
        public int CustFontSize { get; set; }
        public PointerDeviceType CurrentPointerDeviceType { get; set; }

        void Infos_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            CurrentPointerDeviceType = e.Pointer.PointerDeviceType;
        }

        void Flyout_Opening(object sender, object e)
        {
            switch (CurrentPointerDeviceType)
            {
                case PointerDeviceType.Touch:
                    (sender as Flyout).Placement = Windows.UI.Xaml.Controls.Primitives.FlyoutPlacementMode.Top;
                    CustFontSize = 35;
                    break;
                case PointerDeviceType.Pen:
                case PointerDeviceType.Mouse:
                    (sender as Flyout).Placement = Windows.UI.Xaml.Controls.Primitives.FlyoutPlacementMode.Top;
                    CustFontSize = 25;
                    break;
                default:
                    break;
            }
        }
        void MP_Btn_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as Control).FontSize = CustFontSize < 6 ? (sender as Control).FontSize : CustFontSize;
        }
        #endregion

        #region Header Button Handler
        void Plus_Click(object sender, RoutedEventArgs e)
        {
            if (Model.MainObject != null)
            {
                string Controller_Name = (((FrameworkElement)sender).Name);

                if (Controller_Name.Contains("Karma_Gesamt"))
                {
                    Model.MainObject.Person.Karma_Gesamt++;
                }
                else if (Controller_Name.Contains("Karma_Aktuell"))
                {
                    Model.MainObject.Person.Karma_Aktuell++;
                }
                else if (Controller_Name.Contains("Edge_Gesamt"))
                {
                    Model.MainObject.Person.Edge_Gesamt++;
                }
                else if (Controller_Name.Contains("Edge_Aktuell"))
                {
                    Model.MainObject.Person.Edge_Aktuell++;
                }
                else if (Controller_Name.Contains("Initiative"))
                {
                    Model.MainObject.Person.Initiative++;
                }
                else if (Controller_Name.Contains("Runs"))
                {
                    Model.MainObject.Person.Runs++;
                }
            }
        }

        void Minus_Click(object sender, RoutedEventArgs e)
        {
            if (Model.MainObject != null)
            {
                string Controller_Name = (((FrameworkElement)sender).Name);

                if (Controller_Name.Contains("Karma_Gesamt"))
                {
                    Model.MainObject.Person.Karma_Gesamt--;
                }
                else if (Controller_Name.Contains("Karma_Aktuell"))
                {
                    Model.MainObject.Person.Karma_Aktuell--;
                }
                else if (Controller_Name.Contains("Edge_Gesamt"))
                {
                    Model.MainObject.Person.Edge_Gesamt--;
                }
                else if (Controller_Name.Contains("Edge_Aktuell"))
                {
                    Model.MainObject.Person.Edge_Aktuell--;
                }
                else if (Controller_Name.Contains("Initiative"))
                {
                    Model.MainObject.Person.Initiative--;
                }
                else if (Controller_Name.Contains("Runs"))
                {
                    Model.MainObject.Person.Runs--;
                }
            }
        }

        async void Edit_Click(object sender, RoutedEventArgs e)
        {
            String Controller_Name = ((String)((Button)sender).Name);

            if (Controller_Name.Contains("Person2"))
            {
                Edit_Person_Fast dialog = new Edit_Person_Fast(Model.MainObject.Person);
                await dialog.ShowAsync();
            }
        }

        #endregion

        void EditBox_GotFocus(object sender, RoutedEventArgs e) => SharePageFunctions.EditBox_SelectAll(sender, e);

        void EditBox_PreviewKeyDown(object sender, KeyRoutedEventArgs e) => SharePageFunctions.EditBox_UpDownKeys(sender, e);

    }
}
