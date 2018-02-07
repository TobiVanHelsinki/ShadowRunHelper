using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TLIB_UWPFRAME;

namespace ShadowRunHelper.CharController
{
    public class BerechnetController : Controller<Berechnet>
    {
        // Variable Stuff #####################################################
        // Variable Model Stuff ###########################
        public new ObservableCollection<Berechnet> Data; //cause sometimes an very uebel Bug

        public Berechnet Essenz;
        public Berechnet Limit_K;
        public Berechnet Limit_G;
        public Berechnet Limit_S;
        public Berechnet Limit_S;

        AllListEntry MI_Essenz;
        AllListEntry MI_Limit_K;
        AllListEntry MI_Limit_G;
        AllListEntry MI_Limit_S;
        //TODO add laufen, springen, etc

        AttributController AttributeRef;
        Person PersonRef;
        ObservableCollection<Implantat> lstImplantateRef;
        // Variable Logik Stuff ###########################
        bool m_MutexDataColectionChange = false;

        // Start Stuff ########################################################
        public BerechnetController()
        {
           
            Essenz = new Berechnet();
            Limit_K = new Berechnet();
            Limit_G = new Berechnet();
            Limit_S = new Berechnet();
            RefreshBezeichner();
            MI_Essenz = new AllListEntry(Essenz, "");
            MI_Limit_K = new AllListEntry(Limit_K, "");
            MI_Limit_G = new AllListEntry(Limit_G, "");
            MI_Limit_S = new AllListEntry(Limit_S, "");

            
            Essenz.PropertyChanged += (x, y) => RefreshLimitS();
            Data = new ObservableCollection<Berechnet>();
            Data.CollectionChanged += Data_CollectionChanged;
            RefreshDataList();
        }

        void RefreshBezeichner()
        {
            Essenz.Bezeichner = CrossPlatformHelper.GetString("Model_Berechnet_Essenz/Text");
            Limit_K.Bezeichner = CrossPlatformHelper.GetString("Model_Berechnet_Limit_K/Text");
            Limit_G.Bezeichner = CrossPlatformHelper.GetString("Model_Berechnet_Limit_G/Text");
            Limit_S.Bezeichner = CrossPlatformHelper.GetString("Model_Berechnet_Limit_S/Text");
        }

        public void SetDependencies(Person p, ObservableCollection<Implantat> i, AttributController a)
        {
            AttributeRef = a;
            PersonRef = p;
            lstImplantateRef = i;
            PersonRef.PropertyChanged += (x, y) => RefreshEssenz();
            lstImplantateRef.CollectionChanged += (x, y) => RegisterRefreshers();
            AttributeRef.Konsti.PropertyChanged += (x, y) => { RefreshLimitK(); RefreshLimitSchaden(); };
            AttributeRef.Reaktion.PropertyChanged += (x, y) => RefreshLimitK();
            AttributeRef.Staerke.PropertyChanged += (x, y) => RefreshLimitK();
            AttributeRef.Charisma.PropertyChanged += (x, y) => RefreshLimitS();
            AttributeRef.Logik.PropertyChanged += (x, y) => RefreshLimitG();
            AttributeRef.Intuition.PropertyChanged += (x, y) => RefreshLimitG();
            AttributeRef.Willen.PropertyChanged += (x, y) => { RefreshLimitS(); RefreshLimitG(); RefreshLimitSchaden(); };
            RefreshLimitS(); RefreshLimitG(); RefreshLimitSchaden();
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
            Essenz.Wert = 6;
            Essenz.Wert += PersonRef.Essenz;
            foreach (var item in lstImplantateRef)
            {
                Essenz.Wert -= item.Essenz;
            }
        }

        protected void RefreshLimitSchaden()
        {
            if (PersonRef == null)
            {
                return;
            }
            PersonRef.Schaden_G_max = 8 + Math.Ceiling(AttributeRef.Willen.Wert / 2);
            PersonRef.Schaden_K_max = 8 + Math.Ceiling(AttributeRef.Konsti.Wert / 2);
        }

        //Physical Limit: (STR x2 + BOD + REA) / 3
        protected void RefreshLimitK()
        {
            Limit_K.Wert = Math.Ceiling( (AttributeRef.Staerke.GetValue() * 2 + AttributeRef.Konsti.GetValue() + AttributeRef.Reaktion.GetValue()) / 3);
        }

        //Mental Limit: (LOG x2 + INT +WIL) / 3

        protected void RefreshLimitG()
        {
            Limit_G.Wert = Math.Ceiling((AttributeRef.Logik.GetValue() * 2 + AttributeRef.Intuition.GetValue() + AttributeRef.Willen.GetValue()) / 3);
        }
        //Social Limit: (CHA x2 + WIL + Essence) /3

        protected void RefreshLimitS()
        {
            Limit_S.Wert = Math.Ceiling((AttributeRef.Charisma.GetValue() * 2 + AttributeRef.Willen.GetValue() + Essenz.GetValue()) / 3);
        }

        // DataList Handling ##############################
        void RefreshDataList()
        {
            RefreshBezeichner();
            Data.Clear();
            Data.Add(Essenz);
            Data.Add(Limit_K);
            Data.Add(Limit_G);
            Data.Add(Limit_S);
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
        public override IEnumerable<AllListEntry> GetElementsForThingList()
        {
            var lstReturn = new List<AllListEntry>();
            lstReturn.Add(MI_Essenz);
            lstReturn.Add(MI_Limit_K);
            lstReturn.Add(MI_Limit_G);
            lstReturn.Add(MI_Limit_S);
            return lstReturn;
        }

        //Override cController ############################
        public override Berechnet AddNewThing()
        {
            throw new NotSupportedException();
        }

        public override void RemoveThing(Berechnet tRem)
        {
            throw new NotSupportedException();
        }
    }
}