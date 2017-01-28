using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class cCyberDeckController : CharController.cController<CharModel.CyberDeck>
    {
        //public class CyberDeck_A : CharModel.CyberDeck{ }
        //public class CyberDeck_S : CharModel.CyberDeck { }
        //public class CyberDeck_F : CharModel.CyberDeck { }
        //public class CyberDeck_D : CharModel.CyberDeck { }
        //private CyberDeck MI_V;
        private KeyValuePair<Thing, string> MI_V;
        private KeyValuePair<Thing, string> MI_A;
        private KeyValuePair<Thing, string> MI_S;
        private KeyValuePair<Thing, string> MI_F;
        private KeyValuePair<Thing, string> MI_D;
        private CyberDeck ActiveDeck;
        //private CyberDeck MI_S;
        //private CyberDeck MI_F;
        //private CyberDeck MI_D;

        public cCyberDeckController()
        {
            ActiveDeck = new CyberDeck();
            ActiveDeck.Bezeichner = "ActiveDeck";
            MI_V = new KeyValuePair<Thing, string>(ActiveDeck, "Deck-Stärke");
            MI_A = new KeyValuePair<Thing, string>(ActiveDeck, "Angriff");
            MI_S = new KeyValuePair<Thing, string>(ActiveDeck, "Schleicher");
            MI_F = new KeyValuePair<Thing, string>(ActiveDeck, "Firewall");
            MI_D = new KeyValuePair<Thing, string>(ActiveDeck, "Datenverarbeitung");

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
                    //ActiveDeck.Schleicher = item.Schleicher;
                    //ActiveDeck.Bezeichner = item.Bezeichner;
                    return;
                }
            }
            ActiveDeck.Reset();
        }

        public new List<KeyValuePair<Thing, string>> GetElements()
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
