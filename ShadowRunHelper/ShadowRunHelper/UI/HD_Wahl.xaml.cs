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
        public ShadowRun_Charakter_Helper.CharModel.Handlung data;
        private List<KeyValuePair<System.Int32, DictionaryCharEntry>> HD_List = new List<KeyValuePair<int, DictionaryCharEntry>>();
        int Modus;
        private int tepmindex;

        public HD_Wahl(Handlung data, HashDictionary hD, int modus)
        {
            this.InitializeComponent();
            this.data = data;
            foreach (var item in hD.Data)
            {
                HD_List.Add(item);
            }
            Modus = modus;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Dictionary<int, DictionaryCharEntry> tempDic = new Dictionary<int, DictionaryCharEntry>();
            foreach (var item in Zus_ListVIew.SelectedRanges)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    tempDic.Add(HD_List[item.FirstIndex + i].Key, HD_List[item.FirstIndex + i].Value);
                }
            }
            if (Modus == 1)
            {
                data.Zusammensetzung = tempDic;
            }
            else if(Modus == 2)
            {
                data.GrenzeZusammensetzung = tempDic;
            }
            

            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }

        private void Zus_ListVIew_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Modus == 1)
            {
                foreach (var item in data.Zusammensetzung)
                {
                    tepmindex= HD_List.FindIndex(x => x.Key==item.Key);

                    Zus_ListVIew.SelectRange(new ItemIndexRange(tepmindex, 1));
                }
            }
            else if (Modus == 2)
            {
                foreach (var item in data.GrenzeZusammensetzung)
                {
                    Zus_ListVIew.SelectRange(new ItemIndexRange(item.Key - 1, 1));
                }
            }
        }

        private void Zus_ListVIew_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            KeyValuePair<System.Int32, DictionaryCharEntry> temp;
            foreach (var item in Zus_ListVIew.SelectedRanges)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    temp = HD_List[item.FirstIndex + i];
                    if (temp.Value.Typ.Contains("Handlung") || temp.Value.Typ.Contains("error"))
                    {
                        Zus_ListVIew.DeselectRange(new ItemIndexRange(item.FirstIndex + i, 1));
                    }
                }
            }
        }
    }
}
