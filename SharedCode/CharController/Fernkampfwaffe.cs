///Author: Tobi van Helsinki

using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using SharedCode.Ressourcen;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace ShadowRunHelper.CharController
{
    public class FernkampfwaffeController : Controller<Fernkampfwaffe>
    {
        AllListEntry MI_Wert;
        AllListEntry MI_DK;
        AllListEntry MI_RK;
        AllListEntry MI_Pr;
        public Fernkampfwaffe ActiveItem;

        public FernkampfwaffeController()
        {
            ActiveItem = new Fernkampfwaffe();
            //ActiveItem.Bezeichner = CrossCustomManager.GetString("Model_Fernkampfwaffe__Aktiv");
            MI_Wert = new AllListEntry(ActiveItem, ModelResources.Waffe_Wert, "Wert");
            MI_DK = new AllListEntry(ActiveItem, ModelResources.Waffe_DK, "DK");
            MI_Pr = new AllListEntry(ActiveItem, ("Waffe_Praezision"), "Praezision");
            MI_RK = new AllListEntry(ActiveItem, ModelResources.Fernkampfwaffe_RK, "RK");

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
            var item = Data.FirstOrDefault(x => x.Aktiv == true);
            if (item != null)
            {
                item.TryCopy(ActiveItem);
            }
            else
            {
                ActiveItem.Reset();
            }
        }

        public override IEnumerable<AllListEntry> GetElementsForThingList()
        {
            List<AllListEntry> lstReturn = new List<AllListEntry>();
            lstReturn.Add(MI_Wert);
            lstReturn.Add(MI_DK);
            lstReturn.Add(MI_RK);
            lstReturn.Add(MI_Pr);
            lstReturn.AddRange(Data.Select(item => new AllListEntry(item)));
            return lstReturn;
        }
    }
}