using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;
using System.Linq;
using ShadowRunHelper.Model;

using TLIB;
using Microsoft.AppCenter.Analytics;

namespace ShadowRunHelper.UI
{

    public partial class Auswahl : ContentDialog
    {
        ObservableThingListEntryCollection lstZusammensetzung;
        List<AllListEntry> lstThings;
        bool isMultichoice;
        IEnumerable<ThingDefs> FilterOut;

        public Auswahl(List<AllListEntry> i_lstAll, ObservableThingListEntryCollection data, bool Multichoice = true, IEnumerable<ThingDefs> Filter = null)
        {
            InitializeComponent();

            lstThings = i_lstAll ?? throw new AllListChooserError();
            lstZusammensetzung = data ?? throw new AllListChooserError();
            if (i_lstAll.Count <= 0)
            {
                throw new AllListChooserError();
            }
            isMultichoice = Multichoice;
            if (!Multichoice)
            {
                Zus_ListVIew.SelectionMode = ListViewSelectionMode.Single;
            }
            FilterOut = Filter ?? throw new AllListChooserError();
            PrepareGuiList();
        }
        public virtual void PrepareGuiList()
        {
            List<ThingDefs> Einzahl = new List<ThingDefs>() {
                ThingDefs.Attribut, ThingDefs.CyberDeck, ThingDefs.Fernkampfwaffe,
                ThingDefs.Kommlink, ThingDefs.Nachteil, ThingDefs.Panzerung, ThingDefs.Vehikel };
            ObservableCollection<CustomAllList> groups = new ObservableCollection<CustomAllList>();
            IEnumerable<CustomAllList> GroupedAllList = lstThings.GroupBy(item => item.Object.ThingType).Where(g => !FilterOut.Contains(g.Key)).Select(
                group =>
                {
                    var customgroup = new CustomAllList()
                    {
                        Name = TypeHelper.ThingDefToString(group.Key, !Einzahl.Contains(group.Key)),
                        Anzahl = group.Count()
                    };
                    customgroup.AddRange(group);
                    return customgroup;
                }
            );
            groups.AddRange(GroupedAllList);
            GroupedList.Source = groups;
        }
        /// <summary>
        /// Save Selection as new Zusammensetzung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            lstZusammensetzung.Clear();
            foreach (AllListEntry item in Zus_ListVIew.SelectedItems)
            {
                lstZusammensetzung.Add(item);
            }
            deferral.Complete();
            Analytics.TrackEvent("LinkChoosing Used");

        }

        protected virtual void Zus_ListVIew_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (isMultichoice)
            {
                foreach (var item in lstZusammensetzung)
                {
                    var ItemToUse = Zus_ListVIew.Items.FirstOrDefault(x=> (x as AllListEntry).Object == item.Object && (x as AllListEntry).PropertyID == item.PropertyID);
                    var tepmindex = Zus_ListVIew.Items.IndexOf(ItemToUse);
                    Zus_ListVIew.SelectRange(new ItemIndexRange(tepmindex, 1));
                }
            }
            else
            {
                Zus_ListVIew.SelectedItem = lstZusammensetzung.FirstOrDefault();
            }
            if (Zus_ListVIew.SelectedItems.Count < lstZusammensetzung.Count)
            {
                Analytics.TrackEvent("Err_LinkChoosing Zus_ListVIew.SelectedItems.Count < lstZusammensetzung.Count");
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
#endif
                AppModel.Instance.NewNotification(Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView().GetString("Notification_Error_AuswahlToLess"));
            }
        }

    }
    public class CustomAllList : List<AllListEntry>
    {
        public string Name { get; set; }
        public int Anzahl { get; set; }
    }
}
