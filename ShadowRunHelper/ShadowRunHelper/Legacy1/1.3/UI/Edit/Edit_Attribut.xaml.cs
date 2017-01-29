using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Inhaltsdialog" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRunHelper1_3.UI.Edit
{
    public sealed partial class Edit_Attribut : ContentDialog
    {
        public CharModel.Attribut Data;
        public Controller.HashDictionary HD;
        public List<string> MyStringOptions { get; set; }

        public Edit_Attribut(CharModel.Attribut data, Controller.HashDictionary hd)
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
