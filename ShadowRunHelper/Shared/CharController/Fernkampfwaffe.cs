using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharController
{
    public class cFernkampfwaffeController : cController<Fernkampfwaffe>
    {
        private ThingListEntry MI_Wert;
        private ThingListEntry MI_DK;
        private ThingListEntry MI_RK;
        private ThingListEntry MI_Pr;
        public Fernkampfwaffe ActiveItem; 

        public cFernkampfwaffeController()
        {
            var res = ResourceLoader.GetForCurrentView();
            ActiveItem = new Fernkampfwaffe();
            ActiveItem.Bezeichner = res.GetString("Model_Fernkampfwaffe__Aktiv/Text");
            MI_Wert = new ThingListEntry(ActiveItem, res.GetString("Model_Waffe_Wert/Text"));
            MI_DK = new ThingListEntry(ActiveItem, res.GetString("Model_Waffe_PB/Text"), "PB");
            MI_Pr = new ThingListEntry(ActiveItem, res.GetString("Model_Waffe_Präzision/Text"), "Präzision");
            MI_RK = new ThingListEntry(ActiveItem, res.GetString("Model_Fernkampfwaffe_Rückstoß/Text"), "Rückstoß");

            Data.CollectionChanged += Data_CollectionChanged;
        }

        private void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Refresh();
            foreach (var item in Data)
            {
                item.PropertyChanged -= (x, y) => Refresh();
                item.PropertyChanged += (x, y) => Refresh();
            }
        }

        private void Refresh()
        {
            foreach (var item in Data)
            {
                if (item.Aktiv == true)
                {
                    Thing Temp = (Thing)ActiveItem;
                    item.Copy(ref Temp);
                    return;
                }
            }
            ActiveItem.Reset();
        }

        public new List<ThingListEntry> GetElementsForThingList()
        {
            List<ThingListEntry> lstReturn = new List<ThingListEntry>();
            lstReturn.Add(MI_Wert);
            lstReturn.Add(MI_DK);
            lstReturn.Add(MI_RK);
            lstReturn.Add(MI_Pr);
            return lstReturn;
        }
    }
}