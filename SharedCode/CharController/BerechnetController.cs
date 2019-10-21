///Author: Tobi van Helsinki

using Newtonsoft.Json;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using SharedCode.Ressourcen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using TLIB;

namespace ShadowRunHelper.CharController
{
    [ShadowRunHelperController(SupportsEdit = false)]
    public class BerechnetController : Controller<Berechnet>
    {
        [JsonIgnore]
        public new ObservableCollection<Berechnet> Data { get; protected set; }

        readonly AllListEntry MI_Essenz;
        public Berechnet Essenz { get; set; } = new Berechnet();
        readonly AllListEntry MI_Limit_K;
        public Berechnet Limit_K { get; set; } = new Berechnet();
        readonly AllListEntry MI_Limit_G;
        public Berechnet Limit_G { get; set; } = new Berechnet();
        readonly AllListEntry MI_Limit_S;
        public Berechnet Limit_S { get; set; } = new Berechnet();
        readonly AllListEntry MI_Laufen;
        public Berechnet Laufen { get; set; } = new Berechnet();
        readonly AllListEntry MI_Rennen;
        public Berechnet Rennen { get; set; } = new Berechnet();
        readonly AllListEntry MI_Tragen;
        public Berechnet Tragen { get; set; } = new Berechnet();
        readonly AllListEntry MI_MaxDamageK;
        public Berechnet MaxDamageK { get; set; } = new Berechnet();
        readonly AllListEntry MI_MaxDamageG;
        public Berechnet MaxDamageG { get; set; } = new Berechnet();

        AttributController AttributeRef;
        Person Person;
        ObservableCollection<Implantat> lstImplantateRef;

        // Start Stuff ########################################################
        public BerechnetController()
        {
            RefreshIdentifiers(this);
            MI_Essenz = new AllListEntry(Essenz, "");
            MI_Limit_K = new AllListEntry(Limit_K, "");
            MI_Limit_G = new AllListEntry(Limit_G, "");
            MI_Limit_S = new AllListEntry(Limit_S, "");
            MI_Laufen = new AllListEntry(Laufen, "");
            MI_Rennen = new AllListEntry(Rennen, "");
            MI_Tragen = new AllListEntry(Tragen, "");
            MI_MaxDamageG = new AllListEntry(MaxDamageG, "");
            MI_MaxDamageK = new AllListEntry(MaxDamageK, "");

            Data = new ObservableCollection<Berechnet>
            {
                Essenz,
                Limit_K,
                Limit_G,
                Limit_S,
                Laufen,
                Rennen,
                Tragen
            };
        }

        public void SetDependencies(Person p, ObservableCollection<Implantat> i, AttributController a)
        {
            AttributeRef = a;
            Person = p;
            lstImplantateRef = i;

            Essenz.PropertyChanged += (x, y) => { if (y.PropertyName == nameof(Essenz.Wert) || y.PropertyName == nameof(Essenz.WertCalced)) RefreshLimitS(); };
            Person.PropertyChanged += (x, y) => { if (y.PropertyName == nameof(Person.Essenz)) RefreshEssenz(); };
            Person.PropertyChanged += (x, y) => { if (y.PropertyName == nameof(Person.Schaden_G_max_mod)) RefreshLimitSchadenG(); };
            Person.PropertyChanged += (x, y) => { if (y.PropertyName == nameof(Person.Schaden_K_max_mod)) RefreshLimitSchadenK(); };
            lstImplantateRef.CollectionChanged += (x, y) => RegisterEssenzRefreshers();

            AttributeRef.Geschick.PropertyChanged += (x, y) => { RefreshLaufen(); RefreshRennen(); };
            AttributeRef.Konsti.PropertyChanged += (x, y) => { RefreshLimitK(); RefreshLimitSchadenK(); };
            AttributeRef.Reaktion.PropertyChanged += (x, y) => RefreshLimitK();
            AttributeRef.Staerke.PropertyChanged += (x, y) => { RefreshTragen(); RefreshLimitK(); };
            AttributeRef.Charisma.PropertyChanged += (x, y) => RefreshLimitS();
            AttributeRef.Logik.PropertyChanged += (x, y) => RefreshLimitG();
            AttributeRef.Intuition.PropertyChanged += (x, y) => RefreshLimitG();
            AttributeRef.Willen.PropertyChanged += (x, y) => { RefreshLimitS(); RefreshLimitG(); RefreshLimitSchadenG(); };

            RefreshEssenz();
            RefreshLimitS();
            RefreshLimitG();
            RefreshLimitK();
            RefreshLimitSchadenG();
            RefreshLimitSchadenK();
            RefreshLaufen();
            RefreshRennen();
            RefreshTragen();
        }

        private void MyEventHandler(object x, PropertyChangedEventArgs y) => RefreshEssenz();

        private void RegisterEssenzRefreshers()
        {
            foreach (var item in lstImplantateRef)
            {
                item.PropertyChanged -= MyEventHandler;
                item.PropertyChanged += MyEventHandler;
            }
            RefreshEssenz();
        }

        #region Refresh Methods

        protected void RefreshEssenz()
        {
            Essenz.Wert = 6;
            Essenz.Wert += Person.Essenz;
            Essenz.Wert -= lstImplantateRef.Where(x => x.Besitz == true).Sum(x => x.Essenz);
        }

        protected void RefreshLimitSchadenG()
        {
            Person.Schaden_G_max = 8 + Math.Ceiling(AttributeRef.Willen.ValueOf("Wert") / 2) + Person.Schaden_G_max_mod;
            MaxDamageG.Wert = Person.Schaden_G_max;
        }

        protected void RefreshLimitSchadenK()
        {
            Person.Schaden_K_max = 8 + Math.Ceiling(AttributeRef.Konsti.ValueOf("Wert") / 2) + Person.Schaden_K_max_mod;
            MaxDamageK.Wert = Person.Schaden_K_max;
        }

        //Physical Limit: (STR x2 + BOD + REA) / 3
        protected void RefreshLimitK()
        {
            Limit_K.Wert = Math.Ceiling((AttributeRef.Staerke.ValueOf("Wert") * 2 + AttributeRef.Konsti.ValueOf("Wert") + AttributeRef.Reaktion.ValueOf("Wert")) / 3);
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
        #endregion Refresh Methods

        // Implement IController ##########################
        public override IEnumerable<AllListEntry> GetElementsForThingList()
        {
            RefreshIdentifiers(this);
            var lstReturn = new List<AllListEntry>
            {
                MI_Essenz,
                MI_Limit_K,
                MI_Limit_G,
                MI_Limit_S,
                MI_Laufen,
                MI_Rennen,
                MI_Tragen
            };
            return lstReturn;
        }

        //Override cController ############################
        public override IEnumerable<Thing> GetElements()
        {
            var lstReturn = new List<Thing>
            {
                Essenz,
                Limit_K,
                Limit_G,
                Limit_S,
                Laufen,
                Rennen,
                Tragen
            };
            return lstReturn;
        }

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