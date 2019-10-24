﻿///Author: Tobi van Helsinki

using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TAPPLICATION;
using TLIB;

namespace ShadowRunHelper.CharController
{
    public class CyberDeckController : Controller<CyberDeck>, INotifyPropertyChanged
    {
        #region event
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PlatformHelper.CallPropertyChanged(PropertyChanged, this, propertyName);
        }
        #endregion event

        AllListEntry MI_V;
        AllListEntry MI_A;
        AllListEntry MI_S;
        AllListEntry MI_F;
        AllListEntry MI_D;

        CyberDeck _ActiveItem = new CyberDeck();
        public CyberDeck ActiveItem
        {
            //do not set any of these other than public because of serialisation
            get { return _ActiveItem; }
            set
            {
                if (value != _ActiveItem)
                {
                    _ActiveItem = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public CyberDeckController()
        {
            ActiveItem.PropertyChanged += (x, y) => RefreshOriginDeck();
            //ActiveItem.Bezeichner = CrossCustomManager.GetString("Model_CyberDeck__Aktiv/Text");
            MI_V = new AllListEntry(ActiveItem, ("Model_Thing_Wert/Text"), "Wert");
            MI_A = new AllListEntry(ActiveItem, ("Model_CyberDeck_Angriff/Text"), "Angriff");
            MI_S = new AllListEntry(ActiveItem, ("Model_CyberDeck_Schleicher/Text"), "Schleicher");
            MI_F = new AllListEntry(ActiveItem, ("Model_Kommlink_Firewall/Text"), "Firewall");
            MI_D = new AllListEntry(ActiveItem, ("Model_Kommlink_Datenverarbeitung/Text"), "Datenverarbeitung");

            Data.CollectionChanged += Data_CollectionChanged;
        }

        private void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshActiveDeck();
            foreach (var item in Data)
            {
                item.PropertyChanged -= (x, y) => RefreshActiveDeck();
                item.PropertyChanged += (x, y) => RefreshActiveDeck();
            }
        }
        bool bIsRefreshInProgress = false;

        /// <summary>
        /// sets a new active deck from the list of all decks. should occur, when user changes active decks
        /// </summary>
        private void RefreshActiveDeck()
        {
            // aber ich könnte auch einfach statt active deck immer ein anderes einsetzen. dann müssten sich aber die registriere immer neu registrieren ...
            // außer, ich schaffe es, nur den registrierten besheid zu geben, sie sollen sich auf ein neues ziel registrieren!
            if (bIsRefreshInProgress)
            {
                return;
            }
            bIsRefreshInProgress = true;
            var item = Data.FirstOrDefault(x => x.Aktiv == true);
            if (item != null)
            {
                item.TryCopy(ActiveItem);
            }
            else
            {
                ActiveItem.Reset();
            }
            bIsRefreshInProgress = false;
        }

        /// <summary>
        /// copy changes from the active deck var to the original deck. this occurs, when the sliders of the main page are used to set deck damage.
        /// </summary>
        private void RefreshOriginDeck()
        {
            if (bIsRefreshInProgress)
            {
                return;
            }
            bIsRefreshInProgress = true;
            var item = Data.FirstOrDefault(x => x.Aktiv == true);
            if (item != null)
            {
                item.Schaden = ActiveItem.Schaden;
            }
            bIsRefreshInProgress = false;
        }

        public override IEnumerable<AllListEntry> GetElementsForThingList()
        {
            List<AllListEntry> lstReturn = new List<AllListEntry>();
            lstReturn.Add(MI_V);
            lstReturn.Add(MI_A);
            lstReturn.Add(MI_S);
            lstReturn.Add(MI_F);
            lstReturn.Add(MI_D);
            lstReturn.AddRange(Data.Select(item => new AllListEntry(item)));
            return lstReturn;
        }
    }
}