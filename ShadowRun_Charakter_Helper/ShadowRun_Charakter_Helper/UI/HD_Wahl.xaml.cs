using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using ShadowRun_Charakter_Helper.Controller;
using ShadowRun_Charakter_Helper.Model;
using ShadowRun_Charakter_Helper.CharModel;
using Windows.UI.Xaml.Data;

namespace AppUIBasics.ControlPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HD_Wahl : ContentDialog
    {
        public ShadowRun_Charakter_Helper.Controller.HashDictionary HD;
        public ShadowRun_Charakter_Helper.CharModel.Handlung data;

        public HD_Wahl(Handlung data, HashDictionary hD)
        {
            this.InitializeComponent();
            this.data = data;
            HD = hD;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Dictionary<int, DictionaryCharEntry> tempDic = new Dictionary<int, DictionaryCharEntry>();
            foreach (var item in Zus_ListVIew.SelectedRanges)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    tempDic.Add(item.FirstIndex + i + 1, HD[item.FirstIndex + i + 1]);
                }
            }
            data.Zusammensetzung = tempDic;

            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }

        private void Zus_ListVIew_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            foreach (var item in data.Zusammensetzung)
            {
                Zus_ListVIew.SelectRange(new ItemIndexRange(item.Key - 1, 1));
            }
        }

        private void Zus_ListVIew_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            foreach (var item in Zus_ListVIew.SelectedRanges)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    if (HD[item.FirstIndex+1+i].Typ.Contains("Handlung") || HD[item.FirstIndex+1+i].Typ.Contains("error"))
                    {
                        Zus_ListVIew.DeselectRange(new ItemIndexRange(item.FirstIndex + i, 1));
                    }
                }
            }
        }
    }
}
