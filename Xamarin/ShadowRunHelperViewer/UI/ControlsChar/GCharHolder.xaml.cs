//Author: Tobi van Helsinki

///Author: Tobi van Helsinki

using dotMorten.Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.Platform;
using ShadowRunHelperViewer.UI.Resources;
using SharedCode.Ressourcen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using TAPPLICATION.IO;
using TLIB;
using Xam.Plugin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GCharHolder : ContentView, INotifyPropertyChanged, IDisposable
    {
        #region NotifyPropertyChanged
        public new event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion NotifyPropertyChanged

        public AppModel Model => AppModel.Instance;

        private CharHolder _MyChar;
        public CharHolder MyChar
        {
            get => _MyChar;
            set { if (_MyChar != value) { _MyChar = value; NotifyPropertyChanged(); } }
        }

        public virtual void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool t)
        {
            Features.Ui.CustomTitleBarChanges -= CustomTitleBarChanges;
        }

        private readonly IEnumerable<StackLayout> ButtonsPanels;
        private IEnumerable<Button> Buttons => ButtonsPanels.SelectMany(x => x.Children.OfType<Button>());

        public GCharHolder(CharHolder myChar)
        {
            MyChar = myChar;
            Model.AddMainObject(myChar);
            InitializeComponent();
            ButtonsPanels = new[] { s1, s2, s3, s4, s5 };

            SizeChanged += (a, b) => SetViewParameters();
            BindingContext = this;
            Infogrid.GestureRecognizers.Add(new TapGestureRecognizer { NumberOfTapsRequired = 1, Command = new Command(Infogrid_Tapped) });
            SettingsModel.I.FIRST_START = false;
            InitButtons();
            SetViewParameters();
            MenuOpen = true;
            Features.Ui.IsCustomTitleBarEnabled = true;
            //Features.Ui.SetCustomTitleBar(DependencyService.Get<IFormsInteractions>().GetRenderer(CharTitleBar));
            Features.Ui.SetCustomTitleBar(null);
            Features.Ui.CustomTitleBarChanges += CustomTitleBarChanges;
            Features.Ui.TriggerCustomTitleBarChanges();
        }

        private void CustomTitleBarChanges(double LeftSpace, double RigthSpace, double Heigth)
        {
            CharTitleBar.MinimumHeightRequest = Heigth;
            //CharHeadControls.Padding = new Thickness(LeftSpace.LowerB(5), 5, RigthSpace.LowerB(5), 5);
        }

        private async void Infogrid_Tapped()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new ControlCenter(MyChar));
            }
            catch (Exception)
            {
            }
        }

        #region Category Buttons

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
                var btt = new Button
                {
                    Padding = new Thickness(2),
                    Text = ModelResources.Person_,
                };
                btt.Clicked += CreatePersonView;
                s4.Children.Add(btt);
            }
        }

        private void EditPerson_Click(object sender, EventArgs e)
        {
            CreatePersonView(this, new EventArgs());
        }

        /// <summary>
        /// CreatePersonView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="InvalidOperationException">Ignore.</exception>
        /// <exception cref="TypeLoadException">Ignore.</exception>
        private void CreatePersonView(object sender, EventArgs e)
        {
            var EditMode = false;
            if (sender is Button b)
            {
                HighlightButton(b);
            }
            else if (sender is GCharHolder)
            {
                EditMode = true;
            }
            else
            {
                return;
            }
            if (CharPerson.CreateContent() is View v && v.FindByName("ContentLayout") is Layout<View> contentLayout)
            {
                v.BindingContext = MyChar.Person;
                ContentPanel.Content = v;
                var entryTemplate = v.Resources["EntryTemplate"] as DataTemplate;
                foreach (var item in ReflectionHelper.GetProperties(MyChar.Person, typeof(Used_UserAttribute)).Where(x => x.GetCustomAttributes(true).OfType<Used_UserAttribute>().FirstOrDefault().UIRelevant))
                {
                    var entry = entryTemplate.CreateContent() as View;
                    entry.FindByName<Label>("Type").Text = ModelResources.ResourceManager.GetStringSafe(nameof(Person) + "_" + item.Name);

                    View entrycontent;
                    if (EditMode)
                    {
                        if (item.PropertyType == typeof(DateTime))
                        {
                            entrycontent = new DatePicker
                            {
                                Format = "d"
                            };
                            entrycontent.SetBinding(DatePicker.DateProperty, item.Name);
                        }
                        else
                        {
                            entrycontent = new Entry();
                            entrycontent.Margin = new Thickness(entrycontent.Margin.VerticalThickness + 4);
                            entrycontent.SetBinding(Entry.TextProperty, item.Name);
                        }
                    }
                    else
                    {
                        entrycontent = new Label();
                        entrycontent.SetBinding(Label.TextProperty, item.Name);
                        if (item.PropertyType == typeof(DateTime))
                        {
                            entrycontent.SetBinding(Label.TextProperty, item.Name, stringFormat: "{0:d}");
                        }
                    }
                    entry.FindByName<Layout<View>>("ContentPanel").Children.Add(entrycontent);
                    contentLayout.Children.Add(entry);
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
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }

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

        #endregion Category Buttons

        #region AdaptiveUI

        private bool _Narrow;
        public bool Narrow
        {
            get => _Narrow;
            set { if (_Narrow != value) { _Narrow = value; ChangeUi(); } }
        }

        private bool _MenuOpen = true;
        public bool MenuOpen
        {
            get => _MenuOpen;
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

        private void SetViewParameters()
        {
            Narrow = Width < 650;
            if (Width > 650 && MyChar != null)
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

        #endregion AdaptiveUI

        #region Char Actions

        private void Toggle(object sender, EventArgs e)
        {
            MenuOpen = !MenuOpen;
        }

        private (string, Action)[] MenuItems => new (string, Action)[] {
                        (UiResources.SaveAtCurrentPlace,()=>{SharedIO.SaveAtCurrentPlace(MyChar); }),
                        (UiResources.SaveExtern,async ()=>{SharedIO.Save(MyChar, new FileInfo(Path.Combine((await SharedIO.CurrentIO.PickFolder()).FullName, MyChar.FileInfo.Name))); }),
                        (UiResources.OpenFolder,()=>{SharedIO.CurrentIO.OpenFolder(MyChar.FileInfo.Directory); }),
                        (UiResources.SubtractLifeStyleCost,MyChar.SubtractLifeStyleCost),
                        (UiResources.CharSettings,()=>{ }),
                        (UiResources.Repair,MyChar.Repair),
                        (UiResources.Unload,Unload),
                    };

        private void MoreMenu(object sender, System.EventArgs e)
        {
            try
            {
                var Popup = new PopupMenu
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

        private static void Unload()
        {
        }
        #endregion Char Actions

        #region Search Stuff

        /// <summary>
        /// AutoSuggestBox_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="InvalidOperationException">Ignore.</exception>
        private void AutoSuggestBox_TextChanged(object sender, AutoSuggestBoxTextChangedEventArgs e)
        {
            if (sender is AutoSuggestBox asb)
            {
                if (e.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
                {
                    asb.ItemsSource = MyChar.Things.Where(x => MyChar.Settings.CategoryOptions.First(y => y.ThingType == x.ThingType).Visibility).Where(x => x.SimilaritiesTo(asb.Text) > 0).OrderByDescending(x => x.SimilaritiesTo(asb.Text)).ToList();
                }
            }
        }

        private async void AutoSuggestBox_QuerySubmitted(object sender, AutoSuggestBoxQuerySubmittedEventArgs e)
        {
            if (sender is AutoSuggestBox asb)
            {
                if ((e.ChosenSuggestion as Thing ?? asb.ItemsSource?.OfType<Thing>()?.FirstOrDefault()) is Thing t)
                {
                    await PopupNavigation.Instance.PushAsync(new DetailsPage(t, MyChar));
                    asb.Text = "";
                    asb.ItemsSource = null;
                    asb.IsSuggestionListOpen = false;
                }
            }
        }
        #endregion Search Stuff
    }
}