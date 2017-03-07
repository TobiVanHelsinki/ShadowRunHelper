using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class cPanzerungController : cController<Panzerung>
    {

        private ThingListEntry MI_1;
        private ThingListEntry MI_2;
        private ThingListEntry MI_3;
        public Panzerung ActiveItem;

        public cPanzerungController()
        {
            ActiveItem = new Panzerung();
            ActiveItem.Bezeichner = CrossPlattformHelper.GetString("Model_Panzerung__Aktiv/Text");
            MI_1 = new ThingListEntry(ActiveItem, CrossPlattformHelper.GetString("Model_Thing_Wert/Text"));
            MI_2 = new ThingListEntry(ActiveItem, CrossPlattformHelper.GetString("Model_Panzerung_Kapazität/Text"), "Kapazität");
            MI_3 = new ThingListEntry(ActiveItem, CrossPlattformHelper.GetString("Model_Panzerung_Stoß/Text"), "Stoß");
            
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
                    var Temp = (Thing)ActiveItem;
                    item.Copy(ref Temp);
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
            return lstReturn;
        }

    }
}
