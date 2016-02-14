using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Vehikel : CharController.ControllerMultiItems<CharModel.Vehikel> 
    {
        public Vehikel()
        {
        }

        public Vehikel(ObservableCollection<CharModel.Vehikel> obj)
        {
        }
    }
}
