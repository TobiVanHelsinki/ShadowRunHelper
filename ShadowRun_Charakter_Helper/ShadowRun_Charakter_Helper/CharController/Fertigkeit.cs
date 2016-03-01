using System;
namespace ShadowRun_Charakter_Helper.CharController
{
    public class Fertigkeit : CharController.ControllerSingle<CharModel.Fertigkeit>
    {
        public Fertigkeit()
        {
        }

        public Fertigkeit(Controller.HashDictionary hD, int hD_ID)
        {
            this.HD_ID = hD_ID;
            this.setHD(hD);
            
            Data.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(DataChanged);
        }

        private void DataChanged(object sender, EventArgs e)
        {
            DataHasUpdatet(sender);
        }

        ~Fertigkeit()
        {
            if (Data!=null){Data.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(DataChanged);}
            remove_from_HD();
        }
    }
}