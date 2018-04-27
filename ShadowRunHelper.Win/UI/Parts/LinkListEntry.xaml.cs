using ShadowRunHelper.Model;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper.UI
{
    public sealed partial class LinkListEntry : UserControl
    {
        CharHolder Model => AppModel.Instance.MainObject;
        public LinkListEntry()
        {
            InitializeComponent();
        }

        public LinkList List
        {
            get { return (LinkList)GetValue(ListProperty); }
            set { SetValue(ListProperty, value); }
        }
        public static readonly DependencyProperty ListProperty =
            DependencyProperty.Register("List", typeof(LinkList), typeof(LinkListEntry), new PropertyMetadata(0));

        public string Text
        {
            get { return GetValue(TextProperty) as string; }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(LinkListEntry), new PropertyMetadata(0));

        public string Value
        {
            get { return GetValue(ValueProperty) as string; }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(LinkListEntry), new PropertyMetadata(0));

        public string RawValue
        {
            get { return GetValue(RawValueProperty) as string; }
            set { SetValue(RawValueProperty, value); }
        }
        public static readonly DependencyProperty RawValueProperty =
            DependencyProperty.Register("RawValue", typeof(string), typeof(LinkListEntry), new PropertyMetadata(0));


        async void Edit_Click(object sender, RoutedEventArgs e)
        {
            LinkListChoice dialog = new LinkListChoice(Model, List, Filter: List.FilterOut);
            var ergebnis = await dialog.ShowAsync();
        }

    }
}
