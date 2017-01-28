using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using ShadowRunHelper.Controller;
using ShadowRunHelper.Model;
using ShadowRunHelper.CharModel;
using Windows.UI.Xaml.Data;
using System.Collections.ObjectModel;

namespace ShadowRunHelper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HD_Wahl : ContentDialog
    {
        public ShadowRunHelper.CharModel.Handlung data;
        //private List<KeyValuePair<System.Int32, DictionaryCharEntry>> HD_List = new List<KeyValuePair<int, DictionaryCharEntry>>();
        private List<KeyValuePair<CharModel.Thing, string>> lstAll;
        int Modus;
        //private int tepmindex;

        public HD_Wahl(Handlung data, List<KeyValuePair<CharModel.Thing, string>> i_lstAll, int modus)
        {
            this.lstAll = i_lstAll;
            //lstAll = new ObservableCollection<Thing>();
            this.data = data;
            this.Modus = modus;
            this.InitializeComponent();
        }

        /// <summary>
        /// Save Selection as new Zusammensetzung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            List<KeyValuePair<CharModel.Thing, string>> tempDic = new List<KeyValuePair<CharModel.Thing, string>>();
            foreach (KeyValuePair<CharModel.Thing, string> item in Zus_ListVIew.SelectedItems)
            {
                tempDic.Add(item);
            }

            if (Modus == 1)
            {
                RefreshList(data.Zusammensetzung, tempDic);
            }
            else if(Modus == 2)
            {
                RefreshList(data.GrenzeZusammensetzung, tempDic);
            }
            else if (Modus == 3)
            {
                RefreshList(data.GegenZusammensetzung, tempDic);
            }

            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }

        private void RefreshList(ObservableCollection<KeyValuePair<CharModel.Thing, string>> lstTarget, List<KeyValuePair<CharModel.Thing, string>> lstSource)
        {
            lstTarget.Clear();
            foreach (KeyValuePair<CharModel.Thing, string> item in lstSource)
            {
                lstTarget.Add(item);
            }
        }


        private void Zus_ListVIew_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //if (Modus == 1)
            //{
            //    foreach (var item in data.Zusammensetzung)
            //    {
            //        tepmindex= HD_List.FindIndex(x => x.Key==item.Key);

            //        Zus_ListVIew.SelectRange(new ItemIndexRange(tepmindex, 1));
            //    }
            //}
            //else if (Modus == 2)
            //{
            //    foreach (var item in data.GrenzeZusammensetzung)
            //    {
            //        Zus_ListVIew.SelectRange(new ItemIndexRange(item.Key - 1, 1));
            //    }
            //}
            //else if (Modus == 3)
            //{
            //    foreach (var item in data.GegenZusammensetzung)
            //    {
            //        Zus_ListVIew.SelectRange(new ItemIndexRange(item.Key - 1, 1));
            //    }
            //}
        }

        private void Zus_ListVIew_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            //KeyValuePair<System.Int32, DictionaryCharEntry> temp;
            //foreach (var item in Zus_ListVIew.SelectedRanges)
            //{
            //    for (int i = 0; i < item.Length; i++)
            //    {
            //        temp = HD_List[item.FirstIndex + i];
            //        if (temp.Value.Typ.Contains("Handlung") || temp.Value.Typ.Contains("error"))
            //        {
            //            Zus_ListVIew.DeselectRange(new ItemIndexRange(item.FirstIndex + i, 1));
            //        }
            //    }
            //}
        }
    }
}
