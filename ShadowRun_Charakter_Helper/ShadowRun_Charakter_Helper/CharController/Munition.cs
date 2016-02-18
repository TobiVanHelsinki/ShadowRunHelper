using System;
namespace ShadowRun_Charakter_Helper.CharController
{
    public class Munition : CharController.ControllerSingle<CharModel.Munition>
    {
        public Munition()
        {
        }

        public Munition(Controller.HashDictionary hD)
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