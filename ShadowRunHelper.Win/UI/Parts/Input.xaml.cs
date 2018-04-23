using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace ShadowRunHelper.UI
{
    public sealed partial class Input : ContentDialog
    {
        public string InputValue = "";

        public Input()
        {
            InitializeComponent();
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }

        void EditBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        void EditBox_ProcessKeyboardAccelerators(UIElement sender, Windows.UI.Xaml.Input.ProcessKeyboardAcceleratorEventArgs args)
        {
            if (args.Key == Windows.System.VirtualKey.Up)
            {
                if (double.TryParse((sender as TextBox).Text, out double result))
                {
                    result++;
                    (sender as TextBox).Text = result.ToString();
                }
            }
            else if (args.Key == Windows.System.VirtualKey.Down)
            {
                if (double.TryParse((sender as TextBox).Text, out double result))
                {
                    result--;
                    (sender as TextBox).Text = result.ToString();
                }
            }
            args.Handled = true;
        }
    }
}
