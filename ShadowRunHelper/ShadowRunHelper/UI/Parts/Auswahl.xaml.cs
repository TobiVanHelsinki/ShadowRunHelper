using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using ShadowRunHelper.CharModel;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;
using System;

namespace ShadowRunHelper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Auswahl : ContentDialog
    {
        public Handlung data;
        //private List<KeyValuePair<System.Int32, DictionaryCharEntry>> HD_List = new List<KeyValuePair<int, DictionaryCharEntry>>();
        private List<KeyValuePair<Thing, string>> lstAll;
        //Handlung.Mode Modus;
        //private int tepmindex;

        public Auswahl(Handlung data, List<KeyValuePair<Thing, string>> i_lstAll)
        {
            if (i_lstAll == null)
            {
                throw new System.ArgumentNullException(ExceptionMessages.AllListChooser_AllList_Null);
            }
            if (i_lstAll.Count <= 0)
            {
                throw new System.ArgumentNullException(ExceptionMessages.AllListChooser_AllList_Empty);
            }
            if (data == null)
            {
                throw new System.ArgumentNullException(ExceptionMessages.AllListChooser_Data_Null);
            }
            this.lstAll = i_lstAll;
            this.data = data;
            //this.Modus = modus;
            this.InitializeComponent();
        }

        /// <summary>
        /// Save Selection as new Zusammensetzung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            List<KeyValuePair<Thing, string>> tempDic = new List<KeyValuePair<Thing, string>>();
            foreach (KeyValuePair<Thing, string> item in Zus_ListVIew.SelectedItems)
            {
                tempDic.Add(item);
            }
            //switch (Modus)
            //{
            //    case Handlung.Mode.Wert:
            //        RefreshList(data.WertZusammensetzung, tempDic);
            //        break;
            //    case Handlung.Mode.Grenze:
            //        RefreshList(data.GrenzeZusammensetzung, tempDic);
            //        break;
            //    case Handlung.Mode.Gegen:
            //        RefreshList(data.GegenZusammensetzung, tempDic);
            //        break;
            //    default:
            //        break;
            //}

            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }

        private void RefreshList(ObservableCollection<KeyValuePair<Thing, string>> lstTarget, List<KeyValuePair<Thing, string>> lstSource)
        {
            lstTarget.Clear();
            foreach (KeyValuePair<Thing, string> item in lstSource)
            {
                lstTarget.Add(item);
            }
        }

        private void Zus_ListVIew_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //switch (Modus)
            //{
            //    case Handlung.Mode.Wert:
            //        SelectGui(data.WertZusammensetzung);
            //        break;
            //    case Handlung.Mode.Grenze:
            //        SelectGui(data.GrenzeZusammensetzung);
            //        break;
            //    case Handlung.Mode.Gegen:
            //        SelectGui(data.GegenZusammensetzung);
            //        break;
            //    default:
            //        break;
            //}
        }

        private void SelectGui(ObservableCollection<KeyValuePair<Thing, string>> SourceList)
        {
            foreach (var item in SourceList)
            {
                var tepmindex = lstAll.FindIndex(x => (x.Key == item.Key && x.Value == item.Value));
                Zus_ListVIew.SelectRange(new ItemIndexRange(tepmindex, 1));
            }
        }
    }
}
