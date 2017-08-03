using System;
namespace ShadowRunHelper1_3.CharController
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
            if (Data!=null){Data.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(DataChanged);}
        }
    }
}