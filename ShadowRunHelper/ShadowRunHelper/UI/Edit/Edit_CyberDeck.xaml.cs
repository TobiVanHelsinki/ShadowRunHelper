using Windows.UI.Xaml.Controls;

namespace ShadowRun_Charakter_Helper.UI.Edit
{
    public sealed partial class Edit_CyberDeck : ContentDialog
    {
        public CharModel.CyberDeck Data;
        public Controller.HashDictionary HD;

        public Edit_CyberDeck(CharModel.CyberDeck data, Controller.HashDictionary hd)
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
