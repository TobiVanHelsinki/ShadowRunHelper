using Newtonsoft.Json;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace ShadowRunHelper.CharController
{
    public class PanzerungController : Controller<Panzerung>
    {
        [JsonIgnore]
        public AllListEntry MI_Wert { get; set; }
        [JsonIgnore]
        public AllListEntry MI_Kapa { get; set; }
        public Panzerung ActiveItem;

        public PanzerungController()
        {
            ActiveItem = new Panzerung();
            MI_Wert = new AllListEntry(ActiveItem, ("Model_Thing_Wert/Text"), "Wert");
            MI_Kapa = new AllListEntry(ActiveItem, ("Model_Panzerung_Kapazitaet/Text"), "Kapazitaet");
            
            Data.CollectionChanged += Data_CollectionChanged;
        }

        void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Refresh();
            foreach (var item in Data.Where(x => x != null))
            {
                item.PropertyChanged -= (x, y) => Refresh();
                item.PropertyChanged += (x, y) => Refresh();
            }
        }

        void Refresh()
        {
            var item = Data.FirstOrDefault(x => x.Aktiv == true);
            if (item != null)
            {
                item.Copy(ActiveItem);
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
            lstReturn.Add(MI_Kapa);
            lstReturn.AddRange(Data.Select(item => new AllListEntry(item)));
            return lstReturn;
        }

    }
}
