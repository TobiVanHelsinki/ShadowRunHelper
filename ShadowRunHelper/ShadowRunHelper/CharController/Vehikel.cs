using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharController
{
    public class cVehikelController : cController<Vehikel>
    {

        private KeyValuePair<Thing, string> MI_1;
        private KeyValuePair<Thing, string> MI_2;
        private KeyValuePair<Thing, string> MI_3;
        private KeyValuePair<Thing, string> MI_4;
        private KeyValuePair<Thing, string> MI_5;
        private KeyValuePair<Thing, string> MI_6;
        private KeyValuePair<Thing, string> MI_7;
        private KeyValuePair<Thing, string> MI_8;
        private KeyValuePair<Thing, string> MI_9;
        private KeyValuePair<Thing, string> MI_10;
        public Vehikel ActiveItem;

        public cVehikelController()
        {
            var res = ResourceLoader.GetForCurrentView();
            ActiveItem = new Vehikel();
            ActiveItem.Bezeichner = res.GetString("Model_Vehikel__Aktiv/Text");
            MI_1 = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Thing_Wert/Text"));
            MI_2 = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Vehikel_Sitze/Text"));
            MI_3 = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Vehikel_Sensor/Text"));
            MI_4 = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Vehikel_Rumpf/Text"));
            MI_5 = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Vehikel_Pilot/Text"));
            MI_6 = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Vehikel_Panzerung/Text"));
            MI_7 = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Vehikel_Handling/Text"));
            MI_8 = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Vehikel_Gewicht/Text"));
            MI_9 = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Vehikel_Geschwindigkeit/Text"));
            MI_10 = new KeyValuePair<Thing, string>(ActiveItem, res.GetString("Model_Vehikel_Beschleunigung/Text"));

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
            lstReturn.Add(MI_1);
            lstReturn.Add(MI_2);
            lstReturn.Add(MI_3);
            lstReturn.Add(MI_4);
            lstReturn.Add(MI_5);
            lstReturn.Add(MI_6);
            lstReturn.Add(MI_7);
            lstReturn.Add(MI_8);
            lstReturn.Add(MI_9);
            lstReturn.Add(MI_10);
            return lstReturn;
        }
    }
}
