using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharController
{
    public class cAttributController : CharController.cController<CharModel.Attribut>
    {
        public CharModel.Attribut Konsti;// those have to point at a sepcific list element
        public CharModel.Attribut Geschick;
        public CharModel.Attribut Reaktion;
        public CharModel.Attribut Staerke;
        public CharModel.Attribut Charisma;
        public CharModel.Attribut Logik;
        public CharModel.Attribut Intuition;
        public CharModel.Attribut Willen;

        public CharModel.Attribut Essenz;
        public CharModel.Attribut Limit_K;
        public CharModel.Attribut Limit_G;
        public CharModel.Attribut Limit_S;


        private KeyValuePair<Thing, string> MI_Konsti;
        private KeyValuePair<Thing, string> MI_Geschick;
        private KeyValuePair<Thing, string> MI_Reaktion;
        private KeyValuePair<Thing, string> MI_Staerke;
        private KeyValuePair<Thing, string> MI_Charisma;
        private KeyValuePair<Thing, string> MI_Logik;
        private KeyValuePair<Thing, string> MI_Intuition;
        private KeyValuePair<Thing, string> MI_Willen;

        private KeyValuePair<Thing, string> MI_Essenz;
        private KeyValuePair<Thing, string> MI_Limit_K;
        private KeyValuePair<Thing, string> MI_Limit_G;
        private KeyValuePair<Thing, string> MI_Limit_S;
        public cAttributController()
        {
            var res = ResourceLoader.GetForCurrentView();

            Konsti = new Attribut();
            Konsti.Bezeichner = res.GetString("Model_Attribut_Konsti/Text");
            Geschick = new Attribut();
            Geschick.Bezeichner = res.GetString("Model_Attribut_Geschick/Text");
            Reaktion = new Attribut();
            Reaktion.Bezeichner = res.GetString("Model_Attribut_Reaktion/Text");
            Staerke = new Attribut();
            Staerke.Bezeichner = res.GetString("Model_Attribut_Staerke/Text");
            Charisma = new Attribut();
            Charisma.Bezeichner = res.GetString("Model_Attribut_Charisma/Text");
            Logik= new Attribut();
            Logik.Bezeichner = res.GetString("Model_Attribut_Logik/Text");
            Intuition = new Attribut();
            Intuition.Bezeichner = res.GetString("Model_Attribut_Intuition/Text");
            Willen= new Attribut();
            Willen.Bezeichner = res.GetString("Model_Attribut_Willen/Text");
            Essenz= new Attribut();
            Essenz.Bezeichner = res.GetString("Model_Attribut_Essenz/Text");
            Limit_K= new Attribut();
            Limit_K.Bezeichner = res.GetString("Model_Attribut_Limit_K/Text");
            Limit_G= new Attribut();
            Limit_G.Bezeichner = res.GetString("Model_Attribut_Limit_G/Text");
            Limit_S= new Attribut();
            Limit_S.Bezeichner = res.GetString("Model_Attribut_Limit_S/Text");

            MI_Konsti = new KeyValuePair<Thing, string>(Konsti, "");
            MI_Geschick = new KeyValuePair<Thing, string>(Geschick, "");
            MI_Reaktion = new KeyValuePair<Thing, string>(Reaktion, "");
            MI_Staerke = new KeyValuePair<Thing, string>(Staerke, "");
            MI_Charisma = new KeyValuePair<Thing, string>(Charisma, "");
            MI_Logik = new KeyValuePair<Thing, string>(Charisma, "");
            MI_Intuition = new KeyValuePair<Thing, string>(Charisma, "");
            MI_Willen = new KeyValuePair<Thing, string>(Charisma, "");
            MI_Essenz = new KeyValuePair<Thing, string>(Charisma, "");
            MI_Limit_K = new KeyValuePair<Thing, string>(Charisma, "");
            MI_Limit_G = new KeyValuePair<Thing, string>(Charisma, "");
            MI_Limit_S = new KeyValuePair<Thing, string>(Charisma, "");

            //Konsti.PropertyChanged += (x, y) => Refresh();
            //Geschick.PropertyChanged += (x, y) => Refresh();
            //Reaktion.PropertyChanged += (x, y) => Refresh();
            //Staerke.PropertyChanged += (x, y) => Refresh();
            //Charisma.PropertyChanged += (x, y) => Refresh();
            //Logik.PropertyChanged += (x, y) => Refresh();
            //Intuition.PropertyChanged += (x, y) => Refresh();
            //Willen.PropertyChanged += (x, y) => Refresh();
            //Essenz.PropertyChanged += (x, y) => Refresh();
            //Limit_K.PropertyChanged += (x, y) => Refresh();
            //Limit_G.PropertyChanged += (x, y) => Refresh();
            //Limit_S.PropertyChanged += (x, y) => Refresh();
            Refresh();
    }

        //private void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    Refresh();
        //    foreach (var item in Data)
        //    {
        //        item.PropertyChanged -= (x, y) => Refresh();
        //        item.PropertyChanged += (x, y) => Refresh();
        //    }
        //}

        private void Refresh()
        {
            Data.Clear();
            Data.Add(Charisma);
            Data.Add(Konsti);
            Data.Add(Reaktion);
            Data.Add(Staerke);
            Data.Add(Geschick);
            Data.Add(Logik);
            Data.Add(Intuition);
            Data.Add(Willen);
            Data.Add(Essenz);
            Data.Add(Limit_K);
            Data.Add(Limit_G);
            Data.Add(Limit_S);
        }

        public new List<KeyValuePair<Thing, string>> GetElements()
        {
            List<KeyValuePair<Thing, string>> lstReturn = new List<KeyValuePair<Thing, string>>();
            lstReturn.Add(MI_Charisma);
            lstReturn.Add(MI_Geschick);
            lstReturn.Add(MI_Reaktion);
            lstReturn.Add(MI_Konsti);
            lstReturn.Add(MI_Staerke);
            lstReturn.Add(MI_Logik);
            lstReturn.Add(MI_Intuition);
            lstReturn.Add(MI_Willen);
            lstReturn.Add(MI_Essenz);
            lstReturn.Add(MI_Limit_K);
            lstReturn.Add(MI_Limit_G);
            lstReturn.Add(MI_Limit_S);
            return lstReturn;
        }

    }
}