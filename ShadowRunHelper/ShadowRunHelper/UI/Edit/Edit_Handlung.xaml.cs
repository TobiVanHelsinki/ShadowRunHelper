using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper.UI.Edit
{
    public sealed partial class Edit_Handlung : ContentDialog
    {
        public CharModel.Handlung Data;
        public Controller.HashDictionary HD;
        public List<string> MyStringOptions { get; set; }

        public Edit_Handlung(CharModel.Handlung data, Controller.HashDictionary hd)
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
