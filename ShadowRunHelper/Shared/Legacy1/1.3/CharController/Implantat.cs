using System;
namespace ShadowRunHelper1_3.CharController
{
    public class Implantat : CharController.ControllerSingle<CharModel.Implantat>
    {
        public Implantat()
        {
        }

        public Implantat(Controller.HashDictionary hD, int hD_ID)
        {
            this.HD_ID = hD_ID;
            this.setHD(hD);
            Data.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(DataChanged);
        }

        private void DataChanged(object sender, EventArgs e)
        {
            DataHasUpdatet(sender);
        }



        ~Implantat()
        {
            if (Data!=null){Data.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(DataChanged);}
        }
    }
}