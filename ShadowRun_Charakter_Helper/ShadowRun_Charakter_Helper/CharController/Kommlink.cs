using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Kommlink : CharController.ControllerMultiItems<CharModel.Kommlink>
    {
        public Kommlink()
        {
            DicCD_Typ = "Kommlink";
        }

        public Kommlink(ObservableCollection<CharModel.Kommlink> obj)
        {
            DicCD_Typ = "Kommlink";
        }
    }
}