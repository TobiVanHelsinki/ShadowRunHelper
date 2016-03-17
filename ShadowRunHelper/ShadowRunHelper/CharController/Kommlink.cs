﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
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
            DataList.CollectionChanged += new NotifyCollectionChangedEventHandler(DataListChanged);
        }

        private void DataListChanged(object sender, EventArgs e)
        {
            foreach (var item in DataList)
            {
                item.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(DataChanged);
                item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(DataChanged);
            }
        }

        private void DataChanged(object sender, EventArgs e)
        {
            DataHasUpdatet(sender);
        }
        ~Kommlink()
        {
            if (Data!=null){Data.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(DataChanged);}
        }
    }
}