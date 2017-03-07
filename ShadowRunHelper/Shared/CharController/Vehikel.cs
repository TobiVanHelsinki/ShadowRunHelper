using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class cVehikelController : cController<Vehikel>
    {

        private ThingListEntry MI_1;
        private ThingListEntry MI_2;
        private ThingListEntry MI_3;
        private ThingListEntry MI_4;
        private ThingListEntry MI_5;
        private ThingListEntry MI_6;
        private ThingListEntry MI_7;
        private ThingListEntry MI_8;
        private ThingListEntry MI_9;
        private ThingListEntry MI_10;
        public Vehikel ActiveItem;

        public cVehikelController()
        {
            ActiveItem = new Vehikel();
            //ActiveItem.Bezeichner = CrossPlattformHelper.GetString("Model_Vehikel__Aktiv/Text");
            MI_1 = new ThingListEntry(ActiveItem, ("Model_Thing_Wert/Text"));
            MI_2 = new ThingListEntry(ActiveItem, ("Model_Vehikel_Sitze/Text"), "Sitze");
            MI_3 = new ThingListEntry(ActiveItem, ("Model_Vehikel_Sensor/Text"), "Sensor");
            MI_4 = new ThingListEntry(ActiveItem, ("Model_Vehikel_Rumpf/Text"), "Rumpf");
            MI_5 = new ThingListEntry(ActiveItem, ("Model_Vehikel_Pilot/Text"), "Pilot");
            MI_6 = new ThingListEntry(ActiveItem, ("Model_Vehikel_Panzerung/Text"), "Panzerung");
            MI_7 = new ThingListEntry(ActiveItem, ("Model_Vehikel_Handling/Text"), "Handling");
            MI_8 = new ThingListEntry(ActiveItem, ("Model_Vehikel_Gewicht/Text"), "Gewicht");
            MI_9 = new ThingListEntry(ActiveItem, ("Model_Vehikel_Geschwindigkeit/Text"), "Geschwindigkeit");
            MI_10 = new ThingListEntry(ActiveItem, ("Model_Vehikel_Beschleunigung/Text"), "Beschleunigung");

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
                    Thing TempRef = ActiveItem;
                    item.Copy(ref TempRef);
                    return;
                }
            }
            ActiveItem.Reset();
        }

        public new List<ThingListEntry> GetElementsForThingList()
        {
            List<ThingListEntry> lstReturn = new List<ThingListEntry>();
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
