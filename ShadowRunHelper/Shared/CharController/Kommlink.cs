using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class cKommlinkController : cController<Kommlink>
    {
        private ThingListEntry MI_V;
        private ThingListEntry MI_F;
        private ThingListEntry MI_D;
        public Kommlink ActiveItem;

        public cKommlinkController()
        {
            ActiveItem = new Kommlink();
            //ActiveItem.Bezeichner = CrossPlattformHelper.GetString("Model_Kommlink__Aktiv/Text");
            MI_V = new ThingListEntry(ActiveItem, ("Model_Thing_Wert/Text"), "Wert");
            MI_F = new ThingListEntry(ActiveItem, ("Model_Kommlink_Firewall/Text"),"Firewall");
            MI_D = new ThingListEntry(ActiveItem, ("Model_Kommlink_Datenverarbeitung/Text"), "Datenverarbeitung");

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
                    item.Copy(ActiveItem);
                    return;
                }
            }
            ActiveItem.Reset();
        }

        public new List<ThingListEntry> GetElementsForThingList()
        {
            List<ThingListEntry> lstReturn = new List<ThingListEntry>();
            lstReturn.Add(MI_V);
            lstReturn.Add(MI_F);
            lstReturn.Add(MI_D);
            return lstReturn;
        }
    }
}