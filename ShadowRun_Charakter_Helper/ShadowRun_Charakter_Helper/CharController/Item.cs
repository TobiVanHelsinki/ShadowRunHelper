using System;
namespace ShadowRun_Charakter_Helper.CharController
{
    public class Item : CharController.ControllerSingle<CharModel.Item>
    {
        public Item()
        {
        }

        public Item(Controller.HashDictionary hD)
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