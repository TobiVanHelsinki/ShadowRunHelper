using System;
namespace ShadowRun_Charakter_Helper.CharController
{
    public class Attribut : CharController.ControllerSingle<CharModel.Attribut>
    {
        public Attribut()
        {
        }

        public Attribut(Controller.HashDictionary hD, int hD_ID)
        {
            this.HD_ID = hD_ID;
            this.setHD(hD);
            Data.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(DataChanged);
        }

        private void DataChanged(object sender, EventArgs e)
        {
            DataHasUpdatet(sender);
        }

        ~Attribut()
        {
            Data.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(DataChanged);
            //todo bei allen machen
        }
    }
}