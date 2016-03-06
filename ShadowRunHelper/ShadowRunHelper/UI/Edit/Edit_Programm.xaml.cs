using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper.UI.Edit
    {
        public sealed partial class Edit_Programm : ContentDialog
        {
            public CharModel.Programm Data;
            public Controller.HashDictionary HD;

            public Edit_Programm(CharModel.Programm data, Controller.HashDictionary hd)
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
