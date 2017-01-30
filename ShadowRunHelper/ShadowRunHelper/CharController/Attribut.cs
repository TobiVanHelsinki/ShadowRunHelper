using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharController
{
    public class cAttributController : cController<Attribut>
    {
        //[System.Runtime.Serialization.IgnoreDataMember] //cause sometimes an very übel Bug
        //public new ObservableCollection<Attribut> Data; //cause sometimes an very übel Bug

        public Attribut Konsti;// those have to point at a sepcific list element
        public Attribut Geschick;
        public Attribut Reaktion;
        public Attribut Staerke;
        public Attribut Charisma;
        public Attribut Logik;
        public Attribut Intuition;
        public Attribut Willen;
        public Attribut Essenz; //TODO Essenz berechnen aus Implantaten (+ ein bonus feld?)

        CharModel.Person PersonRef;
        ObservableCollection<Implantat> lstImplantateRef;
        public cAttributController(CharModel.Person P, ObservableCollection<Implantat> I) : this()
        {
            SetDependencies(P, I);
        }

        public void SetDependencies(Person p, ObservableCollection<Implantat> i)
        {
            PersonRef = p;
            lstImplantateRef = i;
            PersonRef.PropertyChanged += (x, y) => RefreshEssenz();
            lstImplantateRef.CollectionChanged += (x, y) => RegisterRefreshers();
        }

        private void RegisterRefreshers()
        {
            foreach (var item in lstImplantateRef)
            {
                item.PropertyChanged -= (x, y) => RefreshEssenz();
                item.PropertyChanged += (x, y) => RefreshEssenz();
            }
            RefreshEssenz();
        }

        protected void RefreshEssenz()
        {
            this.Essenz.Wert = PersonRef.Essenz;
            foreach (var item in lstImplantateRef)
            {
                this.Essenz.Wert -= item.Essenz;
            }
        }

        //Physical Limit: (STR x2 + BOD + REA) / 3
        public Attribut Limit_K;
        protected void RefreshLimitK()
        {
            this.Limit_K.Wert = Math.Ceiling( (this.Staerke.GetValue() * 2 + this.Konsti.GetValue() + this.Reaktion.GetValue()) / 3);
        }

        //Mental Limit: (LOG x2 + INT +WIL) / 3
        public Attribut Limit_G;
        protected void RefreshLimitG()
        {
            this.Limit_G.Wert = Math.Ceiling((this.Logik.GetValue() * 2 + this.Intuition.GetValue() + this.Willen.GetValue()) / 3);
        }
        //Social Limit: (CHA x2 + WIL + Essence) /3
        public Attribut Limit_S;
        protected void RefreshLimitS()
        {
            this.Limit_S.Wert = Math.Ceiling((this.Charisma.GetValue() * 2 + this.Willen.GetValue() + this.Essenz.GetValue()) / 3);
        }

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
            Logik = new Attribut();
            Logik.Bezeichner = res.GetString("Model_Attribut_Logik/Text");
            Intuition = new Attribut();
            Intuition.Bezeichner = res.GetString("Model_Attribut_Intuition/Text");
            Willen = new Attribut();
            Willen.Bezeichner = res.GetString("Model_Attribut_Willen/Text");
            Essenz = new Attribut();
            Essenz.Bezeichner = res.GetString("Model_Attribut_Essenz/Text");
            Limit_K = new Attribut();
            Limit_K.Bezeichner = res.GetString("Model_Attribut_Limit_K/Text");
            Limit_G = new Attribut();
            Limit_G.Bezeichner = res.GetString("Model_Attribut_Limit_G/Text");
            Limit_S = new Attribut();
            Limit_S.Bezeichner = res.GetString("Model_Attribut_Limit_S/Text");

            MI_Konsti = new KeyValuePair<Thing, string>(Konsti, "");
            MI_Geschick = new KeyValuePair<Thing, string>(Geschick, "");
            MI_Reaktion = new KeyValuePair<Thing, string>(Reaktion, "");
            MI_Staerke = new KeyValuePair<Thing, string>(Staerke, "");
            MI_Charisma = new KeyValuePair<Thing, string>(Charisma, "");
            MI_Logik = new KeyValuePair<Thing, string>(Logik, "");
            MI_Intuition = new KeyValuePair<Thing, string>(Intuition, "");
            MI_Willen = new KeyValuePair<Thing, string>(Willen, "");
            MI_Essenz = new KeyValuePair<Thing, string>(Essenz, "");
            MI_Limit_K = new KeyValuePair<Thing, string>(Limit_K, "");
            MI_Limit_G = new KeyValuePair<Thing, string>(Limit_G, "");
            MI_Limit_S = new KeyValuePair<Thing, string>(Limit_S, "");

            Konsti.PropertyChanged += (x, y) => RefreshLimitK();
            Reaktion.PropertyChanged += (x, y) => RefreshLimitK();
            Staerke.PropertyChanged += (x, y) => RefreshLimitK();
            Charisma.PropertyChanged += (x, y) => RefreshLimitS();
            Logik.PropertyChanged += (x, y) => RefreshLimitG();
            Intuition.PropertyChanged += (x, y) => RefreshLimitG();
            Willen.PropertyChanged += (x, y) => { RefreshLimitS(); RefreshLimitG(); };
            Essenz.PropertyChanged += (x, y) => RefreshLimitS();
            Data = new ObservableCollection<Attribut>();
            RefreshDataList();
        }

        public new Attribut AddNewThing()
        {
            RefreshDataList();
            return null;
        }
        public new void RemoveThing(Attribut tRem)
        {
            RefreshDataList();
        }

        private void RefreshDataList()
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

        public new List<KeyValuePair<Thing, string>> GetElementsForThingList()
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