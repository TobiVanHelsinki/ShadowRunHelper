using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharController
{
    public class cNahkampfwaffeController : CharController.cController<Nahkampfwaffe>
    {
        private ThingListEntry MI_Reich;
        private ThingListEntry MI_Pr;
        private ThingListEntry MI_Wert;
        private ThingListEntry MI_DK;
        public Nahkampfwaffe ActiveItem;

        public cNahkampfwaffeController()
        {
            var res = ResourceLoader.GetForCurrentView();
            ActiveItem = new Nahkampfwaffe();
            ActiveItem.Bezeichner = res.GetString("Model_Nahkampfwaffe__Aktiv/Text");
            MI_Wert = new ThingListEntry(ActiveItem, res.GetString("Model_Waffe_Wert/Text"));
            MI_DK = new ThingListEntry(ActiveItem, res.GetString("Model_Waffe_PB/Text"));
            MI_Pr = new ThingListEntry(ActiveItem, res.GetString("Model_Waffe_Pool/Text"));
            MI_Reich = new ThingListEntry(ActiveItem, res.GetString("Model_Nahkampfwaffe_Reichweite/Text"));

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
                    Thing temp = (Thing)ActiveItem;
                    item.Copy(ref temp);
                    return;
                }
            }
            ActiveItem.Reset();
        }

        public new List<ThingListEntry> GetElementsForThingList()
        {
            List<ThingListEntry> lstReturn = new List<ThingListEntry>();
            lstReturn.Add(MI_Reich);
            lstReturn.Add(MI_DK);
            return lstReturn;
        }
    }
}