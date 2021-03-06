﻿//Author: Tobi van Helsinki

using System;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using Rg.Plugins.Popup.Services;
using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.Strings;
using ShadowRunHelperViewer.UI.Controls;
using ShadowRunHelperViewer.UI.Resources;
using Syncfusion.XForms.PopupLayout;
using TLIB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.ControlsChar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsView : ContentView
    {
        public Thing MyThing { get; set; }

        private CharHolder MyChar;

        public DetailsView() => InitializeComponent();

        public void Activate(Thing thing, CharHolder mychar)
        {
            MyChar = mychar;
            MyThing = thing;
            MainContent.BindingContext = MyThing;
            BindingContext = this;
            try
            {
                Headline.Text = MyThing.ThingType.ThingDefToString(false);
                if (MyThing.ThingType == ThingDefs.Attribut || MyThing.ThingType == ThingDefs.Berechnet)
                {
                    NotesContent.IsVisible = false;
                    StandardThingContents.IsVisible = false;
                }
                if (MyThing.ThingType == ThingDefs.Note)
                {
                    NotesContent.IsVisible = false;
                    CalcContent.IsVisible = false;
                    NumberContent.IsVisible = false;
                }
                CreateView();
            }
            catch (Exception ex)
            {
                Log.Write("Unexpected Error", ex);
                CloseSelf();
            }
        }

        public void CloseSelf()
        {
            if (Common.FindParent<SfPopupLayout>(this) is SfPopupLayout popup)
            {
                popup.IsOpen = false;
            }
            ClosingRequested?.Invoke(this, new EventArgs());
        }

        public event EventHandler ClosingRequested;

        /// <summary>
        /// Close this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Clicked(object sender, EventArgs e)
        {
            CloseSelf();
        }

        internal bool OnBackButtonPressed()
        {
            if (LinkListFrame.IsVisible)
            {
                LinkListFrame.Content = null;
                LinkListFrame.IsVisible = false;
            }
            else
            {
                CloseSelf();
            }
            return true;
        }

        #region Create UI

        /// <summary>
        /// Creates the view.
        /// </summary>
        private void CreateView()
        {
            LinkListFrame.Content = null;
            LinkListFrame.IsVisible = false;

            NumberContent.Children.Clear();
            CalcContent.Children.Clear();
            OtherContent.Children.Clear();

            var NumberCounter = 0;
            foreach (var item in MyThing.GetProperties().Reverse().Where(x => !(
                                                                                    x.Name == nameof(Thing.IsFavorite) ||
                                                                                    x.Name == nameof(Thing.Order) ||
                                                                                    x.Name == nameof(Thing.ThingType) ||
                                                                                    x.Name == nameof(Thing.Bezeichner) ||
                                                                                    x.Name == nameof(Thing.Wert) ||
                                                                                    x.Name == nameof(Thing.Typ) ||
                                                                                    //x.Name == nameof(Thing.Zusatz) ||
                                                                                    x.Name == nameof(Thing.Notiz) ||
                                                                                    x.Name == nameof(Thing.FavoriteIndex) ||
                                                                                    x.Name == nameof(Handlung.GegenCalced) ||
                                                                                    x.Name == nameof(Handlung.GrenzeCalced) ||
                                                                                    x.Name == nameof(SuperNaturalSkills.Reichweite) ||
                                                                                    x.Name == nameof(SuperNaturalSkills.Dauer) ||
                                                                                    x.Name == nameof(SuperNaturalSkills.Entzug) ||
                                                                                    x.Name == nameof(Item.Aktiv) ||
                                                                                    x.Name == nameof(Item.Besitz)
                                                                                    )))
            {
                var Name = CreateNameLabel(item);

                View Content;
                //Part 2: Create content of rigth type
                if (item.PropertyType == typeof(bool) || item.PropertyType == typeof(bool?))
                {
                    Content = new Switch();
                    Content.SetBinding(Switch.IsToggledProperty, new Binding(item.Name));
                }
                else if (item.PropertyType == typeof(ConnectProperty))
                {
                    Content = (Resources["ConnectPropertyTemplate"] as DataTemplate).CreateContent() as View;
                    var charCalcProperty = item.GetValue(MyThing) as ConnectProperty;
                    AddConnectedValuesToViewAndRegister(Content, charCalcProperty);
                    Content.BindingContext = charCalcProperty;
                }
                else if (item.DeclaringType == typeof(Note) && item.Name == nameof(Note.Text))
                {
                    Content = (Resources["EditorTemplate"] as DataTemplate).CreateContent() as View;
                    Content.BindingContext = MyThing;
                }
                else
                {
                    Content = new Entry
                    {
                        Keyboard = item.PropertyType == typeof(int) || item.PropertyType == typeof(double) ? Keyboard.Numeric : Keyboard.Text,
                        Style = Resources.MergedDictionaries.Any() ? Resources.MergedDictionaries.FirstOrDefault()["STDInput"] as Style : null,
                    };
                    Content.SetBinding(Entry.TextProperty, new Binding(item.Name));
                }
                //Part 2: Add it to the rigth panel
                if (item.PropertyType == typeof(double) || item.PropertyType == typeof(double?))
                {
                    Content.SetBinding(Entry.TextProperty, new Binding(item.Name, converter: new IO_Calculation()));
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
                else if (item.PropertyType == typeof(ConnectProperty))
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

        private void AddConnectedValuesToViewAndRegister(View Content, ConnectProperty charCalcProperty)
        {
            charCalcProperty.Connected.CollectionChanged += Connected_CollectionChanged;
            void Connected_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => AddConnectedValuesToView(Content, charCalcProperty);
            AddConnectedValuesToView(Content, charCalcProperty);
        }

        private void AddConnectedValuesToView(View Content, ConnectProperty charCalcProperty)
        {
            var panel = Content.FindByName<StackLayout>("CalcPropertyPanel");
            panel.Children.Clear();
            foreach (var item in charCalcProperty.Connected)
            {
                var item1 = (Resources["ConnectedTemplate"] as DataTemplate).CreateContent() as View;
                item1.BindingContext = item;
                panel.Children.Add(item1);
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
                Text = v ?? "//" + item.Name + "\\",
                Style = Resources.MergedDictionaries.Any() ? Resources.MergedDictionaries.FirstOrDefault()["STDText"] as Style : null,
            };
        }
        #endregion Create UI

        private void OpenConnectedChooser(object sender, EventArgs e)
        {
            if (sender is BindableObject b && b.BindingContext is ConnectProperty prop)
            {
                try
                {
                    var page = new LinkListChooser(MyChar, prop);
                    page.ClosingRequested += (s, e) =>
                    {
                        LinkListFrame.Content = null;
                        LinkListFrame.IsVisible = false;
                    };
                    LinkListFrame.Content = page;
                    LinkListFrame.IsVisible = true;
                }
                catch (Exception ex)
                {
                    Log.Write("Could not Create " + nameof(LinkListChooser), ex, logType: LogType.Error);
                }
            }
        }

        #region More

        private void More_Clicked(object sender, EventArgs e)
        {
            if (Common.FindParent<SfPopupLayout>(sender as Element) is SfPopupLayout popup)
            {
                popup.PopupView.ContentTemplate = Resources["CatMoreTemplate"] as DataTemplate;
                popup.PopupView.AnimationMode = AnimationMode.SlideOnTop;
                popup.PopupView.AutoSizeMode = AutoSizeMode.Both;
                popup.PopupView.ShowHeader = false;
                popup.PopupView.ShowFooter = false;
                popup.ShowRelativeToView(sender as View ?? popup, RelativePosition.AlignToRightOf);
            }
        }

        /// <summary>
        /// Removes mything and close the page
        /// </summary>
        private void Delete_Clicked(object sender, EventArgs e)
        {
            MyChar.Remove(MyThing);
            CloseSelf();
        }

        /// <summary>
        /// Take the Name of MyThing and convert all words in lowercase with one leading uppercase
        /// </summary>
        /// <exception cref="InvalidOperationException">Ignore.</exception>
        public void TextRefactoring_Case(object sender, EventArgs e)
        {
            var text = MyThing.Bezeichner.Replace("\r", " ").Replace("\n", " ").Replace("  ", " ");
            var builder = new StringBuilder();
            foreach (var word in text.Split(' ', '-'))
            {
                if (word.Length == 1)
                {
                    builder.Append(word);
                }
                else if (word.Length > 2)
                {
                    builder.Append(word.First());
                    builder.Append(word.Skip(1).Select(x => char.ToLower(x)).ToArray());
                }
                builder.Append(' ');
            }
            MyThing.Bezeichner = builder.ToString(0, builder.Length - 1);
        }

        /// <summary>
        /// Removed all but the first newline of MyThing.Notes. Also removes \r.
        /// </summary>
        public void TextRefactoring_NewLine(object sender, EventArgs e)
        {
            var text = MyThing.Notiz.Replace("\r", "\n").Replace("\n\n", "\n").Trim().Trim('\n');
            var firstindex = text.IndexOf("\n");
            if (firstindex > -1)
            {
                var firstline = text.Substring(0, firstindex);
                var rest = text.Substring(firstindex + 1, text.Length - firstindex - 1);
                text = firstline + "\n" + rest.Replace("\n", " ");
            }
            MyThing.Notiz = text;
        }

        private void CopyTo(object sender, EventArgs e)
        {
            _ = RgPopUp.DisplayDefaultPopUp(new ThingCopyChooser(MyThing, MyChar, false));
            CloseSelf();
        }

        private void MoveTo(object sender, EventArgs e)
        {
            _ = RgPopUp.DisplayDefaultPopUp(new ThingCopyChooser(MyThing, MyChar, true));
            CloseSelf();
        }
        #endregion More
    }
}