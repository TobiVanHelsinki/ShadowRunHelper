using System;
namespace ShadowRunHelper1_3.CharController
{
    public class Nachteil : CharController.ControllerSingle<CharModel.Nachteil>
    {
        public Nachteil()
        {
        }

        public Nachteil(Controller.HashDictionary hD, int hD_ID)
        {
            this.HD_ID = hD_ID;
            this.setHD(hD);
            Data.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(DataChanged);
        }

        private void DataChanged(object sender, EventArgs e)
        {
            DataHasUpdatet(sender);
        }

        ~Nachteil()
        {
            if (Data!=null){Data.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(DataChanged);}
        }
    }
}