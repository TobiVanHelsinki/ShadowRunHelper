using System;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class cAttributController : CharController.cController<CharModel.Attribut>
    {
        public CharModel.Attribut Konsti;// those have to point at a sepcific list element
        public CharModel.Attribut Geschick;
        public CharModel.Attribut Reaktion;
        public CharModel.Attribut Staeke;
        public CharModel.Attribut Charisma;
        public CharModel.Attribut Logik;
        public CharModel.Attribut Intuition;
        public CharModel.Attribut WIllen;

        public CharModel.Attribut Essenz;
        public CharModel.Attribut Limit_K;
        public CharModel.Attribut Limit_G;
        public CharModel.Attribut Limit_S;

        public cAttributController()
        {
        }
        

    }
}