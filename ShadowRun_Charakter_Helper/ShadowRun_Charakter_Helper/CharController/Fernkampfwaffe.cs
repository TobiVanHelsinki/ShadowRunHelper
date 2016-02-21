using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Fernkampfwaffe : CharController.ControllerMultiItems<CharModel.Fernkampfwaffe>
    {
        public Fernkampfwaffe()
        {
            DicCD_Typ = "Fernkampfwaffe";
        }

        public Fernkampfwaffe(ObservableCollection<CharModel.Fernkampfwaffe> obj)
        {
            DicCD_Typ = "Fernkampfwaffe";
        }
    }
}