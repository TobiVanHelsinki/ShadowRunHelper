using System;
namespace ShadowRunHelper.CharController
{
    public class Eigenschaft : CharController.ControllerSingle<CharModel.Eigenschaft>
    {
        public Eigenschaft()
        {
        }

        public Eigenschaft(Controller.HashDictionary hD)
        {
            this.setHD(hD);
            Data.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(DataChanged);
        }

        private void DataChanged(object sender, EventArgs e)
        {
            DataHasUpdatet(sender);
        }

        ~Eigenschaft()
        {
            if (Data!=null){Data.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(DataChanged);}
        }
    }
}