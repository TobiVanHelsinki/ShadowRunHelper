using Windows.UI.Xaml.Controls;

namespace ShadowRun_Charakter_Helper.UI.Edit
{
    public sealed partial class Edit_Fertigkeit : ContentDialog
    {
        public CharModel.Fertigkeit Data;
        public Controller.HashDictionary HD;

        public Edit_Fertigkeit(CharModel.Fertigkeit data, Controller.HashDictionary hd)
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
