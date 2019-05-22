using Rg.Plugins.Popup.Services;
using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public Thing MyThing { get; set; }
        public bool Editable { get; set; }
        public DetailsPage(Thing thing, bool editable)
        {
            MyThing = thing;
            Editable = editable;
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
                CreateItems();
            }
            catch (Exception ex)
            {
                TLIB.Log.Write("Unexpected Error", ex);
                if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
                Close_Clicked(this, new EventArgs());
            }
            base.OnAppearing();
        }

        void CreateItems()
        {
            MainContent.Children.Clear();
            var RowCounter = 0;
            var Ignore = new string[] { nameof(Thing.FavoriteIndex), nameof(Thing.Order), nameof(Thing.ThingType) };
            foreach (var item in Thing.GetProperties(MyThing).Reverse())
            {
                if (Ignore.Contains(item.Name))
                {
                    continue;
                }
                var Name = new Label
                {
                    Text = CustomManager.GetString("Model_" + item.DeclaringType.Name + "_" + item.Name + "/Text") + ": ",
                };

                System.Diagnostics.Debug.WriteLine(item.Name + " - " + Name.Text);
                Grid.SetRow(Name, RowCounter);
                Grid.SetColumn(Name, 2);
                RowCounter++;

                View Content;
                if (item.PropertyType == typeof(bool) || item.PropertyType == typeof(bool?))
                {
                    Content = new Switch();
                    Content.SetBinding(Switch.IsToggledProperty, new Binding(item.Name));
                }
                else if (item.PropertyType == typeof(CharCalcProperty))
                {
                    Name.Text = "Wert2";
                    var CalcPropGrid = new Grid();
                    CalcPropGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
                    CalcPropGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
                    CalcPropGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
                    CalcPropGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                    CalcPropGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
                    View BaseVal;
                    if (Editable)
                    {
                        BaseVal = new Entry();
                        BaseVal.SetBinding(Entry.TextProperty, new Binding(item.Name + "." + nameof(CharCalcProperty.BaseValue)));
                    }
                    else
                    {
                        BaseVal = new Label { VerticalOptions = LayoutOptions.Center };
                        BaseVal.SetBinding(Label.TextProperty, new Binding(item.Name + "." + nameof(CharCalcProperty.BaseValue)));
                    }
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
                    var ConnectedValues = new CollectionView() { // ist beta und löst beim disposen einen fehler aus :/
                        ItemsLayout = ListItemsLayout.HorizontalList,
                        ItemTemplate = Resources["ConnectedTemplate"] as DataTemplate,
                    }; 
                    ConnectedValues.SetBinding(ItemsView.ItemsSourceProperty, new Binding(item.Name + "." + nameof(CharCalcProperty.Connected)));
                    ConnectedValues.VerticalOptions = LayoutOptions.Center;
                    ConnectedValues.HeightRequest = 50;
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
                    if (Editable)
                    {
                        Content = new Entry
                        {
                        };
                        Content.SetBinding(Entry.TextProperty, new Binding(item.Name));
                    }
                    else
                    {
                        var InnerContent = new Label { VerticalOptions = LayoutOptions.Center };
                        InnerContent.SetBinding(Label.TextProperty, new Binding(item.Name));
                        Content = new Frame() {Content = InnerContent, BorderColor = Color.Default };
                    }
                }
                Grid.SetRow(Content, RowCounter);
                Grid.SetColumn(Content, 2);
                if (Name.Text == null || Name.Text == ": ")
                {
                    continue;
                }
                MainContent.Children.Add(Name);
                MainContent.Children.Add(Content);
                RowCounter++;
            }
        }

        private void OpenConnectedChooser(object sender, EventArgs e)
        {
            //tODO
        }

        private void Close_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            Editable = !Editable;
            try
            {
                CreateItems();
            }
            catch (Exception ex)
            {
                TLIB.Log.Write("Unexpected Error", ex);
                if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
                Close_Clicked(this, new EventArgs());
            }
        }
    }
}
