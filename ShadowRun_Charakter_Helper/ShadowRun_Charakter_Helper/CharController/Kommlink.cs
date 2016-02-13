using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Kommlink : CharController.ControllerMultiItems<CharModel.Kommlink>
    {
        public Kommlink()
        {
        }

        public Kommlink(ObservableCollection<CharModel.Kommlink> obj)
        {
        }
    }
}