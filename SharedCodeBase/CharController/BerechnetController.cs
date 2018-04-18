using Newtonsoft.Json;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using TLIB;
using TAPPLICATION;

namespace ShadowRunHelper.CharController
{
    public class BerechnetController : Controller<Berechnet>
    {
        // Variable Stuff #####################################################
        // Variable Model Stuff ###########################
        [JsonIgnore]
        public new ObservableCollection<Berechnet> Data; //cause sometimes an very uebel Bug

        Berechnet essenz = new Berechnet();
        AllListEntry MI_Essenz;
        public Berechnet Essenz { get => essenz; set => essenz = value; }
        Berechnet _Limit_K = new Berechnet();
        AllListEntry MI_Limit_K;
        public Berechnet Limit_K { get => _Limit_K; set => _Limit_K = value; }
        Berechnet _Limit_G = new Berechnet();
        AllListEntry MI_Limit_G;
        public Berechnet Limit_G { get => _Limit_G; set => _Limit_G = value; }
        Berechnet _Limit_S = new Berechnet();
        AllListEntry MI_Limit_S;
        public Berechnet Limit_S { get => _Limit_S; set => _Limit_S = value; }
        Berechnet _Laufen = new Berechnet();
        AllListEntry MI_Laufen;
        public Berechnet Laufen { get => _Laufen; set => _Laufen = value; }
        Berechnet _Rennen = new Berechnet();
        AllListEntry MI_Rennen;
        public Berechnet Rennen { get => _Rennen; set => _Rennen = value; }
        Berechnet _Tragen = new Berechnet();
        AllListEntry MI_Tragen;
        public Berechnet Tragen { get => _Tragen; set => _Tragen = value; }
        Berechnet _MaxDamageK = new Berechnet();
        AllListEntry MI_MaxDamageK;
        public Berechnet MaxDamageK { get => _MaxDamageK; set => _MaxDamageK = value; }
        Berechnet _MaxDamageG = new Berechnet();
        AllListEntry MI_MaxDamageG;
        public Berechnet MaxDamageG { get => _MaxDamageG; set => _MaxDamageG = value; }


        AttributController AttributeRef;
        Person Person;
        ObservableCollection<Implantat> lstImplantateRef;
        // Variable Logik Stuff ###########################
        bool m_MutexDataColectionChange = false;


        // Start Stuff ########################################################
        public BerechnetController()
        {
            RefreshBezeichner();
            MI_Essenz = new AllListEntry(Essenz, "");
            MI_Limit_K = new AllListEntry(Limit_K, "");
            MI_Limit_G = new AllListEntry(Limit_G, "");
            MI_Limit_S = new AllListEntry(Limit_S, "");
            MI_Laufen = new AllListEntry(Laufen, "");
            MI_Rennen = new AllListEntry(Rennen, "");
            MI_Tragen = new AllListEntry(Tragen, "");
            MI_MaxDamageG = new AllListEntry(MaxDamageG, "");
            MI_MaxDamageK = new AllListEntry(MaxDamageK, "");


            Essenz.PropertyChanged += (x, y) => RefreshLimitS();
            Data = new ObservableCollection<Berechnet>();
            Data.CollectionChanged += Data_CollectionChanged;
            RefreshDataList();
        }

        void RefreshBezeichner()
        {
            Essenz.Bezeichner = StringHelper.GetString("Model_Berechnet_Essenz/Text");
            Limit_K.Bezeichner = StringHelper.GetString("Model_Berechnet_Limit_K/Text");
            Limit_G.Bezeichner = StringHelper.GetString("Model_Berechnet_Limit_G/Text");
            Limit_S.Bezeichner = StringHelper.GetString("Model_Berechnet_Limit_S/Text");
            Laufen.Bezeichner = StringHelper.GetString("Model_Berechnet_Laufen/Text");
            Rennen.Bezeichner = StringHelper.GetString("Model_Berechnet_Rennen/Text");
            Tragen.Bezeichner = StringHelper.GetString("Model_Berechnet_Tragen/Text");
            MaxDamageG.Bezeichner = StringHelper.GetString("Model_Person_Schaden_G_max/Text");
            MaxDamageK.Bezeichner = StringHelper.GetString("Model_Person_Schaden_K_max/Text");
        }

