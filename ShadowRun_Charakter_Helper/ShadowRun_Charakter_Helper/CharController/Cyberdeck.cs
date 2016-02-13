using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    class CyberDeck : CharController.ControllerMultiItems<CharModel.CyberDeck>
    {
        public CyberDeck()
        {
        }

        public CyberDeck(ObservableCollection<CharModel.CyberDeck> obj)
        {
        }
    }
}
