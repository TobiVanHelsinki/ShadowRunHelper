using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System.Collections.Generic;
using System.Collections.Specialized;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharController
{
    public class cCyberDeckController : cController<CyberDeck>
    {
        ThingListEntry MI_V;
        ThingListEntry MI_A;
        ThingListEntry MI_S;
        ThingListEntry MI_F;
        ThingListEntry MI_D;
        public CyberDeck ActiveDeck;

        public cCyberDeckController()
        {
            ActiveDeck = new CyberDeck();
            ActiveDeck.PropertyChanged += (x, y) => RefreshOriginDeck();
            var res = ResourceLoader.GetForCurrentView();

            ActiveDeck.Bezeichner = res.GetString("Model_CyberDeck__Aktiv/Text");
            MI_V = new ThingListEntry(ActiveDeck, res.GetString("Model_CyberDeck_/Text") + res.GetString("Model_Thing_Wert/Text"));
            MI_A = new ThingListEntry(ActiveDeck, res.GetString("Model_CyberDeck_Angriff/Text"));
            MI_S = new ThingListEntry(ActiveDeck, res.GetString("Model_CyberDeck_Schleicher/Text"));
            MI_F = new ThingListEntry(ActiveDeck, res.GetString("Model_Kommlink_Firewall/Text"));
            MI_D = new ThingListEntry(ActiveDeck, res.GetString("Model_Kommlink_Datenverarbeitung/Text"));

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
                    item.Copy(ActiveDeck);
                    bIsRefreshInProgress = false;
                    return;
                }
            }
            ActiveDeck.Reset();
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
                    item.dSchaden = ActiveDeck.dSchaden;
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