        public void SetDependencies(Person p, ObservableCollection<Implantat> i, AttributController a)
        {
            AttributeRef = a;
            Person = p;
            lstImplantateRef = i;
            Person.PropertyChanged += (x, y) => RefreshEssenz();
            Person.PropertyChanged += (x, y) => RefreshLimitSchaden();
            lstImplantateRef.CollectionChanged += (x, y) => RegisterEssenzRefreshers();
            AttributeRef.Geschick.PropertyChanged += (x, y) => { RefreshLaufen(); RefreshRennen(); };
            AttributeRef.Konsti.PropertyChanged += (x, y) => { RefreshLimitK(); RefreshLimitSchaden(); };
            AttributeRef.Reaktion.PropertyChanged += (x, y) => RefreshLimitK();
            AttributeRef.Staerke.PropertyChanged += (x, y) => { RefreshTragen(); RefreshLimitK(); };
            AttributeRef.Charisma.PropertyChanged += (x, y) => RefreshLimitS();
            AttributeRef.Logik.PropertyChanged += (x, y) => RefreshLimitG();
            AttributeRef.Intuition.PropertyChanged += (x, y) => RefreshLimitG();
            AttributeRef.Willen.PropertyChanged += (x, y) => { RefreshLimitS(); RefreshLimitG(); RefreshLimitSchaden(); };
            RefreshEssenz();
            RefreshLimitS();
            RefreshLimitG();
            RefreshLimitK();
            RefreshLimitSchaden();
            RefreshLaufen(); 
            RefreshRennen(); 
            RefreshTragen();
        }

        void RegisterEssenzRefreshers()
        {
            foreach (var item in lstImplantateRef)
            {
                item.PropertyChanged -= (x, y) => RefreshEssenz();
                item.PropertyChanged += (x, y) => RefreshEssenz();
            }
            RefreshEssenz();
        }

        #region Refresh Methods

        protected void RefreshEssenz()
        {
            Essenz.Wert = 6;
            Essenz.Wert += Person.Essenz;
            foreach (var item in lstImplantateRef.Where(x=>x.Besitz == true))
            {
                Essenz.Wert -= item.Essenz;
            }
        }

        protected void RefreshLimitSchaden()
        {
            Person.Schaden_G_max = 8 + Math.Ceiling(AttributeRef.Willen.ValueOf("Wert") / 2) + Person.Schaden_G_max_mod;
            Person.Schaden_K_max = 8 + Math.Ceiling(AttributeRef.Konsti.ValueOf("Wert") / 2) + Person.Schaden_K_max_mod;
            MaxDamageK.Wert = Person.Schaden_K_max;
            MaxDamageG.Wert = Person.Schaden_G_max;
        }

        //Physical Limit: (STR x2 + BOD + REA) / 3
        protected void RefreshLimitK()
        {
            Limit_K.Wert = Math.Ceiling( (AttributeRef.Staerke.ValueOf("Wert") * 2 + AttributeRef.Konsti.ValueOf("Wert") + AttributeRef.Reaktion.ValueOf("Wert")) / 3);
        }

        //Mental Limit: (LOG x2 + INT +WIL) / 3

        protected void RefreshLimitG()
        {
            Limit_G.Wert = Math.Ceiling((AttributeRef.Logik.ValueOf("Wert") * 2 + AttributeRef.Intuition.ValueOf("Wert") + AttributeRef.Willen.ValueOf("Wert")) / 3);
        }
        //Social Limit: (CHA x2 + WIL + Essence) /3

        protected void RefreshLimitS()
        {
            Limit_S.Wert = Math.Ceiling((AttributeRef.Charisma.ValueOf("Wert") * 2 + AttributeRef.Willen.ValueOf("Wert") + Essenz.ValueOf("Wert")) / 3);
        }

        protected void RefreshLaufen()
        {
            Laufen.Wert = AttributeRef.Geschick.ValueOf("Wert") * 2; 
        }
        protected void RefreshRennen()
        {
            Rennen.Wert = AttributeRef.Geschick.ValueOf("Wert") * 4;
        }
        protected void RefreshTragen()
        {
            Tragen.Wert = AttributeRef.Staerke.ValueOf("Wert") * 10; //5 und 15 auch mgl TODO
        }
        #endregion

        // DataList Handling ##############################
        void RefreshDataList()
        {
            RefreshBezeichner();
            Data.Clear();
            Data.Add(Essenz);
            Data.Add(Limit_K);
            Data.Add(Limit_G);
            Data.Add(Limit_S);
            Data.Add(Laufen);
            Data.Add(Rennen);
            Data.Add(Tragen);
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
            lstReturn.Add(MI_Laufen);
            lstReturn.Add(MI_Rennen);
            lstReturn.Add(MI_Tragen);
            return lstReturn;
        }

        //Override cController ############################
        public override Thing AddNewThing()
        {
            throw new NotSupportedException();
        }

        public override void RemoveThing(Thing tRem)
        {
            throw new NotSupportedException();
        }
    }
}