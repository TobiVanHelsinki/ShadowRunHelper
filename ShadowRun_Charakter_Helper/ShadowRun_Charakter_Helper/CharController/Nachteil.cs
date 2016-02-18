using System;
namespace ShadowRun_Charakter_Helper.CharController
{
    public class Nachteil : CharController.ControllerSingle<CharModel.Nachteil>
    {
        public Nachteil()
        {
        }

        public Nachteil(Controller.HashDictionary hD)
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