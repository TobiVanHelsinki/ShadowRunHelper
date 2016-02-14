using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class CyberDeck : CharController.ControllerMultiItems<CharModel.CyberDeck>
    {
        public CyberDeck()
        {
        }

        public CyberDeck(ObservableCollection<CharModel.CyberDeck> obj)
        {
        }
    }
}
