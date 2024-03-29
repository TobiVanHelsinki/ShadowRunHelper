﻿//Author: Tobi van Helsinki

using dotMorten.Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.Platform.Xam;
using ShadowRunHelperViewer.UI.Pages;
using ShadowRunHelperViewer.UI.Resources;
using SharedCode.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TLIB;
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

        internal bool OnBackButtonPressed()
        {
            if (PopupNavigation.Instance.PopupStack.Count != 0)
            {
                PopupNavigation.Instance.PopAsync();
                return true;
            }
            if (ContentPanel.Content is GController gCtrl)
            {
                if (gCtrl.OnBackButtonPressed())
                {
                    return true;
                }
                else
                {
                    if (!MenuOpen)
                    {
                        MenuOpen = true;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
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
            BindingContext = this;
            Infogrid.GestureRecognizers.Add(new TapGestureRecognizer { NumberOfTapsRequired = 1, Command = new Command(Infogrid_Tapped) });
            SettingsModel.I.FIRST_START = false;
            InitButtons();
            MainPage.Instance.ViewModeChanged += Instance_ViewModeChanged;
            Instance_ViewModeChanged(ViewModes.NotSet, MainPage.Instance.CurrentViewMode);
            Features.Ui.IsCustomTitleBarEnabled = true;
            Features.Ui.SetCustomTitleBar(null);
            Features.Ui.CustomTitleBarChanges += CustomTitleBarChanges;
            Features.Ui.TriggerCustomTitleBarChanges();
            ActivateControllerOfType(MyChar.Favorites.Count == 0 ? ThingDefs.Handlung : ThingDefs.Favorite);
        }

        public void AfterLoad(ProjectPagesOptions pageOptions)
        {
            Features.Ui.SetCustomTitleBar(DependencyService.Get<IFormsInteractions>().GetRenderer(WindowsDropFrame));

            if (pageOptions == ProjectPagesOptions.CharNewChar)
            {
                CreatePersonView(this, new EventArgs());
                HighlightButton(s4.Children.OfType<Button>().LastOrDefault());
            }
        }

        private void CustomTitleBarChanges(double LeftSpace, double RigthSpace, double Heigth)
        {
            CharTitleBar.MinimumHeightRequest = Heigth;
        }

        private void Infogrid_Tapped()
        {
            _ = RgPopUp.DisplayDefaultPopUp(new ControlCenter(MyChar));
        }

        #region Category Buttons

        private void InitButtons()
        {
            if (this is ContentView Content)
            {
                foreach (StackLayout item in ButtonsPanels)
                {
                    item.Children.Clear();
                }
                foreach (CategoryOption Category in MyChar.Settings.CategoryOptions.OrderBy(x => x.Pivot).ThenBy(x => x.Order))
                {
                    Button b = new Button
                    {
                        Padding = new Thickness(2),
                        BindingContext = Category.ThingType,
                        Text = Category.ThingType.ThingDefToString(true),
                        IsVisible = Category.Visibility,
                        BackgroundColor = StyleManager.ElementBackgroundColor,
                        BorderColor = StyleManager.ElementBorderColor,
                        TextColor = StyleManager.TextColor,
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
                Button btt = new Button
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
            bool EditMode = false;
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
            if (CharPerson.CreateContent() is View v && v.FindByName("PersonDetailsPanel") is Layout<View> contentLayout)
            {
                v.BindingContext = MyChar.Person;
                ContentPanel.Content = v;
                DataTemplate entryTemplate = v.Resources["EntryTemplate"] as DataTemplate;
                foreach (System.Reflection.PropertyInfo item in ReflectionHelper.GetPropertiesWithAttribute(MyChar.Person, typeof(Used_UserAttribute)).Where(x => x.GetCustomAttributes(true).OfType<Used_UserAttribute>().FirstOrDefault().UIRelevant))
                {
                    View entry = entryTemplate.CreateContent() as View;
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
                            entrycontent = new Entry()
                            {
                                Margin = new Thickness(StyleManager.Spacing),
                                HorizontalOptions = LayoutOptions.Fill,
                            };
                            entrycontent.SetBinding(Entry.TextProperty, item.Name, BindingMode.TwoWay);
                            entrycontent.Focus(); //without content is not shown
                        }
                    }
                    else
                    {
                        entrycontent = new Label
                        {
                            HorizontalOptions = LayoutOptions.End
                        };
                        entrycontent.SetBinding(Label.TextProperty, item.Name);
                        if (item.PropertyType == typeof(DateTime))
                        {
                            entrycontent.SetBinding(Label.TextProperty, item.Name, stringFormat: "{0:d}");
                        }
                    }
                    Grid.SetRow(entrycontent, 1);
                    entry.FindByName<Layout<View>>("EntryPanel").Children.Add(entrycontent);
                    contentLayout.Children.Add(entry);
                }
            }
            if (MainPage.Instance.CurrentViewMode == ViewModes.Mobile || MainPage.Instance.CurrentViewMode == ViewModes.Tall)
            {
                MenuOpen = false;
            }
        }

        private void B_CTRL_Clicked(object sender, EventArgs e)
        {
            if (sender is Button b && b.BindingContext is ThingDefs type)
            {
                ActivateControllerOfType(type);
            }
        }
        public ICommand OpenCategory => new Command<string>(canExecute: (string arg) => true, execute: (string arg) =>
        {
            //TODO anstatt alle öffnen -> eigene darstellung designen und nur aktive anzeigen.
            int pivot = int.Parse(arg);
            StackLayout stackLayout = new StackLayout();
            foreach (ThingTypeProperty typeInThisCategory in TypeHelper.ThingTypeProperties.Where(x => x.Pivot == pivot).OrderBy(x => x.Order))
            {
                var item = CreateControllerOfType(typeInThisCategory.ThingType);
                stackLayout.Children.Add(item);
            }
            ContentPanel.Content = /*new ScrollView() { Content = */stackLayout /*}*/;
            HighlightButton(null);
        });

        private void ActivateControllerOfType(ThingDefs type)
        {
            HighlightButton(type);
            ContentPanel.Content = CreateControllerOfType(type);
        }

        private GController CreateControllerOfType(ThingDefs type)
        {
            GController gCTRL = new GController(MyChar);
            System.Reflection.PropertyInfo CTRL = typeof(CharHolder).GetProperties().FirstOrDefault(x => x.Name == "CTRL" + type);
            if (CTRL != null)
            {
                gCTRL.SetBinding(BindingContextProperty, new Binding($"{nameof(MyChar)}.{CTRL.Name}"));
            }
            else
            {
                Log.Write("Structual Error, Controller Name is wrong");
            }
            if (MainPage.Instance.CurrentViewMode == ViewModes.Mobile || MainPage.Instance.CurrentViewMode == ViewModes.Tall)
            {
                MenuOpen = false;
            }
            return gCTRL;
        }

        private void HighlightButton(ThingDefs type)
        {
            foreach (Button item in Buttons)
            {
                if (item.BindingContext is ThingDefs t && t == type)
                {
                    item.BackgroundColor = StyleManager.AccentColor;
                    item.TextColor = StyleManager.AccentInverseBinary;
                }
                else
                {
                    item.BackgroundColor = StyleManager.ElementBackgroundColor;
                    item.TextColor = StyleManager.TextColor;
                }
            }
        }

        private void HighlightButton(Button myBtn)
        {
            foreach (Button item in Buttons)
            {
                item.BackgroundColor = StyleManager.ElementBackgroundColor;
                item.TextColor = StyleManager.TextColor;
            }
            if (myBtn != null)
            {
                myBtn.BackgroundColor = StyleManager.AccentColor;
                myBtn.TextColor = StyleManager.AccentInverseBinary;
            }
        }
        #endregion Category Buttons

        #region AdaptiveUI

        private void Instance_ViewModeChanged(ViewModes oldMode, ViewModes newMode)
        {
            MenuOpen = newMode switch
            {
                ViewModes.Mobile => ContentPanel.Content == null,
                ViewModes.Tall => ContentPanel.Content == null,
                _ => true,
            };
            ChangeMenubackButtonAppearance(newMode);
            ChangeMenuAppearance(newMode);
            if (newMode != ViewModes.Wide)
            {
                WideCol1.Width = new GridLength(0);
                WideCol2.Width = new GridLength(0);
                Grid.SetColumn(CharTitleBar, 0);
                Grid.SetColumn(CharHeadControls, 0);
                Grid.SetColumn(Infogrid, 0);
                Grid.SetColumnSpan(CharTitleBar, 3);
                Grid.SetColumnSpan(CharHeadControls, 3);
                Grid.SetColumnSpan(Infogrid, 3);
                Grid.SetRow(CharTitleBar, 0);
                Grid.SetRow(CharHeadControls, 1);
                Grid.SetRow(Infogrid, 2);
            }
            else
            {
                WideCol1.Width = new GridLength(1, GridUnitType.Star);
                WideCol2.Width = new GridLength(1, GridUnitType.Star);
                Grid.SetColumn(CharTitleBar, 2);
                Grid.SetColumn(CharHeadControls, 0);
                Grid.SetColumn(Infogrid, 1);
                Grid.SetColumnSpan(CharTitleBar, 1);
                Grid.SetColumnSpan(CharHeadControls, 1);
                Grid.SetColumnSpan(Infogrid, 1);
                Grid.SetRow(CharTitleBar, 0);
                Grid.SetRow(CharHeadControls, 0);
                Grid.SetRow(Infogrid, 0);
            }
        }

        private bool _MenuOpen = true;
        public bool MenuOpen
        {
            get => _MenuOpen;
            set
            {
                if (_MenuOpen != value)
                {
                    _MenuOpen = value;
                    ChangeMenuAppearance(MainPage.Instance.CurrentViewMode);
                    ChangeMenubackButtonAppearance(MainPage.Instance.CurrentViewMode);
                }
            }
        }

        private void ChangeMenubackButtonAppearance(ViewModes newMode)
        {
            MenuToggleButton.IsVisible = !MenuOpen && (newMode == ViewModes.Mobile || newMode == ViewModes.Tall);
        }

        private void ChangeMenuAppearance(ViewModes newMode)
        {
            if (newMode == ViewModes.Mobile || newMode == ViewModes.Tall)
            {
                if (MenuOpen)
                { // just menus are visible
                    LayerContent_Col0.Width = new GridLength(1, GridUnitType.Star);
                    LayerContent_Col1.Width = new GridLength(0, GridUnitType.Absolute);
                    LayerContent_Col2.Width = new GridLength(1, GridUnitType.Star);
                    ContentPanel.IsVisible = false;
                }
                else
                { // just content is visible
                    LayerContent_Col0.Width = new GridLength(0, GridUnitType.Absolute);
                    LayerContent_Col1.Width = new GridLength(1, GridUnitType.Star);
                    LayerContent_Col2.Width = new GridLength(0, GridUnitType.Absolute);
                    ContentPanel.IsVisible = true;
                }
            }
            else
            { // all is visible
                LayerContent_Col0.Width = new GridLength(1, GridUnitType.Auto);
                LayerContent_Col1.Width = new GridLength(1, GridUnitType.Star);
                LayerContent_Col2.Width = new GridLength(1, GridUnitType.Auto);
                ContentPanel.IsVisible = true;
            }
        }

        private void ToggleMenu(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }
        #endregion AdaptiveUI

        #region Search Stuff

        /// <summary>
        /// AutoSuggestBox_TextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="InvalidOperationException">Ignore.</exception>
        private void AutoSuggestBox_TextChanged(object sender, AutoSuggestBoxTextChangedEventArgs e)
        {
            if (sender is AutoSuggestBox asb && !string.IsNullOrEmpty(asb.Text))
            {
                if (e.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
                {
                    asb.ItemsSource = MyChar.Things.Where(x => MyChar.Settings.CategoryOptions.First(y => y.ThingType == x.ThingType).Visibility).Where(x => x.SimilaritiesTo(asb.Text) > 0).OrderByDescending(x => x.SimilaritiesTo(asb.Text)).ToList();
                }
            }
        }

        private void AutoSuggestBox_QuerySubmitted(object sender, AutoSuggestBoxQuerySubmittedEventArgs e)
        {
            if (sender is AutoSuggestBox asb)
            {
                if ((e.ChosenSuggestion as Thing ?? asb.ItemsSource?.OfType<Thing>()?.FirstOrDefault()) is Thing t)
                {
                    if (ContentPanel.Content is GController gctrl)
                    {
                        gctrl.ActivateDetails(t);
                    }
                    asb.Text = "";
                    asb.ItemsSource = new string[0];
                    asb.IsSuggestionListOpen = false;
                }
            }
        }
        #endregion Search Stuff
    }
}