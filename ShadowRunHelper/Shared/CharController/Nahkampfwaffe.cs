using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class cNahkampfwaffeController : CharController.cController<Nahkampfwaffe>
    {
        private ThingListEntry MI_Reich;
        private ThingListEntry MI_Pr;
        private ThingListEntry MI_Wert;
        private ThingListEntry MI_DK;
        public Nahkampfwaffe ActiveItem;

        public cNahkampfwaffeController()
        {
            ActiveItem = new Nahkampfwaffe();
            ActiveItem.Bezeichner = CrossPlattformHelper.GetString("Model_Nahkampfwaffe__Aktiv/Text");
            MI_Wert = new ThingListEntry(ActiveItem, CrossPlattformHelper.GetString("Model_Waffe_Wert/Text"));
            MI_DK = new ThingListEntry(ActiveItem, CrossPlattformHelper.GetString("Model_Waffe_PB/Text"), "PB");
            MI_Pr = new ThingListEntry(ActiveItem, CrossPlattformHelper.GetString("Model_Waffe_Präzision/Text"), "Präzision");
            MI_Reich = new ThingListEntry(ActiveItem, CrossPlattformHelper.GetString("Model_Nahkampfwaffe_Reichweite/Text"), "Reichweite");

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
                    Thing temp = (Thing)ActiveItem;
                    item.Copy(ref temp);
                    return;
                }
            }
            ActiveItem.Reset();
        }

        public new List<ThingListEntry> GetElementsForThingList()
        {
            List<ThingListEntry> lstReturn = new List<ThingListEntry>();
            lstReturn.Add(MI_Wert);
            lstReturn.Add(MI_DK);
            lstReturn.Add(MI_Reich);
            lstReturn.Add(MI_Pr);

            return lstReturn;
        }
    }
}