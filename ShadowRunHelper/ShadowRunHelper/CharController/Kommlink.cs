using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class cKommlinkController : cController<Kommlink>
    {
        private KeyValuePair<Thing, string> MI_V;
        private KeyValuePair<Thing, string> MI_F;
        private KeyValuePair<Thing, string> MI_D;
        public Kommlink ActiveItem;

        public cKommlinkController()
        {
            ActiveItem = new Kommlink();
            ActiveItem.Bezeichner = "ActiveDeck";
            MI_V = new KeyValuePair<Thing, string>(ActiveItem, "Deck-Stärke");
            MI_F = new KeyValuePair<Thing, string>(ActiveItem, "Firewall");
            MI_D = new KeyValuePair<Thing, string>(ActiveItem, "Datenverarbeitung");

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

        public new List<KeyValuePair<Thing, string>> GetElements()
        {
            List<KeyValuePair<Thing, string>> lstReturn = new List<KeyValuePair<Thing, string>>();
            lstReturn.Add(MI_V);
            lstReturn.Add(MI_F);
            lstReturn.Add(MI_D);
            return lstReturn;
        }
    }
}