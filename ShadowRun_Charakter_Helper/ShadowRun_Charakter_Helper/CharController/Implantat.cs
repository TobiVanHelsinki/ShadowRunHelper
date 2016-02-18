using System;
namespace ShadowRun_Charakter_Helper.CharController
{
    public class Implantat : CharController.ControllerSingle<CharModel.Implantat>
    {
        public Implantat()
        {
        }

        public Implantat(Controller.HashDictionary hD)
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