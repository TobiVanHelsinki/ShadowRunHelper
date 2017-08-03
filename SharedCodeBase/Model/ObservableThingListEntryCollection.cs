using ShadowRunHelper;
using ShadowRunHelper.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SharedCodeBase.Model
{
    public class ObservableThingListEntryCollection : ObservableCollection<AllListEntry>
    {
        public ObservableThingListEntryCollection(List<ThingDefs> ForbiddenThingTypes) : base()
        {
            this.ForbiddenThingTypes = ForbiddenThingTypes;
        }
        public List<ThingDefs> ForbiddenThingTypes = new List<ThingDefs>();
        protected override void InsertItem(int index, AllListEntry item)
        {
            if (ForbiddenThingTypes.Contains(item.Object.ThingType))
            {
                return;
            }
            base.InsertItem(index, item);
        }
    }
}
