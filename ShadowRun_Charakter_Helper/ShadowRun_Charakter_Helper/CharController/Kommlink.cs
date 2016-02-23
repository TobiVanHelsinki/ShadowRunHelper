﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Kommlink : CharController.ControllerMultiItems<CharModel.Kommlink>
    {
        public Kommlink()
        {
        }

        public Kommlink(Controller.HashDictionary hD, int hD_ID)
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