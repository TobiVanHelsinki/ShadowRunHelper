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
            CreateItems();
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
                if (Name.Text == null || Name.Text == ": ")
                {

                }
                Grid.SetRow(Name, RowCounter);
                MainContent.Children.Add(Name);
                View Content;
                if (item.PropertyType == typeof(bool) || item.PropertyType == typeof(bool?))
                {
                    Content = new Switch();
                    Content.SetBinding(Switch.IsToggledProperty, new Binding(item.Name));
                }
                else if (item.PropertyType == typeof(CharProperty))
                {
                    Name.Text = "Wert2";
                    Content = new Label() { };
                }
                else
                {
                    if (Editable)
                    {
                       Content = new Entry();
                       Content.SetBinding(Entry.TextProperty, new Binding(item.Name));
                    }
                    else
                    {
                        Content = new Label { VerticalOptions = LayoutOptions.Center };
                        Content.SetBinding(Label.TextProperty, new Binding(item.Name));
                    }
                }
                Grid.SetRow(Content, RowCounter);
                Grid.SetColumn(Content, 2);
                MainContent.Children.Add(Content);
                RowCounter++;
            }
        }

        private void Close_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            Editable = !Editable;
            CreateItems();
        }
    }
}
