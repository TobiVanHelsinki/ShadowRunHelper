﻿using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
        }

        #region GUI Stuff
        private void Pivot_SizeChanged(object sender, SizeChangedEventArgs e) => AdjustHeaderWidth();

        void AdjustHeaderWidth()
        {
            var w = Pivot.ActualWidth / 6;
            if (w> 30 && w < 60 || PivotHeader1Border.MaxWidth < 60)
            {
                PivotHeader1Border.MaxWidth = w;
                PivotHeader2Border.MaxWidth = w;
                PivotHeader3Border.MaxWidth = w;
                PivotHeader4Border.MaxWidth = w;
                PivotHeader5Border.MaxWidth = w;
                PivotHeader6Border.MaxWidth = w;
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
            Model.TutorialStateChanged += TutorialStateChanged;
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
            if (!SettingsModel.I.TutorialCharShown)
            {
#pragma warning disable CS4014
                new Tutorial(20, 23).ShowAsync();
#pragma warning restore CS4014
                SettingsModel.I.TutorialCharShown = true;
            }
            AdjustHeaderWidth();
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
            NavigationCacheMode = NavigationCacheMode.Disabled;
            Model.RequestNavigation(ProjectPages.Char);
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
                var (Block, sv) = LoadedCategoryBlocks.FirstOrDefault(x=>x.Block.Controller.eDataTyp == PendingScrollEntry.ThingType);
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
