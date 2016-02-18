using System.Collections.Generic;
using ShadowRun_Charakter_Helper.CharModel;
using Windows.UI.Xaml.Controls;
using ShadowRun_Charakter_Helper.Controller;
namespace AppUIBasics.ControlPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Edit_Handlung : ContentDialog
    {
        public ShadowRun_Charakter_Helper.CharModel.Handlung data;
        public ShadowRun_Charakter_Helper.Controller.HashDictionary hd;
        private CharHolder current;

        public List<string> MyStringOptions { get; set; }

        public Edit_Handlung(Handlung data, CharHolder current)
        {
            this.InitializeComponent();
            this.data = data;
            this.current = current;
           // ComboBox CB1 = new ComboBox();
         //   CB1.DataContext = current.HD.Data.Values;
            // Set the DataContext of the TextBox MyTextBox.
            //   CB1.ItemsSource=HD.Data;
            CB2.DataContext = current.HD.Data.Values;
        }


        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }
    }
}
