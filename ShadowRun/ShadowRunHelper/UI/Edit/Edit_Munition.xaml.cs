using Windows.UI.Xaml.Controls;

namespace ShadowRun_Charakter_Helper.UI.Edit
{
    public sealed partial class Edit_Munition : ContentDialog
    {
        public CharModel.Munition Data;
        public Controller.HashDictionary HD;
        public Edit_Munition(CharModel.Munition data, Controller.HashDictionary hd)
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
