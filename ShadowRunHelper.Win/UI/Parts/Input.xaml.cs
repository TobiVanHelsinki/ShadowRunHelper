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
    }
}
