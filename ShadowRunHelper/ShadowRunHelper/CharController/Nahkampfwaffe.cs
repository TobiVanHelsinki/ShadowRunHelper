using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharController
{
    public class cNahkampfwaffeController : CharController.cController<Nahkampfwaffe>
    {
        private KeyValuePair<Thing, string> MI_Reich;
        private KeyValuePair<Thing, string> MI_Pr;
        private KeyValuePair<Thing, string> MI_Wert;
        private KeyValuePair<Thing, string> MI_DK;
        public Nahkampfwaffe ActiveItem;

        public cNahkampfwaffeController()
        {
            var res = ResourceLoader.GetForCurrentView();
            ActiveItem = new Nahkampfwaffe();
            ActiveItem.Bezeichner = res.GetString("Model_Nahkampfwaffe__Aktiv/Text");
            MI_Wert = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Waffe_Wert/Text"));
            MI_DK = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Waffe_PB/Text"));
            MI_Pr = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Waffe_Pool/Text"));
            MI_Reich = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Nahkampfwaffe_Reichweite/Text"));

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

        public new List<KeyValuePair<Thing, string>> GetElementsForThingList()
        {
            List<KeyValuePair<Thing, string>> lstReturn = new List<KeyValuePair<Thing, string>>();
            lstReturn.Add(MI_Reich);
            lstReturn.Add(MI_DK);
            return lstReturn;
        }
    }
}