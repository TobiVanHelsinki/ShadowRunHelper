using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

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

        void EditBox_GotFocus(object sender, RoutedEventArgs e) => SharePageFunctions.EditBox_GotFocus(sender, e);

        void EditBox_ProcessKeyboardAccelerators(UIElement sender, ProcessKeyboardAcceleratorEventArgs args) => SharePageFunctions.EditBox_ProcessKeyboardAccelerators(sender, args);

    }
}
