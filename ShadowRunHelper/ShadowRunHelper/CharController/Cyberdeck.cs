using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharController
{
    public class cCyberDeckController : cController<CyberDeck>
    {
        private KeyValuePair<Thing, string> MI_V;
        private KeyValuePair<Thing, string> MI_A;
        private KeyValuePair<Thing, string> MI_S;
        private KeyValuePair<Thing, string> MI_F;
        private KeyValuePair<Thing, string> MI_D;
        public CyberDeck ActiveDeck;

        public cCyberDeckController()
        {
            ActiveDeck = new CyberDeck();
            var res = ResourceLoader.GetForCurrentView();

            ActiveDeck.Bezeichner = res.GetString("Model_CyberDeck__Aktiv/Text");
            MI_V = new KeyValuePair<Thing, string>(ActiveDeck, res.GetString("Model_CyberDeck_/Text") + res.GetString("Model_Thing_Wert/Text"));
            MI_A = new KeyValuePair<Thing, string>(ActiveDeck, res.GetString("Model_CyberDeck_Angriff/Text"));
            MI_S = new KeyValuePair<Thing, string>(ActiveDeck, res.GetString("Model_CyberDeck_Schleicher/Text"));
            MI_F = new KeyValuePair<Thing, string>(ActiveDeck, res.GetString("Model_Kommlink_Firewall/Text"));
            MI_D = new KeyValuePair<Thing, string>(ActiveDeck, res.GetString("Model_Kommlink_Datenverarbeitung/Text"));

            Data.CollectionChanged += Data_CollectionChanged;
        }

        private void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Refresh();
            foreach (var item in Data)
            {
                item.PropertyChanged -= (x,y) => Refresh();
                item.PropertyChanged += (x,y) => Refresh();
            }
        }

        private void Refresh()
        {
            foreach (CyberDeck item in Data)
            {
                if (item.Aktiv == true)
                {
                    item.Copy(ActiveDeck);
                    return;
                }
            }
            ActiveDeck.Reset();
        }

        public new List<KeyValuePair<Thing, string>> GetElementsForThingList()
        {
            List<KeyValuePair<Thing, string>> lstReturn = new List<KeyValuePair<Thing, string>>();
            lstReturn.Add(MI_V);
            lstReturn.Add(MI_A);
            lstReturn.Add(MI_S);
            lstReturn.Add(MI_F);
            lstReturn.Add(MI_D);
            return lstReturn;
        }

    }
}
