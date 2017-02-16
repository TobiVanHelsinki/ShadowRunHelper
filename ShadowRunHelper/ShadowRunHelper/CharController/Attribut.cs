using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharController
{
    public class cAttributController : cController<Attribut>
    {
        // Variable Stuff #####################################################
        // Variable Model Stuff ###########################
        [System.Runtime.Serialization.IgnoreDataMember] //cause sometimes an very übel Bug
        public new ObservableCollection<Attribut> Data; //cause sometimes an very übel Bug

        public Attribut Konsti;// those have to point at a sepcific list element
        public Attribut Geschick;
        public Attribut Reaktion;
        public Attribut Staerke;
        public Attribut Charisma;
        public Attribut Logik;
        public Attribut Intuition;
        public Attribut Willen;
        [System.Runtime.Serialization.IgnoreDataMember] //cause sometimes an very übel Bug
        public Attribut Essenz; //TODO Essenz berechnen aus Implantaten (+ ein bonus feld?)
        public Attribut Limit_K;
        public Attribut Limit_G;
        public Attribut Limit_S;
        public Attribut Magie;

        ThingListEntry MI_Konsti;
        ThingListEntry MI_Geschick;
        ThingListEntry MI_Reaktion;
        ThingListEntry MI_Staerke;
        ThingListEntry MI_Charisma;
        ThingListEntry MI_Logik;
        ThingListEntry MI_Intuition;
        ThingListEntry MI_Willen;

        ThingListEntry MI_Essenz;
        ThingListEntry MI_Limit_K;
        ThingListEntry MI_Limit_G;
        ThingListEntry MI_Limit_S;
        ThingListEntry MI_Magie;

        Person PersonRef;
        ObservableCollection<Implantat> lstImplantateRef;
        // Variable Logik Stuff ###########################
        bool m_MutexDataColectionChange = false;

        // Start Stuff ########################################################
        public cAttributController()
        {
            Konsti = new Attribut();
            Geschick = new Attribut();
            Reaktion = new Attribut();
            Staerke = new Attribut();
            Charisma = new Attribut();
            Logik = new Attribut();
            Intuition = new Attribut();
            Willen = new Attribut();
            Essenz = new Attribut();
            Limit_K = new Attribut();
            Limit_G = new Attribut();
            Limit_S = new Attribut();
            Magie = new Attribut();
            RefreshBezeichner();
            MI_Konsti = new ThingListEntry(Konsti, "");
            MI_Geschick = new ThingListEntry(Geschick, "");
            MI_Reaktion = new ThingListEntry(Reaktion, "");
            MI_Staerke = new ThingListEntry(Staerke, "");
            MI_Charisma = new ThingListEntry(Charisma, "");
            MI_Logik = new ThingListEntry(Logik, "");
            MI_Intuition = new ThingListEntry(Intuition, "");
            MI_Willen = new ThingListEntry(Willen, "");
            MI_Essenz = new ThingListEntry(Essenz, "");
            MI_Limit_K = new ThingListEntry(Limit_K, "");
            MI_Limit_G = new ThingListEntry(Limit_G, "");
            MI_Limit_S = new ThingListEntry(Limit_S, "");
            MI_Magie= new ThingListEntry(Magie, "");

            Konsti.PropertyChanged += (x, y) => RefreshLimitK();
            Reaktion.PropertyChanged += (x, y) => RefreshLimitK();
            Staerke.PropertyChanged += (x, y) => RefreshLimitK();
            Charisma.PropertyChanged += (x, y) => RefreshLimitS();
            Logik.PropertyChanged += (x, y) => RefreshLimitG();
            Intuition.PropertyChanged += (x, y) => RefreshLimitG();
            Willen.PropertyChanged += (x, y) => { RefreshLimitS(); RefreshLimitG(); };
            Essenz.PropertyChanged += (x, y) => RefreshLimitS();
            Data = new ObservableCollection<Attribut>();
            Data.CollectionChanged += Data_CollectionChanged;
            RefreshDataList();
        }

        private void RefreshBezeichner()
        {
            var res = ResourceLoader.GetForCurrentView();
            Konsti.Bezeichner = res.GetString("Model_Attribut_Konsti/Text");
            Geschick.Bezeichner = res.GetString("Model_Attribut_Geschick/Text");
            Reaktion.Bezeichner = res.GetString("Model_Attribut_Reaktion/Text");
            Staerke.Bezeichner = res.GetString("Model_Attribut_Staerke/Text");
            Charisma.Bezeichner = res.GetString("Model_Attribut_Charisma/Text");
            Logik.Bezeichner = res.GetString("Model_Attribut_Logik/Text");
            Intuition.Bezeichner = res.GetString("Model_Attribut_Intuition/Text");
            Willen.Bezeichner = res.GetString("Model_Attribut_Willen/Text");
            Essenz.Bezeichner = res.GetString("Model_Attribut_Essenz/Text");
            Limit_K.Bezeichner = res.GetString("Model_Attribut_Limit_K/Text");
            Limit_G.Bezeichner = res.GetString("Model_Attribut_Limit_G/Text");
            Limit_S.Bezeichner = res.GetString("Model_Attribut_Limit_S/Text");
            Magie.Bezeichner = res.GetString("Model_Attribut_Magie/Text");
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

        // Refresh Stuff ######################################################
        protected void RefreshEssenz()
        {
            this.Essenz.Wert = 6;
            this.Essenz.Wert += PersonRef.Essenz;
            foreach (var item in lstImplantateRef)
            {
                this.Essenz.Wert -= item.Essenz;
            }
        }

        protected void RefreshLimitSchaden()
        {
            PersonRef.Schaden_G_max = 8 + Math.Ceiling(Willen.Wert / 2);
            PersonRef.Schaden_K_max = 8 + Math.Ceiling(Konsti.Wert / 2);
        }

        //Physical Limit: (STR x2 + BOD + REA) / 3
        protected void RefreshLimitK()
        {
            this.Limit_K.Wert = Math.Ceiling( (this.Staerke.GetValue() * 2 + this.Konsti.GetValue() + this.Reaktion.GetValue()) / 3);
        }

        //Mental Limit: (LOG x2 + INT +WIL) / 3

        protected void RefreshLimitG()
        {
            this.Limit_G.Wert = Math.Ceiling((this.Logik.GetValue() * 2 + this.Intuition.GetValue() + this.Willen.GetValue()) / 3);
        }
        //Social Limit: (CHA x2 + WIL + Essence) /3

        protected void RefreshLimitS()
        {
            this.Limit_S.Wert = Math.Ceiling((this.Charisma.GetValue() * 2 + this.Willen.GetValue() + this.Essenz.GetValue()) / 3);
        }

        // DataList Handling ##############################
        private void RefreshDataList()
        {
            RefreshBezeichner();
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
            Data.Add(Magie);
        }
        private void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!m_MutexDataColectionChange && e.Action == NotifyCollectionChangedAction.Add && Data.Count >= 12)
            {
                m_MutexDataColectionChange = true;
                RefreshDataList();
                m_MutexDataColectionChange = false;
            }
        }

        // Implement IController ##########################
        public new List<ThingListEntry> GetElementsForThingList()
        {
            var lstReturn = new List<ThingListEntry>();
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
            lstReturn.Add(MI_Magie);
            return lstReturn;
        }

    }
}