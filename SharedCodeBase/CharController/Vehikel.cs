using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class VehikelController : Controller<Vehikel>
    {

        AllListEntry MI_1;
        AllListEntry MI_2;
        AllListEntry MI_3;
        AllListEntry MI_4;
        AllListEntry MI_5;
        AllListEntry MI_6;
        AllListEntry MI_7;
        AllListEntry MI_8;
        AllListEntry MI_9;
        AllListEntry MI_10;
        public Vehikel ActiveItem;

        public VehikelController()
        {
            ActiveItem = new Vehikel();
            //ActiveItem.Bezeichner = CrossPlatformHelper.GetString("Model_Vehikel__Aktiv/Text");
            MI_1 = new AllListEntry(ActiveItem, ("Model_Vehikel_Wert/Text"), "Wert");
            MI_2 = new AllListEntry(ActiveItem, ("Model_Vehikel_Sitze/Text"), "Sitze");
            MI_3 = new AllListEntry(ActiveItem, ("Model_Vehikel_Sensor/Text"), "Sensor");
            MI_4 = new AllListEntry(ActiveItem, ("Model_Vehikel_Rumpf/Text"), "Rumpf");
            MI_5 = new AllListEntry(ActiveItem, ("Model_Vehikel_Pilot/Text"), "Pilot");
            MI_6 = new AllListEntry(ActiveItem, ("Model_Vehikel_Panzerung/Text"), "Panzerung");
            MI_7 = new AllListEntry(ActiveItem, ("Model_Vehikel_Handling/Text"), "Handling");
            MI_8 = new AllListEntry(ActiveItem, ("Model_Vehikel_Gewicht/Text"), "Gewicht");
            MI_9 = new AllListEntry(ActiveItem, ("Model_Vehikel_Geschwindigkeit/Text"), "Geschwindigkeit");
            MI_10 = new AllListEntry(ActiveItem, ("Model_Vehikel_Beschleunigung/Text"), "Beschleunigung");

            Data.CollectionChanged += Data_CollectionChanged;
        }

        void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Refresh();
            foreach (var item in Data)
            {
                item.PropertyChanged -= (x, y) => Refresh();
                item.PropertyChanged += (x, y) => Refresh();
            }
        }

        void Refresh()
        {
            foreach (var item in Data)
            {
                if (item.Aktiv == true)
                {
                    Thing TempRef = ActiveItem;
                    item.Copy(TempRef);
                    return;
                }
            }
            ActiveItem.Reset();
        }

        public override List<AllListEntry> GetElementsForThingList()
        {
            List<AllListEntry> lstReturn = new List<AllListEntry>();
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
