using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Nahkampfwaffe : CharController.ControllerMultiItems<CharModel.Nahkampfwaffe>
    {
        public Nahkampfwaffe()
        {
            DicCD_Typ = "Nahkampfwaffe";
        }

        public Nahkampfwaffe(ObservableCollection<CharModel.Nahkampfwaffe> obj)
        {
            DicCD_Typ = "Nahkampfwaffe";
        }
    }
}