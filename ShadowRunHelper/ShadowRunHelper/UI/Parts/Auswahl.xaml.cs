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
        public ObservableCollection<ThingListEntry> lstZusammensetzung;
        private List<ThingListEntry> lstThings;

        public Auswahl(ObservableCollection<ThingListEntry> data, List<ThingListEntry> i_lstAll)
        {
            if (i_lstAll == null)
            {
                throw new ArgumentNullException(ExceptionMessages.AllListChooser_AllList_Null);
            }
            if (i_lstAll.Count <= 0)
            {
                throw new ArgumentNullException(ExceptionMessages.AllListChooser_AllList_Empty);
            }
            if (data == null)
            {
                throw new System.ArgumentNullException(ExceptionMessages.AllListChooser_Data_Null);
            }

            lstThings = i_lstAll;
            lstZusammensetzung = data;

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
            lstZusammensetzung.Clear();
            foreach (ThingListEntry item in Zus_ListVIew.SelectedItems)
            {
                lstZusammensetzung.Add(item);
            }
            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }

        private void Zus_ListVIew_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            foreach (var item in lstZusammensetzung)
            {
                var tepmindex = lstThings.FindIndex(x => (x.Object == item.Object && x.strProperty == item.strProperty));
                Zus_ListVIew.SelectRange(new ItemIndexRange(tepmindex, 1));
            }
        }

    }
    internal class CustomList : List<ThingListEntry>
    {
        public object Name { get; set; }
        public object Zahl { get; set; }
    }
}
