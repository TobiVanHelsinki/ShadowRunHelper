using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper.UI.Edit
{
    public sealed partial class Edit_Vorteil : ContentDialog
    {
        public CharModel.Vorteil Data;
        public Controller.HashDictionary HD;

        public Edit_Vorteil(CharModel.Vorteil data, Controller.HashDictionary hd)
        {
            this.InitializeComponent();
            this.Data = data;
            this.HD = hd;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }
    }
}
