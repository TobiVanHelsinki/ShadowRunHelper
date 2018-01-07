using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class NahkampfwaffeController : CharController.Controller<Nahkampfwaffe>
    {
        AllListEntry MI_Reich;
        AllListEntry MI_Pr;
        AllListEntry MI_Wert;
        AllListEntry MI_DK;
        public Nahkampfwaffe ActiveItem;

        public NahkampfwaffeController()
        {
            ActiveItem = new Nahkampfwaffe();
            //ActiveItem.Bezeichner = CrossPlatformHelper.GetString("Model_Nahkampfwaffe__Aktiv/Text");
            MI_Wert = new AllListEntry(ActiveItem, ("Model_Waffe_Wert/Text"), "Wert");
            MI_DK = new AllListEntry(ActiveItem, ("Model_Waffe_PB/Text"), "PB");
            MI_Pr = new AllListEntry(ActiveItem, ("Model_Waffe_Praezision/Text"), "Praezision");
            MI_Reich = new AllListEntry(ActiveItem, ("Model_Nahkampfwaffe_Reichweite/Text"), "Reichweite");

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
                    Thing temp = (Thing)ActiveItem;
                    item.Copy(temp);
                    return;
                }
            }
            ActiveItem.Reset();
        }

        public override List<AllListEntry> GetElementsForThingList()
        {
            List<AllListEntry> lstReturn = new List<AllListEntry>();
            lstReturn.Add(MI_Wert);
            lstReturn.Add(MI_DK);
            lstReturn.Add(MI_Reich);
            lstReturn.Add(MI_Pr);

            return lstReturn;
        }
    }
}