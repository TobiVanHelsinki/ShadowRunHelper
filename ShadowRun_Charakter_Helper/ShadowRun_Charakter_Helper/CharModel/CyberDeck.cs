using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.CharModel
{
    class CyberDeck : CharModel.Item
    {

       

        public CyberDeck(int dicCD_ID)
        {
            this.DicCD_ID = dicCD_ID;
            Random rand;
            rand = new Random();

            this.Bezeichner = "Tes" + rand.Next();
        }

        public CyberDeck()
        {

        }
    }
}
