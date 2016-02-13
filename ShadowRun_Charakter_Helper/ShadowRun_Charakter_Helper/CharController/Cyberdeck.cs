using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    class Cyberdeck : CharController.Controller<CharModel.CyberDeck>
    {

        public Cyberdeck(int ID)
        {
            Dic_ID = ID;
            //DataList = new ObservableCollection<CharModel.Cyberdeck>();
        }
    }
}
