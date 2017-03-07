using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class cCyberDeckController : cController<CyberDeck>
    {
        ThingListEntry MI_V;
        ThingListEntry MI_A;
        ThingListEntry MI_S;
        ThingListEntry MI_F;
        ThingListEntry MI_D;
        public CyberDeck ActiveItem;

        public cCyberDeckController()
        {
            ActiveItem = new CyberDeck();
            ActiveItem.PropertyChanged += (x, y) => RefreshOriginDeck();
            ActiveItem.Bezeichner = CrossPlattformHelper.GetString("Model_CyberDeck__Aktiv/Text");
            MI_V = new ThingListEntry(ActiveItem, CrossPlattformHelper.GetString("Model_Thing_Wert/Text"), "Wert");
            MI_A = new ThingListEntry(ActiveItem, CrossPlattformHelper.GetString("Model_CyberDeck_Angriff/Text"), "Angriff");
            MI_S = new ThingListEntry(ActiveItem, CrossPlattformHelper.GetString("Model_CyberDeck_Schleicher/Text"), "Schleicher");
            MI_F = new ThingListEntry(ActiveItem, CrossPlattformHelper.GetString("Model_Kommlink_Firewall/Text"), "Firewall");
            MI_D = new ThingListEntry(ActiveItem, CrossPlattformHelper.GetString("Model_Kommlink_Datenverarbeitung/Text"), "Datenverarbeitung");

            Data.CollectionChanged += Data_CollectionChanged;
        }

        void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshActiveDeck();
            foreach (var item in Data)
            {
                item.PropertyChanged -= (x,y) => RefreshActiveDeck();
                item.PropertyChanged += (x,y) => RefreshActiveDeck();
            }
        }
        bool bIsRefreshInProgress = false;
        void RefreshActiveDeck()
        {
            if (bIsRefreshInProgress)
            {
                return;
            }
            bIsRefreshInProgress = true;
            foreach (CyberDeck item in Data)
            {
                if (item.Aktiv == true)
                {
                    item.Copy(ActiveItem);
                    bIsRefreshInProgress = false;
                    return;
                }
            }
            ActiveItem.Reset();
            bIsRefreshInProgress = false;
        }

        void RefreshOriginDeck()
        {
            if (bIsRefreshInProgress)
            {
                return;
            }
            bIsRefreshInProgress = true;
            foreach (CyberDeck item in Data)
            {
                if (item.Aktiv == true)
                {
                    item.dSchaden = ActiveItem.dSchaden;
                    bIsRefreshInProgress = false;
                    return;
                }
            }
            bIsRefreshInProgress = false;
        }

        public new List<ThingListEntry> GetElementsForThingList()
        {
            List<ThingListEntry> lstReturn = new List<ThingListEntry>();
            lstReturn.Add(MI_V);
            lstReturn.Add(MI_A);
            lstReturn.Add(MI_S);
            lstReturn.Add(MI_F);
            lstReturn.Add(MI_D);
            return lstReturn;
        }
    }
}
