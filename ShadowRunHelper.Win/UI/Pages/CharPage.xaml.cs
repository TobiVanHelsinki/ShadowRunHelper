using ShadowRunHelper.CharController;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelper.UI;
using ShadowRunHelper.UI.Edit;
using ShadowRunHelper.Win.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TLIB_UWPFRAME.IO;
using Windows.ApplicationModel.Resources;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper
{
    public sealed partial class CharPage : Page
    {


        #region Variables
        readonly AppModel Model = AppModel.Instance;
        public Windows.System.Display.DisplayRequest Char_DisplayRequest;
        CharHolder MainObject;
        ResourceLoader res;
        #endregion

        public CharPage()
        {
            if (MainObject == null)
            {
                MainObject = Model.MainObject;
            }
            res = ResourceLoader.GetForCurrentView();
            InitializeComponent();
        }

        #region Navigation stuff
        protected override void OnNavigatedTo(NavigationEventArgs e)
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
            if (!SettingsModel.I.TutorialCharShown)
            {
#pragma warning disable CS4014
                new Tutorial(20, 23).ShowAsync();
#pragma warning restore CS4014
                SettingsModel.I.TutorialCharShown = true;
            }
        }

        private void TutorialStateChanged(int StateNumber, bool Highlight)
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
            Model?.MainObject?.SetSaveTimerTo();
        }

        async void Edit_Person_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await new Edit_Person_Detail(Model.MainObject.Person).ShowAsync();
            }
            catch (Exception)
            {
            }
        }


        #endregion
        #region Options
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Model.MainObject.Settings.ResetCategoryOptions();
        }
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            Model.RequestNavigation(this, ProjectPages.Char);
        }
        #endregion
        #region  instant search Stuff

        //Dictionary<ThingDefs, (ContentControl Block, ListView ListView, ScrollViewer SV)> GlobalSearchCache = new Dictionary<ThingDefs, (ContentControl Block, ListView ListView, ScrollViewer SV)>();
        Thing PendingScrollEntry;
        public readonly IEnumerable<CategoryOption> lokalCategoryOptions = AppModel.Instance.MainObject.Settings.CategoryOptions;

        void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            switch (args.Reason)
            {
                case AutoSuggestionBoxTextChangeReason.UserInput:
                    (sender as AutoSuggestBox).ItemsSource = MainObject.ThingList.Where(x => lokalCategoryOptions.First(y => y.ThingType == x.ThingType).Visibility).Where((x) => x.SimilaritiesTo((sender as AutoSuggestBox).Text.ToLower()) > 0.3f);
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
                    PendingScrollEntry = MainObject.ThingList.Where(x => lokalCategoryOptions.First(y => y.ThingType == x.ThingType).Visibility).ToList().Find((x) => x.SimilaritiesTo((sender as AutoSuggestBox).Text.ToLower()) > 0.3f);
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

        private void Grid_Entry_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollIntoBlock();
            (sender as Grid).Loaded -= Grid_Entry_Loaded;
        }
        List<(CategoryBlock Block, ScrollViewer sv)> LoadedCategoryBlocks = new List<(CategoryBlock Block, ScrollViewer sv)>();

        private void CategoryBlockLoading(FrameworkElement sender, object args)
        {
            LoadedCategoryBlocks.Add((sender as CategoryBlock, ((sender as CategoryBlock).Parent as Panel).Parent as ScrollViewer));
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
                var TargetBlock = LoadedCategoryBlocks.FirstOrDefault(x=>x.Item1.ThingType == PendingScrollEntry.ThingType);
                double offset = 0;
                foreach (var item in ((TargetBlock.sv as ScrollViewer).Content as Panel).Children)
                {
                    if (item.Equals(TargetBlock.Block))
                    {
                        if ((TargetBlock.sv.Content as Panel).Children.Last() == item)
                        {
                            throw new IndexOutOfRangeException(); //TOFO WTF?
                        }
                        break;
                    }
                    else
                    {
                        offset += item.DesiredSize.Height;
                    }
                }
                // Scroll into ListView
                offset += TargetBlock.Block.GetPositionAtListView(PendingScrollEntry);
                TargetBlock.Block.Select(PendingScrollEntry);
                TargetBlock.sv.ChangeView(null, offset - 100, null);
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
