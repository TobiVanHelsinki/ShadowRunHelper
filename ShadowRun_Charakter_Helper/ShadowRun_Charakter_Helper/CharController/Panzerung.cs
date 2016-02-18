using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Panzerung : CharController.ControllerMultiItems<CharModel.Panzerung>
    {
        public Panzerung()
        {
        }

        public Panzerung(ObservableCollection<CharModel.Panzerung> obj)
        {
        }

        public Panzerung(Controller.HashDictionary hD)
        {
            this.setHD(hD);
            DataList.CollectionChanged += new NotifyCollectionChangedEventHandler(DataChanged);
        }

        private void DataChanged(object sender, EventArgs e)
        {
            DataHasUpdatet(sender);
        }
    }
}
