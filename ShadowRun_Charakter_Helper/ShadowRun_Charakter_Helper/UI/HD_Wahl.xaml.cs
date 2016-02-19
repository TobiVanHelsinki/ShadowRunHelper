using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using ShadowRun_Charakter_Helper.Controller;
using ShadowRun_Charakter_Helper.Model;
namespace AppUIBasics.ControlPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HD_Wahl : ContentDialog
    {
        public ShadowRun_Charakter_Helper.Controller.HashDictionary HD;
        public KeyValuePair<int, DictionaryCharEntry> ActiveElement_old;
        public KeyValuePair<int, DictionaryCharEntry> ActiveElement_new;
        public ShadowRun_Charakter_Helper.CharModel.Handlung data;

        public HD_Wahl(KeyValuePair<int, DictionaryCharEntry> dataContext, HashDictionary hD)
        {
            this.InitializeComponent();
            this.ActiveElement_old = dataContext;
            this.ActiveElement_new = dataContext;
            this.HD = hD;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }

        private void Auswahl_gemacht(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ActiveElement_new = ((System.Collections.Generic.KeyValuePair<int, ShadowRun_Charakter_Helper.Model.DictionaryCharEntry>)(((StackPanel)sender).DataContext));
        }

        //private async void SetActive(ShadowRun_Charakter_Helper.Model.DictionaryCharEntry element, int index)
        //{

        //   Zus_ListVIew.SelectedIndex = 3; //ActiveElement.
        //}
    }
}
