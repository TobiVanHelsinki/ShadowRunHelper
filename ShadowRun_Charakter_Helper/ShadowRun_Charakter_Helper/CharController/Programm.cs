using System;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Programm : CharController.ControllerSingle<CharModel.Programm>
    {
        public Programm()
        {
        }

        public Programm(Controller.HashDictionary hD)
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