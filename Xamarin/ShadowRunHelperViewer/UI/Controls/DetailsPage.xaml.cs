using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.Strings;
using ShadowRunHelperViewer.UI.Controls;
using ShadowRunHelperViewer.UI.Resources;
using SharedCode.Ressourcen;
using System;
using System.Linq;
using System.Reflection;
using TLIB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : PopupPage
    {
        public Thing MyThing { get; set; }
        readonly CharHolder MyChar;
        public DetailsPage(Thing thing, CharHolder mychar)
        {
            MyChar = mychar;
            MyThing = thing;
            InitializeComponent();
            MainContent.BindingContext = MyThing;
        }

        protected override void OnAppearing()
        {
            try
            {
                BindingContext = this;
            }
            catch (Exception ex)
            {
                TAPPLICATION.Debugging.TraceException(ex);
            }
            try
            {
                Headline.Text = MyThing.ThingType.ThingDefToString(false);
                CreateView();
            }
            catch (Exception ex)
            {
                TLIB.Log.Write("Unexpected Error", ex);
                if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
                Close_Clicked(this, new EventArgs());
            }
            base.OnAppearing();
        }

        void CreateView()
        {
            NumberContent.Children.Clear();
            CalcContent.Children.Clear();
            OtherContent.Children.Clear();

            var NumberCounter = 0;
            foreach (var item in Thing.GetProperties(MyThing).Reverse().Where(x => !(
                                                                                    x.Name == nameof(Thing.IsFavorite) ||
                                                                                    x.Name == nameof(Thing.Order) ||
                                                                                    x.Name == nameof(Thing.ThingType) ||
                                                                                    x.Name == nameof(Thing.Bezeichner) ||
                                                                                    x.Name == nameof(Thing.Wert) ||
                                                                                    x.Name == nameof(Thing.Typ) ||
                                                                                    x.Name == nameof(Thing.Zusatz) ||
                                                                                    x.Name == nameof(Thing.Notiz) ||
                                                                                    x.Name == nameof(Thing.FavoriteIndex) ||
                                                                                    x.Name == nameof(Thing.WertCalced) ||
                                                                                    x.Name == nameof(Handlung.GegenCalced) ||
                                                                                    x.Name == nameof(Handlung.GrenzeCalced) ||
                                                                                    x.Name == nameof(Item.Aktiv) ||
                                                                                    x.Name == nameof(Item.Besitz)
                                                                                    )))
            {
                var Name = CreateNameLabel(item);
                
                View Content;
                if (item.PropertyType == typeof(bool) || item.PropertyType == typeof(bool?))
                {
                    Content = new Switch();
                    Content.SetBinding(Switch.IsToggledProperty, new Binding(item.Name));
                }
                else if (item.PropertyType == typeof(CharCalcProperty))
                {
                    var CalcPropGrid = new Grid();
                    CalcPropGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
                    CalcPropGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
                    CalcPropGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
                    CalcPropGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                    CalcPropGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
                    View BaseVal;
                    BaseVal = new Entry();
                    BaseVal.SetBinding(Entry.TextProperty, new Binding(item.Name + "." + nameof(CharCalcProperty.BaseValue)));
                    Grid.SetColumn(BaseVal, 0);
                    BaseVal.VerticalOptions = LayoutOptions.Center;
                    CalcPropGrid.Children.Add(BaseVal);
                    // ####################
                    var PlusButton = new Button() { Text = "+" };
                    PlusButton.Clicked += OpenConnectedChooser;
                    Grid.SetColumn(PlusButton, 1);
                    PlusButton.VerticalOptions = LayoutOptions.Center;
                    CalcPropGrid.Children.Add(PlusButton);
                    // ####################
                    var ConnectedValues = new CollectionView
                    { // ist beta und löst beim disposen einen fehler aus 
                        ItemsLayout = ListItemsLayout.Horizontal,
                        ItemTemplate = Resources["ConnectedTemplate"] as DataTemplate,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HeightRequest = 50
                    };
                    ConnectedValues.SetBinding(ItemsView.ItemsSourceProperty, new Binding(item.Name + "." + nameof(CharCalcProperty.Connected)));
                    var Scroller = new ScrollView() { Content = ConnectedValues };
                    Scroller.VerticalOptions = LayoutOptions.Center;
                    Grid.SetColumn(Scroller, 2);
                    CalcPropGrid.Children.Add(Scroller);
                    // ####################
                    View CalcedVal = new Label { VerticalOptions = LayoutOptions.Center };
                    CalcedVal.SetBinding(Label.TextProperty, new Binding(item.Name + "." + nameof(CharCalcProperty.Value)));
                    Grid.SetColumn(CalcedVal, 3);
                    CalcedVal.VerticalOptions = LayoutOptions.Center;
                    CalcPropGrid.Children.Add(CalcedVal);
                    // ####################
                    Content = CalcPropGrid;
                }
                else
                {
                    Content = new Entry
                    {
                        Keyboard = item.PropertyType == typeof(int) || item.PropertyType == typeof(double) ? Keyboard.Numeric : Keyboard.Text
                    };
                    Content.SetBinding(Entry.TextProperty, new Binding(item.Name));
                }
                if (item.PropertyType == typeof(double) || item.PropertyType == typeof(double?))
                {
                    if (NumberCounter % 2 == 0)
                    {
                        NumberContent.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
                        NumberContent.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
                    }

                    Grid.SetRow(Name, NumberCounter / 2 * 2);
                    Grid.SetRow(Content, NumberCounter / 2 * 2 + 1);

                    Grid.SetColumn(Name, NumberCounter % 2);
                    Grid.SetColumn(Content, NumberCounter % 2);
                    NumberCounter++;
                    NumberContent.Children.Add(Name);
                    NumberContent.Children.Add(Content);
                }
                else if (item.PropertyType == typeof(CharCalcProperty))
                {
                    CalcContent.Children.Add(Name);
                    CalcContent.Children.Add(Content);
                }
                else
                {
                    OtherContent.Children.Add(Name);
                    OtherContent.Children.Add(Content);
                }
            }
        }

        /// <summary>
        /// create a label containing a localized string for this property
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private Label CreateNameLabel(PropertyInfo item)
        {
            var v = MyThing.ThingType.HierarchieUpSearch(s => new ModelResourcesExtension() { Text = s + "_" + item.Name }.ProvideString());
            return new Label
            {
                Text = v ?? "NoValueFor: " + item.Name
            };
        }

        async void OpenConnectedChooser(object sender, EventArgs e)
        {
            //var page = new LinkListChooser(MyChar, MyThing.Wert2.Connected.Select(x => x.Connected.Select(y => y.)));
            var page = new LinkListChooser(MyChar, null);
            page.Disappearing += Page_Disappearing;
            try
            {
                await PopupNavigation.Instance.PushAsync(page);
            }
            catch (Exception)
            {
            }
        }

        private void Page_Disappearing(object sender, EventArgs e)
        {
            if (sender is LinkListChooser page && page.Result)
            {
                if (MyThing is Handlung h)
                {
                    h.Wert2.Connected.Clear();
                    h.Wert2.Connected.AddRange(page.Selected);
                }
                try
                {
                    //CreateView();
                }
                catch (Exception)
                {
                    if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
                }
            }
        }

        /// <summary>
        /// to prevent ugly visual at big screens
        /// border is set to feel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopupPage_SizeChanged(object sender, EventArgs e) => (MainFrame.WidthRequest, MainFrame.HeightRequest) = Common.MaximumDimensions(Width, Height);

        /// <summary>
        /// Removes mything and close the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Clicked(object sender, EventArgs e)
        {
            MyChar.Remove(MyThing);
            Close_Clicked(sender, e);
        }
        /// <summary>
        /// Close this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

    }
}
