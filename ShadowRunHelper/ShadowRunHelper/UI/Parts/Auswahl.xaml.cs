using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using ShadowRunHelper.CharModel;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;
using System;
using System.Linq;
using Windows.UI.Xaml;
using ShadowRunHelper.Model;

namespace ShadowRunHelper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Auswahl : ContentDialog
    {
        public Handlung data;
        private List<ThingListEntry> lstThings;
        Handlung.Mode Modus;

        public Auswahl(Handlung data, List<ThingListEntry> i_lstAll, Handlung.Mode modus)
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
            this.lstThings = i_lstAll;
            this.data = data;
            this.Modus = modus;
            List<ThingDefs> Einzahl = new List<ThingDefs>(new ThingDefs[] { ThingDefs.Attribut, ThingDefs.CyberDeck, ThingDefs.Fernkampfwaffe, ThingDefs.Kommlink, ThingDefs.Nachteil, ThingDefs.Panzerung, ThingDefs.Vehikel});
                ObservableCollection<List<ThingListEntry>> groups = new ObservableCollection<List<ThingListEntry>>();
            {
                var query = from item in lstThings
                            group item by item.Object.ThingType into g
                            //orderby g.Object
                            select new { GroupName = TypenHelper.ThingDefToString(g.Key, (Einzahl.Contains(g.Key))?false:true), Items = g };

                foreach (var g in query)
                {
                    CustomList info = new CustomList();
                    info.Name = g.GroupName;
                    info.Zahl = g.Items.Count() ;
                    foreach (var item in g.Items)
                    {
                        info.Add(item);
                    }
                    groups.Add(info);
                }
            }
            InitializeComponent();
            GroupedList.Source = groups;

        }

        /// <summary>
        /// Save Selection as new Zusammensetzung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            List<ThingListEntry> tempDic = new List<ThingListEntry>();
            foreach (ThingListEntry item in Zus_ListVIew.SelectedItems)
            {
                tempDic.Add(item);
            }
            switch (Modus)
            {
                case Handlung.Mode.Wert:
                    RefreshList(data.WertZusammensetzung, tempDic);
                    break;
                case Handlung.Mode.Grenze:
                    RefreshList(data.GrenzeZusammensetzung, tempDic);
                    break;
                case Handlung.Mode.Gegen:
                    RefreshList(data.GegenZusammensetzung, tempDic);
                    break;
                default:
                    break;
            }

            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }

        private void RefreshList(ObservableCollection<ThingListEntry> lstTarget, List<ThingListEntry> lstSource)
        {
            lstTarget.Clear();
            foreach (ThingListEntry item in lstSource)
            {
                lstTarget.Add(item);
            }
        }

        private void Zus_ListVIew_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            switch (Modus)
            {
                case Handlung.Mode.Wert:
                    SelectGui(data.WertZusammensetzung);
                    break;
                case Handlung.Mode.Grenze:
                    SelectGui(data.GrenzeZusammensetzung);
                    break;
                case Handlung.Mode.Gegen:
                    SelectGui(data.GegenZusammensetzung);
                    break;
                default:
                    break;
            }
        }

        private void SelectGui(ObservableCollection<ThingListEntry> SourceList)
        {
            foreach (var item in SourceList)
            {
                var tepmindex = lstThings.FindIndex(x => (x.Object == item.Object && x.strProperty == item.strProperty));
                Zus_ListVIew.SelectRange(new ItemIndexRange(tepmindex, 1));
            }
        }

        //private void BezeichnerTextBlock_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        //{
        //    if (((TextBlock)sender).DataContext == null)
        //    {
        //        ((TextBlock)sender).DataContext = null;
        //        ((Grid)((TextBlock)sender).Parent).DataContext = null;
        //        var Temp = Zus_ListVIew.IndexFromContainer((Grid)((TextBlock)sender).Parent);
        //        Temp = 6;
        //        //((List<ThingListEntry>)Zus_ListVIew.ItemsSource).inde
        //       ((TextBlock)sender).DataContext= lstThings[Temp];
        //        //return;
        //    }
        //    if (((ThingListEntry)((TextBlock)sender).DataContext).Value == "")
        //    {
        //        ((TextBlock)sender).Text = ((ThingListEntry)((TextBlock)sender).DataContext).Object.Bezeichner;
        //    }
        //    else
        //    {
        //        ((TextBlock)sender).Text = ((ThingListEntry)((TextBlock)sender).DataContext).Value;
        //    }
        //}

        //private void WertTextBlock_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        //{
        //    if (((TextBlock)sender).DataContext == null)
        //    {
        //        return;
        //    }
        //      ((TextBlock)sender).Text = ((ThingListEntry)((TextBlock)sender).DataContext).Object.GetValue(((ThingListEntry)((TextBlock)sender).DataContext).Value).ToString();
        //}

        
    }
    internal class CustomList : List<ThingListEntry>
    {
        public object Name { get; set; }
        public object Zahl { get; set; }
    }
}
