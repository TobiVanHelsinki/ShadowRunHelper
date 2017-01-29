﻿using System;

namespace ShadowRunHelper1_3.CharController
{
    public class Vorteil : CharController.ControllerSingle<CharModel.Vorteil>
    {
        public Vorteil()
        {
        }

        public Vorteil(Controller.HashDictionary hD, int hD_ID)
        {
            this.HD_ID = hD_ID;
            this.setHD(hD);
            Data.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(DataChanged);
        }

        private void DataChanged(object sender, EventArgs e)
        {
            DataHasUpdatet(sender);
        }

        ~Vorteil()
        {
            if (Data!=null){Data.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(DataChanged);}
        }
    }
}