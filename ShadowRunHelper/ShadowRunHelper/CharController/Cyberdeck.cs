using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class cCyberDeckController : CharController.cController<CharModel.CyberDeck>
    {
        public int HD_ID_A;
        public int HD_ID_S;
        public int HD_ID_F;
        public int HD_ID_D;
        static private string HD_Bezeichner_A = "Angriff";
        static private string HD_Bezeichner_S = "Schleicher";
        static private string HD_Bezeichner_F = "Firewall";
        static private string HD_Bezeichner_D = "Datenverarbeitung";

        private CharModel.CyberDeck ActiveCyberDeck;
        private ObservableCollection<Thing> lstActiveCyberDeck;

        public cCyberDeckController()
        {
            lstActiveCyberDeck = new ObservableCollection<Thing>();
            ActiveCyberDeck = new CyberDeck();
            lstActiveCyberDeck.Add(ActiveCyberDeck);
        }

        public new ObservableCollection<Thing> GetElements()
        {
            return lstActiveCyberDeck;
        }

    }
}
