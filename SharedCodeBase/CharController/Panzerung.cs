using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class PanzerungController : Controller<Panzerung>
    {

        AllListEntry MI_1;
        AllListEntry MI_2;
        AllListEntry MI_3;
        public Panzerung ActiveItem;

        public PanzerungController()
        {
            ActiveItem = new Panzerung();
            //ActiveItem.Bezeichner = CrossPlatformHelper.GetString("Model_Panzerung__Aktiv/Text");
            MI_1 = new AllListEntry(ActiveItem, ("Model_Thing_Wert/Text"), "Wert");
            MI_2 = new AllListEntry(ActiveItem, ("Model_Panzerung_Kapazitaet/Text"), "Kapazitaet");
            MI_3 = new AllListEntry(ActiveItem, ("Model_Panzerung_Stoss/Text"), "Stoss");
            
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
                    item.Copy(ActiveItem);
                    return;
                }
            }
            ActiveItem.Reset();
        }

        public override IEnumerable<AllListEntry> GetElementsForThingList()
        {
            List<AllListEntry> lstReturn = new List<AllListEntry>();
            lstReturn.Add(MI_1);
            lstReturn.Add(MI_2);
            lstReturn.Add(MI_3);
            return lstReturn;
        }

    }
}
