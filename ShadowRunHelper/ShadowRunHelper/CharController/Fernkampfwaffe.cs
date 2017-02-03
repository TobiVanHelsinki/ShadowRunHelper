using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharController
{
    public class cFernkampfwaffeController : cController<Fernkampfwaffe>
    {
        private KeyValuePair<Thing, string> MI_Wert;
        private KeyValuePair<Thing, string> MI_DK;
        private KeyValuePair<Thing, string> MI_RK;
        private KeyValuePair<Thing, string> MI_Pr;
        public Fernkampfwaffe ActiveItem; 

        public cFernkampfwaffeController()
        {
            var res = ResourceLoader.GetForCurrentView();
            ActiveItem = new Fernkampfwaffe();
            ActiveItem.Bezeichner = res.GetString("Model_Fernkampfwaffe__Aktiv/Text");
            MI_Wert = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Waffe_Wert/Text"));
            MI_DK = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Waffe_PB/Text"));
            MI_Pr = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Waffe_Pool/Text"));
            MI_RK = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Fernkampfwaffe_Rückstoß/Text"));

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
                    Thing Temp = (Thing)ActiveItem;
                    item.Copy(ref Temp);
                    return;
                }
            }
            ActiveItem.Reset();
        }

        public new List<KeyValuePair<Thing, string>> GetElementsForThingList()
        {
            List<KeyValuePair<Thing, string>> lstReturn = new List<KeyValuePair<Thing, string>>();
            lstReturn.Add(MI_Wert);
            lstReturn.Add(MI_DK);
            lstReturn.Add(MI_RK);
            lstReturn.Add(MI_Pr);
            return lstReturn;
        }
    }
}