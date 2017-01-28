using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper.UI.Edit
{
    public sealed partial class Edit_Person2 : ContentDialog
    {
        public CharModel.Person Data;

        public Edit_Person2(CharModel.Person data)
        {
            this.InitializeComponent();
            this.Data = data;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }
    }
}
