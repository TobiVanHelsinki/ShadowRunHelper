using System;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ShadowRunHelper.UI
{
    public sealed partial class MultiButtonMessageDialog : ContentDialog
    {
        public MultiButtonMessageDialog()
        {
            this.InitializeComponent();
        }

        public MultiButtonMessageDialog(string title, string message, params (string, Action)[] args) : this()
        {
            if (args.Length <= 0)
            {
                throw new IndexOutOfRangeException();
            }
            Title = title;
            MessageBox.Text = message;
            bool twocolumns = false;
            ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new Windows.UI.Xaml.GridLength(1, Windows.UI.Xaml.GridUnitType.Star) });
            if (args.Length % 2 == 0) // even
            {
                twocolumns = true;
                ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new Windows.UI.Xaml.GridLength(1, Windows.UI.Xaml.GridUnitType.Star) });
            }
            for (int i = 0; i < (twocolumns ? args.Length / 2: args.Length); i++)
            {
                ButtonGrid.RowDefinitions.Add(new RowDefinition() { Height = new Windows.UI.Xaml.GridLength(1, Windows.UI.Xaml.GridUnitType.Auto) });
            }
            int index = 0;
            foreach (var item in args)
            {
                var btn = new Button()
                {
                    Content = item.Item1
                };
                btn.Click += (s, e) =>
                {
                    item.Item2?.Invoke();
                    Close();
                };
                Grid.SetColumn(btn, twocolumns ? index % 2 : 0);
                Grid.SetRow(btn, twocolumns ? index / 2 : index);

                btn.Margin = new Windows.UI.Xaml.Thickness(3);
                btn.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;

                ButtonGrid.Children.Add(btn);


                if (args.First() == item)
                {
                    btn.Focus(FocusState.Programmatic);
                    btn.Background = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColor"]);
                    btn.Foreground = new SolidColorBrush(Colors.FloralWhite);
                }
                index++;
            }
        }

        private void Close()
        {
            Hide();
        }
    }
}
