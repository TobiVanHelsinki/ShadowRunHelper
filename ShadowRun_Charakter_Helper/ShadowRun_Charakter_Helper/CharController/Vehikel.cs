using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Vehikel : CharController.ControllerMultiItems<CharModel.Vehikel> 
    {
        public Vehikel()
        {
            DicCD_Typ = "Vehikel";
        }

        public Vehikel(ObservableCollection<CharModel.Vehikel> obj)
        {
            DicCD_Typ = "Vehikel";
        }
    }
}
