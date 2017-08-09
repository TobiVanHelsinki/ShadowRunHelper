﻿using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;
using System;
using System.Linq;
using ShadowRunHelper.Model;
using Shared;

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

            List<ThingDefs> Einzahl = new List<ThingDefs>(new ThingDefs[] { ThingDefs.Attribut, ThingDefs.CyberDeck, ThingDefs.Fernkampfwaffe, ThingDefs.Kommlink, ThingDefs.Nachteil, ThingDefs.Panzerung, ThingDefs.Vehikel});
            ObservableCollection<List<AllListEntry>> groups = new ObservableCollection<List<AllListEntry>>();
            {
                var query = from item in lstThings
                            group item by item.Object.ThingType into g
                            //orderby g.Object
                            select new { GroupName = TypenHelper.ThingDefToString(g.Key, (Einzahl.Contains(g.Key))?false:true), Items = g };

                foreach (var g in query)
                {
                    CustomList info = new CustomList()
                    {
                        Name = g.GroupName,
                        Zahl = g.Items.Count()
                    };
                    foreach (var item in g.Items)
                    {
                        info.Add(item);
                    }
                    if (info.Count != 0)
                    {
                        groups.Add(info);
                    }
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
                var tepmindex = lstThings.FindIndex(x => (x.Object == item.Object && x.strProperty == item.strProperty));
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
    internal class CustomList : List<AllListEntry>
    {
        public object Name { get; set; }
        public object Zahl { get; set; }
    }
}