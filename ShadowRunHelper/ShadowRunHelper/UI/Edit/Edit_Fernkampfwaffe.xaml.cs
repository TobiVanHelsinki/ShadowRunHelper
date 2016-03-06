using Windows.UI.Xaml.Controls;

namespace ShadowRun_Charakter_Helper.UI.Edit
{
    public sealed partial class Edit_Fernkampfwaffe : ContentDialog
    {
        public CharModel.Fernkampfwaffe Data;
        public Controller.HashDictionary HD;

        public Edit_Fernkampfwaffe(CharModel.Fernkampfwaffe data, Controller.HashDictionary hd)
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
