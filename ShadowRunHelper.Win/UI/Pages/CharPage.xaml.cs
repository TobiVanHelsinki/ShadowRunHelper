using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelper.UI;
using ShadowRunHelper.UI.Edit;
using ShadowRunHelper.Win.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper
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
        }

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
            Model.TutorialStateChanged += TutorialStateChanged;
            if (((ProjectPagesOptions)e.Parameter) == ProjectPagesOptions.CharNewChar)
            {
                try
                {
                    await new Edit_Person_Detail(MainObject.Person).ShowAsync();
                }
                catch (Exception)
                {
                }
            }
            if (!SettingsModel.I.TutorialCharShown)
            {
#pragma warning disable CS4014
                new Tutorial(20, 23).ShowAsync();
#pragma warning restore CS4014
                SettingsModel.I.TutorialCharShown = true;
            }
        }

        void TutorialStateChanged(int StateNumber, bool Highlight)
        {
            Style StyleToBeApplied = Highlight ? Tutorial.HighlightBorderStyle_XAML : Tutorial.UnhighlightBorderStyle_XAML;
            switch (StateNumber)
            {
                case 22:
                    PivotHeader1Border.Style = StyleToBeApplied;
                    PivotHeader2Border.Style = StyleToBeApplied;
                    PivotHeader3Border.Style = StyleToBeApplied;
                    PivotHeader4Border.Style = StyleToBeApplied;
                    PivotHeader5Border.Style = StyleToBeApplied;
                    break;
                case 23:
                    MainContentBorder.Style = StyleToBeApplied;
                    break;
                default:
                    //PivotHeader1Border.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    //PivotHeader2Border.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    //PivotHeader3Border.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    //PivotHeader4Border.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    //PivotHeader5Border.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    //MainContentBorder.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    break;
            }
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
        void Click_Save(object sender, RoutedEventArgs e)
        {
            MainObject?.SetSaveTimerTo();
        }

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
        void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            MainObject.Settings.ResetCategoryOptions();
        }
        void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            Model.RequestNavigation(this, ProjectPages.Char);
        }
        #endregion
        #region  instant search Stuff

        Thing PendingScrollEntry;
        List<(CategoryBlock Block, ScrollViewer sv)> LoadedCategoryBlocks = new List<(CategoryBlock Block, ScrollViewer sv)>();
        IEnumerable<CategoryOption> LokalCategoryOptions => MainObject.Settings.CategoryOptions;

        void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            switch (args.Reason)
            {
                case AutoSuggestionBoxTextChangeReason.UserInput:
                    (sender as AutoSuggestBox).ItemsSource = MainObject.ThingList.Where(x => LokalCategoryOptions.First(y => y.ThingType == x.ThingType).Visibility).Where((x) => x.SimilaritiesTo((sender as AutoSuggestBox).Text.ToLower()) > 0.3f);
                    break;
                case AutoSuggestionBoxTextChangeReason.ProgrammaticChange:
                    break;
                case AutoSuggestionBoxTextChangeReason.SuggestionChosen:
                default:
                    break;
            }
        }

        void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                if (args.ChosenSuggestion != null)
                {
                    PendingScrollEntry = (args.ChosenSuggestion as Thing);
                }
                else
                {
                    PendingScrollEntry = MainObject.ThingList.Where(x => LokalCategoryOptions.First(y => y.ThingType == x.ThingType).Visibility).ToList().Find((x) => x.SimilaritiesTo((sender as AutoSuggestBox).Text.ToLower()) > 0.3f);
                }
                if (PendingScrollEntry == null)
                {
                    return;
                }
                Pivot.SelectedIndex = TypeHelper.ThingTypeProperties.Find(x => x.ThingType == PendingScrollEntry.ThingType).Pivot;
            }
            catch { return; }
            ScrollIntoBlock();
            sender.Text = "";
            sender.IsSuggestionListOpen = false;
        }


        void CategoryBlockLoading(FrameworkElement sender, object args)
        {
            LoadedCategoryBlocks.Add((sender as CategoryBlock, ((sender as CategoryBlock).Parent as Panel).Parent as ScrollViewer));
        }
        void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollIntoBlock();
            (sender as FrameworkElement).Loaded -= ScrollViewer_Loaded;
        }
        void ScrollIntoBlock()
        {
            if (PendingScrollEntry == null)
            {
                return;
            }
            try
            {
                // Listenauswahl
                var (Block, sv) = LoadedCategoryBlocks.FirstOrDefault(x=>x.Block.ThingType == PendingScrollEntry.ThingType);
                double offset = 0;
                foreach (var item in ((sv as ScrollViewer).Content as Panel).Children)
                {
                    if (item.Equals(Block))
                    {
                        break;
                    }
                    else
                    {
                        offset += item.DesiredSize.Height;
                    }
                }
                // Scroll into ListView
                offset += Block.GetPositionAtListView(PendingScrollEntry);
                Block.Select(PendingScrollEntry);
                sv.ChangeView(null, offset - 100, null);
            }
            catch (Exception)
            {
                return;
            }
            PendingScrollEntry = null;
        }

        #endregion


    }
}
