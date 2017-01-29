using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper1_3.UI.Edit
{
    public sealed partial class Edit_Nachteil : ContentDialog
    {
        public CharModel.Nachteil Data;
        public Controller.HashDictionary HD;

        public Edit_Nachteil(CharModel.Nachteil data, Controller.HashDictionary hd)
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
