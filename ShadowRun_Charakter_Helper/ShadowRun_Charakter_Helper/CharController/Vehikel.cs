﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Vehikel : CharController.ControllerMultiItems<CharModel.Vehikel> 
    {
        public Vehikel()
        {
            DicCD_Typ = "Vehikel";
        }

        public Vehikel(Controller.HashDictionary hD, int hD_ID)
        {
            this.HD_ID = hD_ID;
            this.setHD(hD);
            DataList.CollectionChanged += new NotifyCollectionChangedEventHandler(DataChanged);
        }

        private void DataChanged(object sender, EventArgs e)
        {
            DataHasUpdatet(sender);
        }
    }
}
