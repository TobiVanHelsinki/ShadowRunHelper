using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TLIB_UWPFRAME;

namespace ShadowRunHelper.CharController
{
    public class cAttributController : cController<Attribut>
    {
        // Variable Stuff #####################################################
        // Variable Model Stuff ###########################
        [Newtonsoft.Json.JsonIgnore] //cause sometimes an very uebel Bug
        //[Newtonsoft.Json.JsonIgnore]
        public new ObservableCollection<Attribut> Data; //cause sometimes an very uebel Bug

        public Attribut Konsti;// those have to point at a sepcific list element
        public Attribut Geschick;
        public Attribut Reaktion;
        public Attribut Staerke;
        public Attribut Charisma;
        public Attribut Logik;
        public Attribut Intuition;
        public Attribut Willen;
        public Attribut Magie;
        public Attribut Resonanz;

        [Newtonsoft.Json.JsonIgnore]
        public Attribut Essenz;
        [Newtonsoft.Json.JsonIgnore]
        public Attribut Limit_K;
        [Newtonsoft.Json.JsonIgnore]
        public Attribut Limit_G;
        [Newtonsoft.Json.JsonIgnore]
        public Attribut Limit_S;

        AllListEntry MI_Konsti;
        AllListEntry MI_Geschick;
        AllListEntry MI_Reaktion;
        AllListEntry MI_Staerke;
        AllListEntry MI_Charisma;
        AllListEntry MI_Logik;
        AllListEntry MI_Intuition;
        AllListEntry MI_Willen;

        AllListEntry MI_Essenz;
        AllListEntry MI_Limit_K;
        AllListEntry MI_Limit_G;
        AllListEntry MI_Limit_S;
        AllListEntry MI_Magie;
        AllListEntry MI_Resonanz;

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
            Resonanz = new Attribut();
            RefreshBezeichner();
            MI_Konsti = new AllListEntry(Konsti, "");
            MI_Geschick = new AllListEntry(Geschick, "");
            MI_Reaktion = new AllListEntry(Reaktion, "");
            MI_Staerke = new AllListEntry(Staerke, "");
            MI_Charisma = new AllListEntry(Charisma, "");
            MI_Logik = new AllListEntry(Logik, "");
            MI_Intuition = new AllListEntry(Intuition, "");
            MI_Willen = new AllListEntry(Willen, "");
            MI_Essenz = new AllListEntry(Essenz, "");
            MI_Limit_K = new AllListEntry(Limit_K, "");
            MI_Limit_G = new AllListEntry(Limit_G, "");
            MI_Limit_S = new AllListEntry(Limit_S, "");
            MI_Magie= new AllListEntry(Magie, "");
            MI_Resonanz= new AllListEntry(Resonanz, "");

            Konsti.PropertyChanged += (x, y) => { RefreshLimitK(); RefreshLimitSchaden(); };
            Reaktion.PropertyChanged += (x, y) => RefreshLimitK();
            Staerke.PropertyChanged += (x, y) => RefreshLimitK();
            Charisma.PropertyChanged += (x, y) => RefreshLimitS();
            Logik.PropertyChanged += (x, y) => RefreshLimitG();
            Intuition.PropertyChanged += (x, y) => RefreshLimitG();
            Willen.PropertyChanged += (x, y) => { RefreshLimitS(); RefreshLimitG(); RefreshLimitSchaden(); };
            Essenz.PropertyChanged += (x, y) => RefreshLimitS();
            Data = new ObservableCollection<Attribut>();
            Data.CollectionChanged += Data_CollectionChanged;
            RefreshDataList();
            RefreshLimitS(); RefreshLimitG(); RefreshLimitSchaden();
        }

        void RefreshBezeichner()
        {
            Konsti.Bezeichner = CrossPlatformHelper.GetString("Model_Attribut_Konsti/Text");
            Geschick.Bezeichner = CrossPlatformHelper.GetString("Model_Attribut_Geschick/Text");
            Reaktion.Bezeichner = CrossPlatformHelper.GetString("Model_Attribut_Reaktion/Text");
            Staerke.Bezeichner = CrossPlatformHelper.GetString("Model_Attribut_Staerke/Text");
            Charisma.Bezeichner = CrossPlatformHelper.GetString("Model_Attribut_Charisma/Text");
            Logik.Bezeichner = CrossPlatformHelper.GetString("Model_Attribut_Logik/Text");
            Intuition.Bezeichner = CrossPlatformHelper.GetString("Model_Attribut_Intuition/Text");
            Willen.Bezeichner = CrossPlatformHelper.GetString("Model_Attribut_Willen/Text");
            Essenz.Bezeichner = CrossPlatformHelper.GetString("Model_Attribut_Essenz/Text");
            Limit_K.Bezeichner = CrossPlatformHelper.GetString("Model_Attribut_Limit_K/Text");
            Limit_G.Bezeichner = CrossPlatformHelper.GetString("Model_Attribut_Limit_G/Text");
            Limit_S.Bezeichner = CrossPlatformHelper.GetString("Model_Attribut_Limit_S/Text");
            Magie.Bezeichner = CrossPlatformHelper.GetString("Model_Attribut_Magie/Text");
            Resonanz.Bezeichner = CrossPlatformHelper.GetString("Model_Attribut_Resonanz/Text");
        }

        public void SetDependencies(Person p, ObservableCollection<Implantat> i)
        {
            PersonRef = p;
            lstImplantateRef = i;
            PersonRef.PropertyChanged += (x, y) => RefreshEssenz();
            lstImplantateRef.CollectionChanged += (x, y) => RegisterRefreshers();
        }

        void RegisterRefreshers()
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
            if (PersonRef == null)
            {
                return;
            }
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
        void RefreshDataList()
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
            Data.Add(Resonanz);
        }
        void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!m_MutexDataColectionChange && e.Action == NotifyCollectionChangedAction.Add && Data.Count >= 12)
            {
                m_MutexDataColectionChange = true;
                RefreshDataList();
                m_MutexDataColectionChange = false;
            }
        }

        // Implement IController ##########################
        public override List<AllListEntry> GetElementsForThingList()
        {
            var lstReturn = new List<AllListEntry>();
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
            lstReturn.Add(MI_Resonanz);
            return lstReturn;
        }

        //Override cController ############################
        public override Attribut AddNewThing()
        {
            throw new NotSupportedException();
        }

        public override void RemoveThing(Attribut tRem)
        {
            throw new NotSupportedException();
        }
    }
}