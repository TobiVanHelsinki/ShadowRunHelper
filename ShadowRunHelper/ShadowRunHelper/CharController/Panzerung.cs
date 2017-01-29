using ShadowRunHelper.CharModel;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class cPanzerungController : cController<Panzerung>
    {

        private KeyValuePair<Thing, string> MI_1;
        private KeyValuePair<Thing, string> MI_2;
        public Panzerung ActiveItem;

        public cPanzerungController()
        {
            ActiveItem = new Panzerung();
            ActiveItem.Bezeichner = "Active";
            MI_1 = new KeyValuePair<Thing, string>(ActiveItem, "Panzer1");
            MI_2 = new KeyValuePair<Thing, string>(ActiveItem, "Panzer2");

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
            return lstReturn;
        }

    }
}
