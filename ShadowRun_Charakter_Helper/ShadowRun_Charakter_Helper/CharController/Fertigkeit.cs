using System;
namespace ShadowRun_Charakter_Helper.CharController
{
    public class Fertigkeit : CharController.ControllerSingle<CharModel.Fertigkeit>
    {
        public Fertigkeit()
        {
            DicCD_Typ = "Fertigkeit";
        }

        public Fertigkeit(Controller.HashDictionary hD)
        {
            DicCD_Typ = "Fertigkeit";
            this.setHD(hD);
            Data.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(DataChanged);
        }

        private void DataChanged(object sender, EventArgs e)
        {
            DataHasUpdatet(sender);
        }
    }
}