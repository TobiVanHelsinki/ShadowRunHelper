using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace ShadowRunHelper.CharController
{
    public class KommlinkController : Controller<Kommlink>
    {
        AllListEntry MI_V;
        AllListEntry MI_F;
        AllListEntry MI_D;
        public Kommlink ActiveItem;

        public KommlinkController()
        {
            ActiveItem = new Kommlink();
            //ActiveItem.Bezeichner = CrossPlatformHelper.GetString("Model_Kommlink__Aktiv/Text");
            MI_V = new AllListEntry(ActiveItem, ("Model_Thing_Wert/Text"), "Wert");
            MI_F = new AllListEntry(ActiveItem, ("Model_Kommlink_Firewall/Text"),"Firewall");
            MI_D = new AllListEntry(ActiveItem, ("Model_Kommlink_Datenverarbeitung/Text"), "Datenverarbeitung");

            Data.CollectionChanged += Data_CollectionChanged;
        }

        void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Refresh();
            foreach (var item in Data)
            {
                item.PropertyChanged -= (x, y) => Refresh();
                item.PropertyChanged += (x, y) => Refresh();
            }
        }

        void Refresh()
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

        public override IEnumerable<AllListEntry> GetElementsForThingList()
        {
            List<AllListEntry> lstReturn = new List<AllListEntry>();
            lstReturn.Add(MI_V);
            lstReturn.Add(MI_F);
            lstReturn.Add(MI_D);
            lstReturn.AddRange(Data.Select(item => new AllListEntry(item)));
            return lstReturn;
        }
    }
}