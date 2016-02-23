using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class CyberDeck : CharController.ControllerMultiItems<CharModel.CyberDeck>
    {
        public CyberDeck()
        {
            DicCD_Typ = "CyberDeck";
        }

        public CyberDeck(Controller.HashDictionary hD, int hD_ID)
        {
            this.HD_ID = hD_ID;
            this.setHD(hD);
            DataList.CollectionChanged += new NotifyCollectionChangedEventHandler(DataChanged);
        }

        private void DataChanged(object sender, EventArgs e)
        {
            DataHasUpdatet(sender);
        }
        //todo cyberdeck controller braucht 4 einträge im hd
        //5 ! wert des Decks + attribute
    }
}
