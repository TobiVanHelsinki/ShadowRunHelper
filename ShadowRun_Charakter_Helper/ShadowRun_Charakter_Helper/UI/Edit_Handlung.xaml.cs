using System.Collections.Generic;
using ShadowRun_Charakter_Helper.CharModel;
using Windows.UI.Xaml.Controls;
using ShadowRun_Charakter_Helper.Controller;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace AppUIBasics.ControlPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Edit_Handlung : ContentDialog
    {
        public ShadowRun_Charakter_Helper.CharModel.Handlung data;
        public ShadowRun_Charakter_Helper.Controller.HashDictionary HD;
        public List<string> MyStringOptions { get; set; }

        public Edit_Handlung(Handlung data, HashDictionary hd)
        {
            this.InitializeComponent();
            this.data = data;
            this.HD = hd;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }
    }
}
