using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper.CharController
{
    public class cCyberDeckController : cController<CyberDeck>,  INotifyPropertyChanged
    {
        #region event
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            SharedCodeBase.Model.ModelResources.CallPropertyChangedAtDispatcher(PropertyChanged, this, propertyName);
        }
        #endregion
        AllListEntry MI_V;
        AllListEntry MI_A;
        AllListEntry MI_S;
        AllListEntry MI_F;
        AllListEntry MI_D;

        CyberDeck _ActiveItem;
        [Newtonsoft.Json.JsonIgnore]
        public CyberDeck ActiveItem
        {
            get { return _ActiveItem; }
            protected set
            {
                if (value != _ActiveItem)
                {
                    _ActiveItem = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public cCyberDeckController()
        {
            ActiveItem = new CyberDeck();
            ActiveItem.PropertyChanged += (x, y) => RefreshOriginDeck();
            //ActiveItem.Bezeichner = CrossPlatformHelper.GetString("Model_CyberDeck__Aktiv/Text");
            MI_V = new AllListEntry(ActiveItem, ("Model_Thing_Wert/Text"), "Wert");
            MI_A = new AllListEntry(ActiveItem, ("Model_CyberDeck_Angriff/Text"), "Angriff");
            MI_S = new AllListEntry(ActiveItem, ("Model_CyberDeck_Schleicher/Text"), "Schleicher");
            MI_F = new AllListEntry(ActiveItem, ("Model_Kommlink_Firewall/Text"), "Firewall");
            MI_D = new AllListEntry(ActiveItem, ("Model_Kommlink_Datenverarbeitung/Text"), "Datenverarbeitung");

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

        public override List<AllListEntry> GetElementsForThingList()
        {
            List<AllListEntry> lstReturn = new List<AllListEntry>();
            lstReturn.Add(MI_V);
            lstReturn.Add(MI_A);
            lstReturn.Add(MI_S);
            lstReturn.Add(MI_F);
            lstReturn.Add(MI_D);
            return lstReturn;
        }
    }
}
