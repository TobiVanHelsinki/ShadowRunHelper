using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper1_3.UI.Edit
{
    public sealed partial class Edit_Nahkampfwaffe : ContentDialog
    {
        public CharModel.Nahkampfwaffe Data;
        public Controller.HashDictionary HD;

        public Edit_Nahkampfwaffe(CharModel.Nahkampfwaffe data, Controller.HashDictionary hd)
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
