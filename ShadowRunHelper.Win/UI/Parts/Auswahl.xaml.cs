using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;
using System;
using System.Linq;
using ShadowRunHelper.Model;
using Shared;
using TLIB_UWPFRAME.Resources;

namespace ShadowRunHelper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Auswahl : ContentDialog
    {
        public ObservableCollection<AllListEntry> lstZusammensetzung;
        private List<AllListEntry> lstThings;

        public Auswahl(ObservableCollection<AllListEntry> data, List<AllListEntry> i_lstAll)
        {
            lstThings = i_lstAll ?? throw new AllListChooserError();
            lstZusammensetzung = data ?? throw new AllListChooserError();
            if (i_lstAll.Count <= 0)
            {
                throw new AllListChooserError();
            }

            List<ThingDefs> Einzahl = new List<ThingDefs>() {
                ThingDefs.Attribut, ThingDefs.CyberDeck, ThingDefs.Fernkampfwaffe,
                ThingDefs.Kommlink, ThingDefs.Nachteil, ThingDefs.Panzerung, ThingDefs.Vehikel };
            List<ThingDefs> Ohne = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Connection
            };
                ObservableCollection < CustomAllList> groups = new ObservableCollection<CustomAllList>();
            IEnumerable<CustomAllList> GroupedAllList = lstThings.GroupBy(item => item.Object.ThingType).Where(g=>!Ohne.Contains(g.Key)).Select(
                group =>
                {
                    var customgroup = new CustomAllList()
                    {
                        Name = TypenHelper.ThingDefToString(group.Key, !Einzahl.Contains(group.Key)),
                        Anzahl = group.Count()
                    };
                    customgroup.AddRange(group);
                    return customgroup;
                }
            );
            groups.AddRange(GroupedAllList);
            var z = GroupedAllList.ElementAt(0).Anzahl;
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
            foreach (AllListEntry item in Zus_ListVIew.SelectedItems)
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
                var tepmindex = lstThings.FindIndex(x => (x.Object == item.Object && x.PropertyID == item.PropertyID));
                Zus_ListVIew.SelectRange(new ItemIndexRange(tepmindex, 1));
                
            }
            if (Zus_ListVIew.SelectedItems.Count < lstZusammensetzung.Count)
            {
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
#endif
                AppModel.Instance.NewNotification(Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView().GetString("Notification_Error_AuswahlToLess"));
            }
        }

    }
    internal class CustomAllList : List<AllListEntry>
    {
        public string Name { get; set; }
        public int Anzahl { get; set; }
    }
}
