using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Nahkampfwaffe : CharController.ControllerMultiItems<CharModel.Nahkampfwaffe>
    {
        public Nahkampfwaffe()
        {
        }

        public Nahkampfwaffe(ObservableCollection<CharModel.Nahkampfwaffe> obj)
        {
        }
    }
}