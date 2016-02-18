using System;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Vorteil : CharController.ControllerSingle<CharModel.Vorteil>
    {
        public Vorteil()
        {
        }

        public Vorteil(Controller.HashDictionary hD)
        {
            this.setHD(hD);
            Data.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(DataChanged);
        }

        private void DataChanged(object sender, EventArgs e)
        {
            DataHasUpdatet(sender);
        }
    }
}