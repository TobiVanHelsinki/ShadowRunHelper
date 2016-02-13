using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Panzerung : CharController.ControllerMultiItems<CharModel.Panzerung> 
    {
        public Panzerung()
        {
        }

        public Panzerung(ObservableCollection<CharModel.Panzerung> obj)
        {
        }
    }
}
