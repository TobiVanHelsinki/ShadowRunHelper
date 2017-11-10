using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System.Collections.Generic;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class cFernkampfwaffeController : cController<Fernkampfwaffe>
    {
        AllListEntry MI_Wert;
        AllListEntry MI_DK;
        AllListEntry MI_RK;
        AllListEntry MI_Pr;
        public Fernkampfwaffe ActiveItem; 

        public cFernkampfwaffeController()
        {
            ActiveItem = new Fernkampfwaffe();
            //ActiveItem.Bezeichner = CrossPlatformHelper.GetString("Model_Fernkampfwaffe__Aktiv/Text");
            MI_Wert = new AllListEntry(ActiveItem, ("Model_Waffe_Wert/Text"), "Wert");
            MI_DK = new AllListEntry(ActiveItem, ("Model_Waffe_PB/Text"), "PB");
            MI_Pr = new AllListEntry(ActiveItem, ("Model_Waffe_Praezision/Text"), "Praezision");
            MI_RK = new AllListEntry(ActiveItem, ("Model_Fernkampfwaffe_Rueckstoss/Text"), "Rueckstoss");

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
                    Thing Temp = (Thing)ActiveItem;
                    item.Copy(Temp);
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
            lstReturn.Add(MI_RK);
            lstReturn.Add(MI_Pr);
            return lstReturn;
        }
    }
}