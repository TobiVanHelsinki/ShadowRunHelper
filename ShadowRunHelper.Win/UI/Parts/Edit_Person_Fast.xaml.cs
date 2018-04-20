using ShadowRunHelper.CharModel;
using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper.UI
{
    public sealed partial class Edit_Person_Fast : ContentDialog
    {
        public Person Data;

        public Edit_Person_Fast(Person data)
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
