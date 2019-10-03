using dotMorten.Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.Platform;
using SharedCode.Ressourcen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TLIB;
using Xam.Plugin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GCharHolder : ContentView, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public new event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public AppModel Model => AppModel.Instance;
        CharHolder _MyChar;
        public CharHolder MyChar
        {
            get { return _MyChar; }
            set { if (_MyChar != value) { _MyChar = value; NotifyPropertyChanged(); } }
        }
        IEnumerable<StackLayout> ButtonsPanels;
        IEnumerable<Button> Buttons => ButtonsPanels.SelectMany(x => x.Children.OfType<Button>());
        static GCharHolder Instance;
        public GCharHolder(CharHolder myChar)
        {
            this.MyChar = myChar;
            Model.AddMainObject(myChar);
            Instance = this;
            InitializeComponent();
            ButtonsPanels = new List<StackLayout>() { s1, s2, s3, s4, s5 };

            SizeChanged += (a, b) => SetViewParameters();
            BindingContext = this;
            Infogrid.GestureRecognizers.Add(new TapGestureRecognizer { NumberOfTapsRequired = 1, Command = new Command(Infogrid_Tapped) });
            SettingsModel.I.FIRST_START = false;
            InitButtons();
            SetViewParameters();
            MenuOpen = true;

            Features.Ui.IsCustomTitleBarEnabled = true; //TODO Dispse?
            Features.Ui.SetCustomTitleBar(DependencyService.Get<IFormsInteractions>().GetRenderer(CharTitleBar));
            Features.Ui.SetCustomTitleBar(null);
            Features.Ui.CustomTitleBarChanges += CustomTitleBarChanges; //TODO Dispose
            Features.Ui.TriggerCustomTitleBarChanges();
        }

        private void CustomTitleBarChanges(double LeftSpace, double RigthSpace, double Heigth)
        {
            CharTitleBar.MinimumHeightRequest = Heigth;
            //CharHeadControls.Padding = new Thickness(LeftSpace.LowerB(5), 5, RigthSpace.LowerB(5), 5);
        }

        async void Infogrid_Tapped()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new ControlCenter(MyChar));
            }
            catch (Exception)
            {
            }
        }

        #region Menu Buttons
        private void InitButtons()
        {
            if (this is ContentView Content)
            {
                foreach (var item in ButtonsPanels)
                {
                    item.Children.Clear();
                }
                foreach (var Category in MyChar.Settings.CategoryOptions.OrderBy(x => x.Pivot).ThenBy(x => x.Order))
                {
                    var b = new Button
                    {
                        Padding = new Thickness(2),
                        BindingContext = Category.ThingType,
                        Text = Category.ThingType.ThingDefToString(true),
                        IsVisible = Category.Visibility,
                    };
                    switch (Category.Pivot)
                    {
                        case 0:
                            s1.Children.Add(b);
                            break;
                        case 1:
                            s2.Children.Add(b);
                            break;
                        case 2:
                            s3.Children.Add(b);
                            break;
                        case 3:
                            s4.Children.Add(b);
                            break;
                        case 4:
                            s5.Children.Add(b);
                            break;
                        default:
                            break;
                    }
                    b.Clicked += B_CTRL_Clicked;
                }
                foreach ((var name, var template, var layout) in new (string, DataTemplate, StackLayout)[] {
                    ("Favorites", CharFavs, s5),
                    ("Notes", CharNotes, s5),
                    ("Person", CharPerson, s4) })
                {
                    var btt = new Button
                    {
                        Padding = new Thickness(2),
                        Text = name,
                        BindingContext = template
                    };
                    btt.Clicked += B_More_Clicked;
                    layout.Children.Add(btt);
                }
            }
        }

        private void B_More_Clicked(object sender, EventArgs e)
        {
            if (sender is Button b && b.BindingContext is DataTemplate dt)
            {
                if (dt.CreateContent() is View tv)
                {
                    tv.BindingContext = MyChar;
                    ContentPanel.Content = tv;
                    HighlightButton(b);
                }
            }
            if (Narrow)
            {
                MenuOpen = false;
            }
        }

        private void B_CTRL_Clicked(object sender, EventArgs e)
        {
            if (sender is Button b && b.BindingContext is ThingDefs type)
            {
                HighlightButton(b);
                var gCTRL = new GController(MyChar);
                var CTRL = typeof(CharHolder).GetProperties().FirstOrDefault(x => x.Name == "CTRL" + type);
                if (CTRL != null)
                {
                    gCTRL.SetBinding(BindingContextProperty, new Binding($"{nameof(MyChar)}.{CTRL.Name}"));
                }
                else
                {
                    if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
                    Log.Write("Structual Error, Controller Name is wrong");
                }
                ContentPanel.Content = gCTRL;
                if (Narrow)
                {
                    MenuOpen = false;
                }
            }
        }

        private void HighlightButton(Button myBtn)
        {
            foreach (var item in Buttons)
            {
                item.BackgroundColor = Color.Default;
                item.TextColor = Color.Default;
            }
            myBtn.BackgroundColor = Color.Accent;
            myBtn.TextColor = Color.FloralWhite;
        }

        #endregion
        #region AdaptiveUI

        bool _Narrow;
        public bool Narrow
        {
            get { return _Narrow; }
            set { if (_Narrow != value) { _Narrow = value; ChangeUi(); } }
        }
        bool _MenuOpen = true;
        public bool MenuOpen
        {
            get { return _MenuOpen; }
            set { if (_MenuOpen != value) { _MenuOpen = value; ChangeUi(); } }
        }

        private void ChangeUi()
        {
            MenuToggleButton.IsVisible = Narrow;
            if (Narrow)
            {
                if (MenuOpen)
                {
                    LayerContent_Col0.Width = new GridLength(1, GridUnitType.Star);
                    LayerContent_Col1.Width = new GridLength(0, GridUnitType.Absolute);
                    LayerContent_Col2.Width = new GridLength(1, GridUnitType.Star);
                    ContentPanel.IsVisible = false;
                }
                else
                {
                    LayerContent_Col0.Width = new GridLength(0, GridUnitType.Absolute);
                    LayerContent_Col1.Width = new GridLength(1, GridUnitType.Star);
                    LayerContent_Col2.Width = new GridLength(0, GridUnitType.Absolute);
                    ContentPanel.IsVisible = true;
                }
            }
            else
            {
                //Toggle.
                //if (Open)
                {
                    LayerContent_Col0.Width = new GridLength(1, GridUnitType.Auto);
                    LayerContent_Col1.Width = new GridLength(1, GridUnitType.Star);
                    LayerContent_Col2.Width = new GridLength(1, GridUnitType.Auto);
                    ContentPanel.IsVisible = true;
                }
                //else
                //{
                //    LayerContent_Col0.Width = new GridLength(0, GridUnitType.Absolute);
                //    LayerContent_Col1.Width = new GridLength(1, GridUnitType.Star);
                //    LayerContent_Col2.Width = new GridLength(0, GridUnitType.Absolute);
                //    ContentPanel.IsVisible = true;
                //}
            }
        }
        void SetViewParameters()
        {
            Narrow = Width < 550;
            if (Width > 550 && MyChar != null)
            {
                MenuOpen = true;
            }
            else
            {
                if (ContentPanel.Content != null)
                {
                    MenuOpen = false;
                }
            }
        }


        #endregion
        #region Menu Stuff

        private void Toggle(object sender, EventArgs e)
        {
            MenuOpen = !MenuOpen;
        }

        readonly (string, Action)[] MenuItems = new (string, Action)[] {
                        (UiResources.SaveAtCurrentPlace,null),
                        (UiResources.SaveExtern,null),
                        (UiResources.OpenFolder,null),
                        (UiResources.SubtractLifeStyleCost,null),
                        (UiResources.CharSettings,null),
                        (UiResources.Repair,null),
                        (UiResources.Unload,Unload),
                    };

        private static void Unload()
        {
        }

        void MoreMenu(object sender, System.EventArgs e)
        {
            try
            {
                PopupMenu Popup = new PopupMenu
                {
                    ItemsSource = MenuItems.Select(x => x.Item1).ToArray()
                };
                Popup.OnItemSelected += Popup_OnItemSelected;
                Popup?.ShowPopup(sender as Button);

                //https://baskren.github.io/Forms9Patch/guides/GettingStartedWindows.html
                //https://docs.microsoft.com/de-de/xamarin/xamarin-forms/app-fundamentals/navigation/pop-ups
            }
            catch (Exception)
            {
            }
        }

        private void Popup_OnItemSelected(string item)
        {
            MenuItems.FirstOrDefault(x => x.Item1 == item).Item2?.Invoke();
        }

        private void Save(object sender, System.EventArgs e)
        {
            MyChar.SetSaveTimerTo(0, true);
        }

        #endregion
        #region Search Stuff
        private void AutoSuggestBox_TextChanged(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxTextChangedEventArgs e)
        {
            switch (e.Reason)
            {
                case AutoSuggestionBoxTextChangeReason.UserInput:
                    if (sender is AutoSuggestBox asb)
                    {
                        asb.ItemsSource = MyChar.ThingList.Where(x => MyChar.Settings.CategoryOptions.First(y => y.ThingType == x.ThingType).Visibility).Where(x => x.SimilaritiesTo(asb.Text) > 0).OrderByDescending(x => x.SimilaritiesTo(asb.Text)).ToList();
                    }
                    break;
                case AutoSuggestionBoxTextChangeReason.ProgrammaticChange:
                    break;
                case AutoSuggestionBoxTextChangeReason.SuggestionChosen:
                    break;
                default:
                    break;
            }
        }
        IEnumerable<CategoryOption> LokalCategoryOptions => Model.MainObject.Settings.CategoryOptions;

        private void AutoSuggestBox_SuggestionChosen(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxSuggestionChosenEventArgs e)
        {
            if (sender is AutoSuggestBox asb)
            {
                asb.Text = e.SelectedItem.ToString();
            }
        }

        async void AutoSuggestBox_QuerySubmitted(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxQuerySubmittedEventArgs e)
        {
            if (sender is AutoSuggestBox asb)
            {
                try
                {
                    if (e.ChosenSuggestion != null)
                    {
                        Model.PendingScrollEntry = (e.ChosenSuggestion as Thing);
                    }
                    else
                    {
                        Model.PendingScrollEntry = (asb.ItemsSource as IOrderedEnumerable<Thing>).FirstOrDefault();

                        Model.PendingScrollEntry = Model.MainObject.ThingList.Where(x => LokalCategoryOptions.First(y => y.ThingType == x.ThingType).Visibility).OrderByDescending(x => x.SimilaritiesTo(asb.Text)).FirstOrDefault();
                    }
                    asb.ItemsSource = null;
                    if (Model.PendingScrollEntry == null)
                    {
                        return;
                    }
                }
                catch { return; }
                asb.Text = "";
                asb.IsSuggestionListOpen = false;
            }
            await PopupNavigation.Instance.PushAsync(new DetailsPage(Model.PendingScrollEntry, MyChar));
            Model.PendingScrollEntry = null;
        }
        #endregion

    }
}