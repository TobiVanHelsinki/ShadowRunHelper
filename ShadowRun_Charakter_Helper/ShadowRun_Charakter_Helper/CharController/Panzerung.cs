using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Panzerung : CharController.ControllerMultiItems<CharModel.Panzerung>
    {
        public Panzerung()
        {
            DicCD_Typ = "Panzerung";
        }

        public Panzerung(ObservableCollection<CharModel.Panzerung> obj)
        {
            DicCD_Typ = "Panzerung";
        }

        public Panzerung(Controller.HashDictionary hD)
        {
            DicCD_Typ = "Panzerung";
            this.setHD(hD);
            DataList.CollectionChanged += new NotifyCollectionChangedEventHandler(DataChanged);
        }

        private void DataChanged(object sender, EventArgs e)
        {
            DataHasUpdatet(sender);
        }
    }
}
