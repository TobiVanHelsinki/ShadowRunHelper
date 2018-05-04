﻿using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TAMARIN.IO;
using TAPPLICATION.IO;
using TLIB;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
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
            LoadCategoryOptions();
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
        void Click_Save(object sender, RoutedEventArgs e)
        {
            var x = (sender as Button).DataContext;
            MainObject?.SetSaveTimerTo();
        }

        async void Click_SaveAtCurrentPlace(object sender, RoutedEventArgs e)
        {
            try
            {
                var i = await SharedIO.SaveAtCurrentPlace(Model.MainObject);
            }
            catch (Exception ex)
            {
                Model.NewNotification(StringHelper.GetString("Notification_Error_SaveFail"), ex);
            }
        }

        async void Click_SaveExtern(object sender, RoutedEventArgs e)
        {
            try
            {
                var i = await SharedIO.Save(Model.MainObject, Info: new FileInfoClass() { Fileplace = Place.Extern, FolderToken = "Export" });
                Model.MainObject.FileInfo.Fileplace = i.Fileplace;
                Model.MainObject.FileInfo.Filepath = i.Filepath;
                Model.MainObject.FileInfo.Filename = i.Filename;
            }
            catch (Exception ex)
            {
                Model.NewNotification(StringHelper.GetString("Notification_Error_FileExportFail"), ex);
            }
        }
        void Click_UI_TxT_CSV_Cat_Exportport(object sender, RoutedEventArgs e)
        {
                SharedUIActions.UI_TxT_CSV_Cat_Exportport(Model.MainObject);
        }
        void Click_Repair(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.MainObject?.Repair();
            }
            catch (Exception ex)
            {
                Model.NewNotification(StringHelper.GetString("Notification_Error_RepairFail"), ex);
            }
        }
        void Click_OpenFolder(object sender, RoutedEventArgs e)
        {
            SharedIO.CurrentIO.OpenFolder(Model.MainObject.FileInfo);
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
        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            Model.RequestNavigation(ProjectPages.Administration);
            Model.MainObject = null;
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
        public void LoadCategoryOptions()
        {
            List<GroupInfoList<object>> DataGrouped = new List<GroupInfoList<object>>();
            var query = from opt in Model.MainObject.Settings.CategoryOptions
                        group opt by opt.Pivot into g
                        select new { GroupNr = g.Key, Items = g };

            foreach (var g in query)
            {
                GroupInfoList<object> info = new GroupInfoList<object>();
                switch (g.GroupNr)
                {
                    case 0:
                        info.Key = StringHelper.GetString("Char_View_Pivot_Aktion/Label");
                        break;
                    case 1:
                        info.Key = StringHelper.GetString("Char_View_Pivot_Item/Label");
                        break;
                    case 2:
                        info.Key = StringHelper.GetString("Char_View_Pivot_Kampf/Label");
                        break;
                    case 3:
                        info.Key = StringHelper.GetString("Char_View_Pivot_Person/Label");
                        break;
                    default:
                        break;
                }

                foreach (var item in g.Items)
                {
                    info.Add(item);
                }
                DataGrouped.Add(info);
            }
            var cvs = (CollectionViewSource)Resources["GroupedCategoryOptions"];
            cvs.Source = DataGrouped;
        }
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
